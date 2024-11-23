using DWHNorthwindOrders.Northwind;
using DWHNorthwindOrders.DWHNorthwindOrders;
using System;
using System.Runtime.Serialization;

class Program
{
    static async Task Main(string[] args)
    {
        using (var dwhContext = new DWHNorthwindOrdersContext())
        {
            using (var northwindContext = new NorthwindContext())
            {
                var service = new DWHNorthwindOrdersServices(dwhContext, northwindContext);

                service.CleanFactTables();

                DWHNorthwindOrdersServices.CleanTables(dwhContext);

                Console.WriteLine("Limpieza completada.");

                DWHNorthwindOrdersServices.LoadDimData(dwhContext, northwindContext);

                Console.WriteLine("Limpieza completada.");

                Console.WriteLine("Carga de datos de las dimensiones completada.");

                DWHNorthwindOrdersServices.LoadFactOrders(dwhContext, northwindContext);
                DWHNorthwindOrdersServices.LoadFactCustomerAttended(dwhContext, northwindContext);

                Console.WriteLine("Carga de datos de las tablas de hechos completada.");

                var dataView = new ViewDWHNorthwindOrders(dwhContext, northwindContext);
                dataView.ViewData();
            }
        }
    }
}
