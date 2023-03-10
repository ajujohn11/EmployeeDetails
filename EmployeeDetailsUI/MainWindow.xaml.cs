using EmployeeDetailsUI.UserControls;
using EmployeeServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace EmployeeDetailsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly HomePage homePage; 
        readonly EmployeePage employeePage;
        internal readonly IEmployeeService EmployeeService;
        internal static MainWindow AppWindow;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        public MainWindow(IEmployeeService employeeService)
        {
            AppWindow = this;
            EmployeeService = employeeService;

            InitializeComponent();

            homePage = new HomePage();
            employeePage = new EmployeePage();
            MainContent.Content = employeePage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = homePage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = employeePage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            CloseProgram();
        }
        /// <summary>
        /// CloseProgram
        /// </summary>
        private void CloseProgram()
        {
            App.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }

    }
}
