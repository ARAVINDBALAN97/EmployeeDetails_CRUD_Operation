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

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    class ServiceApiCall
    {
        #region Decalre Objects
        HttpClient postclient = new HttpClient();
        Common objcom = new Common();
        ApiServiceCall objApiSerCall = new ApiServiceCall();
        #endregion

        #region Decalre variables
        string strAccesstoken = string.Empty;
        string strBaseaddress = string.Empty;
        string strEndpoints = string.Empty;
        string getDetailById = string.Empty;
        #endregion

        #region Constructor
        public ServiceApiCall()
        {
            InitiateValue();
            postclient.BaseAddress = new Uri(strBaseaddress);
            postclient.DefaultRequestHeaders.Accept.Clear();
            postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            postclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + strAccesstoken);
        }
        #endregion

        #region initiate values for endpoint
        public void InitiateValue()
        {
            try
            {
                strBaseaddress = objApiSerCall.IBaseAddress();
                strAccesstoken = objApiSerCall.IAccessToken();
                strEndpoints = objApiSerCall.IEndPoints();

            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Get all the Employees
        public async Task<List<Employee>> GetEmployeeDetails()
        {
            List<Employee> lstEmp = new List<Employee>();

            try
            {
                var response = await postclient.GetStringAsync(strEndpoints);
                var employee = JsonConvert.DeserializeObject<List<Employee>>(response);

                lstEmp = employee;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return lstEmp;
        }
        #endregion

        #region Get one record by Id

        public async Task<Employee> GetEmployeeById(int Id)
        {

            Employee emp = new Employee();

            try
            {
                

                var response = await postclient.GetStringAsync(strEndpoints);
                var employee = JsonConvert.DeserializeObject<Employee>(response);

                emp = employee;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return emp;
        }


        #endregion


        #region HttpPost post new records in End Poin
        public async void SaveEmployee(Employee emp)
        {

            try
            {
                string json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await postclient.PostAsJsonAsync(strEndpoints, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
            }

            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion

        #region HttpPut Update the existing Details
        public async void UpdateEmployee(Employee emp)
        {

            try
            {

                var json = JsonConvert.SerializeObject(emp);

                var response = await postclient.PutAsJsonAsync(strEndpoints + emp.Id, json);

                //if (response.IsSuccessStatusCode)
                //{
                //    MessageBox.Show("Employee With Id " + emp.Name + "&" + emp.Id + " has been updated");
                //}

                //else
                //{
                //    //MessageBox.Show(response.ToString());
                //}


            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        #endregion

        #region HttpDelete Delete the Existing records
        public async void DelelteEmployee(int EmpID)
        {
            try
            {
                await postclient.DeleteAsync(strEndpoints + EmpID);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}
