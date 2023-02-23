using ApiManager;
using EmployeeDetailsUI.ViewModels;
using EmployeeServices;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace EmployeeDetailsUI.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// 
        /// </summary>
        private readonly EmployeeViewModel _employeeViewModel;
        /// <summary>
        /// 
        /// </summary>
        BackgroundWorker worker;

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeePage()
        {
            InitializeComponent();


            if (MainWindow.AppWindow?.EmployeeService != null)
                _employeeService = MainWindow.AppWindow.EmployeeService;

            _employeeViewModel = new EmployeeViewModel(_employeeService);
            this.DataContext = _employeeViewModel;
        }
        #endregion

        #region Worker
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() => _employeeViewModel.GetEmployeeList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Add tasks after completion
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchEmployeesButton_Click(object sender, RoutedEventArgs e)
        {

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Create"))
            {
                if(_employeeViewModel.SelectedEmployee==null)
                {

                }
                var apiResponseMessage = await _employeeViewModel.AddEmployee();
                MessageBox.Show(apiResponseMessage, "Add Employee", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Update"))
            {
                var apiResponseMessage = await _employeeViewModel.UpdateEmployee();
                MessageBox.Show(apiResponseMessage, "Update Employee", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Delete"))
            {
                var apiResponseMessage = await _employeeViewModel.DeleteEmployee();
                MessageBox.Show(apiResponseMessage, "Delete Employee", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        #endregion

        #region Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private bool DataValidation(string operation)
        {
            bool valid = true;

            if(_employeeViewModel.SelectedEmployee == null)
            {
                MessageBox.Show($"Enter employee details to create, update or delete", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            else if (operation != "Create" &&  _employeeViewModel.SelectedEmployee.id <= 0)
            {
                MessageBox.Show($"Select an employee to update or delete", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            else if (string.IsNullOrEmpty(_employeeViewModel.SelectedEmployee.name))
            {
                MessageBox.Show($"Enter employee name", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            else if (string.IsNullOrEmpty(_employeeViewModel.SelectedEmployee.email))
            {
                MessageBox.Show("Enter employee email", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            else if (string.IsNullOrEmpty(_employeeViewModel.SelectedEmployee.gender))
            {
                MessageBox.Show("Select employee gender", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            else if (string.IsNullOrEmpty(_employeeViewModel.SelectedEmployee.status))
            {
                MessageBox.Show("Select employee status", $"{operation} Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
                valid = false;
            }
            return valid;
        }
        #endregion
    }
}
