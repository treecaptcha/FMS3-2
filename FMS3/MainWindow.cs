using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Xml;

namespace FMS3
{
	/*
	 * 
	 */
	public partial class MainWindow : Form
	{
		// Random number generation [e.g., picking test tones to verify robot connectivity]
		Random rand = new Random();

		// Small dialog box which is displayed while the program is testing out the connectivity of all attached bricks
		Form updateForm = new Form();

		// Initialize various TimeSpan variables with bogus defaults (these get overridden when the program reads in the initial XML)
		private TimeSpan TIMESPAN_ONE_SECOND = new TimeSpan(0, 0, 1);
		private TimeSpan autonomousLength = new TimeSpan(0, 0, 1);
		private TimeSpan teleoperatedLength = new TimeSpan(0, 0, 1);
		// Used to calculate the new end timestamp after the period was paused and then resumed
		private TimeSpan resumeLength;
		// The target timestamp for the end of the period; at any given moment, the display shows 'endTime' minus 'now'
		private DateTime endTime;

		// File names of the sounds to be loaded & played for different events (the actual names are read in from the XML)
		private string soundStart;
		private string soundPause;
		private string soundEnd;
		private string soundAbort;

		// Time announcement items
		private bool useTimeAnnounce = true;
		List<string> announceSounds = new List<string>();
		List<bool> didAnnounceSound = new List<bool>();

		private string seconds120 = "120seconds.wav";
		private string seconds60 = "60seconds.wav";
		private string seconds30 = "30seconds.wav";
		private string seconds10 = "10seconds.wav";
		private string seconds5 = "5seconds.wav";
		private string seconds3 = "3seconds.wav";

		private bool did120sec = false;
		private bool did60sec = false;
		private bool did30sec = false;
		private bool did10sec = false;
		private bool did5sec = false;
		private bool did3sec = false;

		// Child window which can be shown or hidden; original intent is to put on a 2nd display & maximized for audience view
		private BigTimer bigTimer = new BigTimer();

		// While setting up a match, can have button presses on controllers cause the associated brick to beep.
		// Good for making sure a controller is actually mapped to the right brick, verifying that the bricks are connected, etc.
		// Not so good when folks are just button mashing and making a cacaphony. So you can turn that off. This tracks whether it's on or off.
		private bool bBeepWhenButtonPressed = false;

		// *All* the bricks which show up in the Bluetooth list
		// (whether or not currently Bluetooth available, whether or not connected by Monobrick)
		List<string> allBrickList = new List<string>();
		// Subset of bricks which have been successfully connected to by the Monobrick library
		List<string> connBrickList = new List<string>();

		// Sets of connected bricks assigned to recognized joysticks
		List<JoystickBrickPair> brickPairs = new List<JoystickBrickPair>();

		// Tracks the current mode
		private int mode = MODE_NOMATCH;
		// The various potential modes
		// TODO: Potentially make the states objects (ala Command pattern)?
		public const int MODE_NOMATCH = 0;
		public const int MODE_FREEDRIVE = 1;
		public const int MODE_AUTONOMOUS = 2;
		public const int MODE_INTERPERIOD = 3;
		public const int MODE_TELEOPERATED = 4;
		public const int MODE_PAUSED = 5;
		public const int MODE_ABORTSPECIAL = 6;
		public static string[] MODE_NAMES = new string[] {
			"NO MATCH", "FREEDRIVE", "AUTONOMOUS", "INTERPERIOD", "TELEOPERATION", "PAUSED"
		};
		public static Color[] MODE_COLORS = new Color[] {
			Color.Coral, Color.LimeGreen, Color.Turquoise, Color.Yellow, Color.PaleGreen, Color.MediumOrchid
		};

		// Caclulate individual 'ids' for each kind of transition
		// Used to determine what needs to change during each type of transition
		public const int TRANSITION_NOMATCH_TO_FREEDRIVE = MODE_NOMATCH * 100 + MODE_FREEDRIVE;
		public const int TRANSITION_NOMATCH_TO_AUTONOMOUS = MODE_NOMATCH * 100 + MODE_AUTONOMOUS;
		public const int TRANSITION_NOMATCH_TO_TELEOPERATED = MODE_NOMATCH * 100 + MODE_TELEOPERATED;
		public const int TRANSITION_FREEDRIVE_TO_NOMATCH = MODE_FREEDRIVE * 100 + MODE_NOMATCH;
		public const int TRANSITION_AUTONOMOUS_TO_NOMATCH = MODE_AUTONOMOUS * 100 + MODE_NOMATCH;
		public const int TRANSITION_AUTONOMOUS_TO_INTERPERIOD = MODE_AUTONOMOUS * 100 + MODE_INTERPERIOD;
		public const int TRANSITION_AUTONOMOUS_TO_TELEOPERATED = MODE_AUTONOMOUS * 100 + MODE_TELEOPERATED;
		public const int TRANSITION_INTERPERIOD_TO_NOMATCH = MODE_INTERPERIOD * 100 + MODE_NOMATCH;
		public const int TRANSITION_INTERPERIOD_TO_TELEOPERATED = MODE_INTERPERIOD * 100 + MODE_TELEOPERATED;
		public const int TRANSITION_TELEOPERATED_TO_NOMATCH = MODE_TELEOPERATED * 100 + MODE_NOMATCH;
		public const int TRANSITION_TELEOPERATED_TO_PAUSED = MODE_TELEOPERATED * 100 + MODE_PAUSED;
		public const int TRANSITION_PAUSED_TO_NOMATCH = MODE_PAUSED * 100 + MODE_NOMATCH;
		public const int TRANSITION_PAUSED_TO_TELEOPERATED = MODE_PAUSED * 100 + MODE_TELEOPERATED;

