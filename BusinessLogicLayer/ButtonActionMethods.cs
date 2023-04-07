using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using EmployeeDetails_CRUD_Operation.Model;
using EmployeeDetails_CRUD_Operation.WebServiceLayer;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace EmployeeDetails_CRUD_Operation.BusinessLogicLayer
{
    class ButtonActionMethods
    {
        #region Object Call
        ServiceApiCall objSeriveCall = new ServiceApiCall();
        #endregion

        #region Get All Employees Call Method
        public async Task<List<Employee>> GetAllDetails()
        {
            List<Employee> lstAllEmp = new List<Employee>();

            try
            {
                lstAllEmp = await objSeriveCall.GetEmployeeDetails();
            }

            catch (Exception ex)
            {
                throw (ex);
            }

            return lstAllEmp;

        }
        #endregion

        #region Get Single Employee Method
        public async Task<List<Employee>> GetById(int Id)
        {
            List<Employee> lstEmp = new List<Employee>();

            try
            {
                lstEmp = await objSeriveCall.GetEmployeeById(Id);
            }

            catch (Exception ex)
            {
                throw (ex);
            }

            return lstEmp;

        }
        #endregion

        #region Call Update Service EndPoint
        public void UpdateDetails(Employee emp)
        {
            try
            {
                objSeriveCall.UpdateEmployee(emp);
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}
