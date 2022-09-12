using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public ProposalOrderController(IProposalAndOrder proposalAndOrderService, IHttpContextAccessor httpContextAccessor)
        {
            _proposalAndOrderService = proposalAndOrderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public JsonResult CreatePriceProposal([FromBody] CalculatePriceProposalViewModel model)
        {
            var getCurrentUser = HttpContext.User.Identity.Name;
         
            var result = _proposalAndOrderService.CreatePriceProposal(model, getCurrentUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public JsonResult GetAllProposalsAndOrdersByUserNameFiltered([FromBody] ProposalSerchViewModel serachModel)
        {
            var userName = HttpContext.User.Identity.Name;
            var result = _proposalAndOrderService.GetAllProposalsAndOrdersByUserNameFiltered(serachModel, userName);
            return new JsonResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public JsonResult CreateOrder([FromBody] int proposalId)
        {
            var result = _proposalAndOrderService.CreateOrder(proposalId);
            return new JsonResult(result);
        }


        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public JsonResult GetProposalByProposalId([FromBody] int proposalId)
        {
            var result = _proposalAndOrderService.GetProposalByProposalId(proposalId);
            return new JsonResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Sales, Client")]
        public JsonResult GetCurrenUser()
        {
            var getCurrentUser = HttpContext.User.Identity.Name;
            var result = _proposalAndOrderService.GetCurrenUser(getCurrentUser);
            return new JsonResult(result);
        }
   


    }
}
