using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using PMS.Domain;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Configuration
{
    public class RegisterMappers: Profile
    {
        public RegisterMappers()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<PriceProposal, PriceProposalViewModel>();
            CreateMap<Proposal, ProposalViewModel>();
            CreateMap<Order, OrderViewModel>();
        }
    }
}
