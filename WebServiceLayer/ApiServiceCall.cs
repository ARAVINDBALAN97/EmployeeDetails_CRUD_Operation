using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    public class ApiServiceCall : IConfigApi
    {
        Common objcom = new Common();

        string strAddress = string.Empty;
        string strToken = string.Empty;
        string strEndpoint = string.Empty;


        public string BaseAddressCall()
        {
            strAddress = objcom.GetFilePath("BaseAddress").ToString();
            return strAddress;
        }

        public string ApiTokenCall()
        {
            strAddress = objcom.GetFilePath("accesstoken").ToString();
            return strAddress;
        }

        public string EndPointCall()
        {
            strAddress = objcom.GetFilePath("endpoints").ToString();
            return strAddress;
        }

        public string IAccessToken()
        {
            string aToken;
            aToken = ApiTokenCall();
            return aToken;
        }

        public string IBaseAddress()
        {
            string baseAdd;
            baseAdd = BaseAddressCall();
            return baseAdd;
        }

        public string IEndPoints()
        {
            string endPoint;
            endPoint = EndPointCall();
            return endPoint;
        }
    }
}
