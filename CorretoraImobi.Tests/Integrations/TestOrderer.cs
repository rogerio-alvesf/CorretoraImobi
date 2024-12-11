using Xunit.Abstractions;
using Xunit.Sdk;

namespace CorretoraImobi.Tests
{
    public class PriorityOrderer : ITestCaseOrderer 
    { 
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        { 
            var sortedMethods = testCases.OrderBy(testCase => testCase.TestMethod.Method.Name).ToList();
            return sortedMethods;
        }
    }
}