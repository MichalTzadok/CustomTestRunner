using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using CustomTestRunner.Models;

namespace CustomTestRunner.Output
{
    public class ResultWriter
    {
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
