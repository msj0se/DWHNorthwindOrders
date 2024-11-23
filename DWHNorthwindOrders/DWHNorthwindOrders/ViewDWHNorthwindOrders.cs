using DWHNorthwindOrders.Northwind;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders
{
    public class ViewDWHNorthwindOrders
    {
        private readonly DWHNorthwindOrdersContext _dwhContext;
        private readonly NorthwindContext _northwindContext;

        public ViewDWHNorthwindOrders(DWHNorthwindOrdersContext dwhContext, NorthwindContext northwindContext)
        {
            _dwhContext = dwhContext;
            _northwindContext = northwindContext;
        }

        public void ViewData()
        {
            var dimCategory = _dwhContext.DimCategory.ToList();
            var dimCustomer = _dwhContext.DimCustomer.ToList();
            var dimDate = _dwhContext.DimDate.ToList();
            var dimEmployee = _dwhContext.DimEmployee.ToList();
            var dimProduct = _dwhContext.DimProduct.ToList();
            var dimShipper = _dwhContext.DimShipper.ToList();
            var factOrder = _dwhContext.FactOrder.ToList();
            var factCustomerAttended = _dwhContext.FactCustomerAttended.ToList();

            Console.WriteLine($"Datos cargados a DimCategory: {dimCategory.Count} registros.");
            Console.WriteLine($"Datos cargados a DimCustomer: {dimCustomer.Count} registros.");
            Console.WriteLine($"Datos cargados a DimDate: {dimDate.Count} registros.");
            Console.WriteLine($"Datos cargados a DimEmployee: {dimEmployee.Count} registros.");
            Console.WriteLine($"Datos cargados a DimProduct: {dimProduct.Count} registros.");
            Console.WriteLine($"Datos cargados a DimShipper: {dimShipper.Count} registros.");
            Console.WriteLine($"Datos cargados a FactOrder: {factOrder.Count} registros.");
            Console.WriteLine($"Datos cargados a FactCustomerAttended: {factCustomerAttended.Count} registros.");
        }
    }
}
