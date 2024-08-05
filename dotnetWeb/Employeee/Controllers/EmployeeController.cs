
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepo _employeeRepository;

    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeRepo employeeRepository, ILogger<EmployeeController> logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    [HttpGet("{id}")]

    public IActionResult GetEmployeeById(int id)
    {
        try
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Result not found");
            }
            return Ok(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Employee id not found,  {ex.Message}");
            return StatusCode(500, "Internal Server error");
        }
    }

    [HttpGet]
    [Route("GetAllEmployees")]
    public IActionResult GetEmployees()
    {
        try
        {
            var employees = _employeeRepository.GetEmployees();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError($" GetEmployees: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [Route("AddEmployees")]
    public IActionResult AddEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee object is null");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model object");
        }

        try
        {
            bool result = _employeeRepository.AddEmployee(employee);
            if (!result)
            {
                return Conflict("employee already exists");
            }
            return CreatedAtAction("GetEmployeeById", new { id = employee.Id }, employee);
        }
        catch (Exception ex)
        {
            _logger.LogError($"EmployeeController -> AddEmployee: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    [Route("UpdateEmployees")]
    public IActionResult UpdateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee object is null");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model object");
        }

        try
        {
            var existingEmployee = _employeeRepository.GetEmployeeById(employee.Id);
            if (existingEmployee == null || existingEmployee.Id == 0)
            {
                return NotFound("Employee not found");
            }

            bool result = _employeeRepository.UpdateEmployee(employee);
            if (!result)
            {
                return NotFound("Employee not found");
            }

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError($"EmployeeController -> UpdateEmployee: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {

    try
        {
            var employee1 =  _employeeRepository.GetEmployeeById(id);
            if (employee1 == null)
            {
                return NotFound("Employee not found");
            }

            bool result =  _employeeRepository.DeleteEmployee(id);
            if (!result)
            {
                return NotFound("Employee not found");
            }

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }     


}



