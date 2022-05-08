using SlimDX;
using SlimDX.DirectInput;
using System.Collections.Generic;

namespace FMS3
{
	/*
	 * An instance of "JoystickWrapper" represents a joystick attached to the system; also,
	 * how to interpret the inputs, what is its current state, and what has changed about its state
	 */
	class JoystickWrapper
	{
		// SlimDX objects
		private Joystick joystick;
		private JoystickState state = new JoystickState();
		// Unique ID assigned to this joystick
		private string guid;

		// State of the joystick
		private bool analogMode = true;
		private int lMotor = 0;
		private int rMotor = 0;
		private int fineMotor = 0;
		private bool aMotorInc = false;
		private bool aMotorDec = false;
		private bool dMotorInc = false;
		private bool dMotorDec = false;
		private bool reverseToggle = false;
		private bool powerSelect = false;
		private bool gearSelect = false;
		private bool powerReset = false;
		private bool anyButton = false;

		// Which aspects have *changed* since the last scan
		private bool? analogModeChange = null;
		private int? lMotorChange = null;
		private int? rMotorChange = null;
		private int? fineMotorChange = null;
		private bool? aMotorIncChange = null;
		private bool? aMotorDecChange = null;
		private bool? dMotorIncChange = null;
		private bool? dMotorDecChange = null;
		private bool? reverseToggleChange = null;
		private bool? powerSelectChange = null;
		private bool? gearSelectChange = null;
		private bool? powerResetChange = null;
		private bool? anyButtonChange = null;

		// Information from the type of joystick, informing how to read the DirectX inputs for that particular model
		private int lMotorChoice;
		private int rMotorChoice;
		private int fineMotorChoice;
		private int[] buttonChoices = new int[8];

		//
		// Constants & enumerations
		//
		public const int BUTTONCHOICE_AMOTORINC = 0;
		public const int BUTTONCHOICE_AMOTORDEC = 1;
		public const int BUTTONCHOICE_DMOTORINC = 2;
		public const int BUTTONCHOICE_DMOTORDEC = 3;
		public const int BUTTONCHOICE_REVERSETOGGLE = 4;
		public const int BUTTONCHOICE_POWERSELECT = 5;
		public const int BUTTONCHOICE_GEARSELECT = 6;
		public const int BUTTONCHOICE_POWERRESET = 7;

		private static Dictionary<string, int> xmlIdToConst = null;
		public const int STICK_XA = 0;
		public const int STICK_YA = 1;
		public const int STICK_ZA = 2;
		public const int STICK_XR = 3;
		public const int STICK_YR = 4;
		public const int STICK_ZR = 5;
		public const int STICK_S0 = 6;
		public const int STICK_S1 = 7;
		public const int STICK_P0 = 8;

		private static void setupIdToConst()
		{
			xmlIdToConst = new Dictionary<string, int>();
			xmlIdToConst.Add("XA", STICK_XA);
			xmlIdToConst.Add("YA", STICK_YA);
			xmlIdToConst.Add("ZA", STICK_ZA);
			xmlIdToConst.Add("XR", STICK_XR);
			xmlIdToConst.Add("YR", STICK_YR);
			xmlIdToConst.Add("ZR", STICK_ZR);
			xmlIdToConst.Add("S0", STICK_S0);
			xmlIdToConst.Add("S1", STICK_S1);
			xmlIdToConst.Add("P0", STICK_P0);
		}

		// Constructor of the wrapper: Takes the GUID, the SlimDX Joystick object, and the various "how to read it" data
		public JoystickWrapper(string guid, Joystick joystick, string lMotorChoiceString, string rMotorChoiceString, string fineMotorChoiceString, int[] buttonChoices)
		{
			if (xmlIdToConst == null)
				setupIdToConst();

			this.joystick = joystick;
			this.buttonChoices = buttonChoices;

			lMotorChoice = xmlIdToConst[lMotorChoiceString];
			rMotorChoice = xmlIdToConst[rMotorChoiceString];
			fineMotorChoice = xmlIdToConst[fineMotorChoiceString];

			this.guid = guid + "";
		}

		// Updates the "how to read it" data with new settings (i.e., if you change the type in the UI)
		public void resetChoices(string lMotorChoiceString, string rMotorChoiceString, string fineMotorChoiceString, int[] buttonChoices)
		{
			this.buttonChoices = buttonChoices;

			lMotorChoice = xmlIdToConst[lMotorChoiceString];
			rMotorChoice = xmlIdToConst[rMotorChoiceString];
			fineMotorChoice = xmlIdToConst[fineMotorChoiceString];
		}

		// call this for every tick
		public void readAndProcessData()
		{
			if (joystick.Acquire().IsFailure)
				return;

			if (joystick.Poll().IsFailure)
				return;

			state = joystick.GetCurrentState();
			if (Result.Last.IsFailure)
				return;

			// Interpret the data from button presses / releases
			processButtons();
			// Interpret the data from the 'variable' controls (i.e., the joysticks)
			processSticks();
		}

