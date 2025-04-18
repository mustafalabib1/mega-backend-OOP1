/*
 * OOP - CONSTRUCTOR
 * Constructors are special methods within a class that are responsible for initializing objects
 * of that class when they are created (instantiated). They have the same name as the class
 * and do not have a return type (not even void).
 */


/*
 * IMPLICIT CONSTRUCTOR
 * If you do NOT explicitly define *any* constructor in your class, C# will automatically
 * provide a default, parameterless, public constructor. This implicit constructor
 * initializes all fields to their default values (0 for numeric types, null for reference types,
 * false for booleans, etc.).
 */
public class SimpleClass // No constructor defined
{
    public int Value; // Will be initialized to 0 by the implicit constructor
    public string Name; // Will be initialized to null by the implicit constructor
}


/*
 * OVERLOADED CONSTRUCTOR
 * A class can have multiple constructors, provided they have different parameter lists
 * (different signatures). This allows you to create objects in different ways, providing
 * varying degrees of initial state upon creation.
 */
public class Product
{
    public int Id;
    public string Name;
    public decimal Price;

    // Constructor 1: Parameterless constructor
    public Product()
    {
        Id = 0;
        Name = "Unknown";
        Price = 0m;
        Console.WriteLine("Product created with parameterless constructor.");
    }

    // Constructor 2: Constructor with Id and Name
    public Product(int id, string name)
    {
        Id = id;
        Name = name;
        Price = 0m; // Default price
        Console.WriteLine($"Product created with Id '{id}' and Name '{name}'.");
    }

    // Constructor 3: Constructor with Id, Name, and Price
    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
        Console.WriteLine($"Product created with Id '{id}', Name '{name}', and Price '{price}'.");
    }
}


/*
 * THIS KEYWORD
 * The 'this' keyword in C# refers to the current instance of the class. It has several uses:
 * 1. To distinguish instance members from local variables or parameters with the same name.
 * 2. To pass the current instance as an argument to another method.
 * 3. Crucially for constructors: To call another constructor within the same class (constructor chaining).
 * This is done using ': this(...)' after the constructor signature.
 */
public class Employee
{
    public int Id;
    public string Name;
    public double Salary;

    // Constructor 1: Basic constructor
    public Employee(int id, string name)
    {
        // Use 'this.' to clarify setting instance fields, although not strictly necessary here
        this.Id = id;
        this.Name = name;
        this.Salary = 0; // Default salary
        Console.WriteLine($"Employee created: {Name}");
    }

    // Constructor 2: Overloaded constructor chaining to the first one
    public Employee(int id, string name, double salary) : this(id, name) // Calls the Employee(int, string) constructor first
    {
        // 'this' here refers to the current instance
        this.Salary = salary;
        Console.WriteLine($"Employee {Name} salary set to {Salary}.");
    }

    // Example showing 'this' to differentiate parameter from field
    public void SetSalary(double Salary) // Parameter has same name as field
    {
        this.Salary = Salary; // 'this.Salary' refers to the field, 'Salary' refers to the parameter
    }
}


/*
 * NON PUBLIC CONSTRUCTOR
 * Constructors can have access modifiers other than 'public'. This restricts how objects can be created.
 * - private: Prevents any code outside the class (even derived classes) from creating instances directly.
 * Commonly used for static helper classes or implementing the Singleton design pattern (where only one instance is allowed).
 * - protected: Allows instantiation only by derived classes.
 * - internal: Allows instantiation only within the same assembly.
 */
public class Singleton
{
    // Static field to hold the single instance
    private static Singleton _instance;

    // Private constructor: Prevents direct instantiation from outside
    private Singleton()
    {
        Console.WriteLine("Singleton instance created.");
    }

    // Public static method to get the single instance
    public static Singleton GetInstance()
    {
        // Lazy initialization: create the instance only if it doesn't exist
        if (_instance == null)
        {
            _instance = new Singleton();
        }
        return _instance;
    }

    public void DoSomething()
    {
        Console.WriteLine("Singleton is doing something.");
    }
}

// public class DerivedSingleton : Singleton { } // ERROR: Cannot access private constructor


/*
 * OBJECT INITIALIZER
 * A convenient syntax introduced in C# 3.0 to assign values to accessible properties or fields
 * of an object immediately after the constructor is called. It's syntactic sugar and doesn't
 * replace the constructor, which still runs first. Useful for setting many properties concisely.
 */
public class Book
{
    public string Title { get; set; } // Public Property
    public string Author { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }

    // Constructor (can be parameterless or take some parameters)
    public Book()
    {
        Console.WriteLine("Book constructor called.");
        // Default initialization if needed
        Title = "Untitled";
    }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        Console.WriteLine($"Book constructor called for '{Title}' by {Author}.");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Book: '{Title}' by {Author}, {Pages} pages, ${Price}");
    }
}


/*
 * READONLY FIELD
 * A field modifier that indicates the field's value can only be assigned:
 * 1. At the point of declaration.
 * 2. Within the constructor(s) of the class.
 * Once assigned (either at declaration or in the constructor), its value cannot be changed.
 * Useful for setting values that are known at object creation but should not change afterwards.
 * Unlike 'const', 'readonly' can be used with reference types and its value can be determined at runtime (e.g., from constructor parameters or DateTime.Now).
 */
