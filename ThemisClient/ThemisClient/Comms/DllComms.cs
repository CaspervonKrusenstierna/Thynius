using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThermisClient.Comms
{
    class DllComms
    {
        private Thread CommThread;

        private void CommThreadMain()
        {
            var mappedFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew("Global\\ThemisIPC", 256 * sizeof(char));
            using (var accessor = mappedFile.CreateViewAccessor())
            {
                string lastMessage = "";
                while (true)
                {
                    var bytes = new byte[accessor.Capacity];
                    accessor.ReadArray<byte>(0, bytes, 0, bytes.Length);
                    string message = System.Text.Encoding.Unicode.GetString(bytes, 0, bytes.Length);
                    if(message != lastMessage)
                    {
                        lastMessage = message;
                    }
                    Thread.Sleep(100);
                }
            }
        }

        public DllComms() {
            this.CommThread = new Thread(CommThreadMain);
            CommThread.Start();

        }
    }
}
