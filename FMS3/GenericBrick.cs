using System;

namespace FMS3
{
	/*
	 * Wrapper class for two different MonoBrick "Brick" classes - EV3.Brick and NXT.Brick
	 * Keeps track of state
	 */
	class GenericBrick
	{
		// Represent brick 'states' whether connected as NXT or EV3, unconnected, once-connected-but-now-missing, etc.

		// States <=0 are used to denote "invalid" (not connected) bricks.

		// "LOST" (used when a previously good connection has gone bad, and an exception was caught)
		public const int STATE_BROKEN = -2;
		// "new" (initialization state)
		public const int STATE_NEW = -1;
		// "none" (used to be used for a particular broken state)
		public const int STATE_NOTFOUND = 0;

		// States >0 are used to denote "valid" (connected) bricks.

		// "EV3" - brick connected as EV3
		public const int STATE_EV3 = 1;
		// "NXT" - brick connected as NXT
		public const int STATE_NXT = 2;

		public const int OFFSET_STATE_NAMES = -2;
		public static string[] STATE_NAMES = new string[] {
			"LOST", "new", "none", "EV3", "NXT"
		};

		// For tracking the motors
		public const int MOTOR_A = 0;
		public const int MOTOR_B = 1;
		public const int MOTOR_C = 2;
		public const int MOTOR_D = 3;

		// Bricks initialize as 'new'
		private int state = STATE_NEW;
		private string brickName;
		private string connName;

		// Keep a private copy of each of the kinds of MonoBrick bricks
		private MonoBrick.EV3.Brick<MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor> ev3Brick;
		private MonoBrick.NXT.Brick<MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor> nxtBrick;

		//
		// Constructor: name of brick, COM port to use, and whether this is an EV3 or not
		// (MonoBrick cannot auto-detect whether or not a brick is EV3)
		//
		public GenericBrick(string newBrickName, string newConnName, bool isEv3)
		{
			brickName = newBrickName + "";
			connName = newConnName + "";

			connectToBrick(isEv3, true);
		}

		// Attempt to connect to this brick; optionally, play the connection tones to represent a successful connection
		private void connectToBrick(bool isEv3, bool playConnectTones)
		{
			// Hang on to the prior state
			int oldState = state;
			// Reset this to 'NEW'
			state = STATE_NEW;
			// Start off with the brick "not found"; we'll see if we can find it
			bool notFound = true;

			// is it an EV3?
			if (isEv3)
			{
				// Attempt to instantiate the EV3 Brick class, open the connection, and perform some actions [i.e., play tones]
				try
				{
					ev3Brick = new MonoBrick.EV3.Brick<MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor>(connName);
					Console.WriteLine("DEBUG: got an EV3 brick");
					ev3Brick.Connection.Open();
					Console.WriteLine("DEBUG: opened EV3 brick");

					if (playConnectTones)
					{
						ev3Brick.PlayTone(100, 440, 500);
						System.Threading.Thread.Sleep(200);
						ev3Brick.PlayTone(100, 550, 500);
						System.Threading.Thread.Sleep(200);
						ev3Brick.PlayTone(100, 660, 500);
						System.Threading.Thread.Sleep(200);
						ev3Brick.PlayTone(100, 770, 500);
						System.Threading.Thread.Sleep(200);
						ev3Brick.PlayTone(100, 880, 400);
					}

					state = STATE_EV3;
					notFound = false;
				}
				catch (Exception e)
				{
					Console.WriteLine("DEBUG: brick=" + brickName + ",conn=" + connName + " - not an EV3");
				}
			}
			// it's an NXT
			else
			{
				// Attempt to instantiate the NXT Brick class, open the connection, and perform some actions [i.e., play tones]
				try
				{
					nxtBrick = new MonoBrick.NXT.Brick<MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor>(connName);
					Console.WriteLine("DEBUG: got an NXT brick");
					nxtBrick.Connection.Open();
					Console.WriteLine("DEBUG: opened NXT brick");

					if (playConnectTones)
					{
						nxtBrick.PlayTone(440, 500);
						System.Threading.Thread.Sleep(200);
						nxtBrick.PlayTone(550, 500);
						System.Threading.Thread.Sleep(200);
						nxtBrick.PlayTone(660, 500);
						System.Threading.Thread.Sleep(200);
						nxtBrick.PlayTone(770, 500);
						System.Threading.Thread.Sleep(200);
						nxtBrick.PlayTone(880, 400);
					}

					string name = nxtBrick.GetBrickName();
					Console.WriteLine("DEBUG: NXT name='" + name + "'");

					state = STATE_NXT;
					notFound = false;
				}
				catch (Exception e)
				{
					Console.WriteLine("DEBUG: brick=" + brickName + ",conn=" + connName + " - not an NXT");
				}
			}

			// Successful connection would have reset this to false (found). If still not found, something went wrong.
			if (notFound)
			{
				Console.WriteLine("DEBUG: GenericBrick.connectToBrick(): brick=" + brickName + " not found");
				// "none"
				state = STATE_NOTFOUND;
			}

			// special case for unsuccessful reconnection - if was LOST, keep being LOST
			if (state == STATE_NOTFOUND && oldState == STATE_BROKEN)
			{
				Console.WriteLine("DEBUG: GenericBrick.connectToBrick(): brick=" + brickName + " was unable to reconnect");
				state = STATE_BROKEN;
			}
		}

