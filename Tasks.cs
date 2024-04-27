using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

public class Tasks
{
    public static void Main()
    {
        /**
         1.Could you briefly give 1 - 2 examples of test automation tasks that you've had to solve for the last ~6 months?
        

         **/



        /** 
           Task 1: Logger
            Context: Your team is working on a project where you need to log various events and errors.
            You are asked to create a simple logging function that writes messages to a text file with a timestamp.
            Example usage:
            log_message("application.log", "User logged in", "INFO")
            log_message("application.log", "Failed login attempt", "WARNING")

            Expected Output in application.log:
            [2023-04-24 12:34:56] [INFO] User logged in
            [2023-04-24 12:35:10] [WARNING] Failed login attempt 
        **/
        // method LogMessage
        Logger.LogMessage("application.log", "User logged in", "INFO");
        Logger.LogMessage("application.log", "Failed login attempt", "WARNING");


        /**
         Task 1.1: Write tests scenarios for Logger
            TC1 Verify log method creates an application.log file
            TC2 Verify log method writes logs with the following format [YYYY-MM-DD HH:MM:SS][LOG-LEVEL] "Message"
            TC3 Verify log method writes multiple messages
            TC4 Verify log method returns an error message with empty values
            TC5 Verify log method return an error message with invalid values
        **/

         /**
          Task 2: Inventory Management
            Context: You are developing a simple inventory management system for a small store.
            You need to create a function that takes a list of products with their names, prices, and stock levels, and returns a sorted list of products based on a given sort key (name, price, or stock) and order (ascending or descending).

            Example Input:
            products = [
            {"name": "Product A", "price": 100, "stock": 5},
            {"name": "Product B", "price": 200, "stock": 3},
            {"name": "Product C", "price": 50, "stock": 10}
            ]
            sort_key = "price"
            ascending = False

            Expected Output:
            [
            {"name": "Product B", "price": 200, "stock": 3},
            {"name": "Product A", "price": 100, "stock": 5},
            {"name": "Product C", "price": 50, "stock": 10}
            ]
        **/
           // creating products 
            var products = new List<Product>
            {
                new Product { Name = "Product A", Price = 100, Stock = 5 },
                new Product { Name = "Product B", Price = 200, Stock = 3 },
                new Product { Name = "Product C", Price = 50, Stock = 10 }
            };
            var inventary = new Inventary(products);
            
            // sort criteria 
            var sortKey = "price";
            var ascending = true;
            // method SortProducts
            inventary.SortProducts(sortKey, ascending);
    }
}


public static class Logger 

{
    public static void LogMessage(string filePath, string message, string logLevel)
    {
        try {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] {message}";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(logEntry);
            }
        } 
        catch {
            throw new ArgumentException("Invalid values please enter the correct format [YYYY-MM-DD HH:MM:SS][LOG-LEVEL] \"MESSAGE\"");
        }

    }
}


public class Inventary
{
    public List<Product> products;

    public Inventary(List<Product> products)
    {
        this.products = products;
    }

    public void SortProducts(string sortKey, bool ascending)
    {
        products = SortProductsByKey(sortKey, ascending);
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
        }
    }

    private List<Product> SortProductsByKey(string sortKey, bool ascending) 
    {
        switch (sortKey.ToLower())
        {
            case "name":
                return ascending ? products.OrderBy(p => p.Name).ToList() : products.OrderByDescending(p => p.Name).ToList();
            case "price":
                return ascending ? products.OrderBy(p => p.Price).ToList() : products.OrderByDescending(p => p.Price).ToList();
            case "stock":
                return ascending ? products.OrderBy(p => p.Stock).ToList() : products.OrderByDescending(p => p.Stock).ToList();
            default:
                throw new ArgumentException("Invalid sort value: " + sortKey);
        }
    }
}


public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
}