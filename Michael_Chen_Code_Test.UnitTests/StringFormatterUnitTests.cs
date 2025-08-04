using PointsBet_Backend_Online_Code_Test;
using System.Diagnostics;

namespace Michael_Chen_Code_Test.UnitTests
{
    public class StringFormatterUnitTests
    {

        #region
        /* improved code version 1 test */

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV1 method of the StringFormatter class.
        /// This test verifies that the method correctly wraps each string item in the specified quote character
        /// and joins them into a comma-separated list. It covers typical cases, including:
        /// - Multiple items with a standard quote
        /// - A single item
        /// - An empty string item
        /// </summary>
        /// <param name="items"></param>
        /// <param name="quote"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData(new string[] { "apple", "banana", "cherry" }, "\"", "\"apple\", \"banana\", \"cherry\"")]
        [InlineData(new string[] { "one" }, "'", "'one'")]
        [InlineData(new string[] { "" }, "*", "**")]
        public void ToCommaSeparatedListV1_ReturnsCorrectResult(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV1 method to verify behavior with null or empty input.
        /// Ensures that when the input array is null or contains no elements, 
        /// the method returns an empty string as expected, regardless of the quote character.
        /// </summary>
        /// <param name="items">The input array of strings, which is either null or empty in these test cases.</param>
        /// <param name="quote">The quote character to wrap each item (has no effect in these cases).</param>
        /// <param name="expected">The expected result, which should always be an empty string.</param>
        [Theory]
        [InlineData(null, "\"", "")]
        [InlineData(new string[] { }, "\"", "")]
        [InlineData(null, "'", "")]
        [InlineData(new string[] { }, "'", "")]
        public void ToCommaSeparatedListV1_ReturnsEmptyForNullOrEmptyInput(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV1 method to ensure it throws an ArgumentException
        /// when the quote parameter is null or an empty string.
        /// Validates that the method enforces required input validation on the quote parameter
        /// and provides an appropriate exception message.
        /// </summary>
        /// <param name="quote">The invalid quote input (either null or empty) that should trigger an exception.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ToCommaSeparatedListV1_ThrowsException_WhenQuoteIsNullOrEmpty(string quote)
        {
            var items = new[] { "a", "b" };

            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV1(items, quote));
            Assert.Equal("Quote cannot be null or empty (Parameter 'quote')", ex.Message);
        }


        /// <summary>
        /// Unit test for the ToCommaSeparatedListV1 method to verify that it throws an ArgumentException
        /// when the input array contains a null element.
        /// This ensures that the method properly validates each item in the array
        /// and rejects null values with an appropriate exception message.
        /// </summary>

        [Fact]
        public void ToCommaSeparatedListV1_ThrowsException_WhenItemIsNull()
        {
            var items = new[] { "a", null, "b" };
            var quote = "\"";  
            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV1(items, quote));
            Assert.Equal("Item in array cannot be null (Parameter 'items')", ex.Message);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV1 method to verify that it correctly formats a single-item array.
        /// Ensures that the method returns the single item surrounded by the provided quote character,
        /// without any commas or additional spacing.
        /// </summary>
        [Theory]
        [InlineData(new[] { "onlyOne" }, "\"", "\"onlyOne\"")]
        [InlineData(new[] { "onlyOne" }, "'", "'onlyOne'")]
        [InlineData(new[] { "onlyOne" }, "*", "*onlyOne*")]
        public void ToCommaSeparatedListV1_SingleItemArray_ReturnsQuotedSingleItem(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV1 correctly handles multi-character quote strings.
        /// Verifies that each item in the input array is wrapped with the full quote string on both sides,
        /// and that items are correctly joined with a comma and space.
        /// </summary>
        /// <param name="items">The array of string items to format.</param>
        /// <param name="quote">The multi-character quote string to wrap each item.</param>
        /// <param name="expected">The expected formatted string result.</param>
        [Theory]
        [InlineData(new[] { "a", "b" }, "--", "--a--, --b--")]
        [InlineData(new[] { "x", "y", "z" }, "[[", "[[x[[, [[y[[, [[z[[")]
        [InlineData(new[] { "1", "2" }, "<>", "<>1<>, <>2<>")]
        public void ToCommaSeparatedListV1_MultiCharacterQuote_WorksCorrectly(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV1 correctly handles empty and whitespace-only string elements in the input array.
        /// Ensures that empty strings and strings containing only spaces are properly wrapped with the specified quote
        /// and included in the comma-separated output without being omitted or altered.
        /// </summary>
        /// <param name="items">The array of string items, including empty or whitespace strings.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted string output.</param>
        [Theory]
        [InlineData(new[] { "" }, "\"", "\"\"")]                 
        [InlineData(new[] { " " }, "\"", "\" \"")]              
        [InlineData(new[] { "   " }, "\"", "\"   \"")]            
        [InlineData(new[] { "a", "", "b" }, "'", "'a', '', 'b'")] 
        [InlineData(new[] { "a", " ", "b" }, "*", "*a*, * *, *b*")]
        [InlineData(new[] { "x", "   ", "y" }, "#", "#x#, #   #, #y#")] 
        public void ToCommaSeparatedListV1_HandlesEmptyOrWhitespaceElements(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV1 correctly handles string elements containing quotes and commas.
        /// Verifies that quotes within items are preserved as-is and that commas inside items do not interfere
        /// with the overall comma-separated formatting, and each item is wrapped properly with the specified quote.
        /// </summary>
        /// <param name="items">The array of string items containing quotes and/or commas.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted output string.</param>
        [Theory]
        [InlineData(new[] { "he said \"hello\"", "test" }, "\"", "\"he said \\\"hello\\\"\", \"test\"")]  
        [InlineData(new[] { "comma,inside", "value" }, "'", "'comma,inside', 'value'")]
        [InlineData(new[] { "complex \"quote\", and comma" }, "*", "*complex \"quote\", and comma*")]
        public void ToCommaSeparatedListV1_HandlesElementsWithQuotesAndCommas(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }


        /// <summary>
        /// Tests edge cases for the ToCommaSeparatedListV1 method to ensure it correctly handles:
        /// - Strings containing backslashes and quote characters.
        /// - Strings with special characters such as newline (\n), tab (\t), and emojis.
        /// - Strings containing Unicode characters and multiple quote types.
        /// 
        /// Each input array is formatted into a comma-separated string, where each element is:
        /// - Wrapped in the specified quote (if provided).
        /// - Properly escaped if it contains special characters or the quote itself.
        /// </summary>
        /// <param name="items">The array of input strings to format.</param>
        /// <param name="quote">The quote character(s) to wrap around each item (e.g., "\", 🌟, etc.).</param>
        /// <param name="expected">The expected comma-separated and escaped string.</param>
        [Theory]
        [InlineData(new[] { "backslash\\test" }, "\\", "\\backslash\\\\test\\")]
        [InlineData(new[] { "line1\nline2", "tab\tseparated" }, "\"", "\"line1\nline2\", \"tab\tseparated\"")]
        [InlineData(new[] { "hello 🌍" }, "🌟", "🌟hello 🌍🌟")]
        [InlineData(new[] { "\"quoted\"", "\"again\"" }, "\"", "\"\\\"quoted\\\"\", \"\\\"again\\\"\"")]
        public void ToCommaSeparatedListV1_EdgeCases(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV1(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the performance of ToCommaSeparatedListV1 with a large input array.
        /// Ensures that formatting 100,000 items completes within 100 milliseconds,
        /// which serves as a basic performance benchmark to detect potential inefficiencies.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV1_PerformanceTest()
        {
            var largeArray = Enumerable.Repeat("test", 100000).ToArray();
            var stopwatch = Stopwatch.StartNew();
            var result = StringFormatter.ToCommaSeparatedListV1(largeArray, "\"");
            stopwatch.Stop();
            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 100); 
        }
        #endregion


        #region
        /* improved code version 2 test */
        /// <summary>
        /// Unit test for the ToCommaSeparatedListV2 method of the StringFormatter class.
        /// This test verifies that the method correctly wraps each string item in the specified quote character
        /// and joins them into a comma-separated list. It covers typical cases, including:
        /// - Multiple items with a standard quote
        /// - A single item
        /// - An empty string item
        /// </summary>
        /// <param name="items"></param>
        /// <param name="quote"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData(new string[] { "apple", "banana", "cherry" }, "\"", "\"apple\", \"banana\", \"cherry\"")]
        [InlineData(new string[] { "one" }, "'", "'one'")]
        [InlineData(new string[] { "" }, "*", "**")]
        public void ToCommaSeparatedListV2_ReturnsCorrectResult(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV2 method to verify behavior with null or empty input.
        /// Ensures that when the input array is null or contains no elements, 
        /// the method returns an empty string as expected, regardless of the quote character.
        /// </summary>
        /// <param name="items">The input array of strings, which is either null or empty in these test cases.</param>
        /// <param name="quote">The quote character to wrap each item (has no effect in these cases).</param>
        /// <param name="expected">The expected result, which should always be an empty string.</param>
        [Theory]
        [InlineData(null, "\"", "")]
        [InlineData(new string[] { }, "\"", "")]
        [InlineData(null, "'", "")]
        [InlineData(new string[] { }, "'", "")]
        public void ToCommaSeparatedListV2_ReturnsEmptyForNullOrEmptyInput(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV2 method to ensure it throws an ArgumentException
        /// when the quote parameter is null or an empty string.
        /// Validates that the method enforces required input validation on the quote parameter
        /// and provides an appropriate exception message.
        /// </summary>
        /// <param name="quote">The invalid quote input (either null or empty) that should trigger an exception.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ToCommaSeparatedListV2_ThrowsException_WhenQuoteIsNullOrEmpty(string quote)
        {
            var items = new[] { "x", "y", "z" };

            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV2(items, quote));
            Assert.Equal("Quote cannot be null or empty (Parameter 'quote')", ex.Message);
        }

        /// <summary>
        /// Unit test for the ToCommaSeparatedListV2 method to verify that it throws an ArgumentException
        /// when the input array contains a null element.
        /// This ensures that the method properly validates each item in the array
        /// and rejects null values with an appropriate exception message.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV2_ThrowsException_WhenItemIsNull()
        {
            var items = new[] { "a", "b", null };
            var quote = "\"";
            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV2(items, quote));
            Assert.Equal("Item in array cannot be null (Parameter 'items')", ex.Message);
        }
        /// <summary>
        /// Unit test for the ToCommaSeparatedListV2 method to verify that it correctly formats a single-item array.
        /// Ensures that the method returns the single item surrounded by the provided quote character,
        /// without any commas or additional spacing.
        /// </summary>
        [Theory]
        [InlineData(new[] { "onlyOne" }, "\"", "\"onlyOne\"")]
        [InlineData(new[] { "onlyOne" }, "'", "'onlyOne'")]
        [InlineData(new[] { "onlyOne" }, "*", "*onlyOne*")]
        public void ToCommaSeparatedListV2_SingleItemArray_ReturnsQuotedSingleItem(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV2 correctly handles multi-character quote strings.
        /// Verifies that each item in the input array is wrapped with the full quote string on both sides,
        /// and that items are correctly joined with a comma and space.
        /// </summary>
        /// <param name="items">The array of string items to format.</param>
        /// <param name="quote">The multi-character quote string to wrap each item.</param>
        /// <param name="expected">The expected formatted string result.</param>
        [Theory]
        [InlineData(new[] { "a", "b" }, "--", "--a--, --b--")]
        [InlineData(new[] { "x", "y", "z" }, "[[", "[[x[[, [[y[[, [[z[[")]
        [InlineData(new[] { "1", "2" }, "<>", "<>1<>, <>2<>")]
        public void ToCommaSeparatedListV2_MultiCharacterQuote_WorksCorrectly(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV2 correctly handles empty and whitespace-only string elements in the input array.
        /// Ensures that empty strings and strings containing only spaces are properly wrapped with the specified quote
        /// and included in the comma-separated output without being omitted or altered.
        /// </summary>
        /// <param name="items">The array of string items, including empty or whitespace strings.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted string output.</param>
        [Theory]
        [InlineData(new[] { "" }, "\"", "\"\"")]                  
        [InlineData(new[] { " " }, "\"", "\" \"")]                
        [InlineData(new[] { "   " }, "\"", "\"   \"")]            
        [InlineData(new[] { "a", "", "b" }, "'", "'a', '', 'b'")] 
        [InlineData(new[] { "a", " ", "b" }, "*", "*a*, * *, *b*")] 
        [InlineData(new[] { "x", "   ", "y" }, "#", "#x#, #   #, #y#")] 
        public void ToCommaSeparatedListV2_HandlesEmptyOrWhitespaceElements(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV2 correctly handles string elements containing quotes and commas.
        /// Verifies that quotes within items are preserved as-is and that commas inside items do not interfere
        /// with the overall comma-separated formatting, and each item is wrapped properly with the specified quote.
        /// </summary>
        /// <param name="items">The array of string items containing quotes and/or commas.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted output string.</param>
        [Theory]
        [InlineData(new[] { "he said \"hello\"", "test" }, "\"", "\"he said \\\"hello\\\"\", \"test\"")]
        [InlineData(new[] { "comma,inside", "value" }, "'", "'comma,inside', 'value'")]
        [InlineData(new[] { "complex \"quote\", and comma" }, "*", "*complex \"quote\", and comma*")]
        public void ToCommaSeparatedListV2_HandlesElementsWithQuotesAndCommas(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests edge cases for the ToCommaSeparatedListV2 method to ensure it correctly handles:
        /// - Strings containing backslashes and quote characters.
        /// - Strings with special characters such as newline (\n), tab (\t), and emojis.
        /// - Strings containing Unicode characters and multiple quote types.
        /// 
        /// Each input array is formatted into a comma-separated string, where each element is:
        /// - Wrapped in the specified quote (if provided).
        /// - Properly escaped if it contains special characters or the quote itself.
        /// </summary>
        /// <param name="items">The array of input strings to format.</param>
        /// <param name="quote">The quote character(s) to wrap around each item (e.g., "\", 🌟, etc.).</param>
        /// <param name="expected">The expected comma-separated and escaped string.</param>
        [Theory]
        [InlineData(new[] { "backslash\\test" }, "\\", "\\backslash\\\\test\\")]
        [InlineData(new[] { "line1\nline2", "tab\tseparated" }, "\"", "\"line1\nline2\", \"tab\tseparated\"")]
        [InlineData(new[] { "hello 🌍" }, "🌟", "🌟hello 🌍🌟")]
        [InlineData(new[] { "\"quoted\"", "\"again\"" }, "\"", "\"\\\"quoted\\\"\", \"\\\"again\\\"\"")]
        public void ToCommaSeparatedListV2_EdgeCases(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV2(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the performance of ToCommaSeparatedListV2 with a large input array.
        /// Ensures that formatting 100,000 items completes within 100 milliseconds,
        /// which serves as a basic performance benchmark to detect potential inefficiencies.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV2_PerformanceTest()
        {
            var largeArray = Enumerable.Repeat("test", 100000).ToArray();
            var stopwatch = Stopwatch.StartNew();
            var result = StringFormatter.ToCommaSeparatedListV2(largeArray, "\"");
            stopwatch.Stop();
            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 100);
        }
        #endregion


        #region
        /* improved code version 3 test */
        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly formats arrays of strings by wrapping each item with the specified quote
        /// and joining them as a comma-separated list using LINQ and string.Join.
        /// Covers typical multiple-item arrays as well as arrays with empty string elements.
        /// </summary>
        /// <param name="items">The array of string items to format.</param>
        /// <param name="quote">The quote string to wrap each item.</param>
        /// <param name="expected">The expected formatted output string.</param>
        [Theory]
        [InlineData(new string[] { "a", "b", "c" }, "-", "-a-, -b-, -c-")]
        [InlineData(new string[] { "" }, "-", "--")]
        public void ToCommaSeparatedListV3_WorksCorrectly(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }
   
        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly handles a null input array by returning an empty string.
        /// Ensures the method gracefully handles null without throwing exceptions.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV3_HandlesNullInput()
        {
            var result = StringFormatter.ToCommaSeparatedListV3(null, "\"");
            Assert.Equal(string.Empty, result);
        }


        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly handles an empty input array by returning an empty string.
        /// Verifies that the method does not throw exceptions and returns the expected result for empty input.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV3_HandlesEmptyArray()
        {
            var result = StringFormatter.ToCommaSeparatedListV3(new string[0], "\"");
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV3 throws an ArgumentException when the quote parameter is null or an empty string.
        /// Ensures that the method validates the quote parameter and provides a clear error message for invalid input.
        /// </summary>
        /// <param name="quote">The invalid quote value (null or empty) expected to cause the exception.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ToCommaSeparatedListV3_ThrowsException_WhenQuoteIsNullOrEmpty(string quote)
        {
            var items = new[] { "1", "2", "4" };

            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV3(items, quote));
            Assert.Equal("Quote cannot be null or empty (Parameter 'quote')", ex.Message);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV3 throws an ArgumentException when the input array contains a null element.
        /// Validates that the method enforces non-null elements within the input array and provides a meaningful error message.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV3_ThrowsException_WhenItemIsNull()
        {
            var items = new[] { "a", "b", "c", null };
            var quote = "\"";
            var ex = Assert.Throws<ArgumentException>(() => StringFormatter.ToCommaSeparatedListV3(items, quote));
            Assert.Equal("Item in array cannot be null (Parameter 'items')", ex.Message);
        }
        [Theory]
        [InlineData(new[] { "onlyOne" }, "\"", "\"onlyOne\"")]
        [InlineData(new[] { "onlyOne" }, "'", "'onlyOne'")]
        [InlineData(new[] { "onlyOne" }, "*", "*onlyOne*")]
        public void ToCommaSeparatedListV3_SingleItemArray_ReturnsQuotedSingleItem(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly handles multi-character quote strings.
        /// Verifies that each item in the input array is wrapped with the full quote string on both sides,
        /// and that items are correctly joined with a comma and space.
        /// </summary>
        /// <param name="items">The array of string items to format.</param>
        /// <param name="quote">The multi-character quote string to wrap each item.</param>
        /// <param name="expected">The expected formatted string result.</param>

        [Theory]
        [InlineData(new[] { "a", "b" }, "--", "--a--, --b--")]
        [InlineData(new[] { "x", "y", "z" }, "[[", "[[x[[, [[y[[, [[z[[")]
        [InlineData(new[] { "1", "2" }, "<>", "<>1<>, <>2<>")]
        public void ToCommaSeparatedListV3_MultiCharacterQuote_WorksCorrectly(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly handles empty and whitespace-only string elements in the input array.
        /// Ensures that empty strings and strings containing only spaces are properly wrapped with the specified quote
        /// and included in the comma-separated output without being omitted or altered.
        /// </summary>
        /// <param name="items">The array of string items, including empty or whitespace strings.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted string output.</param>
        [Theory]
        [InlineData(new[] { "" }, "\"", "\"\"")]                  
        [InlineData(new[] { " " }, "\"", "\" \"")]                
        [InlineData(new[] { "   " }, "\"", "\"   \"")]            
        [InlineData(new[] { "a", "", "b" }, "'", "'a', '', 'b'")] 
        [InlineData(new[] { "a", " ", "b" }, "*", "*a*, * *, *b*")] 
        [InlineData(new[] { "x", "   ", "y" }, "#", "#x#, #   #, #y#")] 
        public void ToCommaSeparatedListV3_HandlesEmptyOrWhitespaceElements(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToCommaSeparatedListV3 correctly handles string elements containing quotes and commas.
        /// Verifies that quotes within items are preserved as-is and that commas inside items do not interfere
        /// with the overall comma-separated formatting, and each item is wrapped properly with the specified quote.
        /// </summary>
        /// <param name="items">The array of string items containing quotes and/or commas.</param>
        /// <param name="quote">The quote character or string used to wrap each item.</param>
        /// <param name="expected">The expected formatted output string.</param>
        [Theory]
        [InlineData(new[] { "he said \"hello\"", "test" }, "\"", "\"he said \\\"hello\\\"\", \"test\"")]
        [InlineData(new[] { "comma,inside", "value" }, "'", "'comma,inside', 'value'")]
        [InlineData(new[] { "complex \"quote\", and comma" }, "*", "*complex \"quote\", and comma*")]
        public void ToCommaSeparatedListV3_HandlesElementsWithQuotesAndCommas(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }


        /// <summary>
        /// Tests edge cases for the ToCommaSeparatedListV3 method to ensure it correctly handles:
        /// - Strings containing backslashes and quote characters.
        /// - Strings with special characters such as newline (\n), tab (\t), and emojis.
        /// - Strings containing Unicode characters and multiple quote types.
        /// 
        /// Each input array is formatted into a comma-separated string, where each element is:
        /// - Wrapped in the specified quote (if provided).
        /// - Properly escaped if it contains special characters or the quote itself.
        /// </summary>
        /// <param name="items">The array of input strings to format.</param>
        /// <param name="quote">The quote character(s) to wrap around each item (e.g., "\", 🌟, etc.).</param>
        /// <param name="expected">The expected comma-separated and escaped string.</param>
        [Theory]
        [InlineData(new[] { "backslash\\test" }, "\\", "\\backslash\\\\test\\")]
        [InlineData(new[] { "line1\nline2", "tab\tseparated" }, "\"", "\"line1\nline2\", \"tab\tseparated\"")]
        [InlineData(new[] { "hello 🌍" }, "🌟", "🌟hello 🌍🌟")]
        [InlineData(new[] { "\"quoted\"", "\"again\"" }, "\"", "\"\\\"quoted\\\"\", \"\\\"again\\\"\"")]
        public void ToCommaSeparatedListV3_EdgeCases(string[] items, string quote, string expected)
        {
            var result = StringFormatter.ToCommaSeparatedListV3(items, quote);
            Assert.Equal(expected, result);
        }


        /// <summary>
        /// Tests the performance of ToCommaSeparatedListV3 with a large input array.
        /// Ensures that formatting 100,000 items completes within 100 milliseconds,
        /// which serves as a basic performance benchmark to detect potential inefficiencies.
        /// </summary>
        [Fact]
        public void ToCommaSeparatedListV3_PerformanceTest()
        {
            var largeArray = Enumerable.Repeat("test", 100000).ToArray();
            var stopwatch = Stopwatch.StartNew();
            var result = StringFormatter.ToCommaSeparatedListV3(largeArray, "\"");
            stopwatch.Stop();
            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 100);
        }
        #endregion
    }
}