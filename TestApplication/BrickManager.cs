using System;
using System.Collections.Generic;
using System.Management;

namespace FMS3
{
	class BrickManager
	{
		private static BrickManager instance;

		private Dictionary<string, GenericBrick> namesToBricks;
		private Dictionary<string, string> namesToComms;

		private BrickManager()
		{
			namesToBricks = new Dictionary<string, GenericBrick>();
			namesToComms = new Dictionary<string, string>();
		}

		public static BrickManager getInstance()
		{
			if (instance == null)
				instance = new BrickManager();
			return instance;
		}

		/*********************************************/

		public List<string> getListOfAttachedBricks()
		{
			List<string> brickList = new List<string>();

			Dictionary<string, string> btToComDict = new Dictionary<string, string>();

			const string ComQueryString = "SELECT Caption,PNPDeviceID FROM Win32_PnPEntity " +
										"WHERE ConfigManagerErrorCode = 0 AND " +
										"Caption LIKE 'Standard Serial over Bluetooth link (COM%' AND " +
										"PNPDeviceID LIKE '%&001653%'";
			SelectQuery ComWMIquery = new SelectQuery(ComQueryString);
			ManagementObjectSearcher ComWMIqueryResults = new ManagementObjectSearcher(ComWMIquery);
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
					btToComDict.Add(BTaddress, comPort);

					//Console.WriteLine("COM Port: {0} ", comPort);
					//Console.WriteLine("BT Addr:  {0} ", BTaddress);
					//Console.WriteLine("");
				}
			}
			else
			{
				Console.WriteLine("Error executing query");
			}

			//Console.WriteLine("*************");
			//Console.WriteLine("");

			const string NameQueryString = "select Caption,Description,DeviceID,Name,PNPDeviceID from Win32_PnPEntity " +
				"where Description = 'Bluetooth Device' AND " +
				"PNPDeviceID LIKE '%_001653%'";
			SelectQuery NameWMIquery = new SelectQuery(NameQueryString);
			ManagementObjectSearcher NameWMIqueryResults = new ManagementObjectSearcher(NameWMIquery);
			if (NameWMIqueryResults != null)
			{
				Console.WriteLine("Name query... The following bricks were found on your system:");
				foreach (object result in NameWMIqueryResults.Get())
				{
					ManagementObject mo = (ManagementObject)result;
					object captionObject = mo.GetPropertyValue("Caption");
					object pnpIdObject = mo.GetPropertyValue("PNPDeviceID");

					string caption = captionObject.ToString();

					// Extract the BT address from the PNPObjectID property
					string BTaddress = pnpIdObject.ToString().Split('_')[1].Substring(0, 12);

					// Get the COM port
					string comPort = btToComDict[BTaddress];

					Console.WriteLine("BT Addr:  {0} ", BTaddress);
					Console.WriteLine("Name   :  {0} ", caption);
					Console.WriteLine("COM	:  {0} ", comPort);
					Console.WriteLine("");

					if (!namesToComms.ContainsKey(caption))
						namesToComms.Add(caption, comPort);

					string listName = caption + "";
					if (namesToBricks.ContainsKey(caption))
					{
						GenericBrick checkBrick = namesToBricks[caption];
						listName = listName + " [" + GenericBrick.STATE_NAMES[checkBrick.getState() + 2] + "]";
					}
					brickList.Add(listName);
				}
			}
			else
			{
				Console.WriteLine("Error executing query");
			}

			brickList.Sort();
			return brickList;

			//Console.WriteLine("Press the any key to exit");
			//Console.ReadKey();
		}

		public GenericBrick getBrickByName(string name)
		{
			// is it already connected?
			if (namesToBricks.ContainsKey(name))
				return namesToBricks[name];

			// otherwise, return nothing
			return null;
		}

		public GenericBrick getBrickByName(string name, bool isEv3)
		{
			// is it already connected?
			if (namesToBricks.ContainsKey(name))
				return namesToBricks[name];

			// is there a COM port associated?
			if (!namesToComms.ContainsKey(name))
				return null;
			// what is the COM port
			string newComPort = namesToComms[name];

			// try to connect
			GenericBrick newBrick = new GenericBrick(name, newComPort, isEv3);
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