		public MainWindow()
		{
			//
			// Initialize the main app! Set defaults, populate lists, load sounds, etc., etc.
			//

			// Initialize the brick list update dialog box
			updateForm.Text = "Updating...";
			updateForm.ShowIcon = false;
			updateForm.ControlBox = false;
			updateForm.Height = 100;

			//// C-sharp built-in?
			InitializeComponent();

			// Read in the initial settings from an XML file
			// Whether various checkboxes start off checked, the initial amount of time for the periods, which sound files to use, etc.
			using (XmlReader reader = XmlReader.Create("appProfile.xml"))
			{
				while (reader.Read())
				{
					if (reader.IsStartElement())
					{
						switch (reader.Name)
						{
							// Match defaults
							case "AutonomousChecked":
								if (reader.Read())
									if ("true".Equals(reader.Value.ToLower().Trim()))
										checkAutonomous.Checked = true;
									else
										checkAutonomous.Checked = false;
								break;
							case "TeleoperatedChecked":
								if (reader.Read())
									if ("true".Equals(reader.Value.ToLower().Trim()))
										checkTeleoperated.Checked = true;
									else
										checkTeleoperated.Checked = false;
								break;
							case "PauseBetweenPeriodsChecked":
								if (reader.Read())
									if ("true".Equals(reader.Value.ToLower().Trim()))
										checkPauseBetween.Checked = true;
									else
										checkPauseBetween.Checked = false;
								break;
							case "AutonomousTime":
								if (reader.Read()) textAutoTime.Text = reader.Value.Trim();
								textAutoTime_Leave(null, null);
								break;
							case "TeleoperatedTime":
								if (reader.Read()) textTeleTime.Text = reader.Value.Trim();
								textTeleTime_Leave(null, null);
								break;

							// Sound file names for different events
							case "Start":
								if (reader.Read()) soundStart = reader.Value.Trim();
								break;
							case "Pause":
								if (reader.Read()) soundPause = reader.Value.Trim();
								break;
							case "End":
								if (reader.Read()) soundEnd = reader.Value.Trim();
								break;
							case "Abort":
								if (reader.Read()) soundAbort = reader.Value.Trim();
								break;

							// Should we show the debug logging console?
							case "ShowConsole":
								if (reader.Read())
								{
									string showConsoleString = reader.Value.Trim();
									if ("true".Equals(showConsoleString.ToLower().Trim()))
										ConsoleManager.Create();
								}
								break;
						}
					}
				}
			}
			Console.WriteLine("DEBUG: MainWindow() - soundStart1=" + soundStart
				+ ",soundStart2=" + soundPause
				+ ",soundEnd=" + soundEnd
				+ ",soundAbort=" + soundAbort
				 );

			// Initialize the joystick-brick pair objects
			JoystickBrickPair pair1 = new JoystickBrickPair();
			pair1.alias = "1";
			brickPairs.Add(pair1);
			JoystickBrickPair pair2 = new JoystickBrickPair();
			pair2.alias = "2";
			brickPairs.Add(pair2);
			JoystickBrickPair pair3 = new JoystickBrickPair();
			pair3.alias = "3";
			brickPairs.Add(pair3);
			JoystickBrickPair pair4 = new JoystickBrickPair();
			pair4.alias = "4";
			brickPairs.Add(pair4);

			// Click two buttons automatically at the start:
			// Ensure the brick list is initially populated, and the joystick list is initially populated
			buttonUpdateBricks_Click(null, null);
			buttonUpdateJoysticks_Click(null, null);

			// Set the initial mode text & colors, and set the timer to 0:00
			updateModeTextColor();
			updateTimer(TimeSpan.Zero);
		}

		/*************************************************/

		// TODO: These are ~probably~ leftover bits from Designer brainstorming; verify & remove
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
		private void label2_Click(object sender, EventArgs e) { }
		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

		// These too?
		private void textAutoTime_TextChanged(object sender, EventArgs e) { }
		private void textTeleTime_TextChanged(object sender, EventArgs e) { }

		/*************************************************/

		//
		// Sets the text & the color for the current mode, on the main window as well as the optional child 'display' window
		//
		private void updateModeTextColor()
		{
			modeLabel.Text = MODE_NAMES[mode];
			modeLabel.BackColor = MODE_COLORS[mode];

			bigTimer.modeLabel.Text = MODE_NAMES[mode];
			bigTimer.modeLabel.BackColor = MODE_COLORS[mode];
		}

