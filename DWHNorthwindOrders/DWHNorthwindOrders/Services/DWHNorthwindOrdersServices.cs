using DWHNorthwindOrders.DWHNorthwindOrders.Entities;
using DWHNorthwindOrders.DWHNorthwindOrders;
using DWHNorthwindOrders.Northwind;
using Microsoft.EntityFrameworkCore;
using DWHNorthwindOrders.Northwind.Entities;

public class DWHNorthwindOrdersServices
{
    private readonly DWHNorthwindOrdersContext _DWHNorthwindOrdersContext;
    private readonly NorthwindContext _NorthwindContext;

    public DWHNorthwindOrdersServices(DWHNorthwindOrdersContext dWHNorthwindOrdersContext, NorthwindContext northwindContext)
    {
        _DWHNorthwindOrdersContext = dWHNorthwindOrdersContext;
        _NorthwindContext = northwindContext;
    }

    public static void CleanTables(DWHNorthwindOrdersContext context)
    {
        context.DimCategory.RemoveRange(context.DimCategory);
        context.DimCustomer.RemoveRange(context.DimCustomer);
        context.DimDate.RemoveRange(context.DimDate);
        context.DimEmployee.RemoveRange(context.DimEmployee);
        context.DimProduct.RemoveRange(context.DimProduct);
        context.DimShipper.RemoveRange(context.DimShipper);

        context.SaveChanges();
    }

    public void CleanFactTables()
    {
        using (var transaction = _DWHNorthwindOrdersContext.Database.BeginTransaction())
        {
            try
            {
                _DWHNorthwindOrdersContext.Database.ExecuteSqlRaw("EXEC CleanFactTables");

                transaction.Commit();
                Console.WriteLine("Las tablas de hechos han sido limpiadas con éxito.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error al limpiar las tablas de hechos: {ex.Message}");
            }
        }
    }

    public static void LoadDimData(DWHNorthwindOrdersContext context, NorthwindContext northwindContext)
    {
        var dimDates = northwindContext.Orders
            .Select(o => new DimDate
            {
                DateID = o.OrderDate.Year * 100 + o.OrderDate.Month,
                DateYear = o.OrderDate.Year,
                DateMonth = o.OrderDate.Month
            })
            .Distinct()
            .ToList();
        context.DimDate.AddRange(dimDates);

        var dimProducts = northwindContext.Products
            .Select(p => new DimProduct
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryID = p.CategoryID,
                UnitPrice = northwindContext.OrdersDetails
                    .Where(od => od.ProductID == p.ProductID)
                    .Select(od => od.UnitPrice)
                    .FirstOrDefault(),
                Quantity = (short)northwindContext.OrdersDetails
                    .Where(od => od.ProductID == p.ProductID)
                    .Sum(od => od.Quantity)
            })
            .ToList();
        context.DimProduct.AddRange(dimProducts);

        var dimCustomers = northwindContext.Customers
            .Select(c => new DimCustomer
            {
                CustomerID = c.CustomerID,
                CompanyName = c.CompanyName
            })
            .ToList();
        context.DimCustomer.AddRange(dimCustomers);

        var dimCategories = northwindContext.Categories
            .Select(c => new DimCategory
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName
            })
            .ToList();
        context.DimCategory.AddRange(dimCategories);

        var dimShippers = northwindContext.Shippers
            .Select(s => new DimShipper
            {
                ShipperID = s.ShipperID,
                CompanyName = s.CompanyName
            })
            .ToList();
        context.DimShipper.AddRange(dimShippers);

        var dimEmployees = northwindContext.Employees
            .Select(e => new DimEmployee
            {
                EmployeeID = e.EmployeeID,
                Employee = e.FirstName + " " + e.LastName
            })
            .ToList();
        context.DimEmployee.AddRange(dimEmployees);

        context.SaveChanges();
    }

    public static void LoadFactOrders(DWHNorthwindOrdersContext context, NorthwindContext northwindContext)
    {
        var dimDates = context.DimDate.ToList();
        var dimProducts = context.DimProduct.ToList();
        var dimCustomers = context.DimCustomer.ToList();
        var dimCategories = context.DimCategory.ToList();
        var dimEmployees = context.DimEmployee.ToList();
        var dimShippers = context.DimShipper.ToList(); //Emelyn Del Carmen Jose

        var factOrders = northwindContext.VwFactOrder
            .AsEnumerable()
            .Select(f => new FactOrder
            {
                OrderID = f.OrderID,
                DateID = dimDates
                    .Where(d => d.DateYear == f.DateID / 100 && d.DateMonth == f.DateID % 100)
                    .Select(d => d.DateID)
                    .FirstOrDefault(),
                ProductID = dimProducts
                    .Where(p => p.ProductID == f.ProductID)
                    .Select(p => p.ProductID)
                    .FirstOrDefault(),
                CustomerID = dimCustomers
                    .Where(c => c.CustomerID == f.CustomerID)
                    .Select(c => c.CustomerID)
                    .FirstOrDefault(),
                CategoryID = dimCategories
                    .Where(c => c.CategoryID == f.CategoryID)
                    .Select(c => c.CategoryID)
                    .FirstOrDefault(),
                EmployeeID = dimEmployees
                    .Where(e => e.EmployeeID == f.EmployeeID)
                    .Select(e => e.EmployeeID)
                    .FirstOrDefault(),
                ShipperID = dimShippers
                    .Where(s => s.ShipperID == f.ShipperID)
                    .Select(s => s.ShipperID)
                    .FirstOrDefault(),
                ShipRegion = f.ShipRegion,
                ShipCity = f.ShipCity,
                UnitPrice = f.UnitPrice,
                Quantity = f.Quantity,
                TotalOrders = f.TotalOrders,
                TotalQuantity = f.TotalQuantity
            })
            .ToList();

        context.FactOrder.AddRange(factOrders);
        context.SaveChanges();
    }

    public static void LoadFactCustomerAttended(DWHNorthwindOrdersContext context, NorthwindContext northwindContext)
    {
        var factCustomerAttendedData = northwindContext.VwFactCustomerAttended.ToList();

        var factCustomerAttended = factCustomerAttendedData.Select(f => new FactCustomerAttended
        {
            CustomerAttended = f.CustomerAttended,
            EmployeeID = context.DimEmployee
                .Where(e => e.EmployeeID == f.EmployeeID)
                .Select(e => e.EmployeeID)
                .FirstOrDefault(),
            TotalCustomer = (int)f.TotalCustomer
        }).ToList();

        context.FactCustomerAttended.AddRange(factCustomerAttended);
        context.SaveChanges();
    }
}