		//
		// Static helper method: Given a state constant, return the name. Use the 'offset' because the state constants start below zero (0)
		//
		public static string getStateName(int state)
		{
			return STATE_NAMES[state - OFFSET_STATE_NAMES];
		}

		/*******************************************************************/

		//
		// Various getters
		//
		public int getState()
		{
			return state;
		}
		public string getStateName()
		{
			return GenericBrick.getStateName(state);
			//return STATE_NAMES[state - OFFSET_STATE_NAMES];
		}
		public string getBrickName()
		{
			return brickName;
		}
		public string getConnName()
		{
			return connName;
		}

		/*
		 * For the "actions" below, only do the action if it's connected as an EV3 or NXT.
		 * In each case, if connected, use the appropriate private Brick object (MonoBrick.EV3.Brick or NXT)
		 * Use try/catch, and set brick state to BROKEN ("LOST") if an action fails.
		 * Why might it fail? -- Battery could have died, it was powered off, taken out of range, etc.
		 */

		//
		// Make the brick beep.
		//
		public int beep(ushort durationMs)
		{
			switch (state)
			{
				case STATE_EV3:
					try
					{
						ev3Brick.Beep(100, durationMs);
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;

				case STATE_NXT:
					try
					{
						nxtBrick.Beep(durationMs);
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;
			}

			return state;
		}

		//
		// Make the brick play a particular tone.
		//
		public int playTone(ushort frequency, ushort durationMs)
		{
			switch (state)
			{
				case STATE_EV3:
					try
					{
						ev3Brick.PlayTone(100, frequency, durationMs);
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;

				case STATE_NXT:
					try
					{
						nxtBrick.PlayTone(frequency, durationMs);
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;
			}

			return state;
		}

		//
		// Stop all the motors. For EV3, include stopping the 'D' motor.
		//
		public int stopAllMotors()
		{
			switch (state)
			{
				case STATE_EV3:
					try
					{
						ev3Brick.MotorA.Off();
						ev3Brick.MotorB.Off();
						ev3Brick.MotorC.Off();
						ev3Brick.MotorD.Off();
						/*
						ev3Brick.MotorA.On(0);
						ev3Brick.MotorB.On(0);
						ev3Brick.MotorC.On(0);
						ev3Brick.MotorD.On(0);
						 */
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;

				case STATE_NXT:
					try
					{
						nxtBrick.MotorA.Off();
						nxtBrick.MotorB.Off();
						nxtBrick.MotorC.Off();
						/*
						nxtBrick.MotorA.On(0);
						nxtBrick.MotorB.On(0);
						nxtBrick.MotorC.On(0);
						 */
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;
			}

			return state;
		}

		//
		// Stop all programs.
		//
		public int stopAllPrograms()
		{
			switch (state)
			{
				case STATE_EV3:
					try
					{
						ev3Brick.StopProgram();
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;

				case STATE_NXT:
					try
					{
						nxtBrick.StopProgram();
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;
			}

			return state;
		}

		//
		// Set a specific motor to a particular speed. Automatically restricts speed to -100 to 100.
		//
		public int setMotor(int whichMotor, int speed)
		{
			int newSpeed = speed;
			if (newSpeed > 100)
				newSpeed = 100;
			if (newSpeed < -100)
				newSpeed = -100;

			switch (state)
			{
				case STATE_EV3:
					setEv3Motor(whichMotor, newSpeed);
					break;

				case STATE_NXT:
					setNxtMotor(whichMotor, newSpeed);
					break;
			}

			return state;
		}

		//
		// Set a specific motor to a speed.
		//
		private int setEv3Motor(int whichMotor, int speed)
		{
			//if (speed == 0)
			//  return zeroEv3Motor(whichMotor);

			try
			{
				switch (whichMotor)
				{
					case MOTOR_A:
						ev3Brick.MotorA.On((sbyte)speed);
						break;
					case MOTOR_B:
						ev3Brick.MotorB.On((sbyte)speed);
						break;
					case MOTOR_C:
						ev3Brick.MotorC.On((sbyte)speed);
						break;
					case MOTOR_D:
						ev3Brick.MotorD.On((sbyte)speed);
						break;
				}
			}
			catch (Exception e)
			{
				state = STATE_BROKEN;
			}

			return state;
		}
		private int setNxtMotor(int whichMotor, int speed)
		{
			//if (speed == 0)
			//  return zeroNxtMotor(whichMotor);

			try
			{
				switch (whichMotor)
				{
					case MOTOR_A:
						nxtBrick.MotorA.On((sbyte)speed);
						break;
					case MOTOR_B:
						nxtBrick.MotorB.On((sbyte)speed);
						break;
					case MOTOR_C:
						nxtBrick.MotorC.On((sbyte)speed);
						break;
						// Special: NXT does not have a 'D' motor [todo: send message to 'A' motor instead?]
						//case MOTOR_D:
						//nxtBrick.MotorA.On((sbyte)speed);
						//break;
				}
			}
			catch (Exception e)
			{
				state = STATE_BROKEN;
			}

			return state;
		}

		//
		// When a brick is "LOST", we can try to reconnect to it; but if so, we need to close the old Connection
		//
		public int resetBrokenState()
		{
			if (state == STATE_BROKEN)
			{
				if (ev3Brick != null)
				{
					ev3Brick.Connection.Close();
					connectToBrick(true, true);
					//state = STATE_EV3;
				}
				else
				{
					nxtBrick.Connection.Close();
					connectToBrick(false, true);
					//state = STATE_NXT;
				}
			}

			return state;
		}
	}
}
