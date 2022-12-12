using PMS.Data.Repositories;
using System.Collections.Generic;
using System;
using PMS.ViewModels;
using Microsoft.Extensions.Logging;

namespace Wrapper
{
    public class Wrapper
    {

        private readonly ILogger _logger;
        public Wrapper(ILogger logger)
        {
            _logger = logger;
        }

        public ErrorHandler CheckTheMethod(Action method)
        {
            try
            {
                method();
                return new ErrorHandler { responseCode = 200, responseMessage = "OK" };

            }
            catch (Exception ex)
            {
                _logger.LogError("Error code 999 " + ex.Message);
                return new ErrorHandler { responseCode = 404, responseMessage = "NOK" };
                throw;
            }
        }
    }
}
