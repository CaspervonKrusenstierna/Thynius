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

namespace ThemisClient.Comms
{
    class DllComms : IDisposable
    {
        private Thread CommThread;
        private System.IO.MemoryMappedFiles.MemoryMappedFile MappedFile;
        private System.IO.MemoryMappedFiles.MemoryMappedViewAccessor MFAccessor;
        private ushort SleepBetweenCommsCheck;

        private bool DisposedValue;
        private bool ExitCommThreadMain = false;
        private void CommThreadMain()
        {
            MappedFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew("Global\\ThemisIPC", 256 * sizeof(char));
            MFAccessor = MappedFile.CreateViewAccessor();

            string lastMessage = "";
            while (!ExitCommThreadMain)
            {
                var bytes = new byte[MFAccessor.Capacity];
                MFAccessor.ReadArray(0, bytes, 0, bytes.Length);
                string message = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
                if (message != lastMessage)
                {
                    lastMessage = message;
                }
                Thread.Sleep(SleepBetweenCommsCheck);
            }


        }
        public DllComms(ushort SleepBetweenCommsCheck = 100)
        {
            this.SleepBetweenCommsCheck = SleepBetweenCommsCheck;
            CommThread = new Thread(CommThreadMain);
            CommThread.Start();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    ExitCommThreadMain = true;
                    MappedFile.Dispose();
                    MFAccessor.Dispose();
                }

                DisposedValue = true;
            }
        }
    }
}
