using EmployeeServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppUnitTest
{
    [TestClass]
    public class EmployeeDetailsUnitTest
    {
        
        public IEmployeeService employeeService = new EmployeeService(new ApiManager.ApiHelper());


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
