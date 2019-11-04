using System.Collections.Generic;
using System;
using System.IO;
namespace KSU.Visio.Lib.StateDiagram
{
    public class Expression
    {
        public static string GenerateInputsToCSV(Dictionary<string, object> dict)
        {
            List<string> header = new List<string>();
            var testComplect = dict["TestComplect"] as List<List<Dictionary<string, object>>>;
            string text = ""; foreach (List<Dictionary<string, object>> testCase in testComplect)
            {
                text += "TEST_CASE_START" + "\r\n"; foreach (Dictionary<string, object> order in testCase)
                {
                    List<string> line = new List<string>(); 
                    foreach (string orderKey in order.Keys)
                    {
                        int indColumn = header.IndexOf(orderKey);
                        if (indColumn == -1)
                        {
                            header.Add(orderKey);
                            indColumn = header.Count - 1;
                        }
                        while (indColumn >= line.Count)
                            line.Add("");
                        line[indColumn] = order[orderKey].ToString();
                    }
                    text += string.Join(";", line.ToArray()) + "\r\n";
                }
                text += "TEST_CASE_END" + "\r\n";
            }
            text = string.Join(";", header.ToArray()) + "\r\n" + text; return text;
        }
        public void Run(object obj)
        {
            Dictionary<string, object> dict = obj as Dictionary<string, object>;
            try
            {
                var testComplect = dict["TestComplect"] as List<List<Dictionary<string, object>>>;
                var testCase = testComplect[testComplect.Count - 1] as List<Dictionary<string, object>>;
                var order = testCase[testCase.Count - 1] as Dictionary<string, object>;
                var users = dict["Users"] as Dictionary<string, string>;
                order.Add("User", users["Priority1"]);
            }
            catch (Exception ex)
            {
                using (FileStream fs = File.Open(
                    DateTime.Now.ToString("yyyyMMdd") + ".log",
                    FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.BaseStream.Position = sw.BaseStream.Length;
                        sw.WriteLine("DEBUG|" + DateTime.Now.ToString() + "|" + ex.ToString() + "\n" +
                            GenerateInputsToCSV(dict));
                    }
                }
            }
        }
    }
}
