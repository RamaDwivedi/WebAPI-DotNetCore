
public class EmployeeRepo : IEmployeeRepo
{
    private AppDbContext appDbContext;
    private ILogger<EmployeeRepo> logger;
    public EmployeeRepo(AppDbContext appDbContext, ILogger<EmployeeRepo> logger)
    {
        this.appDbContext = appDbContext;
        this.logger = logger;
    }
    public Employee GetEmployeeById(int id)
    {
        try
        {
            var employee = appDbContext.Employee.Where(x => x.Id == id).FirstOrDefault();
            if (employee != null)
            {
                return employee;
            }
            else
            {
                logger.LogError("Employee record not found");
                return new Employee();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, "Record not found");
            throw;
        }

    }


    public List<Employee> GetEmployees()
    {
        try
        {
            return appDbContext.Employee.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(" Error: ", ex.Message);
            throw;
        }
    }


    public bool AddEmployee(Employee employee)
    {
        try
        {
            var existemployee = appDbContext.Employee.Where(x => x.Name.ToLower() == employee.Name.ToLower()).FirstOrDefault();

            if (existemployee == null)
            {
                appDbContext.Employee.Add(employee);
                appDbContext.SaveChanges();
                return true;
            }
            logger.LogError(" AddEmployee: Error: Employee Already Present.");
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError("EmployeeRepository  Error: ", ex.Message);
            throw;
        }
    }

    public bool UpdateEmployee(Employee employee)
    {
        try
        {
           

            var existemployee = appDbContext.Employee.Where(x => x.Id.ToString()== employee.Id.ToString()).FirstOrDefault();
            if (existemployee != null)
            {
                existemployee.Name = employee.Name;
                existemployee.City = employee.City;
                existemployee.PhoneNumber = employee.PhoneNumber;
                appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError("UpdateEmployee: Error: ", ex.Message);
            throw;
        }
    }

    public bool DeleteEmployee(int id)
    {
        try
        {
            var employee = appDbContext.Employee.Find(id);
            if (employee != null)
            {
                appDbContext.Employee.Remove(employee);
                appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError("DeleteEmployee: Error: ", ex.Message);
            throw;
        }
    }

}




