using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;

namespace GWO
{
    public class StateWriter : IStateWriter
    {
        StateSerial state;
        public StateWriter(StateSerial state)
        {
            this.state = state;
        }
        public void SaveToFileStateOfAlghoritm(string path, StateSerial state)
        {
            string jsonString = JsonSerializer.Serialize(state);
            File.WriteAllText(path, jsonString);
        }
        public void DeleteSaveAfterCompletion(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}
