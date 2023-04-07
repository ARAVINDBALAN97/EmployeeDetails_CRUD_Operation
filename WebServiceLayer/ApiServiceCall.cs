using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    public class ApiServiceCall : IConfigApi
    {
        #region Object call
        Common objcom = new Common();
        #endregion

        #region Variable Declaration
        string strAddress = string.Empty;
        string strToken = string.Empty;
        string strEndpoint = string.Empty;
        #endregion

        #region Access Token
        public string IAccessToken()
        {
            strAddress = objcom.GetFilePath("accesstoken").ToString();
            return strAddress;
        }
        #endregion

        #region Base address
        public string IBaseAddress()
        {
            strAddress = objcom.GetFilePath("BaseAddress").ToString();
            return strAddress;
        }
        #endregion

        #region End Point
        public string IEndPoints()
        {
            strAddress = objcom.GetFilePath("endpoints").ToString();
            return strAddress;
        }
        #endregion
    }
}
