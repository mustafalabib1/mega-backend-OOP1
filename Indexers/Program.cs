using System;
using System.Collections.Generic; // Needed for Dictionary in the Single Dimensional example

/*
 * OOP - INDEXERS
 * Indexers are a C# feature that allows instances of a class or struct to be indexed
 * like an array. They enable you to access elements within an object using the
 * square bracket syntax, like `myObject[index]` or `myObject[key]`.
 */


/*
 * WHAT IS INDEXERS
 * Indexers are essentially properties that are accessed using one or more arguments
 * (indices or keys) within square brackets `[]` rather than a property name.
 * They allow an object to behave like a collection or an array from the outside.
 */


/*
 * SCENARIOS WHEN TO USE
 * Indexers are ideal for classes that represent a list, collection, dictionary, sequence,
 * or matrix of data. Use them when:
 * - Your class is a container for other objects.
 * - You want to provide an intuitive way to access elements by index or key.
 * - You need to control how elements are accessed or modified (validation, logging, etc.)
 * similar to how properties control field access.
 * - Examples: Custom list classes, dictionary wrappers, matrix or grid representations,
 * classes modeling sets of data points accessible by a descriptor.
 */


/*
 * INDEXER SYNTAX
 * Indexers are declared using the `this` keyword followed by square brackets `[]`
 * containing the parameters (index or key). They include `get` and `set` accessors,
 * similar to properties.
 * Syntax:
 * public Type this[ParameterType parameterName, ...]
 * {
 * get { // Code to return the element based on parameter(s) }
 * set { // Code to set the element based on parameter(s) and the 'value' keyword }
 * }
 * Indexers must have at least one parameter.
 */


/*
 * SINGLE DIMENSIONAL MAP
 * An indexer that uses a single parameter, mimicking a one-dimensional array or dictionary.
 * The parameter can be an integer (for index-based access) or any other type like a string (for key-based access).
 */
public class StringContainer
{
    private string[] data;

    public StringContainer(int size)
    {
        data = new string[size];
    }

    // Single-dimensional indexer (integer index)
    public string this[int index]
    {
        get
        {
            if (index >= 0 && index < data.Length)
            {
                return data[index];
            }
            // In a real app, handle index out of range (throw exception, return default, etc.)
            Console.WriteLine($"Warning: Index {index} is out of bounds.");
            return null;
        }
        set
        {
            if (index >= 0 && index < data.Length)
            {
                data[index] = value; // 'value' is the string being assigned
            }
            else
            {
                Console.WriteLine($"Warning: Index {index} is out of bounds. Cannot set value.");
            }
        }
    }
}

public class DictionaryWrapper
{
    private Dictionary<string, string> dictionary = new Dictionary<string, string>();

    // Single-dimensional indexer (string key)
    public string this[string key]
    {
        get
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            Console.WriteLine($"Warning: Key '{key}' not found.");
            return null; // Or throw new KeyNotFoundException();
        }
        set
        {
            // Add or update the dictionary entry
            dictionary[key] = value; // 'value' is the string being assigned
            Console.WriteLine($"Key '{key}' set/updated.");
        }
    }
    // Optional: Add a way to check if key exists
    public bool ContainsKey(string key)
    {
        return dictionary.ContainsKey(key);
    }
}


/*
 * MULTI-DIMENSIONAL MAPS
 * Indexers can take multiple parameters, allowing you to mimic multi-dimensional array
 * or matrix access using syntax like `myObject[row, col]`.
 */
public class Matrix
{
    private int[,] data;
    private int rows;
    private int cols;

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        data = new int[rows, cols];
    }

    // Multi-dimensional indexer (int row, int col)
    public int this[int row, int col]
    {
        get
        {
            if (row >= 0 && row < rows && col >= 0 && col < cols)
            {
                return data[row, col];
            }
            Console.WriteLine($"Warning: Index [{row},{col}] is out of bounds.");
            throw new IndexOutOfRangeException($"Index [{row},{col}] is out of bounds.");
        }
        set
        {
            if (row >= 0 && row < rows && col >= 0 && col < cols)
            {
                data[row, col] = value; // 'value' is the int being assigned
            }
            else
            {
                Console.WriteLine($"Warning: Index [{row},{col}] is out of bounds. Cannot set value.");
            }
        }
    }
}


/*
 * SUDOKO EXAMPLE
 * A practical example of using a multi-dimensional indexer to represent a Sudoku board.
 * The indexer provides a convenient way to access and potentially validate cell values.
 * The validation logic here is simple (checking range 1-9 or 0 for empty).
 * A real Sudoku game would need much more complex validation (row, column, 3x3 square rules).
 */
public class SudokuBoard
{
    private int[,] board = new int[9, 9];

    // Multi-dimensional Indexer for the Sudoku board
    public int this[int row, int col]
    {
        get
        {
            if (IsValidCoordinate(row, col))
            {
                return board[row, col];
            }
            throw new IndexOutOfRangeException($"Sudoku coordinate [{row},{col}] is out of bounds. Must be between [0-8, 0-8].");
        }
        set
        {
            if (!IsValidCoordinate(row, col))
            {
                throw new IndexOutOfRangeException($"Sudoku coordinate [{row},{col}] is out of bounds. Must be between [0-8, 0-8].");
            }

            // Basic validation: check if the value is 0 (empty) or between 1 and 9
            if (value >= 0 && value <= 9)
            {
                board[row, col] = value;
            }
            else
            {
                Console.WriteLine($"Warning: Invalid Sudoku value {value} at [{row},{col}]. Value must be between 0 and 9.");
                // Optionally, throw an exception instead
                // throw new ArgumentOutOfRangeException(nameof(value), "Sudoku cell value must be between 0 and 9.");
            }
        }
    }