		//
		// Scan the buttons to read in their current state and to flag if any of those states are different from the last reading
		//
		private void processButtons()
		{
			// Ask SlimDX for the current button states
			bool[] buttons = state.GetButtons();

			// check if ~any~ button is pressed
			bool new_anyButton = false;
			for (int i = 0; i < buttons.Length; i++)
			{
				if (buttons[i])
				{
					new_anyButton = true;
					break;
				}
			}
			if (new_anyButton == anyButton)
				anyButtonChange = null;
			else
				anyButtonChange = new_anyButton;
			anyButton = new_anyButton;

			//
			// Individual buttons:
			// Using the "how to read it" configuration, check each relevant button for state & if it changed
			//
			bool new_aMotorInc = buttons[buttonChoices[BUTTONCHOICE_AMOTORINC]];
			if (new_aMotorInc == aMotorInc)
				aMotorIncChange = null;
			else
				aMotorIncChange = new_aMotorInc;
			aMotorInc = new_aMotorInc;

			bool new_aMotorDec = buttons[buttonChoices[BUTTONCHOICE_AMOTORDEC]];
			if (new_aMotorDec == aMotorDec)
				aMotorDecChange = null;
			else
				aMotorDecChange = new_aMotorDec;
			aMotorDec = new_aMotorDec;

			bool new_dMotorInc = buttons[buttonChoices[BUTTONCHOICE_DMOTORINC]];
			if (new_dMotorInc == dMotorInc)
				dMotorIncChange = null;
			else
				dMotorIncChange = new_dMotorInc;
			dMotorInc = new_dMotorInc;

			bool new_dMotorDec = buttons[buttonChoices[BUTTONCHOICE_DMOTORDEC]];
			if (new_dMotorDec == dMotorDec)
				dMotorDecChange = null;
			else
				dMotorDecChange = new_dMotorDec;
			dMotorDec = new_dMotorDec;

			bool new_reverseToggle = buttons[buttonChoices[BUTTONCHOICE_REVERSETOGGLE]];
			if (new_reverseToggle == reverseToggle)
				reverseToggleChange = null;
			else
				reverseToggleChange = new_reverseToggle;
			reverseToggle = new_reverseToggle;

			bool new_powerSelect = buttons[buttonChoices[BUTTONCHOICE_POWERSELECT]];
			if (new_powerSelect == powerSelect)
				powerSelectChange = null;
			else
				powerSelectChange = new_powerSelect;
			powerSelect = new_powerSelect;

			bool new_gearSelect = buttons[buttonChoices[BUTTONCHOICE_GEARSELECT]];
			if (new_gearSelect == gearSelect)
				gearSelectChange = null;
			else
				gearSelectChange = new_gearSelect;
			gearSelect = new_gearSelect;

			bool new_powerReset = buttons[buttonChoices[BUTTONCHOICE_POWERRESET]];
			if (new_powerReset == powerReset)
				powerResetChange = null;
			else
				powerResetChange = new_powerReset;
			powerReset = new_powerReset;
		}

		//
		// Check "stick" states, and determine what (if anything) has changed
		//
		private void processSticks()
		{
			int new_fineMotor = getAnalogValue(fineMotorChoice);
			if (new_fineMotor == fineMotor)
				fineMotorChange = null;
			else
				fineMotorChange = new_fineMotor;
			fineMotor = new_fineMotor;

			int new_lMotor = getAnalogValue(lMotorChoice);
			if (new_lMotor == lMotor)
				lMotorChange = null;
			else
				lMotorChange = new_lMotor;
			lMotor = new_lMotor;

			int new_rMotor = getAnalogValue(rMotorChoice);
			if (new_rMotor == rMotor)
				rMotorChange = null;
			else
				rMotorChange = new_rMotor;
			rMotor = new_rMotor;

			// special case for non-analog mode
			bool new_analogMode = true;
			if (lMotor == -1 || rMotor == -1)
				new_analogMode = false;
			if (new_analogMode == analogMode)
				analogModeChange = null;
			else
				analogModeChange = new_analogMode;
			analogMode = new_analogMode;
		}

		//
		// Given a type of "stick" / slider, read in the value for that particular input
		// X, Y, Z; "Rotation" values for X, Y, Z; Sliders 0, 1; Potentiometer 0
		//
		private int getAnalogValue(int whichStickValue)
		{
			int[] slider = state.GetSliders();
			int[] pov = state.GetPointOfViewControllers();

			switch (whichStickValue)
			{
				case STICK_P0:
					return pov[0];
				case STICK_S0:
					return slider[0];
				case STICK_S1:
					return slider[1];
				case STICK_XA:
					return state.X;
				case STICK_YA:
					return state.Y;
				case STICK_ZA:
					return state.Z;
				case STICK_XR:
					return state.RotationX;
				case STICK_YR:
					return state.RotationY;
				case STICK_ZR:
					return state.RotationZ;
				default:
					return 0;
			}
		}

		//
		// Special case: If the "analog mode" is off, the sticks act like D-Pads? when not in use, return '-1' for both
		//
		public bool isAnalogMode()
		{
			if (lMotor == -1 || rMotor == -1)
				return false;
			return true;
		}

		/***************************************/

		public string getGuid() { return guid; }

		public bool? getAnalogModeChange() { return analogModeChange; }
		public int? getLMotorChange() { return lMotorChange; }
		public int? getRMotorChange() { return rMotorChange; }
		public int? getFineMotorChange() { return fineMotorChange; }
		public bool? getAMotorIncChange() { return aMotorIncChange; }
		public bool? getAMotorDecChange() { return aMotorDecChange; }
		public bool? getDMotorIncChange() { return dMotorIncChange; }
		public bool? getDMotorDecChange() { return dMotorDecChange; }
		public bool? getReverseToggleChange() { return reverseToggleChange; }
		public bool? getPowerSelectChange() { return powerSelectChange; }
		public bool? getGearSelectChange() { return gearSelectChange; }
		public bool? getPowerResetChange() { return powerResetChange; }
		public bool? getAnyButtonChange() { return anyButtonChange; }

		public bool getAMotorInc() { return aMotorInc; }
		public bool getAMotorDec() { return aMotorDec; }
		public bool getDMotorInc() { return dMotorInc; }
		public bool getDMotorDec() { return dMotorDec; }
	}
}
