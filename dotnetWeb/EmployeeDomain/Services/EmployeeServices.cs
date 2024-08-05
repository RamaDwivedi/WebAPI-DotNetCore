using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

public class EmployeeServices:IEmployeeServices{
    private readonly HttpClient _client;
     private readonly string _baseUrl = "https://localhost:7169/api/employee";
    public EmployeeServices(HttpClient client)
    {
    //    _client=client;
          var clientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
 
        _client = new HttpClient(clientHandler);
      
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<Employee>($"{_baseUrl}/{id}");
        }

         public async Task<List<Employee>> GetEmployeeAsync()
        {
          
            try
        {
                HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/GetAllEmployees");

        response.EnsureSuccessStatusCode(); 

        var employee = await response.Content.ReadFromJsonAsync<List<Employee>>();
        return employee;
   
            // return await _client.GetFromJsonAsync<List<Employee>>(_baseUrl);
        }
        catch (HttpRequestException ex)
        {
            // Handle the exception (log it, rethrow it, or return a default value)
           Console.WriteLine(ex.Message);
            throw;
        }
        }

          public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/AddEmployees", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var response = await _client.PutAsJsonAsync($"{_baseUrl}/UpdateEmployees", employee);
            return response.IsSuccessStatusCode;
        }
       
       public async Task<bool> DeleteEmployeeAsync(int id)
       {
         var response = await _client.DeleteAsync($"{_baseUrl}/{id}");
           return response.IsSuccessStatusCode;
       
       }
}