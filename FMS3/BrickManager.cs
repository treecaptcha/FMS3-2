using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using InTheHand.Net.Sockets;
using MonoBrick.FiveOne;
using System.Management;
using System.Management.Automation.Security;
using System.Media;
using System.IO.Ports;

namespace FMS3
{
	//
	// Handles finding bricks known to the computer, brick connection states, etc.
	//
	// Connectivity notes:
	// -- Bricks have text names associated by their owners, visible from the brick screen
	// -- These names are associated with Bluetooth addresses; maybe in WMI, maybe in the registry
	// -- Bluetooth addresses are associated with virtual COM ports
	// -- MonoBrick uses the COM ports to send & receive communications
	// So we need to build a connective path from each brick name to its associated virtual COM port
	//
	class BrickManager
	{
		// Singleton
		private static BrickManager instance;

		// Map of brick names to GenericBrick objects
		private Dictionary<string, GenericBrick> namesToBricks;
		// Map of brick names to virtual COM ports (part of the Bluetooth connectivity)
		private Dictionary<string, string> namesToComms;

		// Private constructor {singleton pattern}
		private BrickManager()
		{
			namesToBricks = new Dictionary<string, GenericBrick>();
			namesToComms = new Dictionary<string, string>();
		}

		// Get the singleton BrickManager (creating it if the 1st call in)
		// NOT MULTITHREADING SAFE
		public static BrickManager getInstance()
		{
			if (instance == null)
				instance = new BrickManager();
			return instance;
		}

		/*********************************************/

		//
		// Return a simple list of strings of which bricks are connected.
		// "Connected" means MonoBrick connected to that brick (even if the connection was since lost).
		// Indicated by a suffix of ' [NXT]', ' [EV3]', ' [LOST]', etc.
		//
		public List<string> getListOfConnectedBricks()
		{
			List<string> connBricks = new List<string>();
			List<string> attachedBricks = getListOfAttachedBricks();
			foreach (string thisBrick in attachedBricks)
				if (thisBrick.Contains("["))
					connBricks.Add(thisBrick);

			return connBricks;
		}

