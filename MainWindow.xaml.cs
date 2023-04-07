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
using EmployeeDetails_CRUD_Operation.BusinessLogicLayer;
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
        ButtonActionMethods objBac = new ButtonActionMethods();
        Common objCom = new Common();
        #endregion

        #region Variables declaration
        int SearchId;
        string empDetails;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            btnCreate.Visibility = Visibility.Hidden;
        }


        #region Button Load All the Employees in Grid
        protected async void btnLoadEmpDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dgrdEmp.ItemsSource = await objBac.GetAllDetails();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Button Create Employee
        protected void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Employee Name");
                txtName.Focus();
            }

            if (txtMail.Text.Length == 0)
            {
                MessageBox.Show("Please enter the valid Email");
                txtMail.Focus();
            }

            else if (!Regex.IsMatch(txtGender.Text
                , @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Please enter the valid Email");
                txtMail.Select(0, txtMail.Text.Length);
                txtEmpId.Focus();
            }

            if (txtGender.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Gender");

                txtGender.Focus();
            }
            if (txtStatus.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Status");
                txtStatus.Focus();
            }

            // text obj call.
            var emp = new Employee()

            {
                //Id = Convert.ToInt32(txtEmpId.Text),
                Name = txtName.Text,
                Email = txtMail.Text,
                Gender = txtGender.Text,
                Status = txtStatus.Text
            };

            objSeriveCall.SaveEmployee(emp);

            //txtEmpId.Text = "";
            txtName.Text = "";
            txtMail.Text = "";
            txtGender.Text = "";
            txtStatus.Text = "";
        }

        #endregion

        #region Button Edit details
        protected void btnEdit(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;

            txtEmpId.Text = emp.Id.ToString();
            txtName.Text = emp.Name;
            txtMail.Text = emp.Email;
            txtGender.Text = emp.Gender;
            txtStatus.Text = emp.Status;
        }
        #endregion

        #region Button onclick delete
        protected async void btnDeleteEmp(object sender, RoutedEventArgs e)
        {
            Employee emp = ((FrameworkElement)sender).DataContext as Employee;

            try
            {
                if (emp.Id.ToString() == null || emp.Id.ToString() == "")
                {
                    MessageBox.Show("Please Select Emlpoyee to delete the Request");
                }

                else
                {
                    empDetails = emp.Id.ToString();
                    objSeriveCall.DelelteEmployee(emp.Id);

                }
                MessageBox.Show("Employee with ID " + empDetails + " has been deleted.", "Response Window.");
                dgrdEmp.ItemsSource = await objBac.GetAllDetails();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        #endregion

        #region Export the employee list to csv
        protected void btnExport_Click(object sender, RoutedEventArgs e)
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
        protected void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtEmpId.IsEnabled = false;

            try
            {
                if (txtName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the Employee Name");
                    txtName.Focus();
                }

                if (txtMail.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the Email");
                    txtMail.Focus();
                }

                if (txtGender.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the Gender");

                    txtGender.Focus();
                }
                if (txtStatus.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the Status");
                    txtStatus.Focus();
                }

                var emp = new Employee()

                {
                    Id = Convert.ToInt32(txtEmpId.Text),
                    Name = txtName.Text,
                    Email = txtMail.Text,
                    Gender = txtGender.Text,
                    Status = txtStatus.Text
                };

                objBac.UpdateDetails(emp);


                txtEmpId.Text = "";
                txtName.Text = "";
                txtMail.Text = "";
                txtGender.Text = "";
                txtStatus.Text = "";
            }

            catch(Exception ex)
            {
                throw(ex);
            }
            
        }
        #endregion

        #region Button Search Single Details
        public async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtEmpId.Text.ToString() == "" || txtEmpId.Text.Length == 0)
                {
                    MessageBox.Show("Please Enter Employee Id to search");
                }
                else
                {
                    SearchId = Convert.ToInt32(txtEmpId.Text);
                }


                dgrdEmp.ItemsSource = await objBac.GetById(SearchId);
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }

}

