namespace FMS3
{
	/*
	 * Used when defining how a particular type of joystick is to be read in from DirectX
	 * Each manufacturer associates aspects of their sticks to different DirectX inputs
	 * We read data in from XML and, by way of this bean, use that to read the correct inputs for each type of joystick
	 */
	class JoystickConfigBean
	{
		// 'EtekCity', 'Saitek', 'Snakebyte', etc.
		public string typeName = "undefType";
		// "Stick" data to read in - will be YA, ZR, P0, etc.
		public string lMotorChoiceString = "undefLMotor";
		public string rMotorChoiceString = "undefRMotor";
		public string fineMotorChoiceString = "undefFineMotor";
		// An array showing which button for which function - ints could be 0, 6, 1, and so forth.
		// Represent which bit position in a particuarl DirectX input; shows pressed / not pressed state for all buttons at once
		public int[] buttonChoices = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };

		public string toString()
		{
			return "typeName=" + typeName
				+ ",lMotorChoice=" + lMotorChoiceString
				+ ",rMotorChoice=" + rMotorChoiceString
				+ ",fineMotorChoice=" + fineMotorChoiceString
				+ "|AINC=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_AMOTORINC]
				+ ",ADEC=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_AMOTORDEC]
				+ ",DINC=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_DMOTORINC]
				+ ",DDEC=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_DMOTORDEC]
				+ ",REV=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_REVERSETOGGLE]
				+ ",POW=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_POWERSELECT]
				+ ",GEA=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_GEARSELECT]
				+ ",RES=" + buttonChoices[JoystickWrapper.BUTTONCHOICE_POWERRESET];
		}
	}
}
