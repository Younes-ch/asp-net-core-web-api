using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(
        this IQueryable<Employee> employees, uint minAge, uint maxAge)
    {
        return employees.Where(emp => emp.Age >= minAge && emp.Age <= maxAge);
    }

    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return employees;

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return employees.Where(emp => emp.Name.ToLower().Contains(lowerCaseSearchTerm));
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString)) return employees.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        return string.IsNullOrWhiteSpace(orderQuery) ? employees.OrderBy(e => e.Name) : employees.OrderBy(orderQuery);
    }
}