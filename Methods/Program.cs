
/*
 * INTRODUCTION: Programming Paradigms
 * Programming paradigms are styles of organizing code. Beyond the simple sequence of instructions (Procedural),
 * you have paradigms like Functional (focus on functions and immutability) and Object-Oriented (OOP).
 *
 * OOP became dominant for large-scale software due to its effectiveness in managing complexity,
 * promoting code reuse, and modeling real-world scenarios through structured units.
 *
 * OOP DEFINITION
 * Object-Oriented Programming (OOP) is a paradigm centered around objects, which bundle data (fields/attributes)
 * and behavior (methods/functions) into single units, created from blueprints called classes.
 */

/*
 * CLASS VS. OBJECT
 * Class: A blueprint or template defining the structure (fields) and behavior (methods) that objects of that class will have. It's a definition, not a concrete instance.
 * Object: An instance of a class. A concrete entity created from the blueprint, holding specific data values for its fields.
 */
public class Dog // Use public for visibility
{
    public string Name; // Field (data) - C# convention often uses Properties, but Field works for example

    // Method (behavior)
    public void Bark()
    {
        Console.WriteLine($"{Name} says Woof!"); // C# string interpolation
    }
}


/*
 * CLASS MODIFIERS
 * These keywords affect the class itself, controlling its behavior or accessibility (varies by language like Java/C#).
 * - public: Accessible from anywhere.
 * - abstract: Cannot be instantiated; designed for inheritance.
 * - sealed: Cannot be subclassed.
 * - static (for nested classes): Belongs to the outer class, not its instances.
 */
// A sealed class cannot be inherited from
public sealed class ImmutablePoint
{
    public int X { get; } // C# Property syntax
    public int Y { get; }

    public ImmutablePoint(int x, int y)
    {
        X = x; // Initialize in constructor
        Y = y;
    }
    // Methods etc.
}

// An abstract class must be inherited from
public abstract class Shape
{
    // An abstract method - must be implemented by subclasses
    public abstract double CalculateArea(); // C# naming convention

    // A regular method
    public void Display()
    {
        Console.WriteLine("This is a shape.");
    }
}

// A concrete class inheriting from Shape (using colon for inheritance)
public class Circle : Shape
{
    private double radius; // Private field

    // Constructor
    public Circle(double r)
    {
        radius = r;
    }

    // Implementing the abstract method using 'override'
    public override double CalculateArea()
    {
        return Math.PI * radius * radius; // Math.PI is C#'s PI constant
    }
}


/*
 * ACCESS MODIFIERS
 * These control the visibility of class members (fields, methods, properties) and enforce encapsulation.
 * - public: Accessible from anywhere.
 * - protected: Accessible within the class and by derived classes.
 * - internal: Accessible only within the same assembly (the default for types if not specified).
 * - private: Accessible only within the class itself (the default for members if not specified).
 */
public class BankAccount
{
    private double balance; // private: Only accessible inside this class (default for fields)
    public string AccountNumber { get; } // public: Accessible from anywhere (C# Property)

    // Constructor (can access private members)
    public BankAccount(string accNum, double initialBalance)
    {
        AccountNumber = accNum; // Allowed
        if (initialBalance >= 0)
        {
            balance = initialBalance; // Allowed
        }
        else
        {
            balance = 0;
        }
    }

    // Public method to access balance (controlled access)
    public double GetBalance() // C# naming convention
    {
        return balance; // Allowed
    }

    // Public method to modify balance (controlled modification)
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount; // Allowed
            Console.WriteLine($"Deposited {amount}. New balance: {balance}");
        }
    }

    // This method could be internal if only needed within the same project/assembly
    internal void LogTransaction(string details)
    {
        Console.WriteLine($"Transaction Log: {details} for account {AccountNumber}");
    }
}


/*
 * FIELD / CONSTANT
 * Field: A variable declared inside a class but outside methods. Represents an object's state (instance fields) or data shared by all objects of the class (static fields).
 * Constant: A field whose value is fixed and cannot be changed after initialization. Used for values that should remain constant.
 * - 'const': For compile-time constants (primitives, strings). Implicitly static.
 * - 'static readonly': For runtime constants or complex types. Can be set in constructor.
 */
