using System.Net.NetworkInformation;
using System.Text;
namespace PointsBet_Backend_Online_Code_Test
{

    /*
    Improve a block of code as you see fit in C#.
    You may make any improvements you see fit, for example:
      - Cleaning up code
      - Removing redundancy
      - Refactoring / simplifying
      - Fixing typos
      - Any other light-weight optimisation
    */
    public class StringFormatter
    {

        //Code to improve
        public static string ToCommaSepatatedList(string[] items, string quote)
        {
            StringBuilder qry = new StringBuilder(string.Format("{0}{1}{0}", quote, items[0]));

            if (items.Length > 1)
            {
                for (int i = 1; i < items.Length; i++)
                {
                    qry.Append(string.Format(", {0}{1}{0}", quote, items[i]));
                }
            }
            return qry.ToString();
        }


        #region
        /*****************  Improved code version 1 *****************/
        /* - Correct spelling: Sepatated -> Separated
         * - Add Null or empty check for items string array
         * - Remove redundary: string.Format -> use Append method of StringBuilder directly
         * - Remove: qry -> stringBuilder for clarity
         */

        /* Replace all occurrences of the quote string inside each item with a backslash followed by the quote, i.e., \quote */
        public static string Escape(string s, string quote)
        {
            return s.Replace(quote, "\\" + quote);
        }
        public static string ToCommaSeparatedListV1(string[] items, string quote)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty; // Return empty string if items is null or empty
            }
            if (string.IsNullOrEmpty(quote)) //if quote is null or empty, throw an exception
            {
                throw new ArgumentException("Quote cannot be null or empty", nameof(quote));
            }
            if (items.Any(item => item == null)) //if any item in the array is null, throw an exception
            {
                throw new ArgumentException("Item in array cannot be null", nameof(items));
            }
    
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(quote).Append(Escape(items[0],quote)).Append(quote);

            for (int i = 1; i < items.Length; i++)
            {
                stringBuilder.Append(", ").Append(quote).Append(Escape(items[i], quote)).Append(quote);
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region
        /*****************  Optimised code version 2 *****************/
        /* - Add string interpolation to reduce redundancy
         */
        public static string ToCommaSeparatedListV2(string[] items, string quote)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(quote))
            {
                throw new ArgumentException("Quote cannot be null or empty", nameof(quote));
            }
            if (items.Any(item => item == null))
            {
                throw new ArgumentException("Item in array cannot be null", nameof(items));
            }
            var sb = new StringBuilder();

            sb.Append($"{quote}{Escape(items[0], quote)}{quote}");

            for (int i = 1; i < items.Length; i++)
            {
                sb.Append(", ").Append($"{quote}{Escape(items[i], quote)}{quote}");
            }
            return sb.ToString();
        }
        #endregion

        #region
        /*****************  Optimised code version 3 *****************/
        /* - replace StringBuilder with string.Join() to avoid deal with the first item in the array separately
         * - replace for loop with LINQ for simplicity
         */
        public static string ToCommaSeparatedListV3(string[] items, string quote)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(quote))
            {
                throw new ArgumentException("Quote cannot be null or empty", nameof(quote));
            }
            if (items.Any(item => item == null))
            {
                throw new ArgumentException("Item in array cannot be null", nameof(items));
            }
            return string.Join($", ", items.Select(item => $"{quote}{Escape(item, quote)}{quote}"));
        }
        #endregion
    }
}
