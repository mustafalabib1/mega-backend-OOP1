using System.Drawing;
using System.Reflection.Metadata;
using System;
using System.Net.NetworkInformation;
using static System.Collections.Specialized.BitVector32;
using System.Threading.Channels;

namespace Delegate
{
    #region Delegate
    //new Delegate (Class), the reference from this Delegate Can refer to a function or more than function [pointer to function]
    //these function may be class-member function [static] or object-member function [non-static]
    //but these function must have the same signature of delegate
    //regardless function access modifier regardless naming(function name , parameters)
    //ref from delegate have object of delegate that has target and this target has function 

    //step 0. Delegate Declare
    public delegate int StringFuncDelegate(string str);

    #endregion

    internal class Program
    {
        #region Method return Method
        public static Action DelgateDoAction()
        {
            //return delegate() { Console.WriteLine("Hello"); };
            return () => Console.WriteLine("Hello");
        }
        #endregion
        static void Main()
        {
            #region Delegate
            //Delegate Is a C# Language Feature
            //has 2 Usages:-
            // 1. functional programming [stratigay design pattern] => (pass function as parameter, return function, can save function into variable[address of function])
            // 2. Event-Driven Programming [observer design pattern]

            #region Example - 01
            //function take string and return count of uppercase charcters 

            //step 1. Declare ref From Delegate
            //StringFuncDelegate reference;

            //step 2. intial the delegate reference [pointer of function]
            //reference = new StringFuncDelegate(StringFunction.GetCountOfUpperCaseChars);
            //reference += StringFunction.GetCountOfUpperCaseChars; //syntex sugar
            //reference += StringFunction.GetCountOfLowerCaseChars; //syntex sugar
            //reference -= StringFunction.GetCountOfUpperCaseChars; //syntex sugar


            //step 3. use the Delegate Reference
            //int res = reference.Invoke("Ahmed Ali");
            //res = reference("Ahmed Ali"); //syntex sugar

            //Console.WriteLine(res);

            #endregion

            #region Example - 02
            //double[] numbers = { 8, 2, 7, 10, 7, 3, 5, 1, 4, 6 };

            #region without Delegates
            //SortingAlgorithms.BubbleSort(numbers);
            //SortingAlgorithms.BubbleDescSort(numbers); 
            #endregion


            #region with Delegates

            #region Non-Generics
            //SortingAlgorithms.BubbleSortOnType(numbers, SortingTypes.SortDesc);

            //SortingAlgorithms.BubbleSortOnType(numbers, SortingTypes.SortAsc);

            //SortingTypesFuncDelegates refer = SortingTypes.SortDesc;
            //refer += SortingTypes.SortAsc;
            //SortingAlgorithms.BubbleSortOnType(numbers, refer);  
            #endregion

            #region Generics
            //SortingAlgorithms.BubbleSortOnType<double>(numbers,(GenericSortingTypesFuncDelegates<double>)SortingTypes.SortDesc);

            //SortingAlgorithms.BubbleSortOnType<int>(numbers, SortingTypes.SortAsc);

            //SortingTypesFuncDelegates refer = SortingTypes.SortDesc;
            //refer += SortingTypes.SortAsc;
            //SortingAlgorithms.BubbleSortOnType(numbers, refer);  

            //string[] Names = { "Ali", "Mahmoud", "Omar", "Ahmed" };

            //SortingAlgorithms.BubbleSortOnType<string>(Names, (GenericSortingTypesFuncDelegates<string>)SortingTypes.SortDesc);

            //SortingAlgorithms.BubbleSortOnType<string>(Names, (EnhanceGenericSortingTypesFuncDelegates<string, string,bool>)SortingTypes.SortDesc);

            #endregion

            #endregion
            //foreach (var item in Names)
            //    Console.Write($"{item}\t");
            //Console.WriteLine();
            #endregion

            #region Example - 03
            //List<int> Numbers = Enumerable.Range(0, 100).ToList();

            #region Without Delegates
            //List<int> oddnumbers = NumberFinding.FindOddNumbers(Numbers);

            //List<int> Evennumbers = NumberFinding.FindEvenNumbers(Numbers);

            //List<int> BySevennumbers = NumberFinding.FindNumbersDivisibleBySeven(Numbers);
            #endregion

            #region With Delegates
            #region Non-Generic
            //List<int> DelegateOddNumbers = NumberFinding.FindNumbers(Numbers, (NumberFuncDelegate)NumberConditionFunctions.Odd);
            //List<int> DelegateEvenNumbers = NumberFinding.FindNumbers(Numbers, (NumberFuncDelegate)NumberConditionFunctions.Even);
            //List<int> DelegateBySevenNumbers = NumberFinding.FindNumbers(Numbers, (NumberFuncDelegate) NumberConditionFunctions.BySeven);
            #endregion

            #region Generic
            //List<int> DelegateOddNumbers = NumberFinding.FindNumbers(Numbers, (GenericNumberFuncDelegate<int>)NumberConditionFunctions.Odd);
            //List<int> DelegateEvenNumbers = NumberFinding.FindNumbers(Numbers, (GenericNumberFuncDelegate<int>)NumberConditionFunctions.Even);
            //List<int> DelegateBySevenNumbers = NumberFinding.FindNumbers(Numbers, (GenericNumberFuncDelegate<int>)NumberConditionFunctions.BySeven);

            //List<string> Names = new List<string>() { "Ali", "Mahmoud", "Omar", "Ahmed" };
            //List<string> DelegateOddNumbers = NumberFinding.FindNumbers(Names, (EnhanceGenericNumberFuncDelegate<string,bool>)NumberConditionFunctions.CheckLength);
            #endregion

            #endregion

            #endregion

            #region BuiltIn Delegate [Predicate, Func, Action]
            //delegete is c# language feature use to apply features in functional programing
            //there are 3 built in delegete :

            //1.Predicate  => takes one input parameter and returns a boolean - true or false.is a special kind of Func. It represents a method that contains a set of criteria mostly defined inside an if condition and checks whether the passed parameter meets those criteria or not.

            //2.Action     => takes zero, one or more input parameters, but does not return anything.

            //3.Func       => takes zero, one or more input parameters, and returns a value (with its out parameter)

            #region Example - 01 => BuiltIn Predicate in NumberFinding Class 
            #region Odd
            //List<int> Numbers = Enumerable.Range(0, 100).ToList(); // 0..99
            //Predicate<int> numberFuncDelegateOdd = NumberConditionFunctions.Odd;
            //List<int> DelegateOddNumbers = NumberFinding.FindNumbers(Numbers, numberFuncDelegateOdd);

            //foreach (var odd in DelegateOddNumbers)
            //    Console.WriteLine($"{odd} \t");
            //Console.WriteLine();
            #endregion
            #region Check Length
            //List<string> Names = new List<string>() { "Ali", "Mahmoud", "Omar", "Ahmed", "Khaled" };

            //Predicate<string>
            ///*GenericsNumberFuncDelegate<string, bool>*/
            //GenericsRefernce = NumberConditionFunctions.CheckLength;
            //List<string> NamesHavingLengthMoreThanFour = NumberFinding.FindNumbers(Names, GenericsRefernce);

            //foreach (var item in NamesHavingLengthMoreThanFour)
            //    Console.WriteLine($"{item}");
            //Console.WriteLine();
            #endregion
            #endregion

            #region Example - 02 => BuiltIn Func in SortingAlgorithm Class
            //Func => ref must point on method take from zero to 16 parameter and must be return [Deleget Overloading]

            //string[] Names = { "Ali", "Mahmoud", "Omar", "Ahmed" };
            //Func<string, string, bool> EnhanceGenericsReference = SortingTypes.SortAsc;
            //SortingAlgorithms.BubbleSortOnType<string>(Names, EnhanceGenericsReference);

            //foreach (var item in Names)
            //    Console.WriteLine(item);
            //#endregion

            #endregion

            #region Example - 03 => BuiltIn Action
            //Action => has two vergion 1.[none - generic] 2.[generic]
            //ref must point on method take from zero to 16 parameter and dosen't return [Deleget Overloading] 
            #endregion

            #region Example - 04
            //Predicate<string> predicate;
            //predicate = SomeFunction.Test;
            //predicate("Ahmed"); //predicate.Invoke("Ahmed");

            //Func<int, string> func;
            //func = SomeFunction.Cast;
            //func(5);

            //Action action;
            //action = SomeFunction.Print;
            //action();

            //Action<string> action1;
            //action1 = SomeFunction.Print;
            //action1("Ahmed");
            #endregion

            #endregion

            #region Anonymous Function Vs Lambda Expression

            #region Anonymous Function
            // Anonymous Function : c# 2.0 Feature (.net framework 2.0 [2005])

            //Predicate<string> predicate;
            //predicate = /*public*/ /*static*/ /*bool*/ /*Test*/ delegate (string str) { return str.Length > 1; };
            //Console.WriteLine(predicate("Ahmed")); //predicate.Invoke("Ahmed");

            //Func<int, string> func;
            //func =  /*public static string Cast*/ delegate(int str) { return str.ToString(); };
            //func(5);

            //Action action;
            //action = /*public static void Print*/ delegate() { Console.WriteLine("Hello"); };
            //action();

            //Action<string> action1;
            //action1 = /*public static void Print*/ delegate(string str) { Console.WriteLine($"Hello {str}"); };
            //action1("Ahmed");
            #endregion

            #region Lambda Expression
            // Lambda Expression : c# 3.0 Feature (.net framework 3.5 [2007])
            // => : this called as 'FatArrow' and Read as 'GoesTo'

            //Predicate<string> predicate;
            ////predicate = /*delegate (string*/ str /*)*/ => /*{ return*/ str.Length > 1; /*};*/
            //predicate = str => str.Length > 1;
            //Console.WriteLine(predicate("Ahmed")); //predicate.Invoke("Ahmed");

            //Func<int, string> func;
            ////func = /*delegate (int */str =>/*) { return */str.ToString();/* };*/
            //func = str => str.ToString();
            //func(5);

            //Action action;
            ////action = delegate () { Console.WriteLine("Hello"); };
            //action = () => Console.WriteLine("Hello");
            //action();

            //Action<string> action1;
            ////action1 = delegate (string str) { Console.WriteLine($"Hello {str}"); };
            //action1 = str => Console.WriteLine($"Hello {str}");
            //action1("Ahmed");
            #endregion

            #endregion

            #region New Feature in Delegate using "var" keyWord in c# 10.0 (.net 6)
            //var keyword => implicitly typed local variable [C# 2.0] => know it's type form it's intial value
            //var x = "ahmed";
            //var n; //invalid
            //x = 23; //invalid

            // in c# 10.0 Feature (.net 6) we can use [var] as delegate but [var] is from c# 2.0
            //var predicate = (string str) => str.Length > 1;
            //Console.WriteLine(predicate("Ahmed"));

            //var func = (int str) => str.ToString();
            //func(5);

            //var action = () => Console.WriteLine("Hello");
            //action();

            //var action1 = (string str) => Console.WriteLine($"Hello {str}");
            //action1("Ahmed");
            #endregion

            #region List Methods That use Delgates
            List<int> Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //List<int> oddnumbers = NumberFinding.FindNumbers(Numbers, (NumberFuncDelegate)NumberConditionFunctions.Odd);

            //List<int> oddnumbers = NumberFinding.FindNumbers(Numbers,(Predicate<int>) delegate (int n) { return n % 2 == 1; });

            //List<int> oddnumbers = NumberFinding.FindNumbers(Numbers, (Predicate<int>)(n => n % 2 == 1));

            //Numbers.FindAll(n => n % 2 == 1);
            //Numbers.Find(n => n % 2 == 1);
            //Numbers.FindLast(n => n % 2 == 1);
            //Numbers.Exists(n => n % 2 == 1);
            //Numbers.TrueForAll(n => n % 2 == 1);
            //Numbers.ForEach(n =>
            //{
            //    n += 10;
            //    n += 10;
            //    n -= 5;
            //});
            //Numbers.RemoveAll(n => n % 2 == 1);

            //foreach (var item in Numbers)
            //    Console.WriteLine(item);
            #endregion

            #region Method return Method
            //DelgateDoAction()(); //fire invoke
            //var x = DelgateDoAction();
            //x();
            #endregion

            #endregion
        }
    }
}