/*
 * OOP - EVENTS
 * Events in C# are a fundamental part of the .NET framework's design pattern for implementing
 * the 'publish-subscribe' mechanism. They allow an object (the 'publisher') to notify
 * other objects (the 'subscribers') when something of interest happens, without the
 * publisher needing to know exactly which objects are subscribed.
 */


/*
 * WHAT IS EVENT
 * An event is a signal that notifies subscribers that something has happened. It's a
 * way for a class to provide notifications to clients of that class. Events are built
 * upon delegates.
 */


/*
 * EVENT AND DELEGATE
 * Events are intrinsically linked to delegates. A delegate defines the signature
 * (return type and parameters) of the methods (event handlers) that can respond to an event.
 * An event declaration is essentially a type-safe wrapper around a delegate, providing
 * methods for subscribers to attach (`+=`) and detach (`-=`) their handler methods
 * from a list of listeners. The event publisher then 'invokes' the delegate, which
 * in turn calls all the attached handler methods.
 */


/*
 * EVENT PUBLISHER
 * The class that contains the event is called the publisher. The publisher:
 * 1. Defines the delegate type (or uses a built-in one like EventHandler).
 * 2. Declares the event using the `event` keyword and the delegate type.
 * 3. Provides a way (usually a protected virtual method, often named OnEventName)
 * to raise or 'fire' the event.
 * 4. Contains the logic that determines when the event should be raised.
 */
// Example Publisher Class
public class Button
{
    /*
     * EVENT AND DELEGATE Example (within Publisher)
     * Define a delegate that event handlers must match.
     * The standard pattern is void return, object sender, EventArgs (or a derived class) args.
     * EventHandler is a built-in delegate for void(object, EventArgs).
     * EventHandler<TEventArgs> is for void(object, TEventArgs) where TEventArgs is a custom class.
     */
    public event EventHandler Click; // Declaring the event using the built-in EventHandler delegate

    // Method in the publisher that raises the event
    /*
     * EVENT PUBLISHER Example (Raising the Event)
     * Method to trigger the event. Often named OnEventName.
     * Check if there are any subscribers before raising (delegate is not null).
     */
    protected virtual void OnClick(EventArgs e)
    {
        // Check if there are any subscribers attached to the Click event
        EventHandler handler = Click; // Make a temporary copy for thread safety (good practice)
        if (handler != null)
        {
            // Raise the event by invoking the delegate.
            // 'this' is the sender (the button instance itself)
            handler(this, e);
        }
    }

    // A method that simulates the event occurring
    public void SimulateClick()
    {
        Console.WriteLine("Button is being clicked...");
        OnClick(EventArgs.Empty); // Raise the event, passing EventArgs.Empty as there's no extra data
    }
}

// Custom EventArgs if you need to pass specific data with the event
public class TemperatureEventArgs : EventArgs
{
    public double CurrentTemperature { get; }
    public TemperatureEventArgs(double temp)
    {
        CurrentTemperature = temp;
    }
}

public class Thermometer
{
    // Delegate for the event (if not using built-in EventHandler<T>)
    // public delegate void TemperatureChangeHandler(object sender, TemperatureEventArgs e);

    // Event using the generic EventHandler<T> built-in delegate
    public event EventHandler<TemperatureEventArgs> TemperatureChanged;

    private double currentTemp;

    public double CurrentTemp
    {
        get { return currentTemp; }
        set
        {
            if (currentTemp != value) // Only raise event if temperature actually changes
            {
                currentTemp = value;
                OnTemperatureChanged(new TemperatureEventArgs(currentTemp)); // Raise the event
            }
        }
    }

    protected virtual void OnTemperatureChanged(TemperatureEventArgs e)
    {
        EventHandler<TemperatureEventArgs> handler = TemperatureChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}


/*
 * SUBSCRIBE VS UNSUBSCRIBE
 * - Subscribe: Register a method (event handler) to be called when the event is raised.
 * Uses the `+=` operator.
 * - Unsubscribe: Remove a previously subscribed method from the event.
 * Uses the `-=` operator. This is crucial to prevent memory leaks, especially if
 * the subscriber's lifetime is shorter than the publisher's. The handler must
 * match the signature and the exact method reference used during subscription.
 */
public class Light
{
    // Event handler method - signature matches EventHandler (void, object, EventArgs)
    /*
     * EVENT HANDLER Example
     * A method in the subscriber class that will be called when the event fires.
     * Its signature MUST match the event's delegate.
     */
    public void SwitchOn(object sender, EventArgs e)
    {
        Console.WriteLine("Light: Turning ON.");
        // 'sender' refers to the object that raised the event (the Button instance in this case)
    }

    public void SwitchOff(object sender, EventArgs e)
    {
        Console.WriteLine("Light: Turning OFF.");
    }
}

public class Heater
{
    // Event handler for the TemperatureChanged event
    public void OnTemperatureLow(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"Heater: Temperature is {e.CurrentTemperature}°C. Turning on.");
        // 'sender' here would be the Thermometer instance
        // 'e' is the custom TemperatureEventArgs containing the temperature data
    }

