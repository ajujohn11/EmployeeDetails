using ApiManager;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// 
        /// </summary>
        public EmployeeService()
        {
            _apiClient = new ApiHelper().ApiClient;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<Employee>> GetEmployeesRequest(ApiRequest apiRequest)
        {
            ApiPagedResult<Employee> pagedResult = new ApiPagedResult<Employee>();

            string searchParams = $"users?page={apiRequest.page}&limit={apiRequest.limit}";
            ////string searchParams = $"users?page={apiRequest.page}&limit={apiRequest.limit}&per_page={apiRequest.limit}";
            if (apiRequest.id.HasValue)
                searchParams += $"&id={apiRequest.id.Value}";

            if (!string.IsNullOrEmpty(apiRequest.name))
                searchParams += $"&name={apiRequest.name}";

            if (!string.IsNullOrEmpty(apiRequest.email))
                searchParams += $"&email={apiRequest.email}";

            if (!string.IsNullOrEmpty(apiRequest.gender) && !apiRequest.gender.Equals("all"))
                searchParams += $"&gender={apiRequest.gender}";

            if (!string.IsNullOrEmpty(apiRequest.status) && !apiRequest.status.Equals("all"))
                searchParams += $"&status={apiRequest.status}";

            ////Get response from api
            var response = await _apiClient.GetAsync(searchParams);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiBaseResponse>(apiResponse);
                if (result.code != (int)HttpStatusCode.OK)  //Error from api
                {
                    var errorResponse = JsonConvert.DeserializeObject<ApiError>(apiResponse);
                    var errorData = errorResponse.data.Select(s => $"{ s.field}:{s.message}");
                    pagedResult.ApiResponseMessage = $"{result.code} : Error occured while processing data: {errorData}";
  
                }
                pagedResult = JsonConvert.DeserializeObject<ApiPagedResult<Employee>>(apiResponse);  
            }
            else
            {
                pagedResult.ApiResponseMessage = response.ReasonPhrase;
            }
                
            return pagedResult;
        }

        /// <summary>
        /// GET by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeebyId(int employeeId)
        {
            var apiRequest = new ApiRequest { id = employeeId };
            var employee = await GetEmployeesRequest(apiRequest);
            return employee.data.FirstOrDefault();
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<Employee>> CreateEmployeeRequest(ApiRequest apiRequest)
        {
            ApiPagedResult<Employee> pagedResult = new ApiPagedResult<Employee> { data = new List<Employee>() };

            //Post data
            var response = await _apiClient.PostAsync("users", new StringContent(JsonConvert.SerializeObject(apiRequest), Encoding.UTF8, "application/json"));
            
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiBaseResponse>(apiResponse);
                pagedResult.code = result.code;
                if (result.code != (int)HttpStatusCode.Created)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ApiError>(apiResponse);
                    var errorData = errorResponse.data.Select(s => $"{ s.field}:{s.message}").FirstOrDefault();
                    pagedResult.ApiResponseMessage = $"{result.code} : Error occured while processing request, {errorData}";
                }
                else
                {
                    var dataResult = JsonConvert.DeserializeObject<ApiDataResult<Employee>>(apiResponse);
                    pagedResult.data.Add(dataResult.data);
                    pagedResult.ApiResponseMessage = $"Employee created successfully";
                }
            }
            else
            {
                pagedResult.ApiResponseMessage = response.ReasonPhrase;
            }

            return pagedResult;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<Employee>> UpdateEmployeeRequest(ApiRequest apiRequest)
        {
            ApiPagedResult<Employee> pagedResult = new ApiPagedResult<Employee> { data = new List<Employee>() };

            //Put data
            var response = await _apiClient.PutAsync($"users/{apiRequest.id}", new StringContent(JsonConvert.SerializeObject(apiRequest), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiBaseResponse>(apiResponse);
                pagedResult.code = result.code;
                if (result.code != (int)HttpStatusCode.OK)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ApiError>(apiResponse);
                    var errorData = errorResponse.data.Select(s => $"{ s.field}:{s.message}").FirstOrDefault();
                    pagedResult.ApiResponseMessage = $"{result.code} : Error occured while processing request, {errorData}";
                }
                else
                {
                    var dataResult = JsonConvert.DeserializeObject<ApiDataResult<Employee>>(apiResponse);
                    pagedResult.data.Add(dataResult.data);
                    pagedResult.ApiResponseMessage = $"Employee updated successfully";
                }
            }
            else
            {
                pagedResult.ApiResponseMessage = response.ReasonPhrase;
            }

            return pagedResult;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<Employee>> DeleteEmployeeRequest(ApiRequest apiRequest)
        {
            ApiPagedResult<Employee> pagedResult = new ApiPagedResult<Employee>();
            //Delete Data
            var response = await _apiClient.DeleteAsync($"users/{apiRequest.id}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiBaseResponse>(apiResponse);
                pagedResult.code = result.code;
                if (result.code != (int)HttpStatusCode.NoContent)
                {
                    pagedResult.ApiResponseMessage = $"{result.code} : Error occured while processing delete request.";
                }
                else
                {
                    pagedResult.ApiResponseMessage = $"Employee deleted successfully";
                }

            }
            else
            {
                pagedResult.ApiResponseMessage = response.ReasonPhrase;
            }

            return pagedResult;
        }
    }
}
