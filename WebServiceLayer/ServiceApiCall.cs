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

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    class ServiceApiCall
    {
        #region Decalre Objects
        HttpClient postclient = new HttpClient();
        Common objcom = new Common();


        ApiServiceCall apiscall = new ApiServiceCall();
        #endregion
        #region Decalre variables



        string strAccesstoken = string.Empty;
        string strBaseaddress = string.Empty;
        string strEndpoints = string.Empty;
        #endregion

        #region Constructor
        public ServiceApiCall()
        {
            InitialVal();
            postclient.BaseAddress = new Uri(strBaseaddress);
            postclient.DefaultRequestHeaders.Accept.Clear();
            postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            postclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + strAccesstoken);
        }
        #endregion

        public void InitialVal()
        {
            try
            {
                strBaseaddress = apiscall.IBaseAddress();
                strAccesstoken = apiscall.IAccessToken();
                strEndpoints = apiscall.IEndPoints();

            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }


        #region Get all the Employees
        public async Task<List<Employee>> GetEmployeeDetails()
        {
            List<Employee> lstemp = new List<Employee>();

            try
            {
                var response = await postclient.GetStringAsync(strEndpoints);
                var employee = JsonConvert.DeserializeObject<List<Employee>>(response);

                lstemp = employee;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return lstemp;
        }
        #endregion

        #region HttpPost post new records in End Poin
        public async void SaveEmployee(Employee emp)
        {

            try
            {
                string json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await postclient.PostAsync(strEndpoints, content);

                //if (response.IsSuccessStatusCode)
                //{
                //    MessageBox.Show(response.ToString());
                //}
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


        #region Method to call the service values 
        public void SetValues()
        {
            try
            {
                strBaseaddress = objcom.GetFilePath("BaseAddress").ToString();
                strAccesstoken = objcom.GetFilePath("accesstoken").ToString();
                strEndpoints = objcom.GetFilePath("endpoints").ToString();

            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}
