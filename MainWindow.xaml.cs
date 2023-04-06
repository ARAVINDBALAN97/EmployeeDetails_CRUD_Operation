using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EmployeeDetails_CRUD_Operation.Model;
using EmployeeDetails_CRUD_Operation.WebServiceLayer;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace EmployeeDetails_CRUD_Operation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        #region Object Call
        ServiceApiCall objSeriveCall = new ServiceApiCall();
        Common objCom = new Common();
        #endregion

        #region Variables declaration
        int SearchId;
        string empDetails;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }


        #region Button Load All the Employees in Grid
        public async void btnLoadEmpDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dgrdEmp.ItemsSource = await objSeriveCall.GetEmployeeDetails();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Button Create Employee
        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Employee Name");
                txtName.Focus();
            }

            if (txtmail.Text.Length == 0)
            {
                MessageBox.Show("Please enter the valid Email");
                txtmail.Focus();
            }

            else if (!Regex.IsMatch(txtgender.Text
                , @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Please enter the valid Email");
                txtmail.Select(0, txtmail.Text.Length);
                txtEmpId.Focus();
            }

            if (txtgender.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Gender");

                txtgender.Focus();
            }
            if (txtstaus.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Status");
                txtstaus.Focus();
            }

            // text obj call.
            var emp = new Employee()

            {
                //Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtmail.Text,
                Gender = txtgender.Text,
                Status = txtstaus.Text
            };

            objSeriveCall.SaveEmployee(emp);

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

            try
            {
                if (emp.Id.ToString() == null || emp.Id.ToString() =="")
                {
                    MessageBox.Show("Please Select Emlpoyee to delete the Request");
                }

                else
                {
                    empDetails = emp.Id.ToString();
                    objSeriveCall.DelelteEmployee(emp.Id);
                    MessageBox.Show("Employee with ID " + empDetails + " has been deleted.", "Response Window.");
                    await objSeriveCall.GetEmployeeDetails();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #endregion

        #region Export the employee list to csv
        protected void btnexport_Click(object sender, RoutedEventArgs e)
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
        protected void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Employee Name");
                txtName.Focus();
            }

            if (txtmail.Text.Length == 0)
            {
                MessageBox.Show("Please enter the valid Email");
                txtmail.Focus();
            }

            else if (!Regex.IsMatch(txtgender.Text
                , @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Please enter the valid Email");
                txtmail.Select(0, txtmail.Text.Length);
                txtEmpId.Focus();
            }

            if (txtgender.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Gender");

                txtgender.Focus();
            }
            if (txtstaus.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Status");
                txtstaus.Focus();
            }

            var emp = new Employee()

            {
                Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtmail.Text,
                Gender = txtgender.Text,
                Status = txtstaus.Text
            };

            objSeriveCall.UpdateEmployee(emp);


            txtEmpId.Text = "";
            txtName.Text = "";
            txtmail.Text = "";
            txtgender.Text = "";
            txtstaus.Text = "";
        }

        #endregion

        protected async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Employee> lstEmp = new List<Employee>();

                if (txtEmpId.Text.ToString() == "" || txtEmpId.Text.Length == 0)
                {
                    MessageBox.Show("Please Enter Employee Id to search");
                }
                else
                {
                    SearchId = Convert.ToInt32(txtEmpId.Text);
                }

                dgrdEmp.ItemsSource = await objSeriveCall.GetEmployeeById(SearchId); ;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }





}

