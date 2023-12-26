using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestsCore.Orderers
{
    public class RandomOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var isRandomOrder = GetIsRandomOrderValue();
            if (!isRandomOrder)
                return testCases;

            var random = new Random(DateTime.Now.Millisecond);
            var randomizedTestCases = testCases.OrderBy(_ => random.Next()).ToList();
            return randomizedTestCases;
        }

        private bool GetIsRandomOrderValue()
        {
            var jsonConfigPath = "./testConfig.json";
            if (File.Exists(jsonConfigPath))
            {
                try
                {
                    var jsonConfig = JObject.Parse(File.ReadAllText(jsonConfigPath));
                    var value = jsonConfig["isRandomOrder"]?.Value<bool>();
                    if (value is null)
                        return false;
                    return (bool)value;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