public class Configuration
{
    public readonly string DatabaseConnection; // Can only be set here or in constructor
    public readonly int MaxRetries;
    public readonly Guid InstanceId = Guid.NewGuid(); // Assigned at declaration (runtime)

    // Constructor where readonly fields can be assigned
    public Configuration(string connectionString, int retries)
    {
        DatabaseConnection = connectionString; // Assigning readonly field
        MaxRetries = retries; // Assigning readonly field
        // InstanceId = Guid.NewGuid(); // Can also be assigned here if not assigned above
        Console.WriteLine("Configuration object created.");
    }

    // public void UpdateConfig(int newRetries)
    // {
    //     MaxRetries = newRetries; // ERROR: Cannot assign to readonly field
    // }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- OOP - Constructor Concepts ---");

        #region Implicit Constructor Example
        /*
         * IMPLICIT CONSTRUCTOR Example
         * Shows how fields get default values if no constructor is defined.
         */
        Console.WriteLine("\n#region Implicit Constructor Example");
        SimpleClass simple = new SimpleClass();
        Console.WriteLine($"SimpleClass defaults: Value = {simple.Value}, Name = {simple.Name ?? "null"}"); // Use ?? to handle null
        Console.WriteLine("#endregion\n");
        #endregion

        #region Overloaded Constructor Example
        /*
         * OVERLOADED CONSTRUCTOR Example
         * Shows creating objects using different constructors.
         */
        Console.WriteLine("\n#region Overloaded Constructor Example");
        Product p1 = new Product(); // Uses parameterless constructor
        Product p2 = new Product(101, "Laptop"); // Uses constructor with id and name
        Product p3 = new Product(102, "Keyboard", 75.50m); // Uses constructor with all parameters
        Console.WriteLine("#endregion\n");
        #endregion

        #region This Keyword Example
        /*
         * THIS KEYWORD Example
         * Shows using 'this' for constructor chaining and parameter differentiation.
         */
        Console.WriteLine("\n#region This Keyword Example");
        Employee emp1 = new Employee(1, "Alice"); // Calls constructor(int, string)
        Console.WriteLine($"Emp1: Id={emp1.Id}, Name={emp1.Name}, Salary={emp1.Salary}");

        Employee emp2 = new Employee(2, "Bob", 60000); // Calls constructor(int, string, double) which chains to constructor(int, string)
        Console.WriteLine($"Emp2: Id={emp2.Id}, Name={emp2.Name}, Salary={emp2.Salary}");

        // Example using this. to differentiate parameter
        double annualSalary = 70000;
        emp1.SetSalary(annualSalary);
        Console.WriteLine($"Emp1 after SetSalary: Salary={emp1.Salary}"); // field was updated
        Console.WriteLine("#endregion\n");
        #endregion

        #region Non Public Constructor Example (Singleton)
        /*
         * NON PUBLIC CONSTRUCTOR Example (Singleton Pattern)
         * Shows a private constructor used to ensure only one instance is created via a static method.
         */
        Console.WriteLine("\n#region Non Public Constructor Example (Singleton)");
        // Singleton s1 = new Singleton(); // ERROR: Cannot access private constructor
        Singleton s1 = Singleton.GetInstance(); // Get the instance
        Singleton s2 = Singleton.GetInstance(); // Get the same instance

        Console.WriteLine($"Are s1 and s2 the same instance? {object.ReferenceEquals(s1, s2)}"); // Should be true
        s1.DoSomething();
        s2.DoSomething(); // Operating on the same single instance
        Console.WriteLine("#endregion\n");
        #endregion

        #region Object Initializer Example
        /*
         * OBJECT INITIALIZER Example
         * Shows convenient syntax to set properties after constructor call.
         */
        Console.WriteLine("\n#region Object Initializer Example");
        // Using parameterless constructor + object initializer
        Book book1 = new Book
        {
            Title = "The Hitchhiker's Guide to the Galaxy",
            Author = "Douglas Adams",
            Pages = 200,
            Price = 10.99m
        };
        book1.DisplayInfo();

        // Using a parameterized constructor + object initializer
        Book book2 = new Book("The Lord of the Rings", "J.R.R. Tolkien")
        {
            Pages = 1000,
            Price = 25.00m // Override or add properties not set by constructor
            // Author = "Tolkien, J.R.R." // You could even re-assign, though usually not intended
        };
        book2.DisplayInfo();
        Console.WriteLine("#endregion\n");
        #endregion

        #region Readonly Field Example
        /*
         * READONLY FIELD Example
         * Shows a field that can only be assigned in the constructor or at declaration.
         */
        Console.WriteLine("\n#region Readonly Field Example");
        Configuration config1 = new Configuration("Server=.;Database=TestDb;", 5);
        Console.WriteLine($"Config 1: Conn={config1.DatabaseConnection}, Retries={config1.MaxRetries}, Id={config1.InstanceId}");

        // Cannot change these after initialization:
        // config1.MaxRetries = 10; // ERROR

        Configuration config2 = new Configuration("Server=BackupDb;", 3);
        Console.WriteLine($"Config 2: Conn={config2.DatabaseConnection}, Retries={config2.MaxRetries}, Id={config2.InstanceId}");
        // Note: InstanceId is different for each object because it was assigned using Guid.NewGuid()
        // at the declaration point *for each new instance*.
        Console.WriteLine("#endregion\n");
        #endregion


        Console.ReadKey(); 
    }
}