		//
		// Sets the display of time on the main window as we as the optional child 'display' window
		//
		private void updateTimer(TimeSpan time)
		{
			timerLabel.Text = time.ToString(@"m\:ss");

			bigTimer.timerLabel.Text = time.ToString(@"m\:ss");

			//Console.WriteLine("useTimeAnnounce && mode -> " + (useTimeAnnounce) +
			//	"; mode == MODE_AUTONOMOUS -> " + (mode == MODE_AUTONOMOUS) +
			//	"; timerLabel.Text.Equals(\"0:30\") -> " + timerLabel.Text.Equals("0:30") +
			//	"; did30sec == false -> " + (did30sec == false));
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("2:00") && did120sec == false)
			{
				did120sec = true;
				playSound(seconds120);
			}
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("1:00") && did60sec == false)
			{
				did60sec = true;
				playSound(seconds60);
			}
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("0:30") && did30sec == false)
			{
				did30sec = true;
				playSound(seconds30);
			}
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("0:10") && did10sec == false)
			{
				did10sec = true;
				playSound(seconds10);
			}
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("0:05") && did5sec == false)
			{
				did5sec = true;
				playSound(seconds5);
			}
			if (useTimeAnnounce && mode == MODE_TELEOPERATED && timerLabel.Text.Equals("0:03") && did3sec == false)
			{
				did3sec = true;
				playSound(seconds3);
			}
		}

		//
		// Clears all the assigned bricks from the joystick-brick pairs, and clears the display combo boxes
		// TODO: Redefine the 'comboBrick' ComboBox elements into an array, rather than four individual variables indexed with suffixes
		//
		private void buttonClearBricksFromPairs_Click(object sender, EventArgs e)
		{
			comboBrick0.SelectedIndex = -1;
			comboBrick1.SelectedIndex = -1;
			comboBrick2.SelectedIndex = -1;
			comboBrick3.SelectedIndex = -1;

			for (int i = 0; i < 4; i++)
				brickPairs[i].brick = null;
		}

		//
		// Update the list of available bricks (as listed in Bluetooth / the registry / WMI),
		// and test connected bricks to see if they're still connected
		//
		private void buttonUpdateBricks_Click(object sender, EventArgs e)
		{
			// Show the 'Updating...' dialog box
			updateForm.Show();

			// Get the BrickManager singleton
			BrickManager brickManager = BrickManager.getInstance();

			// Get the list of bricks the 1st time...
			// TODO: Right now, it's just a list of Strings. So we pack 'annotations' into the name...
			// E.g.: ' [NXT]' & ' [EV3]' to denote connected bricks (and their type), or ' [none]' or ' [LOST]' to denote disconnected bricks
			// (no [] suffix means no attempt to connect has been made yet)
			// ...Probably, should use state objects instead.  :-/
			allBrickList = brickManager.getListOfAttachedBricks();

			// Attempt to make each connected brick beep;
			// if they've since been disconnected [turned off, etc.] realize this & mark them no longer connected
			foreach (string thisBrick in allBrickList)
			{
				// Is this brick annotated? Annotations will be in square brackets 
				if (thisBrick.Contains("["))
				{
					// Is this brick supposed to be connected? Anything besides [LOST] or [none] (e.g., [EV3] or [NXT] implies we expect it there)
					if (!(thisBrick.Contains(" [LOST]") || thisBrick.Contains(" [none]")))
					{
						// Ok, we expect it to be connected... so send it a command to beep
						GenericBrick targetBrick = brickManager.getBrickByName(thisBrick);
						int toneIndex = rand.Next(0, 8);
						targetBrick.playTone((ushort)(440 + (55 * toneIndex)), 200);
						// It'll either beep, or (if disconnected) there will be a timeout delay and then it'll update its status to [LOST]
					}
				}
			}

			// ...Get the list of bricks again a 2nd time after trying to beep (and updating statuses accordingly)...
			allBrickList = brickManager.getListOfAttachedBricks();

			// Update the UI list of available bricks
			comboAllBrick.Items.Clear();
			comboAllBrick.Items.AddRange(allBrickList.ToArray());
			comboAllBrick.SelectedIndex = -1;

			// Update the combo box list options
			updateBrickSelectors(brickManager);

			// Stop showing the 'Updating...' dialog box when we're done
			updateForm.Hide();
		}

		//
		// Click the button to attempt to connect to the selected brick
		//
		private void buttonConnectBrick_Click(object sender, EventArgs e)
		{
			// Is nothing selected?
			if (comboAllBrick.SelectedIndex < 0)
				return;

			// Which brick was picked?
			int thisIndex = comboAllBrick.SelectedIndex;
			string thisBrick = allBrickList[comboAllBrick.SelectedIndex];

			// Get the brick manager singleton
			BrickManager brickManager = BrickManager.getInstance();

			// 1. Are we trying a reconnect? Check if there is a disconnect annotation
			if (thisBrick.Contains(" [LOST]") || thisBrick.Contains(" [none]"))
			{
				// Attempt to reconnect
				GenericBrick reconnBrick = brickManager.getBrickByName(thisBrick);
				int reconnState = reconnBrick.resetBrokenState();
				if (reconnState > 0)
					MessageBox.Show("Reconnected successfully!", "Success",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("The brick was not recognized", "Error when connecting",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			// 2. Only attempt to make a new connection if it's not already been connected (check for '[')
			if (!thisBrick.Contains("["))
			{
				// We can't tell via software the brick type, so we need to ask
				/*
				 * I have no clue how to make proper dialog boxes  
				 */
				DialogResult result = MessageBox.Show("Is '" + thisBrick + "' brick an EV3?", "Brick Type",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				bool isEv3 = true;
				bool isFiveOne = false;
				DialogResult? resulte = null;
				if (result == DialogResult.No) {
					isEv3 = false;
					MessageBox.Show("Is '" + thisBrick + "' brick an 51515?", "Brick Type",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (!(resulte == DialogResult.No)) {
						isFiveOne = true;
					}
				}
				
				// The user didn't cancel, right?
				if (result != DialogResult.Cancel)
				{
					// Attempt to connect to the brick, passing in whether or not the user identified it as an EV3 or not
					GenericBrick newBrick = brickManager.getBrickByName(thisBrick, isEv3, isFiveOne);

					// Did we get a connection?
					if (newBrick != null)
					{
						// w00t
						MessageBox.Show("Connected successfully!", "Success",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						// Sad! It didn't work for some reason.
						// [Often, it's as simple as they they changed the batteries, forgot to turn it on, let it turn off, etc.]
						MessageBox.Show("The brick was not recognized", "Error when connecting",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			// Ok, refresh the list of bricks & update the UI components
			allBrickList = brickManager.getListOfAttachedBricks();
			comboAllBrick.Items.Clear();
			comboAllBrick.Items.AddRange(allBrickList.ToArray());
			comboAllBrick.SelectedIndex = thisIndex;
			updateBrickSelectors(brickManager);
		}

		//
		// Update the UI components which show lists of connected bricks
		//
		private void updateBrickSelectors(BrickManager brickManager)
		{
			// Get the list of names
			List<string> connBrickList = brickManager.getListOfConnectedBricks();

			// We want a blank at the beginning of the list
			if (connBrickList.Count > 0)
				connBrickList.Insert(0, "");

			// Update the combo boxes
			updateComboBricks(connBrickList);
		}

		//
		// Update the combo boxes which allow assigning a connected brick to a joystick (i.e., defining up to four [4] joystick/brick pairs)
		//
		private void updateComboBricks(List<string> connBrickList)
		{
			/*
			string oldItem = "";
			int comboIndex0 = comboBrick0.SelectedIndex;
			oldItem = "";
			if (comboIndex0 >= 0)
				oldItem = (string)comboBrick0.SelectedItem;
			comboBrick0.Items.Clear();
			comboBrick0.Items.AddRange(connBrickList.ToArray());
			if (comboIndex0 >= 0)
				comboBrick0.SelectedItem = oldItem;
			 */

			string[] newBrickList = connBrickList.ToArray();
			updateComboBoxList(comboBrick0, newBrickList);
			updateComboBoxList(comboBrick1, newBrickList);
			updateComboBoxList(comboBrick2, newBrickList);
			updateComboBoxList(comboBrick3, newBrickList);
		}

		//
		// Update the combo boxes which allow defining the type/manufacturer of joystick plugged into each port (e.g., Saitek or EtechCity or whatnot)
		//
		private void updateComboTypes(List<string> typeList)
		{
			string[] newTypeList = typeList.ToArray();
			updateComboBoxList(comboType0, newTypeList);
			updateComboBoxList(comboType1, newTypeList);
			updateComboBoxList(comboType2, newTypeList);
			updateComboBoxList(comboType3, newTypeList);
		}

		//
		// Update a combo box with a new list of items; keep the old item selected if possible
		//
		private static void updateComboBoxList(ComboBox thisComboBox, string[] newItemList)
		{
			string thisSelectedItem = "";
			int thisComboIndex = thisComboBox.SelectedIndex;
			if (thisComboIndex >= 0)
				thisSelectedItem = (string)thisComboBox.SelectedItem;
			thisComboBox.Items.Clear();
			thisComboBox.Items.AddRange(newItemList);
			if (thisComboIndex >= 0)
				thisComboBox.SelectedItem = thisSelectedItem;
		}

		//
		// This is ~supposed~ to allow one to plug in (or remove) joysticks on the fly;
		// and to re-order the joystick/brick pairs alphabetically based on the joystick labels.
		//
		// But at the moment, it doesn't work to click this after adding, removing, and/or relabelling controllers.  :-(
		//
		private void buttonUpdateJoysticks_Click(object sender, EventArgs e)
		{
			// update the list of joysticks
			JoystickManager joystickManager = JoystickManager.getInstance();
			joystickManager.scanForJoysticks(this);
			// refresh the lists of types
			updateComboTypes(joystickManager.getConfigTypes());
			// walk the list of joysticks, looking for new additions
			Dictionary<string, JoystickWrapper> joysticksByGuid = joystickManager.getJoysticksByGuid();
			// clone the dictionary
			Dictionary<string, JoystickWrapper> joysticksByGuidClone = new Dictionary<string, JoystickWrapper>();
			foreach (string thisGuid in joysticksByGuid.Keys)
				joysticksByGuidClone.Add(thisGuid, joysticksByGuid[thisGuid]);
			// scan through the current pairs & remove matches
			for (int i = 0; i < 4; i++)
			{
				Console.WriteLine("DEBUG: buttonUpdateJoysticks_Click() - scan i=" + i);
				if (brickPairs[i].joystick != null)
				{
					Console.WriteLine("DEBUG: buttonUpdateJoysticks_Click() - ...removing guid=" + brickPairs[i].guid + " at i=" + i);
					joysticksByGuidClone.Remove(brickPairs[i].guid);
				}
			}
			// are any left?
			if (joysticksByGuidClone.Keys.Count > 0)
			{
				Console.WriteLine("DEBUG: buttonUpdateJoysticks_Click() - Still have " + joysticksByGuidClone.Keys.Count);
				// add them to blank spaces
				foreach (string addGuid in joysticksByGuidClone.Keys)
				{
					for (int i = 0; i < 4; i++)
					{
						if (brickPairs[i].joystick == null)
						{
							// found a blank
							Console.WriteLine("DEBUG: buttonUpdateJoysticks_Click() - Found a blank @ i=" + i);
							brickPairs[i].guid = addGuid;
							brickPairs[i].joystick = joysticksByGuid[addGuid];
							setComboType(i, joystickManager.getDefaultConfigBean().typeName);
							setGuidLabel(i, addGuid);
							setGuidLabelBackground(i, false);

							Console.WriteLine("DEBUG: buttonUpdateJoysticks_Click() - pair validity = " + brickPairs[i].isValidPair());
							break;
						}
					}
				}
			}

			// refresh joystick types


			timer.Stop();
			timer.Start();
		}

		//
		// Updates what is selected in a given joystick-type combo box
		//
		private void setComboType(int index, string type)
		{
			switch (index)
			{
				case 0:
					comboType0.SelectedItem = type;
					break;
				case 1:
					comboType1.SelectedItem = type;
					break;
				case 2:
					comboType2.SelectedItem = type;
					break;
				case 3:
					comboType3.SelectedItem = type;
					break;
			}
		}

		//
		// Changes the GUID text label for a given joystick (every joystick is expected to have a unique GUID)
		//
		private void setGuidLabel(int index, string guid)
		{
			switch (index)
			{
				case 0:
					guidLabel0.Text = guid;
					break;
				case 1:
					guidLabel1.Text = guid;
					break;
				case 2:
					guidLabel2.Text = guid;
					break;
				case 3:
					guidLabel3.Text = guid;
					break;
			}
		}

		//
		// Changes the background color of the GUID label...
		// -- Yellow: No joystick for that row [initial default]
		// -- Red:	Joystick is plugged in, but it's detected that the 'Analog' mode is not enabled
		// -- Green:  Joystick plugged in, and the 'Analog' light is lit
		// -- Blue:   A button is being pressed on the controller
		//			(so as to validate that the controller is working, the right robot is assigned, etc.)
		//
		private void setGuidLabelBackground(int index, bool? pressed)
		{
			Color newColor = Color.LightGreen;
			if (pressed == null)
				newColor = Color.Red;
			else if ((bool)pressed)
				newColor = Color.Blue;

			switch (index)
			{
				case 0:
					guidLabel0.BackColor = newColor;
					break;
				case 1:
					guidLabel1.BackColor = newColor;
					break;
				case 2:
					guidLabel2.BackColor = newColor;
					break;
				case 3:
					guidLabel3.BackColor = newColor;
					break;
			}
		}

		//
		// When the user changes the type of controller for a given row, notify the joystickManager
		//
		private void comboType0_SelectedIndexChanged(object sender, EventArgs e)
		{
			//JoystickManager joystickManager = JoystickManager.getInstance();
			//joystickManager.setType(brickPairs[0].joystick, (string)comboType0.SelectedItem);
			changeJoystickType(0, (string)comboType0.SelectedItem);
		}
		private void comboType1_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeJoystickType(1, (string)comboType1.SelectedItem);
		}
		private void comboType2_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeJoystickType(2, (string)comboType2.SelectedItem);
		}
		private void comboType3_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeJoystickType(3, (string)comboType3.SelectedItem);
		}
		//
		// When the user changes the type of controller for a given row, notify the joystickManager
		//
		private void changeJoystickType(int index, string selected)
		{
			JoystickManager joystickManager = JoystickManager.getInstance();
			joystickManager.setType(brickPairs[index].joystick, selected);
		}

		//
		// When a user changes the brick selection
		//
		private void comboBrick0_SelectedIndexChanged(object sender, EventArgs e)
		{
			/*
			if (comboBrick0.SelectedItem != null)
			{
				string selected = (string)comboBrick0.SelectedItem;
				Console.WriteLine("DEBUG: comboBrick0_SelectedIndexChanged() - '" + selected + "'");
				BrickManager brickManager = BrickManager.getInstance();
				brickPairs[0].brick = brickManager.getBrickByName(selected);
				brickPairs[0].resetDefaults();
				Console.WriteLine("DEBUG: comboBrick0_SelectedIndexChanged() - pair validity = " + brickPairs[0].isValidPair());
			}
			else
				brickPairs[0].brick = null;
			 */

			comboBrickSelectedIndexChanged(comboBrick0, 0);
		}

		private void comboBrick1_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBrickSelectedIndexChanged(comboBrick1, 1);
		}

		private void comboBrick2_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBrickSelectedIndexChanged(comboBrick2, 2);
		}
		private void comboBrick3_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBrickSelectedIndexChanged(comboBrick3, 3);
		}
		//
		// When a user changes the brick selection
		//
		private void comboBrickSelectedIndexChanged(ComboBox thisComboBox, int thisComboBoxNumber)
		{
			if (thisComboBox.SelectedItem != null)
			{
				string selected = (string)thisComboBox.SelectedItem;
				Console.WriteLine("DEBUG: comboBrick" + thisComboBoxNumber + "_SelectedIndexChanged() - '" + selected + "'");
				BrickManager brickManager = BrickManager.getInstance();
				brickPairs[thisComboBoxNumber].brick = brickManager.getBrickByName(selected);
				brickPairs[thisComboBoxNumber].resetDefaults();
				Console.WriteLine("DEBUG: comboBrick" + thisComboBoxNumber + "_SelectedIndexChanged() - pair validity = " + brickPairs[thisComboBoxNumber].isValidPair());
			}
			else
				brickPairs[thisComboBoxNumber].brick = null;
		}

		//
		// Handle when user clicks on 'Toggle free-drive'. Only works when not in a match, or currently in free drive
		//
		private void buttonToggleFreeDrive_Click(object sender, EventArgs e)
		{
			if (mode == MODE_NOMATCH)
			{
				transitionMode(MODE_FREEDRIVE);
				return;
			}
			if (mode == MODE_FREEDRIVE)
			{
				transitionMode(MODE_NOMATCH);
				return;
			}

			// Otherwise, DNGN
			return;
		}

		//
		// Toggles whether or not the bricks will beep if the associated controller's buttons are pressed while waiting for a match to start
		//
		private void checkBox_beepWhenButtonPressed(object sender, EventArgs e)
		{
			bBeepWhenButtonPressed = checkBeepForButtonPressed.Checked;
			Console.WriteLine("DEBUG: checkBox_beepWhenButtonPressed() - bBeep=" + bBeepWhenButtonPressed);
		}

		//
		// Toggles whether the 2nd screen "scoreboard" window is displayed or not
		//
		private void checkShowChildWindow_CheckedChanged(object sender, EventArgs e)
		{
			if (checkShowChildWindow.Checked)
			{
				bigTimer.Show();
				bigTimer.resizeFontsToWindow();
			}
			else
				bigTimer.Hide();
		}

		//
		// Handle when someone updates how long the automomous period should be
		//
		private void textAutoTime_Leave(object sender, EventArgs e)
		{
			string autoTimeValue = textAutoTime.Text;
			TimeSpan interval;
			if (TimeSpan.TryParseExact(autoTimeValue, @"m\:ss", null, out interval))
			{
				autonomousLength = interval;
				Console.WriteLine("DEBUG: textAutoTime_Leave() - autonomousLength=" + autonomousLength);
			}
			else
			{
				textAutoTime.Text = autonomousLength.ToString(@"m\:ss");
				MessageBox.Show(autoTimeValue + " is not a valid time string (m:ss)", "Invalid Time String",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//
		// Handle when someone updates how long the teleoperated period should be
		//
		private void textTeleTime_Leave(object sender, EventArgs e)
		{
			string teleTimeValue = textTeleTime.Text;
			TimeSpan interval;
			if (TimeSpan.TryParseExact(teleTimeValue, @"m\:ss", null, out interval))
			{
				teleoperatedLength = interval;
				Console.WriteLine("DEBUG: textTeleTime_Leave() - teleoperatedLength=" + teleoperatedLength);
			}
			else
			{
				textTeleTime.Text = teleoperatedLength.ToString(@"m\:ss");
				MessageBox.Show(teleTimeValue + " is not a valid time string (m:ss)", "Invalid Time String",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//
		// Handle when someone clicks the Abort button; submit a request to transition to 'ABORTSPECIAL'
		//
		private void buttonAbort_Click(object sender, EventArgs e)
		{
			transitionMode(MODE_ABORTSPECIAL);
		}

		//
		// Handle when someone clicks the Start/Pause button; submit a request to transition based on the current mode
		//
		private void buttonStartPause_Click(object sender, EventArgs e)
		{
			// if we're paused, or in interperiod - resume!
			if (mode == MODE_PAUSED || mode == MODE_INTERPERIOD)
			{
				transitionMode(MODE_TELEOPERATED);
				return;
			}

			// if we're teleoperating - pause!
			if (mode == MODE_TELEOPERATED)
			{
				transitionMode(MODE_PAUSED);
				return;
			}

			// otherwise - try to start it up!
			if (checkAutonomous.Checked)
				transitionMode(MODE_AUTONOMOUS);
			else if (checkTeleoperated.Checked)
				transitionMode(MODE_TELEOPERATED);
		}

		//
		// Halt everything! Send a request to all bricks to stop all motors & programs.
		//
		private void allHalt()
		{
			int index = 0;
			foreach (JoystickBrickPair thisPair in brickPairs)
			{
				if (brickPairs[index].brick != null)
				{
					brickPairs[index].brick.stopAllMotors();
					brickPairs[index].brick.stopAllPrograms();
				}
				index += 1;
			}
		}

		/*****************************************************************/

		//
		// Background loop:
		//
		// 1) Handle joystick data reads
		// 2) Handle UI changes based on joystick inputs
		// 3) Process the 'mode' that the software is in
		//
		private void timer1_Tick(object sender, EventArgs e)
		{
			//
			// do these all the time
			//

			// update attached joysticks
			foreach (JoystickBrickPair thisPair in brickPairs)
				if (thisPair.joystick != null)
					thisPair.joystick.readAndProcessData();

			// UI button toggles
			int buttonIndex = 0;
			foreach (JoystickBrickPair thisPair in brickPairs)
			{
				if (thisPair.joystick != null)
				{
					// UI indicates if analog mode is off
					bool toggleGuidIndicator = false;
					bool? pressed = null;
					if (thisPair.joystick.getAnalogModeChange() == true)
					{
						pressed = false;
						toggleGuidIndicator = true;
					}
					// UI indicates if someone is pressing a button
					if (thisPair.joystick.getAnyButtonChange() != null && thisPair.joystick.isAnalogMode())
					{
						pressed = false;
						if (thisPair.joystick.getAnyButtonChange() == true)
						{
							pressed = true;
							// Also have the brick beep when a button is pressed [if the checkbox is set]
							if (mode == MODE_NOMATCH && thisPair.brick != null && bBeepWhenButtonPressed)
								thisPair.brick.playTone((ushort)(440 + (110 * buttonIndex)), 200);
						}
						toggleGuidIndicator = true;
					}
					// Check to see if the analog mode was changed
					if (thisPair.joystick.getAnalogModeChange() == false)
					{
						pressed = null;
						toggleGuidIndicator = true;
					}
					// Change the GUID field background if analog off {or} if button pressed
					if (toggleGuidIndicator)
						setGuidLabelBackground(buttonIndex, pressed);
				}
				buttonIndex += 1;
			}

			//
			// Process active 'modes' (free driving, autonomous, etc.)
			//
			switch (mode)
			{
				// Freedrive - no time limit, just plain direct driving
				case MODE_FREEDRIVE:
					doDriving();
					break;

				// Teleoperated - direct driving until time runs out
				case MODE_TELEOPERATED:
					doDriving();
					// if we haven't reached the end time yet, update the timer(s)
					if (DateTime.Now.CompareTo(endTime) < 0)
						// We add 1 second so it "looks right"
						updateTimer(endTime.Subtract(DateTime.Now).Add(TIMESPAN_ONE_SECOND));
					else
						transitionMode(MODE_NOMATCH);
					break;

				// Autonomous - when the time runs out, terminate programs
				case MODE_AUTONOMOUS:
					// if we haven't reached the end time yet, update the timer(s)
					if (DateTime.Now.CompareTo(endTime) < 0)
						// We add 1 second so it "looks right"
						updateTimer(endTime.Subtract(DateTime.Now).Add(TIMESPAN_ONE_SECOND));
					else
					{
						// do we have a teleop period checked? if not, just end
						if (!checkTeleoperated.Checked)
							transitionMode(MODE_NOMATCH);
						// otherwise - act depending on whether or not we have an interop period
						else
						{
							if (checkPauseBetween.Checked)
								transitionMode(MODE_INTERPERIOD);
							else
								transitionMode(MODE_TELEOPERATED);
						}
					}
					break;
			}
		}

		//
		// The meat of the driving operations: Read joystick activity, turn that into commands to the bricks
		//
		private void doDriving()
		{
			int index = 0;
			// Cycle through the joystick/brick pairs
			foreach (JoystickBrickPair thisPair in brickPairs)
			{
				if (thisPair.joystick != null)
				{
					// Only if this pair has a valid joystick & a connected brick, ~and~ the joystick is in 'analog' mode
					if (thisPair.isValidPair() && thisPair.joystick.isAnalogMode())
					{
						// Get the current values for the motor settings
						int currentLMotor = thisPair.lMotorValue;
						int currentRMotor = thisPair.rMotorValue;
						int newLMotor = currentLMotor;
						int newRMotor = currentRMotor;

						// In the last loop, was there a change to the fine motor controls?
						// i.e.: fine motor d-pad was pressed OR released
						if (thisPair.joystick.getFineMotorChange() != null)
						{
							Console.WriteLine("DEBUG: doDriving() - getFineMotorChange=" + thisPair.joystick.getFineMotorChange());
							// default to 'released' (no direction)
							int padDirec = 0;
							// check to see if the change was a d-pad press [if so, pull the direction]
							if (thisPair.joystick.getFineMotorChange() > -1)
								padDirec = ((int)thisPair.joystick.getFineMotorChange() / 4500) + 1;

							// set the targeted new motor values to the appropriate fine power values for the d-pad direction
							newLMotor = JoystickBrickPair.FINE_POWER * JoystickBrickPair.FINE_LMOTOR[padDirec];
							newRMotor = JoystickBrickPair.FINE_POWER * JoystickBrickPair.FINE_RMOTOR[padDirec];
							Console.WriteLine("DEBUG: doDriving() - padDirec=" + padDirec + ":newLMotor=" + newLMotor + ",newRMotor=" + newRMotor);
						}

						// Check the L & R sticks, see if there was a change to them; if so, set new value(s) for the associated motors
						if (thisPair.joystick.getLMotorChange() != null)
							newLMotor = -(int)thisPair.joystick.getLMotorChange() / JoystickBrickPair.GEAR_LEVELS[thisPair.gearIndex];
						if (thisPair.joystick.getRMotorChange() != null)
							newRMotor = -(int)thisPair.joystick.getRMotorChange() / JoystickBrickPair.GEAR_LEVELS[thisPair.gearIndex];

						// Send motor values to the robot motors! B & C are the drive motors.
						// Note, need to use negative values for normal motion (unless 'reverseMotor' toggle is on...)
						// ONLY send a command if the new value is not the same as the current value
						// If motors are set to be reversed, (a) send negative values & (b) send the values to the C & B [instead of B & C]
						if (newLMotor != currentLMotor)
						{
							if (!thisPair.reverseMotors)
							{
								//Console.WriteLine("DEBUG: doDriving() - newL,!reverse=" + newLMotor);
								thisPair.brick.setMotor(GenericBrick.MOTOR_B, newLMotor);
							}
							else
							{
								//Console.WriteLine("DEBUG: doDriving() - newL,reverse=" + newLMotor);
								thisPair.brick.setMotor(GenericBrick.MOTOR_C, -newLMotor);
							}

							thisPair.lMotorValue = newLMotor;
						}
						if (newRMotor != currentRMotor)
						{
							if (!thisPair.reverseMotors)
							{
								//Console.WriteLine("DEBUG: doDriving() - newR,!reverse=" + newRMotor);
								thisPair.brick.setMotor(GenericBrick.MOTOR_C, newRMotor);
							}
							else
							{
								//Console.WriteLine("DEBUG: doDriving() - newR,reverse=" + newRMotor);
								thisPair.brick.setMotor(GenericBrick.MOTOR_B, -newRMotor);
							}

							thisPair.rMotorValue = newRMotor;
						}

						// "A" motor (Action motor)
						// Check to see if there was a control change for the A motor, whether to increment it or decrement it
						// ...or if one of the buttons was ~released~, sets to '0' power
						if (thisPair.joystick.getAMotorDecChange() != null || thisPair.joystick.getAMotorIncChange() != null)
						{
							int power = 0;
							if (thisPair.joystick.getAMotorInc())
								power += JoystickBrickPair.POWER_LEVELS[thisPair.powerIndex];
							if (thisPair.joystick.getAMotorDec())
								power -= JoystickBrickPair.POWER_LEVELS[thisPair.powerIndex];
							thisPair.brick.setMotor(GenericBrick.MOTOR_A, power);
						}

						// "D" motor (Action motor for EV3) ~ same style as "A" motor
						if (thisPair.joystick.getDMotorDecChange() != null || thisPair.joystick.getDMotorIncChange() != null)
						{
							int power = 0;
							if (thisPair.joystick.getDMotorInc())
								power += JoystickBrickPair.POWER_LEVELS[thisPair.powerIndex];
							if (thisPair.joystick.getDMotorDec())
								power -= JoystickBrickPair.POWER_LEVELS[thisPair.powerIndex];
							thisPair.brick.setMotor(GenericBrick.MOTOR_D, power);
						}

						// If the joystick button to toggle the drive motor directions was pressed, flip the motor direction flag
						if (thisPair.joystick.getReverseToggleChange() == true)
							thisPair.reverseMotors = !thisPair.reverseMotors;

						// If the joystick button to change the action motor power was selected, cycle through the array of action motor powers
						if (thisPair.joystick.getPowerSelectChange() == true)
						{
							thisPair.powerIndex += 1;
							if (thisPair.powerIndex > JoystickBrickPair.POWER_LEVEL_MAX_INDEX)
							{
								// When it cycles back to the beginning, make the robot beep so the driver has an audible indication
								thisPair.powerIndex = 0;
								thisPair.brick.playTone((ushort)(440 + (110 * index)), 1000);
							}
						}

						// If the drive motor "gear" button is pressed, change high/medium/low gear
						if (thisPair.joystick.getGearSelectChange() == true)
						{
							thisPair.gearIndex += 1;
							if (thisPair.gearIndex > JoystickBrickPair.GEAR_LEVEL_MAX_INDEX)
								thisPair.gearIndex = 0;
						}

						// If the button to reset everything to full power is pressed
						if (thisPair.joystick.getPowerResetChange() == true)
						{
							thisPair.gearIndex = JoystickBrickPair.GEAR_LEVEL_MAX_INDEX;
							thisPair.powerIndex = JoystickBrickPair.POWER_LEVEL_MAX_INDEX;
						}
					}
				}
				index += 1;
			}
		}

		/*****************************************************************/

		//
		// Handle what happens when the program transitions to a new mode
		//
		public int transitionMode(int toMode)
		{
			// Calculate an integer representing the combination of the ~current~ mode and the ~new~ mode
			// These will be checked against a back of constants for specific transition behaviors
			int transitionValue = mode * 100 + toMode;

			// Special case: aborting - can ~always~ abort to "NO_MATCH" [unless we're already there!]
			if (toMode == MODE_ABORTSPECIAL && mode != MODE_NOMATCH)
			{
				allHalt();
				playSound(soundAbort);
				mode = MODE_NOMATCH;
				updateModeTextColor();
				updateTimer(TimeSpan.Zero);
				return mode;
			}

			// Go through all the allowed transition types
			switch (transitionValue)
			{
				case TRANSITION_NOMATCH_TO_FREEDRIVE:
					//for (int i = 0; i < 4; i++)
					//{
					//	brickPairs[i].resetDefaults();
					//	Console.WriteLine("DEBUG: buttonToggleFreeDrive_Click() - i=" + i + ",pair=" + brickPairs[i].toString());
					//}
					buttonToggleFreeDrive.BackColor = Color.Green;
					playSyncSound(soundStart);
					mode = MODE_FREEDRIVE;
					updateModeTextColor();
					break;

				case TRANSITION_FREEDRIVE_TO_NOMATCH:
					buttonToggleFreeDrive.BackColor = Color.Transparent;
					allHalt();
					playSound(soundEnd);
					mode = MODE_NOMATCH;
					updateModeTextColor();
					break;

				case TRANSITION_NOMATCH_TO_AUTONOMOUS:
					playSound(soundStart);
					// Calculate when the autonomous mode will be ending
					endTime = DateTime.Now.Add(autonomousLength);
					mode = MODE_AUTONOMOUS;
					updateModeTextColor();
					break;

				case TRANSITION_AUTONOMOUS_TO_NOMATCH:
					// Stop all programs at the end of autonomous
					allHalt();
					playSound(soundEnd);
					// Make sure the timer shows 0:00
					updateTimer(TimeSpan.Zero);
					mode = MODE_NOMATCH;
					updateModeTextColor();
					break;

				case TRANSITION_AUTONOMOUS_TO_INTERPERIOD:
					// Stop all programs at the end of autonomous
					allHalt();
					playSound(soundEnd);
					// Make sure the timer shows 0:00
					updateTimer(TimeSpan.Zero);
					mode = MODE_INTERPERIOD;
					updateModeTextColor();
					break;

				case TRANSITION_AUTONOMOUS_TO_TELEOPERATED:
					// Stop all programs at the end of autonomous
					allHalt();
					// Make sure the timer shows 0:00
					updateTimer(TimeSpan.Zero);
					this.Refresh();
					playSyncSound(soundEnd);

					// Go straight into teleoperated...
					mode = MODE_TELEOPERATED;
					updateModeTextColor();
					this.Refresh();
					playSyncSound(soundStart);
					// Calculate when the teleoperated mode will be ending
					endTime = DateTime.Now.Add(teleoperatedLength);

					// TODO
					did3sec = false;
					did5sec = false;
					did10sec = false;
					did30sec = false;
					did60sec = false;
					did120sec = false;

					break;

				case TRANSITION_NOMATCH_TO_TELEOPERATED:
				case TRANSITION_INTERPERIOD_TO_TELEOPERATED:
					mode = MODE_TELEOPERATED;
					updateModeTextColor();
					this.Refresh();
					playSyncSound(soundStart);
					endTime = DateTime.Now.Add(teleoperatedLength);

					// TODO
					did3sec = false;
					did5sec = false;
					did10sec = false;
					did30sec = false;
					did60sec = false;
					did120sec = false;

					break;

				case TRANSITION_TELEOPERATED_TO_NOMATCH:
					// Stop all motors
					allHalt();
					playSound(soundEnd);
					// Make sure the timer shows 0:00
					updateTimer(TimeSpan.Zero);
					mode = MODE_NOMATCH;
					updateModeTextColor();
					break;

				case TRANSITION_TELEOPERATED_TO_PAUSED:
					// Stop all motors
					allHalt();
					mode = MODE_PAUSED;
					updateModeTextColor();
					this.Refresh();
					playSound(soundPause);
					// Note how much time is left in the match, so we can re-calculate the end time when resuming
					resumeLength = endTime.Subtract(DateTime.Now);
					break;

				case TRANSITION_PAUSED_TO_TELEOPERATED:
					mode = MODE_TELEOPERATED;
					updateModeTextColor();
					this.Refresh();
					playSyncSound(soundStart);
					// Calculate when the teleoperated mode will be ending based on how much time had been left
					endTime = DateTime.Now.Add(resumeLength);
					break;
			}

			return mode;
		}

		/*******************************************************************/

		//
		// Play a sound based on a filename
		//
		private bool playSound(string fileName)
		{
			bool success = true;
			using (SoundPlayer player = new SoundPlayer(fileName))
			{
				try
				{
					player.Play();
				}
				catch (Exception)
				{
					MessageBox.Show("Can't play file " + fileName + "!", "File Not Found",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					success = false;
				}
			}

			return success;
		}

		//
		// Play a sound (in sync mode [?]) based on a filename
		//
		private bool playSyncSound(string fileName)
		{
			bool success = true;
			using (SoundPlayer player = new SoundPlayer(fileName))
			{
				try
				{
					player.PlaySync();
				}
				catch (Exception)
				{
					MessageBox.Show("Can't play file " + fileName + "!", "File Not Found",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					success = false;
				}
			}

			return success;
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{

		}

		/******************************************************/

	}
}
