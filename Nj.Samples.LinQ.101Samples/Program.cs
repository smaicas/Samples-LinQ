using Nj.Samples.LinQ._101Samples;

//
// Restriction operators: The where keyword
//
// The where keyword or Where method provide this capability. These operators restrict,
// or filter, the input sequence to produce an output sequence.
//
public static class Restrictions
{
    private static List<Product> GetProductList() => Products.ProductList;
    private static List<Customer> GetCustomerList() => Customers.CustomerList;

    /// <summary>
    /// This sample uses where to find all elements of an array less than 5.
    /// It demonstrates the components of a query, including a where clause t
    /// hat filters for small numbers.
    /// </summary>
    public static void LowNumbers()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        var lowNums = from num in numbers
                      where num < 5
                      select num;

        Console.WriteLine("Numbers < 5:");
        foreach (var x in lowNums)
        {
            Console.WriteLine(x);
        }

        var lowNums2 = numbers.Where(x => x < 5);

    }

    /// <summary>
    /// This sample uses where to find all products that are out of stock. I
    /// ts where clause examines a property of the items in the input sequence.
    /// </summary>
    public static void ProductsOutOfStock()
    {
        List<Product> products = GetProductList();

        var soldOutProducts = from prod in products
                              where prod.UnitsInStock == 0
                              select prod;

        Console.WriteLine("Sold out products:");
        foreach (var product in soldOutProducts)
        {
            Console.WriteLine($"{product.ProductName} is sold out!");
        }

        var soldOutProducts2 = products.Where(x => x.UnitsInStock == 0);
    }

    /// <summary>
    /// This sample uses where to find all products that are in stock and cost more than 3.00 per unit.
    /// </summary>
    public static void ExpensiveProductsInStock()
    {
        List<Product> products = GetProductList();

        var expensiveInStockProducts = from prod in products
                                       where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                                       select prod;

        Console.WriteLine("In-stock products that cost more than 3.00:");
        foreach (var product in expensiveInStockProducts)
        {
            Console.WriteLine($"{product.ProductName} is in stock and costs more than 3.00.");
        }

        var expensiveInStockProducts2 = products.Where(x => x.UnitsInStock > 0 && x.UnitPrice > 3.00M);
    }

    /// <summary>
    /// This sample uses where to find all customers in Washington and
    /// then uses the resulting sequence to drill down into their orders.
    /// </summary>
    /// <returns></returns>
    public static void DisplayCustomerOrders()
    {
        List<Customer> customers = GetCustomerList();

        var waCustomers = from cust in customers
                          where cust.Region == "WA"
                          select cust;

        Console.WriteLine("Customers from Washington and their orders:");
        foreach (var customer in waCustomers)
        {
            Console.WriteLine($"Customer {customer.CustomerID}: {customer.CompanyName}");
            foreach (var order in customer.Orders)
            {
                Console.WriteLine($"  Order {order.OrderID}: {order.OrderDate}");
            }
        }

        var waCustomers2 = customers.Where(x => x.Region == "WA");
        var orders2 = waCustomers2.Select(x => x.Orders);
    }

    /// <summary>
    /// This sample demonstrates an indexed Where clause that returns digits
    /// whose name is shorter than their value.
    /// </summary>
    public static void IndexedWhere()
    {
        string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var shortDigits = digits.Where((digit, index) => digit.Length < index);

        Console.WriteLine("Short digits:");
        foreach (var d in shortDigits)
        {
            Console.WriteLine($"The word {d} is shorter than its value.");
        }

        var shortDigits2 = from digit in digits
                           where digit.Length < Array.IndexOf(digits, digit)
                           select digit;
    }
}

//
// LINQ - Projection Operators
// The select clause of a LINQ query projects the output sequence. It transforms each
// input element into the shape of the output sequence
//

public static class Projections
{
    private static List<Product> GetProductList() => Products.ProductList;
    private static List<Customer> GetCustomerList() => Customers.CustomerList;

    /// <summary>
    /// This sample uses `select` to produce a sequence of ints one higher
    /// than those in an existing array of ints. It demonstrates how `select` can modify the input sequence.

    /// </summary>
    public static void SelectSyntax()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        var numsPlusOne = from n in numbers
                          select n + 1;

