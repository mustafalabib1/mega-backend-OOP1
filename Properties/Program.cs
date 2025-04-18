
/*
 * OOP - PROPERTIES
 * Properties are members of a class that provide a flexible, convenient, and safe way to
 * access fields (data). They are often described as 'smart fields' because they can include
 * logic when data is read or written, unlike simple public fields.
 */


/*
 * PROPERTY AND ENCAPSULATION
 * Properties are a cornerstone of encapsulation in C#. Instead of making fields public,
 * which allows direct and uncontrolled access, you make fields private (the 'backing field')
 * and expose them through public (or other accessibility) properties. This allows you to:
 * - Control how data is set (e.g., validation).
 * - Control how data is retrieved (e.g., formatting, calculating a value).
 * - Change the internal implementation (e.g., change the backing field type or make it
 * a calculated value) without affecting the code that uses the property.
 */


/*
 * WHY PROPERTY?
 * - Encapsulation: Hide internal data and control access.
 * - Validation: Implement logic in the 'set' accessor to ensure data is valid.
 * - Calculated Values: The 'get' accessor can compute and return a value based on other data,
 * instead of just returning a stored field.
 * - Abstraction: Provide a consistent interface (like a field) while allowing complex logic behind it.
 * - Flexibility: Easier to refactor or add logic later compared to starting with public fields.
 * - Data Binding: Many UI frameworks and libraries work best with properties.
 * - Events/Logging: Log or trigger events when a property's value changes.
 */


/*
 * GET AND SET ACCESSOR
 * A property is defined using 'get' and 'set' blocks (accessors):
 * - get: Contains the code executed when the property's value is read. It MUST return a value
 * of the property's type.
 * - set: Contains the code executed when the property's value is assigned. It implicitly
 * receives the new value in a keyword called 'value'.
 * A property can have only a 'get' (read-only property) or only a 'set' (write-only - rare),
 * or both.
 */


/*
 * PROPERTY/BACKING FIELD
 * The most common way to implement a property is to use a private field to store the actual data.
 * This private field is called the 'backing field'. The 'get' and 'set' accessors then
 * read from or write to this backing field, potentially with added logic.
 */
public class PersonWithProperty
{
    // Private backing field
    private int age;

    // Public property with get and set accessors and backing field
    public int Age
    {
        // Get accessor: executed when you read PersonWithProperty.Age
        get
        {
            // Optional: add logic here, e.g., logging or formatting
            Console.WriteLine("Age accessed.");
            return age; // Return the value from the backing field
        }
        // Set accessor: executed when you write to PersonWithProperty.Age (e.g., PersonWithProperty.Age = 30;)
        set
        {
            // 'value' keyword holds the value being assigned (e.g., 30)
            // Optional: add validation or other logic here
            if (value >= 0)
            {
                age = value; // Assign the 'value' to the backing field
                Console.WriteLine($"Age set to {age}.");
            }
            else
            {
                Console.WriteLine($"Invalid age value: {value}. Age not changed.");
            }
        }
    }

    // Example of a read-only property (only has a get accessor)
    public string Status
    {
        get
        {
            if (age < 18) return "Minor";
            else if (age < 65) return "Adult";
            else return "Senior";
        }
        // No set accessor means you cannot write to Status
    }

    // Example of a calculated property (no backing field needed)
    public int AgeInMonths
    {
        get
        {
            return age * 12; // Calculated directly
        }
    }
}


/*
 * PROPERTY AND ACCESSIBILITY
 * Properties themselves can have access modifiers (public, private, protected, internal).
 * Additionally, one of the accessors ('get' or 'set') can have a *more restrictive*
 * access modifier than the property itself. This is common for properties that are
 * public for reading but only privately settable (public get, private set).
 */
public class Account
{
    // Private backing field for Balance
    private decimal balance;

    // Public property, but only allow setting from inside the class
    public decimal Balance
    {
        get { return balance; }
        private set // Set is private
        {
            if (value >= 0)
            {
                balance = value;
            }
            else
            {
                Console.WriteLine("Balance cannot be negative.");
            }
        }
    }

    // Public method to modify balance, which uses the private set
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount; // This calls the 'set' accessor
            Console.WriteLine($"Deposited {amount}.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount; // This calls the 'set' accessor
            Console.WriteLine($"Withdrew {amount}.");
        }
        else
        {
            Console.WriteLine("Insufficient funds or invalid amount.");
        }
    }

    // Property with different accessibility for get/set (e.g., public get, internal set)
    public int TransactionCount { get; internal set; } // Settable only within the same assembly
}


