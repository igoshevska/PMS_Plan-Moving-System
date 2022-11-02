using Microsoft.AspNetCore.Localization;
using PMS.Domain;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.Interface
{
    public interface IProposalAndOrder
    {
        /// <summary>
        /// Get all proposals and orders by user name
        /// </summary>
        /// <param name="userName">username of current user</param>
        /// <returns></returns>
        SearchResult<ProposalViewModel> GetAllProposalsAndOrdersByUserNameFiltered (ProposalSerchViewModel serachModel,  string userName, string currentCulture);
        /// <summary>
        /// Get proposal by proposalID
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        ProposalViewModel GetProposalByProposalId(int proposalId);

        /// <summary>
        /// Createing price proposal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> CreatePriceProposal(CalculatePriceProposalViewModel model, string currentClientId); 

        /// <summary>
        /// Creating orders
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        string CreateOrder(int proporsalId);
        /// <summary>
        /// Get currently logged user
        /// </summary>
        /// <returns></returns>
        UserViewModel GetCurrenUser(string getCurrentUser);
        /// <summary>
        /// Get Current Culture
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public RequestCulture GetCurrentCulture(string culture);
        /// <summary>
        /// Get all cultures
        /// </summary>
        /// <returns></returns>
        List<string> GetAllCultures();



    }
}
