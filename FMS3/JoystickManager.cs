using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Xml;

namespace FMS3
{
	/*
	 * Reads in the configuration about different types of joysticks from the "joystickProfiles" XML file
	 * Also keeps track of actual joysticks attached to the system and which type they've been told to be
	 */
	class JoystickManager
	{
		// Singleton pattern
		private static JoystickManager instance;

		private Dictionary<string, JoystickConfigBean> configBeans = new Dictionary<string, JoystickConfigBean>();
		private JoystickConfigBean defaultBean;
		private Dictionary<string, JoystickWrapper> joysticksByGuid = new Dictionary<string, JoystickWrapper>();

		private Joystick joystick;
		private JoystickState state = new JoystickState();
		//private int numPOVs;
		//private int SliderCount;

		private JoystickManager()
		{
			JoystickConfigBean bean = null;
			bool foundDefault = false;
			using (XmlReader reader = XmlReader.Create("joystickProfiles.xml"))
			{
				while (reader.Read())
				{
					if (reader.IsStartElement())
					{
						switch (reader.Name)
						{
							case "JoystickConfig":
								// store the old bean?
								if (bean != null)
								{
									configBeans.Add(bean.typeName, bean);
									if (!foundDefault)
									{
										defaultBean = bean;
										foundDefault = true;
									}
								}
								// make a new bean
								bean = new JoystickConfigBean();
								break;
							case "Name":
								if (reader.Read()) bean.typeName = reader.Value.Trim();
								break;
							case "L_Motor":
								if (reader.Read()) bean.lMotorChoiceString = reader.Value.Trim();
								break;
							case "R_Motor":
								if (reader.Read()) bean.rMotorChoiceString = reader.Value.Trim();
								break;
							case "FineMotor":
								if (reader.Read()) bean.fineMotorChoiceString = reader.Value.Trim();
								break;
							case "A_MotorInc":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_AMOTORINC] = Int32.Parse(reader.Value.Trim());
								break;
							case "A_MotorDec":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_AMOTORDEC] = Int32.Parse(reader.Value.Trim());
								break;
							case "D_MotorInc":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_DMOTORINC] = Int32.Parse(reader.Value.Trim());
								break;
							case "D_MotorDec":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_DMOTORDEC] = Int32.Parse(reader.Value.Trim());
								break;
							case "ReverseToggle":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_REVERSETOGGLE] = Int32.Parse(reader.Value.Trim());
								break;
							case "PowerSelect":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_POWERSELECT] = Int32.Parse(reader.Value.Trim());
								break;
							case "GearSelect":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_GEARSELECT] = Int32.Parse(reader.Value.Trim());
								break;
							case "PowerReset":
								if (reader.Read()) bean.buttonChoices[JoystickWrapper.BUTTONCHOICE_POWERRESET] = Int32.Parse(reader.Value.Trim());
								break;
						}
					}
				}
			}
			// store the last bean?
			if (bean != null)
			{
				configBeans.Add(bean.typeName, bean);
				if (!foundDefault)
				{
					defaultBean = bean;
					foundDefault = true;
				}
			}

			// DEBUG
			foreach (JoystickConfigBean thisBean in configBeans.Values)
			{
				Console.WriteLine("DEBUG: thisBean~" + thisBean.toString());
			}
		}

		// Singleton pattern
		public static JoystickManager getInstance()
		{
			if (instance == null)
				instance = new JoystickManager();
			return instance;
		}

		// Get a list of the known types of joysticks (e.g., to show a list in the UI)
		public List<string> getConfigTypes()
		{
			List<string> configTypes = new List<string>();
			foreach (string thisType in configBeans.Keys)
				configTypes.Add(thisType);
			return configTypes;
		}

		public JoystickConfigBean getDefaultConfigBean()
		{
			return defaultBean;
		}

		// Returns the map of Joystick objects by GUID
		public Dictionary<string, JoystickWrapper> getJoysticksByGuid()
		{
			return joysticksByGuid;
		}

		// Assign a "type" to a given Joystick instance
		public void setType(JoystickWrapper joystick, string type)
		{
			JoystickConfigBean configBean = configBeans[type];
			Console.WriteLine("DEBUG: JoystickManager.setType() - configBean=" + configBean);
			if (joystick != null && configBean != null)
				joystick.resetChoices(configBean.lMotorChoiceString, configBean.rMotorChoiceString, configBean.fineMotorChoiceString, configBean.buttonChoices);
		}

		// Uses SlimDX to determine if any joysticks are attached to the system
		// Need the 'form' (the main window) so we know joysticks should be read when the main windoiw is in the foreground [I think]
		public void scanForJoysticks(System.Windows.Forms.Form form)
		{
			// make sure that DirectInput has been initialized
			DirectInput dinput = new DirectInput();

			// search for devices
			string thisGuid = "";
			foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
			{
				// create the device
				try
				{
					joystick = new Joystick(dinput, device.InstanceGuid);
					joystick.SetCooperativeLevel(form, CooperativeLevel.Exclusive | CooperativeLevel.Background);

					thisGuid = device.InstanceGuid.ToString().Split('-')[0];
					Console.WriteLine("DEBUG: scanForJoysticks(): device.InstanceGuid=" + thisGuid);

					// is this a new joystick, not already in the map?
					if (!joysticksByGuid.ContainsKey(thisGuid))
					{
						// set each axis to return data from -100 to 100
						foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
						{
							// set the range from -100 to 100 ~ this is suitable for LEGO brick motor powers
							if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
								joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
						}

						// acquire the device
						joystick.Acquire();

						// use the default bean to start with
						JoystickWrapper newWrapper = new JoystickWrapper(thisGuid, joystick,
							defaultBean.lMotorChoiceString, defaultBean.rMotorChoiceString, defaultBean.fineMotorChoiceString,
							defaultBean.buttonChoices);
						joysticksByGuid.Add(thisGuid, newWrapper);

						Console.WriteLine("DEBUG: Adding joystick at guid=" + thisGuid);
					}
				}
				catch (DirectInputException)
				{
				}
			}

			foreach (string listGuid in joysticksByGuid.Keys)
			{
				JoystickWrapper thisWrapper = joysticksByGuid[listGuid];
				Console.WriteLine("DEBUG: scanForJoysticks listGuid=" + listGuid + ", thisWrapper=" + thisWrapper);
			}
		}
	}
}