    private bool IsValidCoordinate(int row, int col)
    {
        return row >= 0 && row < 9 && col >= 0 && col < 9;
    }

    // Method to display the board (basic)
    public void Display()
    {
        Console.WriteLine("-------------------------");
        for (int r = 0; r < 9; r++)
        {
            Console.Write("|");
            for (int c = 0; c < 9; c++)
            {
                Console.Write($" {board[r, c]}"); // Accessing via the indexer's get
                if ((c + 1) % 3 == 0) Console.Write(" |");
            }
            Console.WriteLine();
            if ((r + 1) % 3 == 0) Console.WriteLine("-------------------------");
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {

        #region Single Dimensional Map Example (Array-like)
        /*
         * SINGLE DIMENSIONAL MAP Example (Array-like)
         * Demonstrates a class accessed using a single integer index via an indexer.
         */
        Console.WriteLine("\n#region Single Dimensional Map Example (Array-like)");
        StringContainer container = new StringContainer(3);

        // Using the indexer's set accessor
        container[0] = "First";
        container[1] = "Second";
        container[2] = "Third";
        container[3] = "Out of bounds"; // Calls set, shows warning

        // Using the indexer's get accessor
        Console.WriteLine($"Element at index 0: {container[0]}"); // Output: First
        Console.WriteLine($"Element at index 2: {container[2]}"); // Output: Third
        Console.WriteLine($"Element at index 3: {container[3]}"); // Calls get, shows warning, outputs null
        Console.WriteLine("#endregion\n");
        #endregion

        #region Single Dimensional Map Example (Dictionary-like)
        /*
         * SINGLE DIMENSIONAL MAP Example (Dictionary-like)
         * Demonstrates a class accessed using a string key via an indexer.
         */
        Console.WriteLine("\n#region Single Dimensional Map Example (Dictionary-like)");
        DictionaryWrapper config = new DictionaryWrapper();

        // Using the indexer's set accessor
        config["Database"] = "SQLServer";
        config["Timeout"] = "30";

        // Using the indexer's get accessor
        Console.WriteLine($"Database: {config["Database"]}"); // Output: SQLServer
        Console.WriteLine($"Timeout: {config["Timeout"]}");   // Output: 30
        Console.WriteLine($"User: {config["User"]}");         // Calls get, shows warning, outputs null

        if (config.ContainsKey("Timeout"))
        {
            Console.WriteLine("Timeout key exists.");
        }
        Console.WriteLine("#endregion\n");
        #endregion

        #region Multi-Dimensional Map Example
        /*
         * MULTI-DIMENSIONAL MAP Example
         * Demonstrates a class accessed using two integer indices via an indexer.
         */
        Console.WriteLine("\n#region Multi-Dimensional Map Example");
        Matrix matrix = new Matrix(2, 3);

        // Using the indexer's set accessor
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[1, 0] = 4;
        matrix[1, 1] = 5;
        matrix[1, 2] = 6;
        matrix[2, 0] = 99; // Calls set, shows warning

        // Using the indexer's get accessor
        Console.WriteLine($"Matrix[0,0]: {matrix[0, 0]}"); // Output: 1
        Console.WriteLine($"Matrix[1,2]: {matrix[1, 2]}"); // Output: 6
        Console.WriteLine($"Matrix[2,0]: {matrix[2, 0]}"); // Calls get, shows warning, outputs 0
        Console.WriteLine("#endregion\n");
        #endregion

        #region Sudoku Example
        /*
         * SUDOKU EXAMPLE
         * Demonstrates a multi-dimensional indexer for a 9x9 grid with basic validation.
         */
        Console.WriteLine("\n#region Sudoku Example");
        SudokuBoard sudoku = new SudokuBoard();

        // Set some values using the indexer's set
        sudoku[0, 0] = 5;
        sudoku[0, 1] = 3;
        sudoku[1, 0] = 6;
        sudoku[8, 8] = 9;
        sudoku[0, 2] = 0; // Set to 0 for empty cell

        // Try setting an invalid value
        sudoku[0, 3] = 10; // Calls set, shows warning

        // Try accessing/setting out of bounds (will throw exception if not caught)
        // try
        // {
        //     sudoku[9, 0] = 1;
        // }
        // catch (IndexOutOfRangeException ex)
        // {
        //     Console.WriteLine($"Caught expected error: {ex.Message}");
        // }

        // Display the board using the indexer's get
        Console.WriteLine("Sudoku Board:");
        sudoku.Display();

        Console.WriteLine($"Value at [0,0]: {sudoku[0, 0]}"); // Accessing via get
        Console.WriteLine($"Value at [0,2]: {sudoku[0, 2]}"); // Accessing via get (should be 0)

        Console.WriteLine("#endregion\n");
        #endregion

        Console.ReadKey(); 
    }
}