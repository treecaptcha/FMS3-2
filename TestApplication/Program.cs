using System;
using System.Collections.Generic;
using System.Xml;
//using MonoBrick.EV3;//use this to run the example on the EV3
//using MonoBrick.NXT;//use this to run the example on the NXT  
namespace FMS3
{
	public static class Program
	{
		public static ConsoleKeyInfo cki;
		private static BrickManager brickManager;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			brickManager = BrickManager.getInstance();

			using (XmlWriter writer = XmlWriter.Create("test.xml"))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("JoystickConfig");
				writer.WriteStartElement("JoystickConfig2");

				//writer.WriteElementString("elemLocalName", "elemValue");
				writer.WriteAttributeString("attrLocalName", "attrValue");

				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndDocument();
			}

			do
			{
				Console.WriteLine("*** MAIN MENU: Refresh list, Connect brick, Test brick, Quit");

				cki = Console.ReadKey(true); //press a key  
				switch (cki.Key)
				{
					case ConsoleKey.R:
						refreshBrickList();
						break;
					case ConsoleKey.C:
						connectToABrick();
						break;
					case ConsoleKey.T:
						testBrickWithConsole();
						break;
				}
			} while (cki.Key != ConsoleKey.Q);
		}

		/// <summary>
		/// 
		/// </summary>
		private static void refreshBrickList()
		{
			Console.WriteLine("-- List of known bricks:");
			List<string> brickList = brickManager.getListOfAttachedBricks();
			foreach (string brick in brickList)
				Console.WriteLine("*" + brick + "*");
		}

		/// <summary>
		/// 
		/// </summary>
		private static void connectToABrick()
		{
			Console.WriteLine("-- Choose a brick: # to choose, or Back");
			List<string> brickList = brickManager.getListOfAttachedBricks();
			int listSize = brickList.Count;
			for (int i = 0; i < listSize; i++)
			{
				string thisBrick = brickList[i];
				if (!thisBrick.Contains("["))
					Console.WriteLine(i + ": " + thisBrick);
			}

			cki = Console.ReadKey(true); //press a key  
			bool bNumberHit = false;
			int hitNumber = -1;
			switch (cki.Key)
			{
				case ConsoleKey.D0:
					bNumberHit = true;
					hitNumber = 0;
					break;
				case ConsoleKey.D1:
					bNumberHit = true;
					hitNumber = 1;
					break;
				case ConsoleKey.D2:
					bNumberHit = true;
					hitNumber = 2;
					break;
				case ConsoleKey.D3:
					bNumberHit = true;
					hitNumber = 3;
					break;
				case ConsoleKey.D4:
					bNumberHit = true;
					hitNumber = 4;
					break;
				case ConsoleKey.D5:
					bNumberHit = true;
					hitNumber = 5;
					break;
				case ConsoleKey.D6:
					bNumberHit = true;
					hitNumber = 6;
					break;
				case ConsoleKey.D7:
					bNumberHit = true;
					hitNumber = 7;
					break;
				case ConsoleKey.D8:
					bNumberHit = true;
					hitNumber = 8;
					break;
				case ConsoleKey.D9:
					bNumberHit = true;
					hitNumber = 9;
					break;
			}

			if (bNumberHit)
			{
				string selectedBrick = brickList[hitNumber];
				if (!selectedBrick.Contains("["))
				{
					bool isEv3 = true;
					Console.WriteLine("...Is this an EV3? Yes, No");
					cki = Console.ReadKey(true); //press a key  
					switch (cki.Key)
					{
						case ConsoleKey.N:
							isEv3 = false;
							break;
					}
					GenericBrick newBrick = brickManager.getBrickByName(selectedBrick, isEv3);
					if (newBrick != null)
						Console.WriteLine("~ Connection status = " + newBrick.getState());
				}
			}
		}

		public static void testBrickWithConsole()
		{
			Console.WriteLine("-- Choose a brick: # to choose, or Back");
			List<string> brickList = brickManager.getListOfAttachedBricks();
			int listSize = brickList.Count;
			for (int i = 0; i < listSize; i++)
			{
				string thisBrick = brickList[i];
				if (thisBrick.Contains("["))
					Console.WriteLine(i + ": " + thisBrick);
			}

			cki = Console.ReadKey(true); //press a key  
			bool bNumberHit = false;
			int hitNumber = -1;
			switch (cki.Key)
			{
				case ConsoleKey.D0:
					bNumberHit = true;
					hitNumber = 0;
					break;
				case ConsoleKey.D1:
					bNumberHit = true;
					hitNumber = 1;
					break;
				case ConsoleKey.D2:
					bNumberHit = true;
					hitNumber = 2;
					break;
				case ConsoleKey.D3:
					bNumberHit = true;
					hitNumber = 3;
					break;
				case ConsoleKey.D4:
					bNumberHit = true;
					hitNumber = 4;
					break;
				case ConsoleKey.D5:
					bNumberHit = true;
					hitNumber = 5;
					break;
				case ConsoleKey.D6:
					bNumberHit = true;
					hitNumber = 6;
					break;
				case ConsoleKey.D7:
					bNumberHit = true;
					hitNumber = 7;
					break;
				case ConsoleKey.D8:
					bNumberHit = true;
					hitNumber = 8;
					break;
				case ConsoleKey.D9:
					bNumberHit = true;
					hitNumber = 9;
					break;
			}

			string selectedBrick = brickList[hitNumber];
			if (bNumberHit && selectedBrick.Contains("["))
			{
				string selectedBrickName = selectedBrick.Split(' ')[0];
				GenericBrick genBrick = brickManager.getBrickByName(selectedBrickName);

				sbyte speed = 0;
				Console.WriteLine("~~~ ^ up, v down, Stop motors, Halt programs, Back");
				do
				{
					int state = GenericBrick.STATE_NEW;

					cki = Console.ReadKey(true); //press a key  
					switch (cki.Key)
					{
						case ConsoleKey.UpArrow:
							if (speed < 100)
								speed = (sbyte)(speed + 10);
							state = genBrick.setMotor(GenericBrick.MOTOR_A, speed);
							Console.WriteLine("Motor A speed set to " + speed + " [" + state + "]");
							break;
						case ConsoleKey.DownArrow:
							if (speed > -100)
								speed = (sbyte)(speed - 10);
							state = genBrick.setMotor(GenericBrick.MOTOR_A, speed);
							Console.WriteLine("Motor A speed set to " + speed + " [" + state + "]");
							break;
						case ConsoleKey.S:
							state = genBrick.stopAllMotors();
							speed = 0;
							Console.WriteLine("Stop all motors!" + " [" + state + "]");
							break;
						case ConsoleKey.H:
							state = genBrick.stopAllPrograms();
							Console.WriteLine("Stop all programs!" + " [" + state + "]");
							break;
					}
				} while (cki.Key != ConsoleKey.B);
			}

			/*
			GenericBrick genBrick = new GenericBrick("username", "com8", false);
			int newState = genBrick.getState();
			Console.WriteLine("State: " + genBrick.getState());
			if (newState == 0)
			{
				cki = Console.ReadKey(true); //press a key  
				genBrick = new GenericBrick("username", "com8", false);
				Console.WriteLine("State: " + genBrick.getState());
			}
			 */
			//GenericBrick genBrick = new GenericBrick("DvorakII", "com6");
			//GenericBrick genBrick = new GenericBrick("username", "com8", false);
			//GenericBrick genBrick = new GenericBrick("username", "com8");
		}
	}
}
