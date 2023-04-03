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
        HttpClient Postclient = new HttpClient();

        string baseaddress = "https://gorest.co.in/";
        public MainWindow()
        {
                   
        Postclient.BaseAddress = new Uri(baseaddress);
        Postclient.DefaultRequestHeaders.Accept.Clear();
        Postclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


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
            var resp = await Postclient.GetStringAsync("/public/v2/users/");
            var employee = JsonConvert.DeserializeObject<List<Employee>>(resp);

            dgrdEmp.ItemsSource = employee;

        }

        private void SaveEmployee(Employee Emp)
        {
            string json = JsonConvert.SerializeObject(Emp);
           
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseaddress));

            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            string parsedContent = json;
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);
            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();
            var response = http.GetResponse();
            var stream = response.GetResponseStream();
            GetEmployeeDetails();
        }
        private async void UpdateEmployee(Employee Emp)
        {
            await Postclient.PutAsJsonAsync("/public/v2/users/" + Emp.Id, Emp);
        }

        private async void DelelteEmployee(int EmpID)
        {
            await Postclient.DeleteAsync("/public/v2/users/" + EmpID);
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

        void btnDeleteEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;
            this.DelelteEmployee(emp.Id);
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

