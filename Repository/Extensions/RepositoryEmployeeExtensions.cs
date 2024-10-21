using Entities.Models;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(
            this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
            employees.Where(emp => (emp.Age >= minAge && emp.Age <= maxAge));

        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return employees;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return employees.Where(emp => emp.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

    }
}
