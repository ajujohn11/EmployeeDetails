using ApiManager;
using EmployeeDetailsUI.Core;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace EmployeeDetailsUI.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region Ctor
        public EmployeeViewModel()
        {
            _employees = new List<Employee>();
            _searchRequest = new ApiRequest();
            _selectedEmployee = new Employee();
            _paging = new Pageinfo();
            SearchButtonClicked = new RelayCommand(SearchEmployee, p => true);
            CreateButtonClicked = new RelayCommand(AddEmployee, p => true);
        }

        #endregion

        #region props

        private List<Employee> _employees;
        private ApiRequest _searchRequest;
        private Employee _selectedEmployee;
        private Pageinfo _paging;
        private string _pagelabel= $"Displaying 1 of 1";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
            //set
            //{
            //    if (value == _employees) 
            //        return;
            //    _employees = value;
            //    RaisePropertyChanged();
            //}
        }

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

        #region Event Methods  
        private void SearchEmployee(object value)
        {
            // TO DO: Implement MVVM pattern
        } 

        private void AddEmployee(object value)
        {
            // TO DO: Implement MVVM pattern
        }

        #endregion

    }
}
