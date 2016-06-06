using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApp.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext()
            : base("Server=(localdb)\\mssqllocaldb;Database=MvcCoreMigrations;Trusted_Connection=True;MultipleActiveResultSets=true")
        { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        protected bool Equals(Order other) => OrderID == other.OrderID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((Order)obj);
        }

        public override int GetHashCode() => OrderID.GetHashCode();

        public override string ToString() => "Order " + OrderID;
    }

    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

        protected bool Equals(OrderDetail other)
            => OrderID == other.OrderID
               && ProductID == other.ProductID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((OrderDetail)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (OrderID * 397) ^ ProductID;
            }
        }
    }

    public class Product
    {
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        protected bool Equals(Product other) => Equals(ProductID, other.ProductID);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((Product)obj);
        }

        public override int GetHashCode() => ProductID.GetHashCode();

        public override string ToString() => "Product " + ProductID;
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; } // shadow-prop in EF model
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string PhotoPath { get; set; }

        public Employee Manager { get; set; }

        protected bool Equals(Employee other) => EmployeeID == other.EmployeeID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((Employee)obj);
        }

        public override int GetHashCode() => EmployeeID.GetHashCode();

        public override string ToString() => "Employee " + EmployeeID;
    }

    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [NotMapped]
        public bool IsLondon => City == "London";

        protected bool Equals(Customer other) => string.Equals(CustomerID, other.CustomerID);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((Customer)obj);
        }

        public static bool operator ==(Customer left, Customer right) => Equals(left, right);

        public static bool operator !=(Customer left, Customer right) => !Equals(left, right);

        public override int GetHashCode() => CustomerID.GetHashCode();

        public override string ToString() => "Customer " + CustomerID;
    }
}