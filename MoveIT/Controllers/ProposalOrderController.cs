using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Wrapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoveIT.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Sales, Client")]
    public class ProposalOrderController: ControllerBase
    {
        private readonly IProposalAndOrder _proposalAndOrderService;
        private readonly IRequestCultureProvider _cultureProvider;
        private readonly ILogger _logger;

        public ProposalOrderController(IProposalAndOrder proposalAndOrderService, ILogger<ProposalOrderController> logger)
        {
            _proposalAndOrderService = proposalAndOrderService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<ApiResponseModel<string>> CreatePriceProposal([FromBody] CalculatePriceProposalViewModel model)
        {
            var currentUser = HttpContext.User.Identity.Name;

            Task<string> res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => {res = _proposalAndOrderService.CreatePriceProposal(model, currentUser); });

            return new ApiResponseModel<string>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = await res
            };
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<SearchResult<ProposalViewModel>> GetAllProposalsAndOrdersByUserNameFiltered([FromBody] ProposalSerchViewModel serachModel)
        {
            var userName = HttpContext.User.Identity.Name;

            SearchResult<ProposalViewModel> res = null; 
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _proposalAndOrderService.GetAllProposalsAndOrdersByUserNameFiltered(serachModel, userName); });

            return new ApiResponseModel<SearchResult<ProposalViewModel>>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
            };
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public ApiResponseModel<string> CreateOrder([FromBody] int proposalId)
        {
            string res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _proposalAndOrderService.CreateOrder(proposalId); });

            return new ApiResponseModel<string>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
            };
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<ProposalViewModel> GetProposalByProposalId([FromBody] int proposalId)
        {

            ProposalViewModel res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _proposalAndOrderService.GetProposalByProposalId(proposalId); });

            return new ApiResponseModel<ProposalViewModel>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
            };
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<UserViewModel> GetCurrenUser()
        {
            var getCurrentUser = HttpContext.User.Identity.Name;

            UserViewModel res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _proposalAndOrderService.GetCurrenUser(getCurrentUser); });

            return new ApiResponseModel<UserViewModel>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
            };
        }

        [HttpGet]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<string> GetCurrentCulture()
        {
            var acceptLanguage = HttpContext.Request.Headers["accept-language"].ToString();
            var culture = _proposalAndOrderService.GetCurrentCulture(acceptLanguage);
            HttpContext.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(culture, _cultureProvider));
            var currentCulture = HttpContext.Features.Get<IRequestCultureFeature>();

            string res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = currentCulture.RequestCulture.Culture.ToString(); });

            if (currentCulture != null)
            {
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = res
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = "Error"
                };
            }
        }

        [HttpGet]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<List<string>> GetAllCultures()
        {
            List<string> res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _proposalAndOrderService.GetAllCultures(); });

            return new ApiResponseModel<List<string>>
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
            };
        }
    }
}
