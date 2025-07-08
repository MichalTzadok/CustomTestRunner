#  CustomTestRunner

##  Overview

This project is a **lightweight custom test runner** built in C#.  
It uses **reflection** to discover and execute test methods marked with custom attributes like `[MyTest]`, `[Setup]`, and `[Teardown]`.

The runner was created to demonstrate an understanding of test frameworks, reflection, modular design, and clean output handling.

---

##  Features

- Discovers test classes and test methods automatically
- Supports setup (`[Setup]`) and teardown (`[Teardown]`) methods per test
- Measures test execution time (using `Stopwatch`)
- Catches exceptions and logs error type/message
- Outputs results to both `.txt` and `.json` under `TestResults/`
- Cleanly separated to support extensibility and readability

---

##  Design Decisions

###  1. Separation of Concerns

The system is split into logical modules:

| Module           | Responsibility                            |
|------------------|-------------------------------------------|
| `Attributes/`     | Custom attributes used to mark test methods |
| `Discovery/`      | Finds test classes and methods using reflection |
| `Execution/`      | Executes test methods and tracks results |
| `Output/`         | Writes human-readable and JSON output |
| `Models/`         | Shared model classes (e.g., `TestResult`) |
| `Runner/`         | High-level controller to orchestrate everything |

---

###  2. Validation Checks

- Only instantiable classes are used (`!IsAbstract && IsClass`).
- Invalid test methods are ignored with clear structure.

---

###  3. Runtime Metrics

Each test execution is wrapped in a `Stopwatch` to record how long it took in milliseconds.

---

##  How to Run

###  Prerequisites

- .NET 6.0 or newer
- Visual Studio 2022 (or `dotnet CLI`)

###  Run via Visual Studio

1. Open `CustomTestRunner.sln`
2. Set the project as the startup project.
3. Run using `Ctrl+F5`.

###  Run via Command Line

```bash
dotnet run --project CustomTestRunner
``` 

##  Output

After running, results are saved in:
```
TestResults/
├── results.txt # Console-style readable results
└── results.json # Structured JSON for tooling
```

---

##  Example Test Class

```csharp
public class SampleTests
{
    [Setup]
    public void Init() { Console.WriteLine("Setup!"); }

    [MyTest]
    public void PassingTest() { /* ok */ }

    [MyTest]
    public void FailingTest() { throw new Exception("Oops!"); }

    [Teardown]
    public void Cleanup() { Console.WriteLine("Teardown!"); }
}

``` 
## How Tests Are Discovered
[MyTest] is applied to test methods.

[Setup] and [Teardown] wrap each test individually.


## Possible Improvements

 Add support for async tests

 Add test categories

 Add filtering via CLI args

 Add internal unit tests for test runner itself

## Author
Michal Vahab<br>
GitHub: [@MichalVahab](https://github.com/MichalTzadok/)





