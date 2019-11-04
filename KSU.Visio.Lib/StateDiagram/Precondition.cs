using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class Precondition
    {
        protected Emulator emulator;
        public Condition condition;
        public List<Transfer> transfers = new List<Transfer>();
        public Precondition(XmlNode root, Emulator emulator)
        {
            this.emulator = emulator;
            condition = emulator.SearchCondition(root.SelectSingleNode("Condition").Attributes["name"].Value);
            foreach (XmlNode transfer in root.SelectNodes("Transfer"))
                transfers.Add(emulator.SearchTransfer(transfer.Attributes["name"].Value));
        }
        public void Run()
        {
            emulator.ActiveCondition.Add(condition);
            foreach (Transfer transfer in transfers)
                emulator.Run(transfer);
        }
    }
}
