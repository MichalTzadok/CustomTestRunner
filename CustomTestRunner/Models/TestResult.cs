

namespace CustomTestRunner.Models
{
    public class TestResult
    {
        public string TestName { get; set; }
        public bool Passed { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionType { get; set; }
        public long DurationMilliseconds { get; set; }
    }
}