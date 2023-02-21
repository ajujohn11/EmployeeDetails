using ApiManager;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> GetEmployeeListRequest(ApiRequest apiRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResult<Employee>> CreateEmployeeRequest(ApiRequest apiRequest);
    }
}
