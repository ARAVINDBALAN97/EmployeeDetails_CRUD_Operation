using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EmployeeDetails_CRUD_Operation
{
    interface IConfigFilesDetails
    {
        public string IAccessToken();
        public string IBaseAddress();
        public string IEndPoints();

    }


    //class RestServiceApi : IConfigFilesDetails
    //{
    //    //private IConfiguration configuration;

    //    //public RestServiceApi(IConfiguration _configuration)
    //    //{
    //    //    _configuration = configuration; ;
    //    //}


    //    //public RestServiceApi()
    //    //{

    //    //}


    //   string IConfigFilesDetails.IAccessToken()
    //    {
    //        //string accesskey;

    //        var accesskey = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["accesstoken"];

    //        //accesskey = configuration.GetSection("ApiKeyDetails").GetSection("accesstoken").Value;


    //        return accesskey;
    //    }

    //     string IConfigFilesDetails.IBaseAddress()
    //    {
    //        //string baseaddress;
    //        //baseaddress = configuration.GetSection("ApiKeyDetails").GetSection("baseaddress").Value;


    //        var baseaddress = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["baseaddress"];


    //        return baseaddress;
    //    }

    //     string IConfigFilesDetails.IEndPoints()
    //    {
    //        //string endpoints;
    //        //endpoints = configuration.GetSection("ApiKeyDetails").GetSection("endpoints").Value;

    //        var endpoints = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetSection("appsetting")["endpoints"];

    //        return endpoints;
    //    }
    //}

}
