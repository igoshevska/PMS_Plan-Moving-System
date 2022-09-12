using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Configuration;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using PMS.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region ctor
        public ProposalAndOrderService(ILogger<ProposalAndOrderService> logger,
                                 IRepository<User> usersRepo,
                                 IRepository<PriceProposal> priceProposalRepo,
                                 IRepository<Order> orderRepo,
                                 IRepository<Proposal> proposalRepo)
        {
            _logger = logger;
            _usersRepo = usersRepo;
            _priceProposalRepo = priceProposalRepo;
            _orderRepo = orderRepo;
            _proposalRepo = proposalRepo;
        }
        #endregion

        #region CreatingProposalsAndOrders 
        public string CreatePriceProposal(CalculatePriceProposalViewModel model, string currentClientId)
        {
            try
            {
                if (model.distance >= (int)DistanceBase.Distance10km)
                {
                    var pricePropsal = new PriceProposal();
                    pricePropsal.Distance = model.distance;
                    pricePropsal.LivingArea = model.livingArea;
                    pricePropsal.AtticArea = model.atticArea;
                    pricePropsal.HasPiano = model.hasPiano;
                    pricePropsal.Price = CalculatePriceProposal(model);

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

        public SearchResult<ProposalViewModel> GetAllProposalsAndOrdersByUserNameFiltered(ProposalSerchViewModel searchModel, string userName)
        {
            try
            {
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
                                        || x.PriceProposal.Price.ToString().Contains(searchModel.searchText)
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

                if (distance >= (int)DistanceBase.Distance10km && distance < (int)DistanceBase.Distance50km)
                {
                    distancePrice = (int)Prices.BasePriceAbove10Below50km +
                        (distance * (int)Prices.PriceForEachKilometerAbove10km);
                }
                else if (distance >= (int)DistanceBase.Distance50km && distance < (int)DistanceBase.Distance100km)
                {
                    distancePrice = (int)Prices.BasePriceAbove50Below100km +
                       (distance * (int)Prices.PriceForEachKilometerAbove50km);
                }
                else if (distance >= (int)DistanceBase.Distance100km)
                {
                    distancePrice = (int)Prices.BasePriceAbove100km +
                       (distance * (int)Prices.PriceForEachKilometerAbove100km);
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

    }
}