/*
 * AUTOMATIC PROPERTY
 * For simple properties where the 'get' and 'set' accessors just read from and write to
 * a backing field without any extra logic, C# provides a shorthand syntax called
 * automatic properties (or auto-implemented properties).
 * When you use '{ get; set; }', the compiler automatically creates a private,
 * anonymous backing field for you.
 */
public class SimplifiedPerson
{
    // Automatic Property: Compiler creates a private backing field automatically
    public string Name { get; set; }

    // Automatic Property with private set: Readable from anywhere, settable only inside the class
    public int Age { get; private set; }

    // Automatic Property with initializer (C# 6 and later)
    public string City { get; set; } = "Unknown";

    // Constructor setting auto-properties
    public SimplifiedPerson(string name, int age)
    {
        Name = name; // Assigns via the implicit set accessor
        Age = age;   // Assigns via the implicit private set accessor
    }
}


/*
 * PROPERTY INTERNALLY
 * While properties look like fields externally, the C# compiler typically translates
 * them into methods in the compiled code (IL - Intermediate Language).
 * A property named 'MyProperty' with both get and set accessors usually compiles into:
 * - A method like 'get_MyProperty()' that returns the property's type.
 * - A method like 'set_MyProperty(value)' that takes a parameter of the property's type.
 * This behind-the-scenes implementation is why you can add logic inside the accessors –
 * you're essentially adding logic to these generated methods.
 */

public class Program
{
    public static void Main(string[] args)
    {
        #region Property and Backing Field Example
        /*
         * PROPERTY / BACKING FIELD Example
         * Demonstrates a property with explicit get/set and a private backing field.
         */
        Console.WriteLine("\n#region Property and Backing Field Example");
        PersonWithProperty person = new PersonWithProperty();

        // Using the set accessor (calls the code inside set{})
        person.Age = 30;
        person.Age = -5; // Invalid age, set accessor prevents change

        // Using the get accessor (calls the code inside get{})
        int currentAge = person.Age;
        Console.WriteLine($"Current Age: {currentAge}");

        // Read-only property (no set accessor)
        Console.WriteLine($"Status: {person.Status}");
        // person.Status = "Elder"; // ERROR: Property or indexer 'PersonWithProperty.Status' cannot be assigned to -- it is read only

        // Calculated property
        Console.WriteLine($"Age in Months: {person.AgeInMonths}");
        Console.WriteLine("#endregion\n");
        #endregion

        #region Property and Accessibility Example
        /*
         * PROPERTY AND ACCESSIBILITY Example
         * Shows public get, private set and methods interacting with it.
         */
        Console.WriteLine("\n#region Property and Accessibility Example");
        Account acc = new Account();
        // acc.Balance = 100; // ERROR: The property or indexer 'Account.Balance' cannot be used in this context because the set accessor is private

        acc.Deposit(500); // This method calls the private set
        Console.WriteLine($"Account Balance after Deposit: {acc.Balance}"); // Accessing via public get

        acc.Withdraw(100); // This method calls the private set
        Console.WriteLine($"Account Balance after Withdrawal: {acc.Balance}");

        acc.Withdraw(1000); // Should show insufficient funds
        Console.WriteLine($"Account Balance after failed Withdrawal: {acc.Balance}");

        // acc.TransactionCount = 5; // Accessible only if in the same assembly (depends on project setup)
        // Inside this Program class (likely in the same assembly), this IS allowed:
        acc.TransactionCount = 1;
        Console.WriteLine($"Transaction Count: {acc.TransactionCount}");

        Console.WriteLine("#endregion\n");
        #endregion

        #region Automatic Property Example
        /*
         * AUTOMATIC PROPERTY Example
         * Shows the concise syntax for simple properties where the compiler generates the backing field.
         */
        Console.WriteLine("\n#region Automatic Property Example");
        SimplifiedPerson sp = new SimplifiedPerson("Alice", 30);
        Console.WriteLine($"Name: {sp.Name}, Age: {sp.Age}, City: {sp.City}");

        sp.Name = "Alicia"; // Assigns via the implicit set accessor
        // sp.Age = 31; // ERROR: The property or indexer 'SimplifiedPerson.Age.set' is inaccessible due to its protection level

        Console.WriteLine($"Updated Name: {sp.Name}");

        sp.City = "London"; // Can set City as it has public set
        Console.WriteLine($"Updated City: {sp.City}");
        Console.WriteLine("#endregion\n");
        #endregion


        Console.ReadKey();
    }
}