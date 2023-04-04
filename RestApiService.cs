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

namespace EmployeeDetails_CRUD_Operation
{


    class RestApiService
    {
        HttpClient Postclient = new HttpClient();

        RestServiceApi restServiceApi = new RestServiceApi();

        string accesstoken = string.Empty;
        string baseaddress = string.Empty;
        string endpoints = string.Empty;

        // string accesstoken = "2b3419488e21fb40f8b6bff8598bea38131531c70d9b739131adcda2b76cdcb4";

        //string baseaddress = "https://gorest.co.in/";

        public RestApiService()
        {
            
            //accesstoken = _configuration.GetSection("ApiKeyDetails").GetSection("accesstoken").Value;
            //baseaddress = _configuration.GetSection("ApiKeyDetails").GetSection("baseaddress").Value;
            //endpoints = _configuration.GetSection("ApiKeyDetails").GetSection("endpoints").Value;

            Postclient.BaseAddress = new Uri(baseaddress);
            Postclient.DefaultRequestHeaders.Accept.Clear();
            Postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Postclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accesstoken);
        }

        //Method to Load employees
        public async Task<List<Employee>> GetEmployeeDetails()
        {
            List<Employee> lstemp = new List<Employee>();

            try
            {
                var response = await Postclient.GetStringAsync(endpoints);
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
                var response = await Postclient.PostAsync(endpoints, content);

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

                var response = await Postclient.PutAsJsonAsync(endpoints + emp.Id, json);

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
                await Postclient.DeleteAsync(endpoints + EmpID);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


    }




class AccessReSTapiDetails
    {

    }

}
