using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
   public class BasicApiCall : IConfigApiDetails
    {

       public string AccessToken()
        {
            var accesskey = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["accesstoken"];
            return accesskey;
        }

        public string BaseAddress()
        {
            var baseaddress = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["baseaddress"];
            return baseaddress;
        }

       public string EndPoints()
        {
            var endpoints = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["endpoints"];
            return endpoints;
        }
        string IConfigApiDetails.IAccessToken()
        {
            string apikey;
            apikey = AccessToken();
            return apikey;

        }

        string IConfigApiDetails.IBaseAddress()
        {
            string apibaseaddress;
            apibaseaddress = BaseAddress();
            return apibaseaddress;
        }

        string IConfigApiDetails.IEndPoints()
        {
            string endpoint;
            endpoint = EndPoints();
            return endpoint;

        }
    }
}