		//
		/// Find all bricks attached to the system.
		/// "Attached" means they have been connected via Bluetooth (independent of this software).
		/// Different Bluetooth stacks store brick names, Bluetooth IDs, and associated COM ports differently - WMI vs. registry
		//
		public List<string> getListOfAttachedBricks()
		{
			
			

			lock (this)
			{

				Console.WriteLine("DEBUG: getListOfAttachedBricks() *** doing everything");
				BluetoothClient client = new BluetoothClient();
				List<string> items = new List<string>();
				IReadOnlyCollection<BluetoothDeviceInfo> devices = client.DiscoverDevices();
				foreach (BluetoothDeviceInfo d in devices)
				{
					// get bluetooth devices that are connected, have a com port 
					if (d.Connected)
					{
						Console.WriteLine("DEBUG: buildBtToComDict *** Investigating " + d.DeviceName);
						string comPort = GetBluetoothPort(d.DeviceAddress.ToString());
						Console.WriteLine("DEBUG: buildBtToComDict *** " + d.DeviceName + " has com port " + comPort);
						if (!string.IsNullOrWhiteSpace(comPort))
						{
							// to add
							Console.WriteLine("DEBUG: checkAndAddBtNamePair() ~ BTaddress=" + d.DeviceName +
							                  ",brickName=" + d.DeviceName + "~" + comPort);

							if (!namesToComms.ContainsKey(d.DeviceName))
							{
								Console.WriteLine(
									"DEBUG: checkAndAddBtNamePair() ~~ Adding new pair to namesToComms: " +
									d.DeviceName + "~" + comPort);
								namesToComms.Add(d.DeviceName, comPort);
							}

							string listName = d.DeviceName + "";
							if (namesToBricks.ContainsKey(d.DeviceName))
							{
								GenericBrick checkBrick = namesToBricks[d.DeviceName];
								//listName = listName + " [" + GenericBrick.STATE_NAMES[checkBrick.getState() - GenericBrick.OFFSET_STATE_NAMES] + "]";
								listName = listName + " [" + checkBrick.getStateName() + "]";
							}

							items.Add(listName);
						}

					}
				}

				Console.WriteLine("DEBUG: getListOfAttachedBricks() *** items: " + items);
				return items;
			}




			Dictionary<string, string> btToComDict = buildBtToComDict();

			//////////////////////////////////////
			Console.WriteLine("DEBUG: getListOfAttachedBricks() **** Querying for Bluetooth<~>Name relationships {to build 'Name<~>COM' map}");

			// Try WMI query first
			List<string> brickList = queryBtToName(btToComDict);

			// If we found zero (0) bricks via WMI, try a registry query
			if (brickList.Count == 0)
			{
				Console.WriteLine("DEBUG: getListOfAttachedBricks() !!! None found! Nothing left to try :(");
			}

			brickList.Sort();
			return brickList;
		}

		
		
		
		
		
		
		
		//
		/// Locate all Bluetooth connections, where the devices are Mindstorms bricks, and identify the associated COM ports
		//
		private static Dictionary<string, string> buildBtToComDict()
		{
			
			Dictionary<string, string> btCom = new Dictionary<string, string>();
			BluetoothClient client = new BluetoothClient();
			List<string> items = new List<string>();
			IReadOnlyCollection<BluetoothDeviceInfo> devices = client.DiscoverDevices();
			foreach (BluetoothDeviceInfo d in devices)
			{
				// get bluetooth devices that are connected, have a com port 
				if (d.Connected)
				{
					Console.WriteLine("DEBUG: buildBtToComDict *** Investigating " + d.DeviceName);
					string comPort = GetBluetoothPort(d.DeviceAddress.ToString());
					Console.WriteLine("DEBUG: buildBtToComDict *** " + d.DeviceName + " has com port " + comPort);
					if (!string.IsNullOrWhiteSpace(comPort))
					{
						btCom.Add(d.DeviceAddress.ToString(), comPort);
					}

				}
			}

			return btCom;
			
			// Run a COM query to get the COM ports (in the 'Caption') for Mindstorms BT connections
			Console.WriteLine("DEBUG: buildBtToCom() **** Querying for Bluetooth<~>COM relationships");
			Dictionary<string, string> btToComDict = new Dictionary<string, string>();
			const string ComQueryString = "SELECT Caption,PNPDeviceID FROM Win32_PnPEntity " +
										"WHERE ConfigManagerErrorCode = 0 AND " +
										"Caption LIKE 'Standard Serial over Bluetooth link (COM%' AND " +
										"PNPDeviceID LIKE '%&001653%'";
			SelectQuery ComWMIquery = new SelectQuery(ComQueryString);
			ManagementObjectSearcher ComWMIqueryResults = new ManagementObjectSearcher(ComWMIquery);

			// Did we get a result?
			if (ComWMIqueryResults != null)
			{
				//Console.WriteLine("Com query... The following bricks were found on your system:");
				foreach (object result in ComWMIqueryResults.Get())
				{
					ManagementObject mo = (ManagementObject)result;
					object captionObject = mo.GetPropertyValue("Caption");
					object pnpIdObject = mo.GetPropertyValue("PNPDeviceID");

					// Get the COM port name out of the Caption, requires a little search and replacing.
					string caption = captionObject.ToString();
					string comPort = caption.Substring(caption.LastIndexOf("(COM")).
										Replace("(", string.Empty).Replace(")", string.Empty);

					// Extract the BT address from the PNPObjectID property
					string BTaddress = pnpIdObject.ToString().Split('&')[4].Substring(0, 12);

					// add to dictionary
					Console.WriteLine("DEBUG: buildBtToCom() - BTaddress=" + BTaddress + ",COM=" + comPort);
					btToComDict.Add(BTaddress, comPort);

					//Console.WriteLine("COM Port: {0} ", comPort);
					//Console.WriteLine("BT Addr:  {0} ", BTaddress);
					//Console.WriteLine("");
				}
			}
			else
			{
				Console.WriteLine("ERROR: buildBtToCom() - Error executing BT/com query");
			}
			return btToComDict;
			
		}

