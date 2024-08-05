

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public interface IEmployeeRepo{
     List<Employee> GetEmployees();
        bool AddEmployee(Employee emp);
        Employee GetEmployeeById(int id);
        bool UpdateEmployee(Employee employee);

        bool DeleteEmployee(int id);
}