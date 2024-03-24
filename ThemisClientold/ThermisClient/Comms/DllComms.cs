using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThermisClient.Comms
{
    class DllComms
    {
        private Thread _pipeThread;

        private void PipeThreadMain()
        {
           NamedPipeServerStream _pipe = new NamedPipeServerStream("mynamedpipe");
            _pipe.WaitForConnection();
            Debug.WriteLine("Client Connected");
            StreamString ss = new StreamString(_pipe);
            while (true)
            {
                Debug.WriteLine("Message:");
                Debug.WriteLine(ss.ReadString());
                if (ss.ReadString() != null)
                {
                    break;
                }
            }
            _pipe.Close();
        }

        public DllComms() {
            this._pipeThread = new Thread(PipeThreadMain);
            _pipeThread.Start();

        }
    }
}

public class StreamString
{
    private Stream ioStream;
    private UnicodeEncoding streamEncoding;

    public StreamString(Stream ioStream)
    {
        this.ioStream = ioStream;
        streamEncoding = new UnicodeEncoding();
    }

    public string ReadString()
    {
        int len = 0;
        len = ioStream.ReadByte() * 256;
        len += ioStream.ReadByte();
        byte[] inBuffer = new byte[len];
        ioStream.Read(inBuffer, 0, len);

        return streamEncoding.GetString(inBuffer);
    }

    public int WriteString(string outString)
    {
        byte[] outBuffer = streamEncoding.GetBytes(outString);
        int len = outBuffer.Length;
        if (len > UInt16.MaxValue)
        {
            len = (int)UInt16.MaxValue;
        }
        ioStream.WriteByte((byte)(len / 256));
        ioStream.WriteByte((byte)(len & 255));
        ioStream.Write(outBuffer, 0, len);
        ioStream.Flush();

        return outBuffer.Length + 2;
    }
}