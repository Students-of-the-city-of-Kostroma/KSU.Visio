﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class Postcondition
    {
        protected Emulator emulator;
        public List<Transfer> transfers = new List<Transfer>();
        public Postcondition(XmlNode root, Emulator emulator)
        {
            this.emulator = emulator;
            foreach (XmlNode transfer in root.SelectNodes("Transfer"))
                transfers.Add(emulator.SearchTransfer(transfer.Attributes["name"].Value));
        }
        internal List<Transfer> GetStream() { return transfers; }
    }
}
