using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GWO
{
    public class StateReader : IStateReader
    {
        public StateReader() { }
        public StateSerial LoadFromFileStateOfAlghoritm(string path)
        {
            string jsonData = System.IO.File.ReadAllText(path);
            StateSerial state = JsonSerializer.Deserialize<StateSerial>(jsonData);

            return state;
        }
    }
}
