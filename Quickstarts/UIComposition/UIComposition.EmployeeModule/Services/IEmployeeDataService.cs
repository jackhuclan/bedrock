using System.Collections.Generic;
using UIComposition.EmployeeModule.Models;

namespace UIComposition.EmployeeModule.Services
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IEmployeeDataService
    {
        List<Employee> GetEmployees();
        List<Project> GetProjects();
    }
}