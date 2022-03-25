using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace InsertInfoToDevice
{





    public class COMPortProgramme
    {
        ComboBox ComPort_CB;
        SerialPort RS232;
        Label COMStatus_Lab;

        public COMPortProgramme(ComboBox comport_CB, SerialPort rs232, Label comStatus_Lab)
        {
            ComPort_CB = comport_CB;
            RS232 = rs232;
            COMStatus_Lab = comStatus_Lab;

        }


        public void RefreshComPortList()//自行連接COM Port
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(ComPort_CB.Items.Cast<string>(), ComPort_CB.SelectedItem as string, RS232.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!string.IsNullOrEmpty(selected))
            {
                ComPort_CB.Items.Clear();
                ComPort_CB.Items.AddRange(OrderedPortNames());
                ComPort_CB.SelectedItem = selected;
            }
        }
        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }
        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;
            if ((PreviousPortNames.Count() == 0) && (ports.Count() == 0))
            {
                updated = false;
            }

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();
                if (ports.Count() == 0)
                {
                    ComPort_CB.Items.Clear();
                    ComPort_CB.SelectedItem = -1;
                }

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        public bool ConnectCOMPort()
        {
            if (!RS232.IsOpen)
            {
                RS232.Open();
                // Set the port's settings
                //RS232.BaudRate = 57600;
                RS232.DataBits = 8;
                RS232.StopBits = StopBits.One;
                RS232.Parity = Parity.None;
                RS232.RtsEnable = true;
                COMStatus_Lab.Text = "連接成功";

                return true;               

                
            }
            else
            {
                RS232.Close();
                ComPort_CB.Enabled = true;
                return false;
            }
        }
    }


}
