using ApiManager;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> GetEmployeesRequest(ApiRequest apiRequest);

        /// <summary>
        /// GET by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeebyId(int employeeId);

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> CreateEmployeeRequest(ApiRequest apiRequest);

        /// <summary>
        /// PUT
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> UpdateEmployeeRequest(ApiRequest apiRequest);

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> DeleteEmployeeRequest(ApiRequest apiRequest);

    }
}
