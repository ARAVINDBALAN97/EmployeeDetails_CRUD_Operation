using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    interface IConfigApi
    {
        string IBaseAddress();
        string IAccessToken();
        string IEndPoints();

    }
}
