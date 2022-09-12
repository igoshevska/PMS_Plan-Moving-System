﻿using PMS.Domain;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.Interface
{
    public interface IAccount
    {
        /// <summary>
        /// Check if login user  is valid
        /// </summary>
        /// <returns></returns>
        User CheckUserExistence(LoginUserViewModel model);
    }
}