		private List<string> queryBtToName(Dictionary<string, string> btToComDict)
		{
			List<string> Names = new List<string>();
			BluetoothClient client = new BluetoothClient();
			List<string> items = new List<string>();
			IReadOnlyCollection<BluetoothDeviceInfo> devices = client.DiscoverDevices();
			foreach (BluetoothDeviceInfo d in devices)
			{
				// get bluetooth devices that are connected 
				if (d.Connected)
				{
					checkAndAddBtNamePair(btToComDict, Names, d.DeviceName, d.DeviceAddress.ToString());
				}
			}

			return Names;
		}
		
		private static string GetBluetoothPort(string deviceAddress)
		{
			Console.WriteLine("DEBUG: buildBtToCom() **** Querying for Bluetooth<~>COM relationships");
			Dictionary<string, string> btToComDict = new Dictionary<string, string>();
			const string ComQueryString = "SELECT Caption,PNPDeviceID FROM Win32_PnPEntity " +
			                              "WHERE ConfigManagerErrorCode = 0 AND " +
			                              "Caption LIKE 'Standard Serial over Bluetooth link (COM%'";
			SelectQuery ComWMIquery = new SelectQuery(ComQueryString);
			ManagementObjectSearcher ComWMIqueryResults = new ManagementObjectSearcher(ComWMIquery);

			// Did we get a result?
			if (ComWMIqueryResults != null)
			{
				//Console.WriteLine("Com query... The following bricks were found on your system:");
				foreach (object result in ComWMIqueryResults.Get())
				{
					ManagementObject mo = (ManagementObject)result;
					object captionObject = mo.GetPropertyValue("Caption");
					object pnpIdObject = mo.GetPropertyValue("PNPDeviceID");

					// Get the COM port name out of the Caption, requires a little search and replacing.
					string caption = captionObject.ToString();
					string comPort = caption.Substring(caption.LastIndexOf("(COM")).
						Replace("(", string.Empty).Replace(")", string.Empty);
					Console.WriteLine("pnpIdObject.TOString():" + pnpIdObject.ToString());
					// Extract the BT address from the PNPObjectID property

					// add to dictionary
					if (pnpIdObject.ToString().Contains(deviceAddress))
					{
						return comPort;
					}
					//Console.WriteLine("COM Port: {0} ", comPort);
					//Console.WriteLine("BT Addr:  {0} ", BTaddress);
					//Console.WriteLine("");
				}
			}
			else
			{
				Console.WriteLine("ERROR: buildBtToCom() - Error executing BT/com query");
			}
			return null;
			
		

			
			
			
			
			
			
			
			const string Win32_SerialPort = "Win32_SerialPort";
			Console.WriteLine("querrieing");
			SelectQuery q = new SelectQuery(Win32_SerialPort);
			Console.WriteLine("querrieing done");

			ManagementObjectSearcher s = new ManagementObjectSearcher(q);
			ManagementObjectCollection sak = s.Get();
			foreach (ManagementBaseObject cur in sak)
			{
				ManagementObject mo = (ManagementObject)cur;
				string pnpId = mo.GetPropertyValue("PNPDeviceID").ToString();

				if (pnpId.Contains(deviceAddress))
				{
					object captionObject = mo.GetPropertyValue("Caption");
					string caption = captionObject.ToString();
					int index = caption.LastIndexOf("(COM");
					if (index > 0)
					{
						string portString = caption.Substring(index);
						string comPort = portString.
							Replace("(", string.Empty).Replace(")", string.Empty);
						Console.WriteLine("done with getting port");
						return comPort;
					}
				}              
			}
			return null;
		}

