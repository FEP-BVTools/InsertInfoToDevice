using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InsertInfoToDevice
{
    class SerialPortControl
    {
        public SerialPort SerialPort = new SerialPort();
        private Thread ReceivedThread;
        private Queue<byte> ReceivedQueue = new Queue<byte>();
        public SerialPortControl()
        {
            SerialPort.BaudRate = 57600;
        }
        public bool IsOpen()
        {
            return SerialPort.IsOpen;
        }
        public int BaudRate()
        {
            return SerialPort.BaudRate;
        }
        public void BaudRate(int BaudRate)
        {
            SerialPort.BaudRate = BaudRate;
        }
        public string PortName()
        {
            return SerialPort.PortName;
        }
        public void PortName(string PortName)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            SerialPort.PortName = PortName;
        }
        public void Open()
        {
            if (!SerialPort.IsOpen)
            {
                SerialPort.Open();
            }
            ReceivedThread = new Thread(new ThreadStart(DataReceived));
            ReceivedThread.IsBackground = true;
            ReceivedThread.Start();
            Console.WriteLine("開");
        }
        public void Close()
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            ReceivedQueue.Clear();
            Console.WriteLine("關");
        }
        public void Write(byte[] Data)
        {
            SerialPort.Write(Data, 0, Data.Length);
        }
        public byte[] GetReceived()
        {
            return ReceivedQueue.ToArray();
        }
        public int GetReceivedLength()
        {
            return ReceivedQueue.Count;
        }
        public void ClearQueue()
        {
            ReceivedQueue.Clear();
        }
        private void DataReceived()
        {
            while (SerialPort.IsOpen)
            {
                try
                {
                    while (SerialPort.BytesToRead > 0)
                    {
                        ReceivedQueue.Enqueue(Convert.ToByte(SerialPort.ReadByte()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(5);
            }
        }
    }
}
