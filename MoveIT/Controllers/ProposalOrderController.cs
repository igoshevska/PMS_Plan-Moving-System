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
using System.Collections.Generic;
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
        private readonly IRequestCultureProvider _cultureProvider;


        public ProposalOrderController(IProposalAndOrder proposalAndOrderService)
        {
            _proposalAndOrderService = proposalAndOrderService;
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

            return new ApiResponseModel<SearchResult<ProposalViewModel>>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = _proposalAndOrderService.GetAllProposalsAndOrdersByUserNameFiltered(serachModel, userName, "smcdds")
            };
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

        [HttpGet]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<string> GetCurrentCulture()
        {
            var acceptLanguage = HttpContext.Request.Headers["accept-language"].ToString();
            var culture = _proposalAndOrderService.GetCurrentCulture(acceptLanguage);
            HttpContext.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(culture, _cultureProvider));
            var currentCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            if (currentCulture != null)
            {
                return new ApiResponseModel<string>()
                {
                    responseCode = 200,
                    responseMessage = "OK",
                    response = currentCulture.RequestCulture.Culture.ToString()
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    responseCode = 400,
                    responseMessage = "Error",
                    response = "Error"
                };
            }
        }

        [HttpGet]
        [Authorize(Roles = "Sales, Client")]
        public ApiResponseModel<List<string>> GetAllCultures()
        {
            
            return new ApiResponseModel<List<string>>
            {
                responseCode = 200,
                responseMessage = "Success",
                response = _proposalAndOrderService.GetAllCultures()
            };
        }



    }
}
