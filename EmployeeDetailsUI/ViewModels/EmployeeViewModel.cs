using ApiManager;
using EmployeeDetailsUI.Core;
using EmployeeServices;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDetailsUI.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region Declaration
        private List<Employee> _employees;
        private ApiRequest _searchRequest;
        private Employee _selectedEmployee;
        private Pageinfo _paging;
        private string _pagelabel = $"Displaying 1 of 1";
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _employees = new List<Employee>() { new Employee() { id = 0 } };
            _searchRequest = new ApiRequest();
            _selectedEmployee = new Employee();
            _paging = new Pageinfo();
            SearchButtonClicked = new RelayCommand(SearchEmployee, p => true);
            CreateButtonClicked = new RelayCommand(AddEmployee, p => true);
        }
        #endregion

        #region props
        /// <summary>
        /// 
        /// </summary>
        public List<Employee> EmployeesCollection
        {
            get { return _employees; }
            set
            {
                _employees = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EmployeesCollection"));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ApiRequest SearchRequest
        {
            get { return _searchRequest; }
            set
            {
                _searchRequest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchRequest"));

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedEmployee"));

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Pageinfo Paging
        {
            get { return _paging; }
            set
            {
                _paging = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Paging"));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PageLabel
        {
            get
            {
                return _pagelabel;
            }
            set
            {
                _pagelabel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PageLabel"));
                }
            }
        }
        #endregion

        #region ICommands  
        public ICommand SearchButtonClicked { get; set; }
        public ICommand CreateButtonClicked { get; set; }
        #endregion

        #region Events  
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void SearchEmployee(object value)
        {
            // TO DO: Implement MVVM pattern
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void AddEmployee(object value)
        {
            // TO DO: Implement MVVM pattern
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task<List<Employee>> GetEmployeeList()
        {
            var pagedResult = await _employeeService.GetEmployeesRequest(this.SearchRequest);
            this.EmployeesCollection = new List<Employee>(pagedResult.data);
            this.Paging = pagedResult.meta.pagination;
            this.PageLabel = $"Displaying Page {pagedResult.meta.pagination.page} of {pagedResult.meta.pagination.pages}";
            this.SearchRequest = SearchRequest;
            return pagedResult.data;
        }

        internal async Task<string> AddEmployee()
        {
            var createRequest = new ApiRequest
            {
                name = this.SelectedEmployee.name,
                email = this.SelectedEmployee.email,
                gender = this.SelectedEmployee.gender,
                status = this.SelectedEmployee.status
            };

            var result = await _employeeService.CreateEmployeeRequest(createRequest);

            if (result.code == 201)
            {
                var pagedResult = await _employeeService.GetEmployeesRequest(new ApiRequest());
                this.EmployeesCollection = new List<Employee>(pagedResult.data);
            }

            return result.ApiResponseMessage;
        }

        internal async Task<string> UpdateEmployee()
        {
            var updateRequest = new ApiRequest
            {
                id = this.SelectedEmployee.id,
                name = this.SelectedEmployee.name,
                email = this.SelectedEmployee.email,
                gender = this.SelectedEmployee.gender,
                status = this.SelectedEmployee.status
            };

            var result = await _employeeService.UpdateEmployeeRequest(updateRequest);
            return result.ApiResponseMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task<string> DeleteEmployee()
        {
            var deleteRequest = new ApiRequest
            {
                id = this.SelectedEmployee.id,
                name = this.SelectedEmployee.name,
                email = this.SelectedEmployee.email,
                gender = this.SelectedEmployee.gender,
                status = this.SelectedEmployee.status
            };

            var result = await _employeeService.DeleteEmployeeRequest(deleteRequest);

            if (result.code == 204 && this.EmployeesCollection.Remove(this.SelectedEmployee))
            {
                this.EmployeesCollection = new List<Employee>(this.EmployeesCollection);
            }
            return result.ApiResponseMessage;
        }

        #endregion

    }
}