		//
		// Try to get a mapping of brick "name" to Bluetooth addresses by querying WMI (not guaranteed to work with all BT stacks)
		//
		private List<string> queryBtToNameFromWmi(Dictionary<string, string> btToComDict)
		{
			List<string> brickList = new List<string>();
			const string NameQueryString = "select Caption,Description,DeviceID,Name,PNPDeviceID from Win32_PnPEntity " +
				"where Description = 'Bluetooth Device' AND " +
				"PNPDeviceID LIKE '%_001653%'";
			SelectQuery NameWMIquery = new SelectQuery(NameQueryString);
			ManagementObjectSearcher NameWMIqueryResults = new ManagementObjectSearcher(NameWMIquery);

			// Did we get results?
			if (NameWMIqueryResults != null)
			{
				//Console.WriteLine("DEBUG: getListOfAttachedBricks() - Query... The following bricks were found on your system:");
				foreach (object result in NameWMIqueryResults.Get())
				{
					ManagementObject mo = (ManagementObject)result;
					object captionObject = mo.GetPropertyValue("Caption");
					object pnpIdObject = mo.GetPropertyValue("PNPDeviceID");

					// 2014-01-20, M.O'C: Replace spaces with underscores for bricks with spaces in the name
					string brickName = captionObject.ToString().Replace(" ", "_");

					// Extract the BT address from the PNPObjectID property
					string BTaddress = pnpIdObject.ToString().Split('_')[1].Substring(0, 12);

					// Validate & add the BT/name pairing
					checkAndAddBtNamePair(btToComDict, brickList, brickName, BTaddress);
				}
			}
			else
			{
				Console.WriteLine("DEBUG: queryBtToNameFromWmi() - Error executing BT/name query");
			}
			return brickList;
		}

		//
		// Try to get a mapping of brick "name" to Bluetooth addresses by querying the Registry (not guaranteed to work with all BT stacks)
		//
		private List<string> queryBtToNameFromRegistry(Dictionary<string, string> btToComDict)
		{
			//Console.WriteLine("DEBUG: queryBtToNameFromRegistry() - ENTER");
			List<string> brickList = new List<string>();

			// We're going to be making cyclical calls into registry keys as we find them
			String btBaseRegPath = @"SYSTEM\CurrentControlSet\services\BTHPORT\Parameters\LocalServices";
			using (RegistryKey btBaseRegKey = Registry.LocalMachine.OpenSubKey(btBaseRegPath))
			{
				foreach (String btSub1KeyName in btBaseRegKey.GetSubKeyNames())
				{
					//Console.WriteLine("DEBUG: queryBtToNameFromRegistry() - btSub1keyName=" + btSub1KeyName);
					using (RegistryKey btSub1RegKey = btBaseRegKey.OpenSubKey(btSub1KeyName))
					{
						foreach (String btSub2KeyName in btSub1RegKey.GetSubKeyNames())
						{
							//Console.WriteLine("DEBUG: queryBtToNameFromRegistry() - **btSub2KeyName=" + btSub2KeyName);
							using (RegistryKey btSub2RegKey = btSub1RegKey.OpenSubKey(btSub2KeyName))
							{
								// Get the brick name from the 'ServiceName' key
								// 2014-01-20, M.O'C: Replace spaces with underscores for bricks with spaces in the name
								String brickName = ((string)btSub2RegKey.GetValue("ServiceName")).Replace(" ", "_");

								// DEBUG!!!! If serviceName is blank [as is on Dell Win8.1], put in bogo name
								if ("".Equals(brickName.Trim()))
									brickName = "BOGO" + btSub2KeyName;

								// 2015-01-19, M.O'C: If a robot is deleted, the name still sits in the registry - but the Enabled flag is set to 0
								Int32 enabled = ((Int32)btSub2RegKey.GetValue("Enabled"));
								// Not deleted?
								if (enabled.Equals(1))
								{
									Byte[] assocBdAddr = (Byte[])btSub2RegKey.GetValue("AssocBdAddr");
									// reverse the array
									Byte[] assocBdAddrReverse = new Byte[assocBdAddr.Length];
									for (int i = 0; i < assocBdAddr.Length; i++)
									{
										assocBdAddrReverse[(assocBdAddr.Length - i) - 1] = assocBdAddr[i];
									}
									// Build the BT address out of the data from the registry
									string assocBdAddrHex = BitConverter.ToString(assocBdAddrReverse).Replace("-", string.Empty);
									string BTaddress = assocBdAddrHex.Substring(4);

									//Console.WriteLine("DEBUG: queryBtToNameFromRegistry() - >>>>BTaddress=" + BTaddress);
									//Console.WriteLine("DEBUG: queryBtToNameFromRegistry() - >>>>brickName=" + brickName);

									// Validate & add the BT/name pairing
									checkAndAddBtNamePair(btToComDict, brickList, brickName, BTaddress);
								}
							}
						}
					}
				}
			}

			return brickList;
		}

