using ApiManager;
using EmployeeServices;
using System;
using System.Collections.Generic;
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

namespace EmployeeDetailsUI.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : UserControl
    {
        private readonly IEmployeeService _employeeService;
        private readonly Pageinfo _pageinfo;
        private ApiRequest _apiRequest;
        public EmployeePage()
        {
            InitializeComponent();
            _employeeService = new EmployeeService(new ApiHelper());
            _apiRequest = new ApiRequest();
            this.DataContext = _apiRequest;
        }

        public EmployeePage(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _pageinfo = new Pageinfo();
        }

        private void SearchEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeList();
        }

        private async Task GetEmployeeList()
        {

            var apiSearchRequest = _apiRequest;

            //if (!string.IsNullOrEmpty(txtSearchName.Text))
            //    request.name = txtSearchName.Text;

            //if (cbxSearchStatus.SelectedValue != null &&
            //    !string.IsNullOrEmpty(cbxSearchStatus.SelectedValue.ToString()))
            //    request.status = (EnumStatus)Convert.ToInt32(cbxSearchStatus.SelectedValue);

            //if (cbxSearchGender.SelectedValue != null &&
            //    !string.IsNullOrEmpty(cbxSearchGender.SelectedValue.ToString()))
            //    request.gender = (EnumGender)Convert.ToInt32(cbxSearchGender.SelectedValue);

            //if (!string.IsNullOrEmpty(txtSearchEMail.Text))
            //    request.email = txtSearchEMail.Text;

            var emp = await _employeeService.GetEmployeeListRequest(apiSearchRequest);
            EmployeesGridView.ItemsSource = emp.data;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee();
        }

        private async Task AddEmployee()
        {
            var apiCreateRequest = _apiRequest;
            var emp = await _employeeService.CreateEmployeeRequest(apiCreateRequest);
        }
    }
}
