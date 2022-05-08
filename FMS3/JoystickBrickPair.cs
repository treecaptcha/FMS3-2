namespace FMS3
{
	/*
	 * Class for handling the pairing of a joystick with a brick.
	 */
	class JoystickBrickPair
	{
		// When the action motor power levels are cycled, these are the values to use
		public static readonly int[] POWER_LEVELS = { 5, 10, 25, 50, 100 };
		public const int POWER_LEVEL_MAX_INDEX = 4;
		// When the drive motor power levels are cycled, these are the divisor values to use
		// '4' means max 25% (100/4), '2' means max 50% (100/2), and '1' means max 100%, full power
		public static readonly int[] GEAR_LEVELS = { 4, 2, 1 };
		public const int GEAR_LEVEL_MAX_INDEX = 2;

		// When the D-pad is used to drive the robot, for each of the D-pad cardinal directions, what does that mean for motor power?
		// index 0 is no direction - 0 to left motor, 0 to right motor
		// index 1 is straight ahead ("North") - 1 to both motors
		// index 2 is 45 degrees clockwise ("Northeast") - 1 to left motor only
		// etc.
		public static readonly int[] FINE_LMOTOR = { 0, 1, 1, 1, -1, -1, 0, -1, 0 };
		public static readonly int[] FINE_RMOTOR = { 0, 1, 0, -1, 0, -1, -1, 1, 1 };
		// Power level when using the D-pad
		public static readonly int FINE_POWER = 10;

		// GUID of the joystick in this pairing
		public string guid { get; set; }
		// Wrapper object for the joystick
		public JoystickWrapper joystick { get; set; }
		// "Name" of this pairing (defaults in the UI to '1', '2', '3', '4')
		public string alias { get; set; }
		// Wrapper object for the brick in this pairing
		public GenericBrick brick { get; set; }

		// State values for this particular pairing:
		// Whether to the motors are currently flipped
		public bool reverseMotors { get; set; }
		// What is the power level for the action motors ('A' and 'D')
		public int powerIndex { get; set; }
		// What is the gear level for the drive motors ('B' and 'C')
		public int gearIndex { get; set; }

		// What are the motors currently set at?
		public int lMotorValue { get; set; }
		public int rMotorValue { get; set; }

		//
		// Constructor; starts with the default values
		//
		public JoystickBrickPair()
		{
			resetDefaults();
		}

		//
		// Set up the (or reset to) default values
		//
		public void resetDefaults()
		{
			// Brick is not "flipped"
			reverseMotors = false;

			// Power is full for action & driving
			powerIndex = POWER_LEVEL_MAX_INDEX;
			gearIndex = GEAR_LEVEL_MAX_INDEX;

			// Motors aren't running
			lMotorValue = 0;
			rMotorValue = 0;
		}

		//
		// Pair is valid if (a) there is a joystick associated, and (b) there is a brick associated AND it's connected
		//
		public bool isValidPair()
		{
			/*
			Console.WriteLine("DEBUG: isValidPair()1 - " + joystick + "," + brick);
			if (brick != null)
				Console.WriteLine("DEBUG: isValidPair()2 - " + brick.getState());
			 */
			if (joystick != null && brick != null && brick.getState() > 0)
				return true;
			return false;
		}

		//
		// Helper method for debug output
		//
		public string toString()
		{
			string retVal = "";

			if (joystick == null)
				retVal = retVal + "joystick=null";
			else
				retVal = retVal + "joystick=" + joystick.getGuid();

			if (brick == null)
				retVal = retVal + "|brick=null";
			else
				retVal = retVal + "|brick=" + brick.getBrickName();

			return retVal;
		}
	}
}
