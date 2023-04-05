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
using EmployeeDetails_CRUD_Operation.WebServiceLayer;
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

        //Api Service call
        ServiceApiCall rps = new ServiceApiCall();


        public MainWindow()
        {

            InitializeComponent();

            txtEmpId.IsEnabled = false;
        }


        #region Button Load All the Employees in Grid
        private async void btnLoadEmpDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dgrdEmp.ItemsSource = await rps.GetEmployeeDetails();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Button Create Employee
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee()

            {
                //Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtmail.Text,
                Gender = txtgender.Text,
                Status = txtstaus.Text

            };

            rps.SaveEmployee(emp);


            //txtEmpId.Text = "";
            txtName.Text = "";
            txtmail.Text = "";
            txtgender.Text = "";
            txtstaus.Text = "";


        }

        #endregion

        #region Button Edit details
        public void btnEdit(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;

            txtEmpId.Text = emp.Id.ToString();
            txtName.Text = emp.Name;
            txtmail.Text = emp.Email;
            txtgender.Text = emp.Gender;
            txtstaus.Text = emp.Status;
        }
        #endregion


        #region Button onclick delete
        public async void btnDeleteEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;

            string empdetails;

            empdetails = emp.Id.ToString();

            try
            {
                if (emp.Id.ToString() == null)
                {
                    MessageBox.Show("Please Select Emlpoyee to delete the Request");
                }

                else
                {
                    rps.DelelteEmployee(emp.Id);
                    MessageBox.Show("Employee with ID " + empdetails + " has been deleted.", "Response Window.");
                    await rps.GetEmployeeDetails();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #endregion

        #region Export the employee list to csv
        private void btnexport_Click(object sender, RoutedEventArgs e)
        {

            dgrdEmp.SelectAllCells();
            dgrdEmp.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgrdEmp);
            dgrdEmp.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            File.AppendAllText("D:\\EmployeeDetails.csv", result, UnicodeEncoding.UTF8);
        }
        #endregion

        #region Buttton onclick Update
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

            rps.UpdateEmployee(emp);


            txtEmpId.Text = "";
            txtName.Text = "";
            txtmail.Text = "";
            txtgender.Text = "";
            txtstaus.Text = "";
        }

        #endregion
    }



}

