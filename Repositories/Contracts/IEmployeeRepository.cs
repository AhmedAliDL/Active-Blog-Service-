using Active_Blog_Service.Models;

namespace Active_Blog_Service.Repositories.Contracts
{
    public interface IEmployeeRepository : IAddScopedService
    {
        List<Employee> GetAllEmployees();
    }
}
