# PointsBet_Backend_Online_Code_Test
This is a simple folder containing a `StringFormatter.cs` file for code enhancement.

For candidates, please follow the below points to complete the test:
- Read the `README.md` file in the root directory, which should be the same file you are reading now. :)
- Go to the `README.md` file
  - Read the comment in the `StringFormatter.cs` file
  - Follow the instructions in the comment to complete the task
- Once complete your solution, please submit the codebase to your GitHub and share the public link with our HR team

Thank you for completing the code test with PointsBet.

# Answer to the PointsBet Backend Online Code Test 

## Author 
  **Michael Chen**  


```bash
File Structure /
├── Michael_Chen_Code_Test/              # Main project including StringFormatter.cs
│   └── StringFormatter.cs
│
├── Michael_Chen_Code_Test.UnitTests/    # Unit Tests
│   └── StringFormatterUnitTests.cs
│
├── .giattributes                       
├── .gitignore
└──  README.md                             
```

# StringFormatter Utility with Unit Tests

- This repository contains a simple but robust utility method for converting an array of strings into a  quoted, comma-separated list. 
- It also includes a suite of unit tests to ensure correctness and handle edge cases.

---

## Project Structure

- `Michael_Chen_Code_Test/`: Main logic project with the `StringFormatter` class.
- `Michael_Chen_Code_Test.UnitTests/`: xUnit test project that thoroughly tests all versions of the method.

---

## Features

- `ToCommaSeparatedListV1`, `V2`, `V3`:
  - Handles empty or null input arrays
  - Wraps items in custom quote characters
  - Skips `null` or empty strings safely
  - Supports special characters (e.g., `"`, `,`, etc.)
  - more to see what unit test cover

---

##  Getting Started

### 1. Clone the Repository

```bash

git clone https://github.com/DC-943/Michael_Chen_PointsBet_Code_Test.git
cd folder named "Michael Chen Code Test and Unit Tests"
cd folder named "Michael_Chen_Code_Test"

```

### 2. Open in Visual Studio

- Open Michael_Chen_Code_Test.sln in Visual Studio 2022 or newer.

### 3. Build the Solution

- Use Build > Build Solution (or Ctrl + Shift + B).

### 4. Run the Unit Tests

- Use Visual Studio Test Explorer
  - Open Test > Test Explorer
  - Click Run All Tests

## Tested Scenarios
   
  Unit tests cover:

- Normal input items with multiple elements such as { "apple", "banana", "cherry" }, "\""
- input item is null or new string[] {}
- Quote is null or ""
- null inside input items {"a", null, "b"}
- one item in input such as {"onlyOne"}
- quote has multiple letters such as "<>" "--"
- empty string in the input such as {"a", " ", "b"}
- input items containing quotes and commas such as { "complex \"quote\", and comma" }
- input and quote have special characters such as \, \n, or emoji
- performance test for long strings
- input items such as null and new string[0] for LINQ

## Dependencies

- .NET 6 or later

- xUnit for unit testing

## Example Usage 

```csharp
var result = StringFormatter.ToCommaSeparatedListV1(
    new[] { "apple", "banana","orange"},
    "'"
);
//result:'apple','banana','orange' 
```

## License

- This project is licensed under the MIT License.