    public void OnTemperatureHigh(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"Heater: Temperature is {e.CurrentTemperature}°C. Turning off.");
    }
}


/*
 * LAMBDA EXPRESSION HANDLER
 * For simple event handlers, you can use a lambda expression or an anonymous method
 * directly when subscribing with `+=`. This avoids the need to write a separate named method.
 * However, if you need to UNSUBSCRIBE later, you generally cannot do so easily with a
 * lambda expression subscribed directly, unless you store the lambda in a variable.
 */
public class Program
{
    public static void Main(string[] args)
    {
        #region Basic Event Example (Button Click)
        /*
         * Basic Event Example (Button Click)
         * Demonstrates publisher, subscriber, event, delegate, handler, subscribe, unsubscribe.
         */
        Console.WriteLine("\n#region Basic Event Example (Button Click)");

        Button myButton = new Button(); // The Publisher
        Light livingRoomLight = new Light(); // A Subscriber
        Light kitchenLight = new Light();    // Another Subscriber

        Console.WriteLine("Subscribing lights to button click...");
        // Subscribe livingRoomLight's SwitchOn method to the button's Click event
        myButton.Click += livingRoomLight.SwitchOn; // Using += to subscribe

        // Subscribe kitchenLight's SwitchOn method
        myButton.Click += kitchenLight.SwitchOn;


        Console.WriteLine("\nSimulating Button Click 1:");
        myButton.SimulateClick(); // Raises the event, both lights should turn ON

        Console.WriteLine("\nUnsubscribing kitchen light...");
        // Unsubscribe kitchenLight's SwitchOn method
        myButton.Click -= kitchenLight.SwitchOn; // Using -= to unsubscribe

        Console.WriteLine("\nSimulating Button Click 2:");
        myButton.SimulateClick(); // Raises the event, ONLY living room light should turn ON

        // Demonstrate unsubscribing the remaining handler
        Console.WriteLine("\nUnsubscribing living room light...");
        myButton.Click -= livingRoomLight.SwitchOn;

        Console.WriteLine("\nSimulating Button Click 3 (after all unsubscribed):");
        myButton.SimulateClick(); // Raises the event, NOTHING should happen (no subscribers)

        Console.WriteLine("#endregion\n");
        #endregion

        #region Event with Custom EventArgs and Lambda Handler Example
        /*
        * Event with Custom EventArgs and Lambda Handler Example
        * Demonstrates using EventHandler<TEventArgs> and subscribing with lambda expressions.
        */
        Console.WriteLine("\n#region Event with Custom EventArgs and Lambda Handler Example");

        Thermometer thermometer = new Thermometer(); // The Publisher
        Heater houseHeater = new Heater();         // A Subscriber

        Console.WriteLine("Subscribing heater to thermometer temperature changes...");

        // Subscribe using a named method handler
        thermometer.TemperatureChanged += houseHeater.OnTemperatureLow;

        /*
         * LAMBDA EXPRESSION HANDLER Example
         * Subscribing using a lambda expression directly.
         */
        Console.WriteLine("Subscribing a lambda handler...");
        // Subscribe using a lambda expression for a simple action
        EventHandler<TemperatureEventArgs> alertLambda = (sender, args) =>
        {
            Console.WriteLine($"Alert System: Temperature notification received from {(sender as Thermometer)?.GetType().Name}. Current: {args.CurrentTemperature}°C.");
        }; // Store the lambda in a variable if you need to unsubscribe later

        thermometer.TemperatureChanged += alertLambda;

        // You could also subscribe without storing the lambda, but unsubscribing is harder:
        // thermometer.TemperatureChanged += (sender, args) => Console.WriteLine("Another simple lambda.");


        Console.WriteLine("\nSetting Temperature to 15°C:");
        thermometer.CurrentTemp = 15; // Should trigger OnTemperatureLow and the lambda

        Console.WriteLine("\nSetting Temperature to 25°C:");
        thermometer.CurrentTemp = 25; // Should trigger OnTemperatureHigh (oops, heater isn't subscribed to high temp yet)
                                      // The lambda will still fire as temperature changed

        Console.WriteLine("\nSetting Temperature to 25°C again (should not trigger event):");
        thermometer.CurrentTemp = 25; // Temperature hasn't changed, event should NOT be raised

        Console.WriteLine("\nSetting Temperature to 5°C:");
        thermometer.CurrentTemp = 5; // Should trigger OnTemperatureLow and the lambda

        Console.WriteLine("\nUnsubscribing named handler and lambda handler...");
        thermometer.TemperatureChanged -= houseHeater.OnTemperatureLow;
        thermometer.TemperatureChanged -= alertLambda; // Can unsubscribe because we stored the lambda

        Console.WriteLine("\nSetting Temperature to 20°C (after unsubscribing):");
        thermometer.CurrentTemp = 20; // Temperature changes, but no subscribers are attached

        Console.WriteLine("#endregion\n");
        #endregion

        Console.ReadKey(); // Keep console open in some environments
    }
}