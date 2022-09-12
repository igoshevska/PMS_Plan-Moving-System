using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public RoleViewModel role { get; set; }
        public string email { get; set; }
    }

    public class LoginUserViewModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class ProposalSerchViewModel
    {
        public int page { get; set; }
        public int rows { get; set; }
        public string searchText { get; set; }
        
    }


}
