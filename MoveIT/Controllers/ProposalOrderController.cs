using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoveIT.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Sales, Client")]
    public class ProposalOrderController : ControllerBase
    {
        private readonly IProposalAndOrder _proposalAndOrderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<RequestLocalizationOptions> _options;
        private IRequestCultureProvider winningProvider;

        public ProposalOrderController(IProposalAndOrder proposalAndOrderService, IHttpContextAccessor httpContextAccessor,
            IOptions<RequestLocalizationOptions> options)
        {
            _proposalAndOrderService = proposalAndOrderService;
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<ApiResponseModel<string>> CreatePriceProposal([FromBody] CalculatePriceProposalViewModel model)
        {
            var currentUser = HttpContext.User.Identity.Name;

            return new ApiResponseModel<string>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = await _proposalAndOrderService.CreatePriceProposal(model, currentUser)
            };
        }
        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<SearchResult<ProposalViewModel>> GetAllProposalsAndOrdersByUserNameFiltered([FromBody] ProposalSerchViewModel serachModel)
        {
            var userName = HttpContext.User.Identity.Name;
            var userName1 = HttpContext.User.Identity.IsAuthenticated;
            
            var a = _options.Value.SupportedCultures;
            var acceptLanguage = HttpContext.Request.Headers["accept-language"].ToString();

            int charLocation = acceptLanguage.IndexOf(",", StringComparison.Ordinal);


            //test
           
            var acsa = acceptLanguage.Substring(0, charLocation);
            var requestCulture = new RequestCulture(acsa);
            //var winningProvider = new RequestCultureProvider();
            HttpContext.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, winningProvider));



            SetCurrentThreadCulture(requestCulture);
            var f = CultureInfo.CurrentCulture;
            var g = CultureInfo.CurrentUICulture;
            var h = CultureInfo.DefaultThreadCurrentCulture;
            var RequestCultureInfo = HttpContext.Features.Get<IRequestCultureFeature>();
            var RequestCultureInfo1 = HttpContext.Features.Get<IHttpConnectionFeature>();
            IPAddress RequestCultureInfo1c = RequestCultureInfo1.RemoteIpAddress;
            var RequestCultureInfo1cde = RequestCultureInfo1.LocalIpAddress.ToString();
            return new ApiResponseModel<SearchResult<ProposalViewModel>>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = _proposalAndOrderService.GetAllProposalsAndOrdersByUserNameFiltered(serachModel, userName)
            };


        }






        private static void SetCurrentThreadCulture(RequestCulture requestCulture)
        {
            CultureInfo.CurrentCulture = requestCulture.Culture;
            CultureInfo.CurrentUICulture = requestCulture.UICulture;
        }



        [HttpPost]
        [Authorize(Roles = "Client")]
        public ApiResponseModel<string> CreateOrder([FromBody] int proposalId)
        {
            return new ApiResponseModel<string>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = _proposalAndOrderService.CreateOrder(proposalId)
            };
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<ProposalViewModel> GetProposalByProposalId([FromBody] int proposalId)
        {
            return new ApiResponseModel<ProposalViewModel>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = _proposalAndOrderService.GetProposalByProposalId(proposalId)
            };
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<UserViewModel> GetCurrenUser()
        {
            var getCurrentUser = HttpContext.User.Identity.Name;
            return new ApiResponseModel<UserViewModel>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = _proposalAndOrderService.GetCurrenUser(getCurrentUser)
            };
        }

    }
}