public class CircleWithConstants
{
    // Instance field: Each Circle object has its own radius
    private double radius;

    // Class Constant: Shared by ALL Circle objects, value never changes.
    public const double PI = 3.14159; // const is for compile-time constants

    // static readonly example (value set at runtime)
    public static readonly DateTime CreatedDate = DateTime.Now;


    // Constructor
    public CircleWithConstants(double r)
    {
        radius = r;
        // PI = 3.0; // ERROR: Cannot change value of a const variable
        // CreatedDate = DateTime.Now; // Can only be set here or at declaration for readonly
    }

    public double CalculateArea()
    {
        return PI * radius * radius; // Using the constant
    }

    // Example of a public property for controlled access to the private field
    public double Radius
    {
        get { return radius; }
        set { if (value >= 0) radius = value; } // Add validation if needed
    }
}


/*
 * OPP - METHODS
 * In Object-Oriented Programming, methods define the behavior or actions that an object or a class can perform.
 * They encapsulate a block of code that executes when called.
 */


/*
 * INSTANCE VS. STATIC MEMBER
 * This distinction applies to both fields (data) and methods (behavior).
 * - Instance Members: Belong to a specific instance (object). Accessed via objectName.memberName. Operate on object data.
 * - Static Members: Belong to the class itself. Accessed via ClassName.memberName. Shared by all objects. Cannot access instance members directly.
 */
public class Counter
{
    // Instance field: Each Counter object has its own count
    private int _instanceCount;

    // Static field: Shared by all Counter objects
    private static int _totalCountersCreated = 0;

    // Constructor (instance method)
    public Counter()
    {
        _instanceCount = 0; // Initialize instance count for this object
        _totalCountersCreated++; // Increment static count for the class
    }

    // Instance method: Operates on the instance field
    public void Increment()
    {
        _instanceCount++;
        Console.WriteLine($"Instance count: {_instanceCount}");
    }

    // Static method: Operates on the static field or related to the class
    public static int GetTotalCounters()
    {
        // return _instanceCount; // ERROR: Cannot access instance field from static method
        return _totalCountersCreated; // Allowed
    }
}

/*
 * METHOD SIGNATURES
 * A method signature uniquely identifies a method within its class. It consists of:
 * 1. The method name.
 * 2. The number, type, and order of its parameters.
 * The return type and access modifiers are NOT part of the signature for overloading purposes.
 */
public class Calculator
{
    // Signature 1: Add(int, int)
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Signature 2: Add(double, double) - Same name, different parameter types
    public double Add(double a, double b)
    {
        return a + b;
    }

    // Signature 3: Add(int, int, int) - Same name, different number of parameters
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    // Signature 4: Add(double, int) - Same name, different order/types
    public double Add(double a, int b)
    {
        return a + b;
    }

    // public int Add(int x, int y) { ... } // ERROR: Duplicate signature Add(int, int)
    // public double Add(int a, int b) { ... } // ERROR: Cannot overload solely based on return type
}


/*
 * EXPRESSION BODIED METHOD
 * A concise syntax in C# (using =>) for methods whose body is a single expression.
 * Suitable for methods that return a single value or perform a single action.
 */
public class MathHelper
{
    // Standard method syntax
    public int Multiply(int a, int b)
    {
        return a * b;
    }

    // Expression-bodied method equivalent
    public int MultiplyExpressionBodied(int a, int b) => a * b;

    // Expression-bodied method for a single statement (like void methods)
    public void DisplayProduct(int a, int b) => Console.WriteLine($"Product: {a * b}");

    // Can also be used for properties
    public string Description => "Helper for math operations.";
}


/*
 * METHOD OVERLOAD
 * The ability to define multiple methods in the same class with the same name but different method signatures.
 * The compiler chooses the correct method based on the arguments provided.
 */
public class Printer
{
    // Overload 1: Print a string
    public void Print(string text)
    {
        Console.WriteLine($"Printing String: {text}");
    }

    // Overload 2: Print an integer (different parameter type)
    public void Print(int number)
    {
        Console.WriteLine($"Printing Integer: {number}");
    }