        Console.WriteLine("Numbers + 1:");
        foreach (var i in numsPlusOne)
        {
            Console.WriteLine(i);
        }

        var numsPlusOne2 = numbers.Select(x => x + 1);
    }

    /// <summary>
    /// This sample uses `select` to return a sequence of just the names of a list of products.
    /// </summary>
    public static void SelectProperty()
    {
        List<Product> products = GetProductList();

        var productNames = from p in products
                           select p.ProductName;

        Console.WriteLine("Product Names:");
        foreach (var productName in productNames)
        {
            Console.WriteLine(productName);
        }

        var productNames2 = products.Select(x => x.ProductName);

    }

    /// <summary>
    /// This sample uses `select` to produce a sequence of strings
    /// representing the text version of a sequence of ints.
    /// </summary>
    public static void TransformWithSelect()
    {
        var dic = new Dictionary<int, string>()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
            { 0, "ten" },
        };
        var textNums = from n in dic.Keys
                       select dic[n];

        Console.WriteLine("Number strings:");
        foreach (var s in textNums)
        {
            Console.WriteLine(s);
        }

        var textNums2 = dic.Keys.Select(x => dic[x]);
    }

    /// <summary>
    /// This sample uses select to produce a sequence of the uppercase and
    /// lowercase versions of each word in the original array. The items in
    /// the output sequence are anonymous types. That means the compiler generates
    /// a class for them with the relevant properties, but that type has a name
    /// known only to the compiler.
    /// </summary>
    public static void SelectByCaseAnonymous()
    {
        string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

        var upperLowerWords = from w in words
                              select new { Upper = w.ToUpper(), Lower = w.ToLower() };

        foreach (var ul in upperLowerWords)
        {
            Console.WriteLine($"Uppercase: {ul.Upper}, Lowercase: {ul.Lower}");
        }

        var upperLowerWords2 = words.Select(x => new
        {
            Upper = x.ToUpper(),
            Lower = x.ToLower()
        });
    }

    /// <summary>
    /// Beginning with C# 7, you can also project to tuples, using the following syntax.
    /// The items in the output sequence are instances of System.ValueTuple.
    /// The compiler adds metadata to provide meaningful member names.
    /// </summary>
    public static void SelectByCaseTuple()
    {
        string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

        var upperLowerWords = from w in words
                              select (Upper: w.ToUpper(), Lower: w.ToLower());

        foreach (var ul in upperLowerWords)
        {
            Console.WriteLine($"Uppercase: {ul.Upper}, Lowercase: {ul.Lower}");
        }

        var upperLowerWords2 = words.Select(x => (Upper: x.ToUpper(), Lower: x.ToLower()));
    }

    /// <summary>
    /// This sample uses select to produce a sequence containing text representations
    /// of digits and whether their length is even or odd.
    /// </summary>
    public static void SelectAnonymousConstructions()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var digitOddEvens = from n in numbers
                            select new { Digit = strings[n], Even = (n % 2 == 0) };

        foreach (var d in digitOddEvens)
        {
            Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
        }

        var digitOddEvens2 = numbers.Select(x => new
        {
            Digit = strings[x],
            Even = x % 2 == 0
        });
    }

    /// <summary>
    /// You can choose to create either anonymous types or tuples in these projections, as shown below:
    /// </summary>
    public static void SelectTupleConstructions()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var digitOddEvens = from n in numbers
                            select (Digit: strings[n], Even: (n % 2 == 0));

        foreach (var d in digitOddEvens)
        {
            Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
        }

        var digitOddEvens2 = numbers.Select(x => (Digit: strings[x], Even: (x % 2 == 0)));
    }

    /// <summary>
    /// This sample uses select to produce a sequence containing some properties of Products,
    /// including UnitPrice which is renamed to Price in the resulting type.
    /// </summary>
    public static void SelectPropertySubset()
    {
        List<Product> products = GetProductList();

        var productInfos = from p in products
                           select (p.ProductName, p.Category, Price: p.UnitPrice);

        Console.WriteLine("Product Info:");
        foreach (var productInfo in productInfos)
        {
            Console.WriteLine($"{productInfo.ProductName} is in the category {productInfo.Category} and costs {productInfo.Price} per unit.");
        }

        var productInfos2 = products.Select(x => (x.ProductName, x.Category, Price: x.UnitPrice));
    }

    /// <summary>
    /// This sample uses an indexed Select clause to determine if the value of
    /// ints in an array match their position in the array.
    /// </summary>
    public static void SelectWithIndex()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        var numsInPlace = numbers.Select((num, index) => (Num: num, InPlace: (num == index)));

        Console.WriteLine("Number: In-place?");
        foreach (var n in numsInPlace)
        {
            Console.WriteLine($"{n.Num}: {n.InPlace}");
        }

        var numsInPlace2 = from n in numbers
                           select (Num: n, InPlace: (n == Array.IndexOf(numbers, n)));
    }

    /// <summary>
    /// This sample combines select and where to make a simple query that
    /// returns the text form of each digit less than 5.
    /// </summary>
    public static void SelectWithWhere()
    {
        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
        string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var lowNums = from n in numbers
                      where n < 5
                      select digits[n];

        Console.WriteLine("Numbers < 5:");
        foreach (var num in lowNums)
        {
            Console.WriteLine(num);
        }

        var lowNums2 = numbers.Where(x => x < 5).Select(x => digits[x]);
    }

    /// <summary>
    /// This sample uses a compound from clause to make a query that returns all pairs of
    /// numbers from both arrays such that the number from numbersA is less than the number
    /// from numbersB.
    /// </summary>
    public static void SelectFromMultipleSequences()
    {
        int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
        int[] numbersB = { 1, 3, 5, 7, 8 };

        var pairs = from a in numbersA
                    from b in numbersB
                    where a < b
                    select (a, b);

        Console.WriteLine("Pairs where a < b:");
        foreach (var pair in pairs)
        {
            Console.WriteLine($"{pair.a} is less than {pair.b}");
        }

        numbersA.Where(x => x < numbersB[Array.IndexOf(numbersA, x)])
            .Select(x => (x, numbersB[Array.IndexOf(numbersA, x)]));
    }

    public static void SelectFromChildSequence()
    {
        #region select-many-drilldown
        List<Customer> customers = GetCustomerList();

        var orders = from c in customers
                     from o in c.Orders
                     where o.Total < 500.00M
                     select (c.CustomerID, o.OrderID, o.Total);

        foreach (var order in orders)
        {
            Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}, Total value: {order.Total}");
        }
        #endregion
    }

    public static void SelectManyWithWhere()
    {
        #region select-many-filter
        List<Customer> customers = GetCustomerList();

        var orders = from c in customers
                     from o in c.Orders
                     where o.OrderDate >= new DateTime(1998, 1, 1)
                     select (c.CustomerID, o.OrderID, o.OrderDate);

        foreach (var order in orders)
        {
            Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}, Total date: {order.OrderDate.ToShortDateString()}");
        }
        #endregion
    }

    public static void SelectManyWhereAssignment()
    {
        #region select-many-assignment
        List<Customer> customers = GetCustomerList();

        var orders = from c in customers
                     from o in c.Orders
                     where o.Total >= 2000.0M
                     select (c.CustomerID, o.OrderID, o.Total);

        foreach (var order in orders)
        {
            Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}, Total value: {order.Total}");
        }
        #endregion

    }
    public static void SelectMultipleWhereClauses()
    {
        #region multiple-where-clauses
        List<Customer> customers = GetCustomerList();

        DateTime cutoffDate = new DateTime(1997, 1, 1);

        var orders = from c in customers
                     where c.Region == "WA"
                     from o in c.Orders
                     where o.OrderDate >= cutoffDate
                     select (c.CustomerID, o.OrderID);

        foreach (var order in orders)
        {
            Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}");
        }
        #endregion

    }
    public static void IndexedSelectMany()
    {
        #region indexed-select-many
        List<Customer> customers = GetCustomerList();

        var customerOrders =
            customers.SelectMany(
                (cust, custIndex) =>
                cust.Orders.Select(o => "Customer #" + (custIndex + 1) +
                                        " has an order with OrderID " + o.OrderID));

        foreach (var order in customerOrders)
        {
            Console.WriteLine(order);
        }
        #endregion

    }
}