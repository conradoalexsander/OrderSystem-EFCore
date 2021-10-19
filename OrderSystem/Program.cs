using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Domain;
using OrderSystem.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ApplicationContext();

            db.Database.Migrate();

            //InsertData();
            //InsertMassData();
            //FetchData();
            //FetchOrderWithEagerLoading();
            //UpdateData();
            //UpdateDataFromDisconnectedCustomer();
            //UpdateDataUsingAttachedCustomer();
            InsertOrder();
            RemoveRecord();
        }

        private static void RemoveRecord()
        {
            var db = new ApplicationContext();

            var customer = db.Customers.OrderBy(c => c.Id).LastOrDefault();
            //db.Customers.Remove(customer);
            //db.Remove(customer);

            db.Customers.Remove(new Customer { Id = 1 });

            db.Entry(customer).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void UpdateDataUsingAttachedCustomer()
        {
            var db = new ApplicationContext();

            var unknownCustomer = new
            {
                Id = 1,
                Name = "Dude from below",
                Telephone = "00000000",
            };

            var customer = new Customer
            {
                Id = 1
            };

            db.Attach(customer); //this will make EF track this object
            db.Entry(customer).CurrentValues.SetValues(unknownCustomer);

            db.SaveChanges();
        }

        private static void UpdateData()
        {
            var db = new ApplicationContext();
            var customer = db.Customers.FirstOrDefault();
            customer.Name = "Anna Doe";

            //this line will make the entire Object (the DataBase Row) to be updated, maybe not recommended
            //db.Customers.Update(customer);

            //since all the objects are tracked, you can just use this method and the data will be update properly
            //This means, only the relevant columns
            db.SaveChanges(); 
        }

        private static void UpdateDataFromDisconnectedCustomer()
        {
            var db = new ApplicationContext();

            var unknownCustomer = new
            {
                Id=1,
                Name = "Dude from Above",
                Telephone = "00000000",
            };

            var customer = db.Customers.Find(unknownCustomer.Id);

            db.Entry(customer).CurrentValues.SetValues(unknownCustomer);
           
            db.SaveChanges(); 
        }

        private static void FetchOrderWithEagerLoading()
        {
            var db = new ApplicationContext();

            //The Include Methods will ensure that the fetch also returns associated objects (1:N, N:N, etc., relationships)
            var orders = db.Orders.Include(p => p.Items)
                                  .ThenInclude(p => p.Product)
                                  .ToList();

            Console.WriteLine(orders.Count);
        }

        private static void InsertOrder()
        {
            var db = new ApplicationContext();

            var customer = db.Customers.FirstOrDefault();
            var product = db.Products.FirstOrDefault();

            var order = new Order
            {
                CustomerId = customer.Id,
                StartedIn = DateTime.Now,
                FinishedIn = DateTime.Now,
                Status = OrderStatus.Analysis,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Discount = 0,
                        Quantity = 1,
                        Value = 10
                    }
                }
            };

            db.Orders.Add(order);
            db.SaveChanges();
        }

        private static List<Customer> FetchData()
        {
            var db = new ApplicationContext();

            //var fetchBySintax = (from c in db.Customers where c.Id > 0 select c); //linq syntaxes

            //this will generate a tracked object list. This means that if you change an object in the list and save the changes, it will be persisted
            //var fetchByMethod = db.Customers.Where(p => p.Id > 0).ToList();

            //The AsNoTracking remove the default tracked objects generation
            var fetchByMethod = db.Customers.AsNoTracking()
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList()
;           
            foreach (var customer in fetchByMethod)
            {
                db.Customers.Find(customer.Id); //Try to fetch first in memory data. If it can't, it fetch the data in DataBase
            }

            return fetchByMethod;
        
        }

        private static void InsertMassData()
        {
            var db = new ApplicationContext();

            var product = new Product
            {
                Description = "Test Product",
                BarCode = "123456789",
                Value = 10m,
                ProductType = ProductType.ProductForResale,
                IsActive = true
            };

            var customer = new Customer
            {
                Name = "John Doe",
                CEP = "999000",
                City = "Test River",
                State = "TS",
                Telephone = "99999999999"
            };

            var customersList = new List<Customer>
            {
                new Customer 
                {
                    Name = "John Doe",
                    CEP = "999000",
                    City = "Test River",
                    State = "TS",
                    Telephone = "99999999999"
                },
                new Customer
                {
                    Name = "John Doe",
                    CEP = "999000",
                    City = "Test River",
                    State = "TS",
                    Telephone = "99999999999"
                }

            };

           db.AddRange(product, customer);
           db.Customers.AddRange(customersList);

            var registers = db.SaveChanges();
            Console.WriteLine($"Total affected registers: {registers}");
        }

        private static void InsertData()
        {
            var db = new ApplicationContext();

            var product = new Product
            {
                Description = "Test Product",
                BarCode = "123456789",
                Value = 10m,
                ProductType = ProductType.ProductForResale,
                IsActive = true
            };

            db.Products.Add(product); //recomended

            //These methods have the same effect
            //db.Set<Product>().Add(product); //recomended
            //db.Entry(product).State = EntityState.Added;
            //db.Add(product); this one can cause a degradation in performance

            var registers = db.SaveChanges(); //EF Core always uses the changes on the stance of the object
            Console.WriteLine(registers);
        }
    }
}
