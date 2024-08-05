
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
    private readonly IEmployeeServices employeeServices;
    public EmployeeController(IEmployeeServices employeeServices)
    {
        this.employeeServices = employeeServices;
    }

    [HttpGet]
    public IActionResult AddEmployees()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEmployees(Employee employee)
    {
        if (ModelState.IsValid)
        {
            employeeServices.AddEmployeeAsync(employee);
            return RedirectToAction("Employees");
        }
        return View(employee);
    }


    [HttpGet]
    public async Task<IActionResult> Employees()
    {
        var employees = await employeeServices.GetEmployeeAsync();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Delete()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (id != null)
        {
            await employeeServices.DeleteEmployeeAsync(id);
            return RedirectToAction("Employees");
        }
        return View(null);
    }

    [HttpGet]

    public async Task<IActionResult> Update(int id)
    {
        var employee = await employeeServices.GetEmployeeByIdAsync(id);

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await employeeServices.UpdateEmployeeAsync(employee);
            return RedirectToAction("Employees");
        }

        return View(employee);
    }

}



