using EmployeeServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace AppUnitTest
{
    [TestClass]
    public class EmployeeDetailsUnitTest
    {

        public IEmployeeService employeeService;

        public EmployeeDetailsUnitTest()
        {
            employeeService = new EmployeeService();
        }


        [TestMethod]
        public async Task CreateUserTest()
        {
            var randomNumber = new System.Random().Next(10000, 100000);
            var name = $"testname{randomNumber}";
            var email = $"testmail{randomNumber}@mail.com";
            var status = "active";
            var gender = "male";

            var result = await employeeService.CreateEmployeeRequest(new ApiManager.ApiRequest
            {
                name = name,
                email = email,
                gender = gender,
                status = status
            });

            var employee = result.data.FirstOrDefault();

            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.id > 0);
            Assert.AreEqual(employee.email, email);
            Assert.AreEqual(employee.name, name);
            Assert.AreEqual(employee.gender, gender.ToString());
            Assert.AreEqual(employee.status, status.ToString());
        }

        [TestMethod]
        public async Task UpdateUserTest()
        {
        }


        [TestMethod]
        public async Task DeleteUserTest()
        {
        }

    }
}
