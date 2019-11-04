using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class TestComplect
    {
        public Precondition precondition;
        public Postcondition postcondition;
        public List<TestCase> testCases = new List<TestCase>();
        protected Emulator emulator;
        public TestComplect (XmlNode testComplect, Emulator emulator)
        {
            this.emulator = emulator;
            precondition = new Precondition( testComplect.SelectSingleNode("Precondition"), emulator);
            foreach (XmlNode testCase in testComplect.SelectNodes("TestCase"))
                testCases.Add(new TestCase(testCase, emulator));
            postcondition = new Postcondition(testComplect.SelectSingleNode("Postcondition"), emulator);
        }
        public void Run()
        {
            precondition.Run();
            foreach (TestCase testCase in testCases)
                testCase.Run();
        }
    }
}
