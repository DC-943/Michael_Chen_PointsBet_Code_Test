using PointsBet_Backend_Online_Code_Test;

namespace MIcahel_Chen_Code_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To Comma Separated list is: \n");
            var result = StringFormatter.ToCommaSepatatedList(new[] { "apple", "banana", "orange" }, "\"");
            var result1 = StringFormatter.ToCommaSeparatedListV1(new[] { "apple", "banana", "orange" },"\"");
            var result2= StringFormatter.ToCommaSeparatedListV2(new[] { "apple", "banana", "orange" }, "\"");
            var result3 = StringFormatter.ToCommaSeparatedListV3(new[] { "apple", "banana", "orange" }, "\"");
           
            Console.WriteLine($"original test result is: {result}");
            Console.WriteLine($"version 1 test result is: {result1}");
            Console.WriteLine($"version 2 test result is: {result2}");
            Console.WriteLine($"version 3 test result is: {result3}");
            
        }
    }
}
