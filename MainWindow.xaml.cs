using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using EmployeeDetails_CRUD_Operation.Model;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.Win32;

namespace EmployeeDetails_CRUD_Operation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        string accesstoken = "2b3419488e21fb40f8b6bff8598bea38131531c70d9b739131adcda2b76cdcb4";

        HttpClient Postclient = new HttpClient();

        string baseaddress = "https://gorest.co.in/";
        public MainWindow()
        {
                   
        Postclient.BaseAddress = new Uri(baseaddress);
        Postclient.DefaultRequestHeaders.Accept.Clear();
        Postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        Postclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accesstoken);


            InitializeComponent();
        }


        //Button Load All the Employees in Grid
        private void btnLoadEmpDetails_Click(object sender, RoutedEventArgs e)
        {
            this.GetEmployeeDetails();
        }

        //Method to Load employees
        private async void GetEmployeeDetails()
        {

            try
            {
                var response = await Postclient.GetStringAsync("/public/v2/users/");
                var employee = JsonConvert.DeserializeObject<List<Employee>>(response);


                HttpResponseMessage responsed = Postclient.GetAsync("/public/v2/users/").Result;
                if (responsed.IsSuccessStatusCode)
                {
                    dgrdEmp.ItemsSource = employee;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Content = ex.Message.ToString();
            }
        }


        private async void SaveEmployee(Employee emp)
        {

          var response =  await Postclient.PostAsJsonAsync("/public/v2/users/", emp);
        }

       
        //Delete end point methods
        private async void DelelteEmployee(int EmpID)
        {

            string Details;

            Details = EmpID.ToString();

            try
            {
                var response = await Postclient.DeleteAsync("/public/v2/users/" + EmpID);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Employee with ID " + Details + " has been deleted.", "Response Window.");
                    GetEmployeeDetails();

                }
            }

            catch (Exception ex)
            {
                lblMessage.Content = ex.Message.ToString();
            }

        }

        //Button Create Employee
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee()

            {
                Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtmail.Text,
                Gender = txtgender.Text,
                Status = txtstaus.Text

            };

                this.SaveEmployee(emp);


            txtEmpId.Text = "";
            txtName.Text = "";
            txtmail.Text = "";
            txtgender.Text = "";
            txtstaus.Text = "";


        }

        void btnEdit(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;

            txtEmpId.Text = emp.Id.ToString();
            txtName.Text = emp.Name;
            txtmail.Text = emp.Email;
            txtgender.Text = emp.Gender;
            txtstaus.Text = emp.Status;
        }

        private void btnDeleteEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;


            try
            {
                if (emp.Id.ToString() == null)
                {
                    MessageBox.Show("Please Select Emloyee to delete the Request");
                }

                else
                {
                    this.DelelteEmployee(emp.Id);
                }
            }

            catch (Exception ex)
            {
                lblMessage.Content = ex.Message;
            }
        }

        //Export the value in CSV file
        private void btnexport_Click(object sender, RoutedEventArgs e)
        {

                dgrdEmp.SelectAllCells();
                dgrdEmp.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dgrdEmp);
                dgrdEmp.UnselectAllCells();
                String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.AppendAllText("D:\\EmployeeDetails.csv", result, UnicodeEncoding.UTF8);
            }


        //Button Update Employee 
        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee()

            {
                Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtmail.Text,
                Gender = txtgender.Text,
                Status = txtstaus.Text

            };

            this.UpdateEmployee(emp);


            txtEmpId.Text = "";
            txtName.Text = "";
            txtmail.Text = "";
            txtgender.Text = "";
            txtstaus.Text = "";
        }
    }



}

