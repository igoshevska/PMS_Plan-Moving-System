using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMS.Configuration;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using PMS.ViewModels.Enums;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static NBRM.KursResult;

namespace PMS.Services.Implementation
{
    public class ProposalAndOrderService : IProposalAndOrder
    {
        #region Declaration
        private readonly ILogger _logger;
        private readonly IRepository<User> _usersRepo;
        private readonly IRepository<PriceProposal> _priceProposalRepo;
        private readonly IRepository<Proposal> _proposalRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IOptions<RequestLocalizationOptions> _options;

        #endregion

        #region ctor
        public ProposalAndOrderService(ILogger<ProposalAndOrderService> logger,
                                 IRepository<User> usersRepo,
                                 IRepository<PriceProposal> priceProposalRepo,
                                 IRepository<Order> orderRepo,
                                 IRepository<Proposal> proposalRepo,
                                 IOptions<RequestLocalizationOptions> options)
        {
            _logger = logger;
            _usersRepo = usersRepo;
            _priceProposalRepo = priceProposalRepo;
            _orderRepo = orderRepo;
            _proposalRepo = proposalRepo;
            _options = options;
        }
        #endregion

        #region CreatingProposalsAndOrders 
        public async Task<string> CreatePriceProposal(CalculatePriceProposalViewModel model, string currentClientId)
        {
            try
            {
                if (model.distance >= (int)DistanceBase.FirstDisatnce)
                {
                    var pricePropsal = new PriceProposal();
                    pricePropsal.Distance = model.distance;
                    pricePropsal.LivingArea = model.livingArea;
                    pricePropsal.AtticArea = model.atticArea;
                    pricePropsal.HasPiano = model.hasPiano;
                    pricePropsal.PriceEur = CalculatePriceProposal(model);
                    pricePropsal.PriceMkd = pricePropsal.PriceEur * (await GetRateExchange());

                    _priceProposalRepo.Create(pricePropsal);

                    try
                    {
                        var proposal = new Proposal();
                        proposal.ProposalNumber = DateTime.Now.ToString("HHmmssff");
                        proposal.AddressFrom = model.addressFrom;
                        proposal.AddressTo = model.addressTo;
                        proposal.OrderId = null;
                        var getUser = _usersRepo.Query().Where(x => x.UserName == currentClientId).FirstOrDefault();
                        proposal.UserId = getUser.Id;
                        var priceProposalId = _priceProposalRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        proposal.PriceProposalId = priceProposalId;
                        _proposalRepo.Create(proposal);

                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        var lastPriceProposal = _priceProposalRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
                        _priceProposalRepo.Delete(lastPriceProposal);
                        _logger.LogError(ex.Message);
                        return "Error";
                    }
                }
                return "Error";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public string CreateOrder(int proporsalId)
        {
            try
            {
                var proposal = _proposalRepo.Query().Include(x => x.PriceProposal).Include(x => x.Order)
                                            .Where(x => x.Id == proporsalId).FirstOrDefault();

                var orderModel = new Order();
                orderModel.NumberOfCars = CalculateNumberOfCarsNeeded(proposal.PriceProposal.LivingArea, proposal.PriceProposal.AtticArea);
                orderModel.SerialNumber = DateTime.Now.ToString("HHmmssff");
                _orderRepo.Create(orderModel);

                try
                {
                    var orderBySerialNumber = _orderRepo.Query().Where(x => x.SerialNumber == orderModel.SerialNumber).FirstOrDefault();
                    proposal.OrderId = orderBySerialNumber.Id;
                    _proposalRepo.Update(proposal);
                }
                catch (Exception ex)
                {
                    _proposalRepo.Delete(proposal);
                    _logger.LogError(ex.Message);
                    return "Error";
                }

                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region GettingProposalsAndOrders

        public ProposalViewModel GetProposalByProposalId(int proposalId)
        {
            try
            {
                return _proposalRepo.Query().Include(x => x.User).Include(x => x.PriceProposal)
                                            .Include(x => x.Order).Where(x => x.Id == proposalId).
                                            FirstOrDefault().ToModel<ProposalViewModel, Proposal>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public SearchResult<ProposalViewModel> GetAllProposalsAndOrdersByUserNameFiltered(ProposalSerchViewModel searchModel, string userName, string currentCulture)
        {
            try
            {
                var request = new GetExchangeRateRequest();
                IQueryable<Proposal> allProposals;
                var currentUser = _usersRepo.Query().Include(x => x.Role).Where(x => x.UserName == userName).FirstOrDefault();

                if (currentUser.RoleId == (int)RoleEnum.Client)
                {
                    allProposals = _proposalRepo.Query().Include(x => x.Order).Include(x => x.PriceProposal).Include(x => x.User)
                                                        .Where(x => x.UserId == currentUser.Id);
                }
                else
                {
                    allProposals = _proposalRepo.Query().Include(x => x.Order).Include(x => x.PriceProposal).Include(x => x.User);
                }

                allProposals = allProposals.Where(x => x.ProposalNumber.Contains(searchModel.searchText)
                                        || x.User.UserName.Contains(searchModel.searchText)
                                        || x.PriceProposal.Distance.ToString().Contains(searchModel.searchText)
                                        || x.PriceProposal.LivingArea.ToString().Contains(searchModel.searchText)
                                        || x.PriceProposal.AtticArea.ToString().Contains(searchModel.searchText)
                                        || x.PriceProposal.PriceEur.ToString().Contains(searchModel.searchText)
                                        || searchModel.searchText == "").OrderBy(x => x.ProposalNumber);

                var allProposalsPaged = allProposals.Skip((searchModel.page - 1) * searchModel.rows).Take(searchModel.rows);
                var count = allProposals.Count();
                var result = allProposalsPaged.Select(x => x.ToModel<ProposalViewModel, Proposal>()).ToList();

                var serachResult = new SearchResult<ProposalViewModel>
                {
                    items = new List<ProposalViewModel>(),
                    totalItems = 0
                };
                if (result != null && result.Count > 0)
                {
                    serachResult.items = result;
                    serachResult.totalItems = count;
                }

                return serachResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Cultures
        public RequestCulture GetCurrentCulture(string culture)
        {
            int charLocation = culture.IndexOf(",", StringComparison.Ordinal);
            SetCurrentThreadCulture(new RequestCulture(culture.Substring(0, charLocation)));
            return new RequestCulture(culture.Substring(0, charLocation));
        }
        private static void SetCurrentThreadCulture(RequestCulture requestCulture)
        {
            CultureInfo.CurrentCulture = requestCulture.Culture;
            CultureInfo.CurrentUICulture = requestCulture.UICulture;
        }
        public List<string> GetAllCultures()
        {
            try 
            {
                var res = new List<string>();
                foreach (var item in _options.Value.SupportedCultures)
                {
                    res.Add(item.ToString());
                }
                return res;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion
       

        #region Users
        public UserViewModel GetCurrenUser(string getCurrentUser)
        {
            try
            {
                return _usersRepo.Query().Include(x => x.Role)
                    .Where(x => x.UserName == getCurrentUser).FirstOrDefault()
                    .ToModel<UserViewModel, User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region CalculatePrice
        private int CalculateDistancePrice(int distance)
        {
            try
            {
                int distancePrice = 0;

                if (distance >= (int)DistanceBase.FirstDisatnce && distance < (int)DistanceBase.SecondDistance)
                {
                    distancePrice = (int)Prices.BasePriceAboveFirstBelowSecondDist +
                        (distance * (int)Prices.PriceForEachKmAboveFirstDist);
                }
                else if (distance >= (int)DistanceBase.SecondDistance && distance < (int)DistanceBase.ThirdDistance)
                {
                    distancePrice = (int)Prices.BasePriceAboveSecondBelowThirdDist +
                       (distance * (int)Prices.PriceForEachKmAboveSecondDist);
                }
                else if (distance >= (int)DistanceBase.ThirdDistance)
                {
                    distancePrice = (int)Prices.BasePriceAboveThirdDist +
                       (distance * (int)Prices.PriceForEachKmAboveThirdDist);
                }

                return distancePrice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private int CalculateNumberOfCarsNeeded(int livingArea, int atticArea)
        {
            try
            {
                int numberOfCars = ((livingArea + (atticArea * 2)) / (int)VolumeBase.BaseVolume50m2) + 1;
                return numberOfCars;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private int CalculatePriceProposal(CalculatePriceProposalViewModel model)
        {
            try
            {
                int price = 0;
                if (model != null)
                {
                    int distancePrice = CalculateDistancePrice(model.distance);
                    int numberOfCars = CalculateNumberOfCarsNeeded(model.livingArea, model.atticArea);
                    price = distancePrice * numberOfCars;
                    if (model.hasPiano)
                    {
                        price = price + (int)Prices.PriceForPiano;
                    }
                    return price;
                }
                return price;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region ClientService
        private async Task<double> GetRateExchange()
        {
            var client = new KursSoapClient(KursSoapClient.EndpointConfiguration.KursSoap);

            client.OpenAsync();

            var exchangeRate = await client.GetExchangeRateAsync(DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"));

            client.CloseAsync();

            XmlRootAttribute xRoot = new XmlRootAttribute();
            XmlSerializer serializer = new XmlSerializer(typeof(dsKurs), xRoot);
            var kurs = new dsKurs();

            using (TextReader reader = new StringReader(exchangeRate.Body.GetExchangeRateResult))
            {
                kurs = (dsKurs)serializer.Deserialize(reader);
            }

            var sredenKurs = 0.0;
            foreach (var item in kurs.KursZbir)
            {
                if (item.Oznaka == "EUR")
                {
                    sredenKurs = item.Sreden;
                    break;
                }
            }
            return sredenKurs;
        }
        #endregion
    }
}
