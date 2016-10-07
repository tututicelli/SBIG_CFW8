//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM FilterWheel driver for CFW8
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM FilterWheel interface version: 1.0
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	1.0.0	Initial edit, from ASCOM FilterWheel Driver template
// --------------------------------------------------------------------------------
//
using System;
using System.Collections;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.Globalization;
using System.IO.Ports;
using System.Threading;

namespace ASCOM.CFW8
{
    //
    // Your driver's ID is ASCOM.CFW8.FilterWheel
    //
    // The Guid attribute sets the CLSID for ASCOM.CFW8.FilterWheel
    // The ClassInterface/None addribute prevents an empty interface called
    // _FilterWheel from being created and used as the [default] interface
    //
    [Guid("a8ae8154-b88f-4909-baf9-ce46d36dfae7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class FilterWheel : IFilterWheelV2
    {
        #region Constants
        //
        // Driver ID and descriptive string that shows in the Chooser
        //
        public const string driverId = "ASCOM.CFW8.FilterWheel";
        // TODO Change the descriptive string for your driver then remove this line
        private const string driverDescription = "CFW8 FilterWheel";
        #endregion

        #region ASCOM Registration
        //
        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var p = new Profile())
            {
                p.DeviceType = "FilterWheel";
                if (bRegister)
                    p.Register(driverId, driverDescription);
                else
                    p.Unregister(driverId);
            }
        }

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }
        #endregion

        #region Implementation of IFilterWheelV2

        bool connected = false;
        string port;
        string[] names;
        short pos;

        public void code()
        {
            SerialPort MyCOMPort = new SerialPort();
            //COM port settings to 8N1 mode 
            MyCOMPort.PortName = "COM3";            // Name of the COM port 
            MyCOMPort.BaudRate = 4800;               // Baudrate = 9600bps
            MyCOMPort.Parity = Parity.None;        // Parity bits = none  
            MyCOMPort.DataBits = 8;                  // No of Data bits = 8
            MyCOMPort.StopBits = StopBits.One;       // No of Stop bits = 1


            MyCOMPort.Open();                        // Open the port
            //MyCOMPort.Write("A");                    // Write an ascii "A"
            for (int i = 1; i <= 51; i++)
            {
                byte[] bytestosend = { 0x80 };
                MyCOMPort.Write(bytestosend, 0, bytestosend.Length);
                Thread.Sleep(18);
            }
            MyCOMPort.Close();
        }

        public void SetupDialog()
        {
            
            using (var f = new SetupDialogForm())
            {
                f.ShowDialog();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            
            throw new ASCOM.MethodNotImplementedException("Action");
        }

        public void CommandBlind(string command, bool raw)
        {            
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {            
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {            
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {            
            
        }

        private SerialPort connexion()
        {
            SerialPort MyCOMPort = new SerialPort();
            
            MyCOMPort.PortName = port;            // Name of the COM port 
            MyCOMPort.BaudRate = 4800;               // Baudrate = 9600bps
            MyCOMPort.Parity = Parity.None;        // Parity bits = none  
            MyCOMPort.DataBits = 8;                  // No of Data bits = 8
            MyCOMPort.StopBits = StopBits.One;       // No of Stop bits = 1


            MyCOMPort.Open();
            return MyCOMPort;
        }

        public bool Connected
        {
            get {
                return connected;                
            }
            set
            {
                if (value)
                {

                    using (ASCOM.Utilities.Profile p = new Utilities.Profile())
                    {
                        p.DeviceType = "FilterWheel";
                        port = p.GetValue(driverId, "ComPort");
                        names = new string[5];
                        for (int i = 0; i < 5; i++)
                        {
                            names[i] = p.GetValue(driverId, "Position" + (i+1));                            
                        }
                    }
                    if (string.IsNullOrEmpty(port))
                    {
                        throw new ASCOM.NotConnectedException("no Com port selected");
                    }
                                        
                    Position = 0;

                    connected = true;
                    
                }
                else
                {
                    connected = false;
                }


            }
        }

        public string Description
        {
            get {
                
                return "RAS"; }
        }

        public string DriverInfo
        {
            get {                
                return "RAS";
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
            }
        }

        public short InterfaceVersion
        {
            get { return 2; }
        }

        public string Name
        {
            get {                
                return "Filter wheel compatible SBIG CFW8"; 
            }
        }

        public ArrayList SupportedActions
        {
            get {                
                return new ArrayList(); }
        }

        public int[] FocusOffsets
        {
            get {                
                throw new System.NotImplementedException(); }
        }

        public string[] Names
        {
            get {                                
                return names;
            }
        }
                
        public short Position
        {
            get {
                return pos;
            }
            set {
                    SerialPort portSerie = null;
                    try
                    {
                        portSerie = connexion();
                        byte[] bytestosend = new byte[1];

                        switch (value)
                        {                               
                            case 0:
                                bytestosend[0] = 0xfc;
                                break;
                            case 1:
                                bytestosend[0] = 0xf8;
                                break;
                            case 2:
                                bytestosend[0] = 0xf0;
                                break;
                            case 3:
                                bytestosend[0] = 0xc0;
                                break;
                            case 4:
                                bytestosend[0] = 0x80;
                                break;
                            default:
                               bytestosend[0] = 0xfc;
                               break;
                        }


                        for (int i = 1; i <= 51; i++)
                        {
                            portSerie.Write(bytestosend, 0, bytestosend.Length);
                            Thread.Sleep(18);
                        }

                        Thread.Sleep(3000);
                    }
                    catch (Exception e)
                    {
                        throw new ASCOM.NotConnectedException("Serial port error", e);
                    }
                    finally
                    {
                        if (portSerie != null && portSerie.IsOpen)
                        {
                            portSerie.Close();
                        }
                    }

                    pos = value;
                }
        }

        #endregion
    }
}