    // Overload 3: Print a string and a number (different number and types of parameters)
    public void Print(string text, int number)
    {
        Console.WriteLine($"Printing String '{text}' and Integer {number}");
    }

    // Overload 4: Print a number and a string (different order of parameters)
    public void Print(int number, string text)
    {
        Console.WriteLine($"Printing Integer {number} and String '{text}'");
    }
}


/*
 * PASS PARAMETER VALUE / REF.
 * How arguments are passed to method parameters, affecting whether changes inside the method impact the original variable outside.
 * - Pass by Value (Default): A copy of the argument's value is passed. Changes inside don't affect original for value types. For reference types, copy of reference is passed (content can be changed, but original variable won't point to a new object).
 * - Pass by Reference ('ref', 'out', 'in'): A reference to the original variable is passed. Changes inside DO affect the original variable.
 * - 'ref': Variable must be initialized before passing. Can read/write.
 * - 'out': Variable need NOT be initialized before passing. MUST be assigned inside the method. Used for multiple returns.
 * - 'in' (C# 7+): Variable must be initialized. Pass by reference but read-only inside the method.
 */
public class ParameterPassing
{
    // Pass by Value (int is a value type)
    public void PassByValue(int number)
    {
        number = 100; // Changes the COPY
        Console.WriteLine($"Inside PassByValue: {number}");
    }

    // Pass by Value (MyClass is a reference type - copy of reference)
    public void PassByValueRef(MyClass obj)
    {
        obj.Value = 200; // Changes the CONTENT of the original object
        Console.WriteLine($"Inside PassByValueRef (changed content): {obj.Value}");

        obj = new MyClass { Value = 300 }; // Changes the COPY of the reference
        Console.WriteLine($"Inside PassByValueRef (assigned new object): {obj.Value}");
    }

    // Pass by Reference (using ref)
    public void PassByRef(ref int number)
    {
        number = 500; // Changes the ORIGINAL number
        Console.WriteLine($"Inside PassByRef: {number}");
    }

    // Pass by Reference (using out)
    public void PassByOut(out int result)
    {
        // Console.WriteLine(result); // ERROR: Cannot use 'out' parameter before assigning
        result = 999; // MUST assign before returning
        Console.WriteLine($"Inside PassByOut: {result}");
    }

    // Pass by Reference (using in - read-only)
    public void PassByIn(in int number)
    {
        Console.WriteLine($"Inside PassByIn: {number}");
        // number = 123; // ERROR: Cannot assign to 'in' parameter
    }
}

public class MyClass // A simple reference type for parameter passing example
{
    public int Value { get; set; }
}


/*
 * LOCAL METHOD
 * A method declared inside the body of another method, constructor, accessor, etc.
 * Only visible and callable within the containing member. Can access local variables of the containing scope.
 */
public class DataProcessor
{
    public void ProcessData(int[] data)
    {
        if (data == null)
        {
            Console.WriteLine("Data is null.");
            return;
        }

        Console.WriteLine("\n--- Processing data with Local Method ---");

        // --- Start of Local Method Definition ---
        // This method is only visible and callable within ProcessData
        int SumArray(int[] arr) // Can access 'data' parameter and other local variables here
        {
            int sum = 0; // Local variable within SumArray
            foreach (int item in arr)
            {
                sum += item;
            }
            return sum;
        }
        // --- End of Local Method Definition ---


        // Call the local method
        int totalSum = SumArray(data);

        Console.WriteLine($"Total sum: {totalSum}");

        // int anotherSum = SumArray(new int[] {1, 2}); // Also works
        // SumArray is NOT accessible outside ProcessData method
    }

    // Console.WriteLine(SumArray(new int[] {1})); // ERROR: SumArray does not exist in this scope
}

