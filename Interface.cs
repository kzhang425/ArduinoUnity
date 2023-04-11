using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lib
{
    public class Interface
    {
        protected SerialPort port = new SerialPort();

        // Depending on how the Arduino code is written, methods that use this enum may need to map the numbers
        // a specific way.
        public enum Movement
        {
            Pan,
            Tilt,
            Step,
        }

        public Interface()
        {
            establish_connection();
        }

        ~Interface()
        {
            close_connection();
        }

        // START OF CLASS METHODS

        // Makes a new port, automatically searches for and binds to a port with an arduino. Call this
        // in conjunction with close_connection to clean up after you are done.
        public virtual void establish_connection()
        {
            // Initialize a new serialport
            port.BaudRate = 9600;
            port.Parity = Parity.None;


            string[] ports = SerialPort.GetPortNames();
            foreach (string com in ports)
            {
                if (com.Substring(0, 3) == "COM")
                {
                    // If port is not open, try to open it. If it doesn't work, then move on to the next
                    // COM option
                    port.PortName = com;
                    
                    try { if (!port.IsOpen) port.Open(); break; }
                    catch { continue; }
                }
            }

            // After iterating through all ports, if the port is still not found and opened
            if (!port.IsOpen)
            {
                Console.WriteLine("No suitable ports found");
                return;
            }
            else
            {
                // Clear the port of residual data and let the data start flowing!
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                Console.WriteLine("Arduino found at: " + port.PortName);
            }
        }

        public virtual void write(string text)
        {
            if (port.IsOpen)
            {
                port.Write(text);
            }
        }

        public virtual string read()
        {
            if (port.IsOpen)
            {
                string buffer = "";
                while (port.BytesToRead != 0)
                {
                    string output = port.ReadLine().TrimEnd('\r');
                    buffer = buffer + '\n' + output;
                }
                return buffer;
            } 
            else
            {
                return "Nothing Read.";
            }
        }

        // Closes the port.
        public void close_connection()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }

        public virtual void write_movement(int pos, Movement cmd)
        {
            if (!port.IsOpen) return;

            // Turn input into suitable string
            string msg = pos.ToString() + ',' + cmd.GetHashCode().ToString() + '\n';

            // If the port is open, initiate sending the data over
            port.Write(msg);

        }
        


    }
}
