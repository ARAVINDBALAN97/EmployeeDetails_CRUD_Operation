using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{

    interface IConfigApiDetails
    {
        public string IAccessToken();
        public string IBaseAddress();
        public string IEndPoints();

    }
}
