using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using CustomTestRunner.Models;

namespace CustomTestRunner.Output
{
    /// <summary>
    /// Writes test results to both text and JSON files under TestResults/.
    /// Also prints a summary to the console.
    /// </summary>

    public class ResultWriter
    {
        /// <summary>
        /// Writes test results to text and JSON files in the TestResults directory.
        /// Also prints a summary to the console.
        /// </summary>
        /// <param name="results">List of test results to write</param>
        public void Write(List<TestResult> results)
        {
            int passed = results.Count(r => r.Passed);
            int failed = results.Count - passed;

            Directory.CreateDirectory("TestResults");
            var textOutput = new StringWriter();

            int index = 1;
            foreach (var result in results)
            {
                textOutput.WriteLine($"Test #{index++}: {result.TestName}");
                if (result.Passed)
                    textOutput.WriteLine("PASSED");
                else
                    textOutput.WriteLine($"FAILED - [{result.ExceptionType}] {result.ErrorMessage}");
                textOutput.WriteLine($"Duration: {result.DurationMilliseconds}ms\n");
            }

            string summary = $"--- Summary ---\nTotal: {results.Count}, Passed: {passed}, Failed: {failed}";
            textOutput.WriteLine(summary);

            File.WriteAllText("TestResults/results.txt", textOutput.ToString());
            File.WriteAllText("TestResults/results.json", JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine(summary);
            Console.WriteLine("Results written to TestResults/ directory.");
        }
    }
}
