using SlimDX.DirectInput;

namespace FMS3
{
	class JoystickManager
	{
		private static JoystickManager instance;

		private Joystick joystick;
		private JoystickState state = new JoystickState();
		private int numPOVs;
		private int SliderCount;

		private JoystickManager()
		{
			// DNGN
		}

		private static JoystickManager getInstance()
		{
			if (instance == null)
				instance = new JoystickManager();
			return instance;
		}


	}
}
