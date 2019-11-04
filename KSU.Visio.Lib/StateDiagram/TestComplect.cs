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
        Precondition precondition;
        Postcondition postcondition;
        List<TestCase> testCases = new List<TestCase>();
        Emulator emulator;
        List<Transfer> stream = new List<Transfer>();
        int currentTransfer = -1;
        public TestComplect (XmlNode testComplect, Emulator emulator)
        {
            this.emulator = emulator;
            precondition = new Precondition( testComplect.SelectSingleNode("Precondition"), emulator);
            stream.AddRange(precondition.GetStream());
            foreach (XmlNode testCaseXML in testComplect.SelectNodes("TestCase"))
            {
                TestCase testCase = new TestCase(testCaseXML, emulator);
                testCases.Add(testCase);
                stream.AddRange(testCase.GetStream());
            }
            postcondition = new Postcondition(testComplect.SelectSingleNode("Postcondition"), emulator);
            stream.AddRange(postcondition.GetStream());
            if (stream.Count > 0) currentTransfer = 0;
        }
        //public void Run()
        //{
        //    precondition.Run();
        //    foreach (TestCase testCase in testCases)
        //        testCase.Run();
        //}
        internal Transfer GetCurrentTransfer()
        {
            if (currentTransfer >= stream.Count) return null;
            else return stream[currentTransfer];
        }
        public Transfer NextTransfer()
        {
            currentTransfer++;
            return GetCurrentTransfer();
        }
    }
}
