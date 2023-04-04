using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using EmployeeDetails_CRUD_Operation.Model;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace EmployeeDetails_CRUD_Operation.WebServiceLayer
{
    class ServiceApiCall
    {
        HttpClient Postclient = new HttpClient();
        Common objcom = new Common();

        
        string strAccesstoken = string.Empty;
        string strBaseaddress = string.Empty;
        string strEndpoints = string.Empty;

        public void InitValues()
        {
            try
            {



                //Postclient.BaseAddress = new Uri(strBaseAddress);
                //Postclient.DefaultRequestHeaders.Accept.Clear();
                //Postclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // String accessToken = getAuthToken(userId, password).GetAwaiter().GetResult();

                //ClientDataTaskCreate(AccessToken).Wait();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        // string accesstoken = "2b3419488e21fb40f8b6bff8598bea38131531c70d9b739131adcda2b76cdcb4";

        //string baseaddress = "https://gorest.co.in/";

        public ServiceApiCall()
        {

            SetValues();
            Postclient.BaseAddress = new Uri(strBaseaddress);
            Postclient.DefaultRequestHeaders.Accept.Clear();
            Postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Postclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + strAccesstoken);


        }

        //Method to Load employees
        public async Task<List<Employee>> GetEmployeeDetails()
        {
            List<Employee> lstemp = new List<Employee>();

            try
            {
                var response = await Postclient.GetStringAsync(strEndpoints);
                var employee = JsonConvert.DeserializeObject<List<Employee>>(response);

                lstemp = employee;

            }
            catch (Exception ex)
            {
                //lblMessage.Content = ex.Message.ToString();
            }

            return lstemp;
        }



        public async void SaveEmployee(Employee emp)
        {

            try
            {
                string json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Postclient.PostAsync(strEndpoints, content);

                //if (response.IsSuccessStatusCode)
                //{
                //    MessageBox.Show(response.ToString());
                //}
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }


        public async void UpdateEmployee(Employee emp)
        {

            try
            {

                var json = JsonConvert.SerializeObject(emp);

                var response = await Postclient.PutAsJsonAsync(strEndpoints + emp.Id, json);

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
                //MessageBox.Show(ex.ToString());
            }


        }



        //Delete end point methods
        public async void DelelteEmployee(int EmpID)
        {
            try
            {
                await Postclient.DeleteAsync(strEndpoints + EmpID);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void SetValues()
        {
            try
            {
                strBaseaddress = objcom.GetFilePath("BaseAddress").ToString();

                strAccesstoken = objcom.GetFilePath("accesstoken").ToString();

                strEndpoints = objcom.GetFilePath("accesstoken").ToString();

            }

            catch(Exception ex)
            {
                throw (ex);
            }
        }

    }
}
