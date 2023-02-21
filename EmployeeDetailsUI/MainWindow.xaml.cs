using EmployeeDetailsUI.UserControls;
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
        HomePage homePage = new HomePage();
        EmployeePage employeePage = new EmployeePage();

        public MainWindow()
        {
            InitializeComponent();
            InitProgram();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = homePage;
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = employeePage;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            CloseProgram();
        }

        private void CloseProgram()
        {
            App.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }

        private void InitProgram()
        {
            MainContent.Content = employeePage;
        }
    }
}