public class Program
{
    public static void Main(string[] args)
    {
        #region Class vs. Object Example
        /*
         * CLASS VS. OBJECT Example
         */
        Console.WriteLine("\n#region Class vs. Object Example");
        // Object: Creating an instance of the Dog class
        Dog myDog = new Dog();

        // Setting the object's data (field value)
        myDog.Name = "Buddy";

        // Calling the object's behavior (method)
        myDog.Bark(); // Output: Buddy says Woof!

        // Another object of the same class
        Dog yourDog = new Dog();
        yourDog.Name = "Lucy";
        yourDog.Bark(); // Output: Lucy says Woof!
        Console.WriteLine("#endregion\n");
        #endregion

        #region Class Modifiers Example
        /*
         * CLASS MODIFIERS Example
         * Illustrates abstract and sealed classes.
         */
        Console.WriteLine("\n#region Class Modifiers Example");
        // ImmutablePoint ip = new ImmutablePoint(1, 2); // You can create instances
        // This would cause a compile-time error as ImmutablePoint is sealed:
        // public class ColoredPoint : ImmutablePoint { ... }

        // Cannot do this (Shape is abstract):
        // Shape s = new Shape(); // Error

        // Can create instance of concrete subclass Circle
        Circle c = new Circle(5); // OK
        Console.WriteLine($"Circle Area: {c.CalculateArea()}");
        c.Display();
        Console.WriteLine("#endregion\n");
        #endregion

        #region Access Modifiers Example
        /*
         * ACCESS MODIFIERS Example
         * Shows public, private members and accessing private via public methods.
         */
        Console.WriteLine("\n#region Access Modifiers Example");
        BankAccount account = new BankAccount("12345", 1000);

        Console.WriteLine("Account: " + account.AccountNumber); // Accessing public property

        // Console.WriteLine("Balance: " + account.balance); // ERROR: Cannot access private field directly

        Console.WriteLine("Balance: " + account.GetBalance()); // Accessing via public method

        account.Deposit(500); // Modifying via public method
        Console.WriteLine("New Balance: " + account.GetBalance());

        // account.LogTransaction("Test log"); // Accessible only if in the same assembly/project
        Console.WriteLine("#endregion\n");
        #endregion

        #region Field / Constant Example
        /*
         * FIELD / CONSTANT Example
         * Shows instance fields, const, and static readonly.
         */
        Console.WriteLine("\n#region Field / Constant Example");
        CircleWithConstants cwc1 = new CircleWithConstants(10);
        CircleWithConstants cwc2 = new CircleWithConstants(5);

        Console.WriteLine($"Area 1: {cwc1.CalculateArea()}");
        Console.WriteLine($"Area 2: {cwc2.CalculateArea()}");

        // Accessing the class constant directly using the class name
        Console.WriteLine($"Value of PI: {CircleWithConstants.PI}");
        Console.WriteLine($"Circle Class Created Date: {CircleWithConstants.CreatedDate}");

        // Accessing/modifying the instance field via the property
        Console.WriteLine($"C1 Radius: {cwc1.Radius}");
        cwc1.Radius = 12;
        Console.WriteLine($"C1 New Area: {cwc1.CalculateArea()}");
        Console.WriteLine("#endregion\n");
        #endregion

        #region Instance vs. Static Member Example
        /*
         * INSTANCE VS. STATIC MEMBER Example
         * Shows static vs. instance fields and methods.
         */
        Console.WriteLine("\n#region Instance vs. Static Member Example");
        // Accessing static member using class name
        Console.WriteLine($"Total counters created initially: {Counter.GetTotalCounters()}"); // Output: 0

        // Creating objects (instances)
        Counter count1 = new Counter();
        Counter count2 = new Counter();

        // Accessing instance method using object reference
        count1.Increment(); // Output: Instance count: 1
        count2.Increment(); // Output: Instance count: 1 (each has its own count)
        count1.Increment(); // Output: Instance count: 2

        // Accessing static member using class name again
        Console.WriteLine($"Total counters created now: {Counter.GetTotalCounters()}"); // Output: 2
        Console.WriteLine("#endregion\n");
        #endregion

        #region Method Signatures Example
        /*
         * METHOD SIGNATURES Example
         * Shows methods with the same name but different parameter lists.
         */
        Console.WriteLine("\n#region Method Signatures Example");
        Calculator calc = new Calculator();
        Console.WriteLine($"Add(int, int): {calc.Add(5, 10)}");
        Console.WriteLine($"Add(double, double): {calc.Add(5.5, 10.1)}");
        Console.WriteLine($"Add(int, int, int): {calc.Add(1, 2, 3)}");
        Console.WriteLine($"Add(double, int): {calc.Add(7.7, 8)}");
        Console.WriteLine("#endregion\n");
        #endregion

        #region Expression Bodied Method Example
        /*
         * EXPRESSION BODIED METHOD Example
         * Shows compact syntax for single-expression methods.
         */
        Console.WriteLine("\n#region Expression Bodied Method Example");
        MathHelper mh = new MathHelper();
        Console.WriteLine($"Multiply (standard): {mh.Multiply(4, 5)}");
        Console.WriteLine($"Multiply (expression-bodied): {mh.MultiplyExpressionBodied(4, 5)}");
        mh.DisplayProduct(6, 7);
        Console.WriteLine($"Description: {mh.Description}");
        Console.WriteLine("#endregion\n");
        #endregion

        #region Method Overload Example
        /*
         * METHOD OVERLOAD Example
         * Shows multiple methods with the same name, differentiated by signature.
         */
        Console.WriteLine("\n#region Method Overload Example");
        Printer p = new Printer();

        p.Print("Hello");         // Calls Overload 1: Print(string)
        p.Print(123);             // Calls Overload 2: Print(int)
        p.Print("Count", 10);     // Calls Overload 3: Print(string, int)
        p.Print(99, "Quantity");  // Calls Overload 4: Print(int, string)
        Console.WriteLine("#endregion\n");
        #endregion

        #region Pass Parameter Value / Ref Example
        /*
         * PASS PARAMETER VALUE / REF Example
         * Shows passing value types by value, reference types by value (copy of reference), and using 'ref', 'out', 'in'.
         */
        Console.WriteLine("\n#region Pass Parameter Value / Ref Example");
        ParameterPassing pp = new ParameterPassing();

        int val = 10;
        Console.WriteLine($"Original Value before PassByValue: {val}");
        pp.PassByValue(val);
        Console.WriteLine($"Outside after PassByValue: {val}"); // Output: 10 (original unchanged)

        Console.WriteLine("---");

        MyClass myObj = new MyClass { Value = 10 };
        Console.WriteLine($"Original Object Value before PassByValueRef: {myObj.Value}");
        pp.PassByValueRef(myObj);
        Console.WriteLine($"Outside after PassByValueRef: {myObj.Value}"); // Output: 200 (content changed, reference copy didn't affect original variable)

        Console.WriteLine("---");

        int refVal = 10;
        Console.WriteLine($"Original Ref Value before PassByRef: {refVal}");
        pp.PassByRef(ref refVal); // Must use 'ref' keyword here too
        Console.WriteLine($"Outside after PassByRef: {refVal}"); // Output: 500 (original changed)

        Console.WriteLine("---");

        int outVal; // No need to initialize
        // Console.WriteLine($"Original Out Value before PassByOut: {outVal}"); // ERROR: Use of unassigned local variable
        pp.PassByOut(out outVal); // Must use 'out' keyword here too
        Console.WriteLine($"Outside after PassByOut: {outVal}"); // Output: 999 (original assigned)

        Console.WriteLine("---");

        int inVal = 42;
        Console.WriteLine($"Original In Value before PassByIn: {inVal}");
        pp.PassByIn(in inVal); // Must use 'in' keyword here too
        Console.WriteLine($"Outside after PassByIn: {inVal}"); // Output: 42 (original unchanged, method couldn't modify)
        Console.WriteLine("#endregion\n");
        #endregion

        #region Local Method Example
        /*
         * LOCAL METHOD Example
         * Shows a method defined inside another method.
         */
        Console.WriteLine("\n#region Local Method Example");
        DataProcessor processor = new DataProcessor();
        int[] myNumbers = { 1, 2, 3, 4, 5 };
        processor.ProcessData(myNumbers);

        processor.ProcessData(null); // Handle null case
        Console.WriteLine("#endregion\n");
        #endregion

        Console.ReadKey(); 
    }
}
