using System;
using System.IO;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace MonoBrick.FiveOne;

public class Connector  {
    private SerialPort sport;
    private bool debug;
    /**
     * represents a REPL connection to a 51515 brick using a serial port
     * @param port - serial port to open transmissions on
     * @param autoInject - whether to stop OS and prepare for commands to be sent
     * @param debug - whether to write helpful messages to the "Console"
     */
    public Connector(string port, bool autoInject = true, bool debug = false) {
        this.debug = debug;
        this.sport = new SerialPort(port, 115200);
        // I love StackOverflow
        sport.Handshake = Handshake.None;
        sport.DtrEnable = true;
        sport.RtsEnable = true;
        sport.StopBits = StopBits.One;
        sport.DataBits = 8;
        sport.Parity = Parity.None;

        sport.Open();
        // Two seconds to attempt to write before throwing an exception
        sport.WriteTimeout = 2000;
        if (autoInject) {
            inject();
        }
    }
    /**
     * Sends an interrupt then the contents of payload.py to the brick
     */
    public void inject() {
        interrupt();
        string payload = File.ReadAllText ("FiveOne" + Path.DirectorySeparatorChar + "payload.py");
        send(payload);
    }

    /**
     * send an interrupt (^c (0x03) and a carriage return (0x0D)) to the brick
     * it will try up to 5 times in ~3 seconds to do this if it cannot it will throw an exception
     * @throws IOException
     */
    public void interrupt() {
        for (int i = 0; i < 5; i++) {
            if (debug) Console.WriteLine("DEBUG: Sending interrupt");
            send("\x03\r", false);
            Thread.Sleep(500);
            if (debug) Console.WriteLine("DEBUG: There are " + sport.BytesToRead + " bytes to read");
            if (sport.BytesToRead > 3) {
                byte[] lastOnes = new byte[3];
                sport.Read(new byte[sport.BytesToRead - 4], 0, sport.BytesToRead - 4);
                sport.Read(lastOnes, 0 , 3);
                if (debug) Console.WriteLine("DEBUG: The last 3 bytes were " + lastOnes[0] + " " + lastOnes[1] + " " + lastOnes[2]);
                bool isThing = true;
                foreach (byte b in lastOnes) {
                    if (b != 62) {
                        isThing = false;
                        break;
                    }
                }

                if (isThing) break;

            }

            if (i == 4) {
                throw new IOException("Failed to take control of 51515 brick on " + sport.PortName + "!");
            }
        }
    }

    /**
     * Encodes a string in utf-8 then sends it to the brick
     * @param instruction - the instruction to be sent
     * @param sendln - should a newline character be appended to the string?
     */
    public void send(string instruction, bool sendln = true) {
        if (sendln) {
            instruction += "\r";
        }
        send(Encoding.UTF8.GetBytes(instruction));
    }
    
    /**
     * Send bytes to the brick
     */
    public void send(byte[] instruction) {
        sport.Write(instruction, 0, instruction.Length);
        
    }
    
    /**
     * Close the connection
     */
    public void close() {
        sport.Close();
    }

    public void logRepl() {
        if (sport.ReadBufferSize > 0) {
            byte[] inBytes = new byte[sport.ReadBufferSize];
            sport.Read(inBytes, 0, sport.ReadBufferSize);
            Console.WriteLine(Encoding.UTF8.GetString(inBytes).Trim());
        }
    }
    
}