		//
		// Make sure the supplied BT address has a COM port associated; that the name isn't already added; etc.
		// Add the pairing to the master list
		//
		private void checkAndAddBtNamePair(Dictionary<string, string> btToComDict, List<string> brickList, string brickName, string BTaddress)
		{
			// Get the COM port
			if (btToComDict.ContainsKey(BTaddress))
			{
				string comPort = btToComDict[BTaddress];

				Console.WriteLine("DEBUG: checkAndAddBtNamePair() ~ BTaddress=" + BTaddress + ",brickName=" + brickName + "~" + comPort);

				if (!namesToComms.ContainsKey(brickName))
				{
					Console.WriteLine("DEBUG: checkAndAddBtNamePair() ~~ Adding new pair to namesToComms: " + brickName + "~" + comPort);
					namesToComms.Add(brickName, comPort);
				}

				string listName = brickName + "";
				if (namesToBricks.ContainsKey(brickName))
				{
					GenericBrick checkBrick = namesToBricks[brickName];
					//listName = listName + " [" + GenericBrick.STATE_NAMES[checkBrick.getState() - GenericBrick.OFFSET_STATE_NAMES] + "]";
					listName = listName + " [" + checkBrick.getStateName() + "]";
				}
				brickList.Add(listName);
			}
			else
				Console.WriteLine("WARN: checkAndAddBtNamePair() - No COM port associated with BT=" + BTaddress + " ~ for name=" + brickName);
		}

		//
		// Helper method, returns a name without the " [TAG]" extension
		//
		private static string stripExtensionFromName(string fullName)
		{
			string name = fullName + "";
			if (name.Contains("["))
				name = name.Split(' ')[0];
			return name;
		}

		//
		// Returns a reference to a 'generic brick' wrapper, given a brick name
		//
		public GenericBrick getBrickByName(string fullName)
		{
			string name = stripExtensionFromName(fullName);

			// is it already connected?
			if (namesToBricks.ContainsKey(name))
				return namesToBricks[name];

			// otherwise, return nothing
			return null;
		}

		//
		// Returns a reference to a 'generic brick' wrapper, given a brick name;
		// attempts to reconnect the brick if it has been disconnected
		//
		public GenericBrick getBrickByName(string fullName, bool isEv3, bool isFiveOne)
		{
			string name = stripExtensionFromName(fullName);

			// is it already connected?
			if (namesToBricks.ContainsKey(name))
				return namesToBricks[name];

			// is there a COM port associated?
			if (!namesToComms.ContainsKey(name))
				return null;
			// what is the COM port
			string newComPort = namesToComms[name];

			// try to connect
			GenericBrick newBrick = new GenericBrick(name, newComPort,  isEv3, isFiveOne);
			if (newBrick.getState() > 0)
			{
				// add the brick to the dictionary
				namesToBricks.Add(name, newBrick);

				return newBrick;
			}

			// if we got to here, connection didn't work
			return null;
		}
	}
}
