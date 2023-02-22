using ApiManager;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EmployeeDetailsUI.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private List<Employee> _employees;
        private ApiRequest _searchRequest;
        private Employee _selectedEmployee;
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeeViewModel()
        {
            _employees = new List<Employee>();
            _searchRequest = new ApiRequest();
            _selectedEmployee = new Employee();
        }

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

    }
}
