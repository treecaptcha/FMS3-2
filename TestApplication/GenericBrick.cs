using System;

namespace FMS3
{
	class GenericBrick
	{
		public const int STATE_BROKEN = -2;
		public const int STATE_NEW = -1;
		public const int STATE_NOTFOUND = 0;
		public const int STATE_EV3 = 1;
		public const int STATE_NXT = 2;

		public static string[] STATE_NAMES = new string[] {
			"LOST", "new", "none", "EV3", "NXT"
		};

		public const int MOTOR_A = 0;
		public const int MOTOR_B = 1;
		public const int MOTOR_C = 2;
		public const int MOTOR_D = 3;

		private int state = STATE_NEW;
		private string brickName;
		private string connName;

		private MonoBrick.EV3.Brick<MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor> ev3Brick;
		private MonoBrick.NXT.Brick<MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor> nxtBrick;

		public GenericBrick(string newBrickName, string newConnName, bool isEv3)
		{
			brickName = newBrickName + "";
			connName = newConnName + "";

			state = STATE_NEW;
			bool notFound = true;

			// is it an EV3?
			if (isEv3)
			{
				try
				{
					ev3Brick = new MonoBrick.EV3.Brick<MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor>(connName);
					Console.WriteLine("DEBUG: got an EV3 brick");
					ev3Brick.Connection.Open();
					Console.WriteLine("DEBUG: opened EV3 brick");

					ev3Brick.PlayTone(100, 440, 500);
					System.Threading.Thread.Sleep(200);
					ev3Brick.PlayTone(100, 550, 500);
					System.Threading.Thread.Sleep(200);
					ev3Brick.PlayTone(100, 660, 500);
					System.Threading.Thread.Sleep(200);
					ev3Brick.PlayTone(100, 770, 500);
					System.Threading.Thread.Sleep(200);
					ev3Brick.PlayTone(100, 880, 400);

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
				try
				{
					nxtBrick = new MonoBrick.NXT.Brick<MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor>(connName);
					Console.WriteLine("DEBUG: got an NXT brick");
					nxtBrick.Connection.Open();
					Console.WriteLine("DEBUG: opened NXT brick");

					nxtBrick.PlayTone(440, 500);
					System.Threading.Thread.Sleep(200);
					nxtBrick.PlayTone(550, 500);
					System.Threading.Thread.Sleep(200);
					nxtBrick.PlayTone(660, 500);
					System.Threading.Thread.Sleep(200);
					nxtBrick.PlayTone(770, 500);
					System.Threading.Thread.Sleep(200);
					nxtBrick.PlayTone(880, 400);

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

			if (notFound)
			{
				state = STATE_NOTFOUND;
			}
		}

		public GenericBrick(string newBrickName, string newConnName)
		{
			brickName = newBrickName + "";
			connName = newConnName + "";

			state = STATE_NEW;
			bool notFound = true;

			// see if it's an NXT
			if (notFound)
			{
				try
				{
					nxtBrick = new MonoBrick.NXT.Brick<MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor, MonoBrick.NXT.Sensor>(connName);
					Console.WriteLine("DEBUG: got an NXT brick");
					nxtBrick.Connection.Open();
					Console.WriteLine("DEBUG: opened NXT brick");
					nxtBrick.Beep(250);

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

			// see if it's an EV3
			if (notFound)
			{
				try
				{
					ev3Brick = new MonoBrick.EV3.Brick<MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor, MonoBrick.EV3.Sensor>(connName);
					Console.WriteLine("DEBUG: got an EV3 brick");
					ev3Brick.Connection.Open();
					Console.WriteLine("DEBUG: opened EV3 brick");
					ev3Brick.Beep(100, 250);

					state = STATE_EV3;
					notFound = false;
				}
				catch (Exception e)
				{
					Console.WriteLine("DEBUG: brick=" + brickName + ",conn=" + connName + " - not an EV3");
				}
			}

			if (notFound)
			{
				state = STATE_NOTFOUND;
			}
		}

		/*******************************************************************/

		public int getState()
		{
			return state;
		}

		public string getBrickName()
		{
			return brickName;
		}

		public string getConnName()
		{
			return connName;
		}

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
					}
					catch
					{
						state = STATE_BROKEN;
					}
					break;
			}

			return state;
		}

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

		private int setEv3Motor(int whichMotor, int speed)
		{
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
					// Special: NXT does not have a 'D' motor, send message to 'A' motor instead
					case MOTOR_D:
						nxtBrick.MotorA.On((sbyte)speed);
						break;
				}
			}
			catch (Exception e)
			{
				state = STATE_BROKEN;
			}

			return state;
		}
	}
}
