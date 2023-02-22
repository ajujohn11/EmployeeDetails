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
        BackgroundWorker worker;
        /// <summary>
        /// 
        /// </summary>
        EmployeeViewModel _employeeViewModel;
        /// <summary>
        /// 
        /// </summary>
        public EmployeePage()
        {
            InitializeComponent();
            _employeeViewModel = new EmployeeViewModel();
            _employeeService = new EmployeeService(new ApiHelper());
            this.DataContext = _employeeViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeePage(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
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
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() => GetEmployeeList());         
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Create"))
            {
                Task.Run(() => AddEmployee());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Update"))
            {
                Task.Run(() => UpdateEmployee());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataValidation("Delete"))
            {
                Task.Run(() => DeleteEmployee());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private bool DataValidation(string operation)
        {
            bool valid = true;

            if (operation != "Create" &&  (_employeeViewModel.SelectedEmployee ==null || _employeeViewModel.SelectedEmployee.id <= 0))
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<Employee>> GetEmployeeList()
        {
            var pagedResult = await _employeeService.GetEmployeesRequest(_employeeViewModel.SearchRequest);
            _employeeViewModel.EmployeesCollection = new List<Employee>(pagedResult.data);
            _employeeViewModel.Paging = pagedResult.meta.pagination;
            _employeeViewModel.PageLabel = $"Displaying {pagedResult.meta.pagination.page} of {pagedResult.meta.pagination.pages}";
            return pagedResult.data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task AddEmployee()
        {
            var createRequest = new ApiRequest
            {
                name = _employeeViewModel.SelectedEmployee.name,
                email = _employeeViewModel.SelectedEmployee.email,
                gender = _employeeViewModel.SelectedEmployee.gender,
                status = _employeeViewModel.SelectedEmployee.status
            };

            var result = await _employeeService.CreateEmployeeRequest(createRequest);

            if (result.code == 201)
            {
                var pagedResult = await _employeeService.GetEmployeesRequest(new ApiRequest());
                _employeeViewModel.EmployeesCollection = new List<Employee>(pagedResult.data);
            }

            MessageBox.Show(result.ApiResponseMessage, "Add Employee", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task UpdateEmployee()
        {
            var updateRequest = new ApiRequest
            {
                id = _employeeViewModel.SelectedEmployee.id,
                name = _employeeViewModel.SelectedEmployee.name,
                email = _employeeViewModel.SelectedEmployee.email,
                gender = _employeeViewModel.SelectedEmployee.gender,
                status = _employeeViewModel.SelectedEmployee.status
            };

            var result = await _employeeService.UpdateEmployeeRequest(updateRequest);

            MessageBox.Show(result.ApiResponseMessage, "Update Employee", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DeleteEmployee()
        {
            var deleteRequest = new ApiRequest
            {
                id = _employeeViewModel.SelectedEmployee.id,
                name = _employeeViewModel.SelectedEmployee.name,
                email = _employeeViewModel.SelectedEmployee.email,
                gender = _employeeViewModel.SelectedEmployee.gender,
                status = _employeeViewModel.SelectedEmployee.status
            };

            var result = await _employeeService.DeleteEmployeeRequest(deleteRequest);

            if (result.code == 204)
            {
                if (_employeeViewModel.EmployeesCollection.Remove(this._employeeViewModel.SelectedEmployee))
                {
                    _employeeViewModel.EmployeesCollection = new List<Employee>(_employeeViewModel.EmployeesCollection);
                }
            }

            MessageBox.Show(result.ApiResponseMessage, "Delete Employee", MessageBoxButton.OK, MessageBoxImage.Information);
           
        }

       
    }
}
