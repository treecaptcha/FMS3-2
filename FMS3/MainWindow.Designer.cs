namespace FMS3
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSetup = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBeepForButtonPressed = new System.Windows.Forms.CheckBox();
			this.buttonToggleFreeDrive = new System.Windows.Forms.Button();
			this.comboBrick3 = new System.Windows.Forms.ComboBox();
			this.comboType3 = new System.Windows.Forms.ComboBox();
			this.textJoystickAlias3 = new System.Windows.Forms.TextBox();
			this.guidLabel3 = new System.Windows.Forms.Label();
			this.comboBrick2 = new System.Windows.Forms.ComboBox();
			this.comboType2 = new System.Windows.Forms.ComboBox();
			this.textJoystickAlias2 = new System.Windows.Forms.TextBox();
			this.guidLabel2 = new System.Windows.Forms.Label();
			this.comboBrick1 = new System.Windows.Forms.ComboBox();
			this.comboType1 = new System.Windows.Forms.ComboBox();
			this.textJoystickAlias1 = new System.Windows.Forms.TextBox();
			this.guidLabel1 = new System.Windows.Forms.Label();
			this.comboBrick0 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonUpdateBricks = new System.Windows.Forms.Button();
			this.spacer1 = new System.Windows.Forms.Label();
			this.comboAllBrick = new System.Windows.Forms.ComboBox();
			this.buttonConnectBrick = new System.Windows.Forms.Button();
			this.guidLabel0 = new System.Windows.Forms.Label();
			this.textJoystickAlias0 = new System.Windows.Forms.TextBox();
			this.comboType0 = new System.Windows.Forms.ComboBox();
			this.buttonUpdateJoysticks = new System.Windows.Forms.Button();
			this.buttonClearBricksFromPairs = new System.Windows.Forms.Button();
			this.tabMatch = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.checkShowChildWindow = new System.Windows.Forms.CheckBox();
			this.textTeleTime = new System.Windows.Forms.TextBox();
			this.textAutoTime = new System.Windows.Forms.TextBox();
			this.checkPauseBetween = new System.Windows.Forms.CheckBox();
			this.checkTeleoperated = new System.Windows.Forms.CheckBox();
			this.checkAutonomous = new System.Windows.Forms.CheckBox();
			this.buttonAbort = new System.Windows.Forms.Button();
			this.buttonStartPause = new System.Windows.Forms.Button();
			this.timerLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.modeLabel = new System.Windows.Forms.Label();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabSetup.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabMatch.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabSetup);
			this.tabControl1.Controls.Add(this.tabMatch);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(640, 472);
			this.tabControl1.TabIndex = 0;
			// 
			// tabSetup
			// 
			this.tabSetup.Controls.Add(this.tableLayoutPanel1);
			this.tabSetup.Location = new System.Drawing.Point(4, 24);
			this.tabSetup.Name = "tabSetup";
			this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetup.Size = new System.Drawing.Size(632, 444);
			this.tabSetup.TabIndex = 0;
			this.tabSetup.Text = "Setup";
			this.tabSetup.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkBeepForButtonPressed, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.buttonToggleFreeDrive, 3, 8);
			this.tableLayoutPanel1.Controls.Add(this.comboBrick3, 3, 7);
			this.tableLayoutPanel1.Controls.Add(this.comboType3, 2, 7);
			this.tableLayoutPanel1.Controls.Add(this.textJoystickAlias3, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.guidLabel3, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.comboBrick2, 3, 6);
			this.tableLayoutPanel1.Controls.Add(this.comboType2, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this.textJoystickAlias2, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.guidLabel2, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.comboBrick1, 3, 5);
			this.tableLayoutPanel1.Controls.Add(this.comboType1, 2, 5);
			this.tableLayoutPanel1.Controls.Add(this.textJoystickAlias1, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.guidLabel1, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.comboBrick0, 3, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.guidLabel0, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.textJoystickAlias0, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.comboType0, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.buttonUpdateJoysticks, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.buttonClearBricksFromPairs, 3, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 10;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 438);
			this.tableLayoutPanel1.TabIndex = 0;
			this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(321, 150);
			this.label4.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138, 13);
			this.label4.TabIndex = 37;
			this.label4.Text = "Joystick types";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(165, 150);
			this.label3.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138, 13);
			this.label3.TabIndex = 36;
			this.label3.Text = "Joystick labels";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// checkBeepForButtonPressed
			// 
			this.checkBeepForButtonPressed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.checkBeepForButtonPressed.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.checkBeepForButtonPressed, 2);
			this.checkBeepForButtonPressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkBeepForButtonPressed.Location = new System.Drawing.Point(9, 353);
			this.checkBeepForButtonPressed.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.checkBeepForButtonPressed.Name = "checkBeepForButtonPressed";
			this.checkBeepForButtonPressed.Size = new System.Drawing.Size(294, 25);
			this.checkBeepForButtonPressed.TabIndex = 35;
			this.checkBeepForButtonPressed.Text = "Beep for button presses";
			this.checkBeepForButtonPressed.UseVisualStyleBackColor = true;
			this.checkBeepForButtonPressed.CheckedChanged += new System.EventHandler(this.checkBox_beepWhenButtonPressed);
			// 
			// buttonToggleFreeDrive
			// 
			this.buttonToggleFreeDrive.AutoSize = true;
			this.buttonToggleFreeDrive.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonToggleFreeDrive.Location = new System.Drawing.Point(471, 347);
			this.buttonToggleFreeDrive.Name = "buttonToggleFreeDrive";
			this.buttonToggleFreeDrive.Size = new System.Drawing.Size(152, 25);
			this.buttonToggleFreeDrive.TabIndex = 34;
			this.buttonToggleFreeDrive.Text = "Toggle free-drive mode";
			this.buttonToggleFreeDrive.UseVisualStyleBackColor = true;
			this.buttonToggleFreeDrive.Click += new System.EventHandler(this.buttonToggleFreeDrive_Click);
			// 
			// comboBrick3
			// 
			this.comboBrick3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBrick3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrick3.FormattingEnabled = true;
			this.comboBrick3.Location = new System.Drawing.Point(477, 310);
			this.comboBrick3.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboBrick3.Name = "comboBrick3";
			this.comboBrick3.Size = new System.Drawing.Size(140, 23);
			this.comboBrick3.TabIndex = 21;
			this.comboBrick3.SelectedIndexChanged += new System.EventHandler(this.comboBrick3_SelectedIndexChanged);
			// 
			// comboType3
			// 
			this.comboType3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType3.FormattingEnabled = true;
			this.comboType3.Location = new System.Drawing.Point(321, 310);
			this.comboType3.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboType3.Name = "comboType3";
			this.comboType3.Size = new System.Drawing.Size(138, 23);
			this.comboType3.TabIndex = 20;
			this.comboType3.SelectedIndexChanged += new System.EventHandler(this.comboType3_SelectedIndexChanged);
			// 
			// textJoystickAlias3
			// 
			this.textJoystickAlias3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textJoystickAlias3.Location = new System.Drawing.Point(165, 310);
			this.textJoystickAlias3.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.textJoystickAlias3.Name = "textJoystickAlias3";
			this.textJoystickAlias3.Size = new System.Drawing.Size(138, 23);
			this.textJoystickAlias3.TabIndex = 19;
			this.textJoystickAlias3.Text = "4";
			this.textJoystickAlias3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// guidLabel3
			// 
			this.guidLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.guidLabel3.AutoSize = true;
			this.guidLabel3.BackColor = System.Drawing.Color.Gold;
			this.guidLabel3.Location = new System.Drawing.Point(9, 310);
			this.guidLabel3.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.guidLabel3.Name = "guidLabel3";
			this.guidLabel3.Size = new System.Drawing.Size(138, 25);
			this.guidLabel3.TabIndex = 18;
			this.guidLabel3.Text = "GUID";
			this.guidLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBrick2
			// 
			this.comboBrick2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBrick2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrick2.FormattingEnabled = true;
			this.comboBrick2.Location = new System.Drawing.Point(477, 267);
			this.comboBrick2.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboBrick2.Name = "comboBrick2";
			this.comboBrick2.Size = new System.Drawing.Size(140, 23);
			this.comboBrick2.TabIndex = 17;
			this.comboBrick2.SelectedIndexChanged += new System.EventHandler(this.comboBrick2_SelectedIndexChanged);
			// 
			// comboType2
			// 
			this.comboType2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType2.FormattingEnabled = true;
			this.comboType2.Location = new System.Drawing.Point(321, 267);
			this.comboType2.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboType2.Name = "comboType2";
			this.comboType2.Size = new System.Drawing.Size(138, 23);
			this.comboType2.TabIndex = 16;
			this.comboType2.SelectedIndexChanged += new System.EventHandler(this.comboType2_SelectedIndexChanged);
			// 
			// textJoystickAlias2
			// 
			this.textJoystickAlias2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textJoystickAlias2.Location = new System.Drawing.Point(165, 267);
			this.textJoystickAlias2.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.textJoystickAlias2.Name = "textJoystickAlias2";
			this.textJoystickAlias2.Size = new System.Drawing.Size(138, 23);
			this.textJoystickAlias2.TabIndex = 15;
			this.textJoystickAlias2.Text = "3";
			this.textJoystickAlias2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// guidLabel2
			// 
			this.guidLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.guidLabel2.AutoSize = true;
			this.guidLabel2.BackColor = System.Drawing.Color.Gold;
			this.guidLabel2.Location = new System.Drawing.Point(9, 267);
			this.guidLabel2.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.guidLabel2.Name = "guidLabel2";
			this.guidLabel2.Size = new System.Drawing.Size(138, 25);
			this.guidLabel2.TabIndex = 14;
			this.guidLabel2.Text = "GUID";
			this.guidLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBrick1
			// 
			this.comboBrick1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBrick1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrick1.FormattingEnabled = true;
			this.comboBrick1.Location = new System.Drawing.Point(477, 224);
			this.comboBrick1.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboBrick1.Name = "comboBrick1";
			this.comboBrick1.Size = new System.Drawing.Size(140, 23);
			this.comboBrick1.TabIndex = 13;
			this.comboBrick1.SelectedIndexChanged += new System.EventHandler(this.comboBrick1_SelectedIndexChanged);
			// 
			// comboType1
			// 
			this.comboType1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType1.FormattingEnabled = true;
			this.comboType1.Location = new System.Drawing.Point(321, 224);
			this.comboType1.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboType1.Name = "comboType1";
			this.comboType1.Size = new System.Drawing.Size(138, 23);
			this.comboType1.TabIndex = 12;
			this.comboType1.SelectedIndexChanged += new System.EventHandler(this.comboType1_SelectedIndexChanged);
			// 
			// textJoystickAlias1
			// 
			this.textJoystickAlias1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textJoystickAlias1.Location = new System.Drawing.Point(165, 224);
			this.textJoystickAlias1.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.textJoystickAlias1.Name = "textJoystickAlias1";
			this.textJoystickAlias1.Size = new System.Drawing.Size(138, 23);
			this.textJoystickAlias1.TabIndex = 11;
			this.textJoystickAlias1.Text = "2";
			this.textJoystickAlias1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// guidLabel1
			// 
			this.guidLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.guidLabel1.AutoSize = true;
			this.guidLabel1.BackColor = System.Drawing.Color.Gold;
			this.guidLabel1.Location = new System.Drawing.Point(9, 224);
			this.guidLabel1.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.guidLabel1.Name = "guidLabel1";
			this.guidLabel1.Size = new System.Drawing.Size(138, 25);
			this.guidLabel1.TabIndex = 10;
			this.guidLabel1.Text = "GUID";
			this.guidLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBrick0
			// 
			this.comboBrick0.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBrick0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrick0.FormattingEnabled = true;
			this.comboBrick0.Location = new System.Drawing.Point(477, 181);
			this.comboBrick0.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboBrick0.Name = "comboBrick0";
			this.comboBrick0.Size = new System.Drawing.Size(140, 23);
			this.comboBrick0.TabIndex = 9;
			this.comboBrick0.SelectedIndexChanged += new System.EventHandler(this.comboBrick0_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label2, 4);
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(3, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(620, 43);
			this.label2.TabIndex = 4;
			this.label2.Text = "USB Joysticks and Teams";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(620, 43);
			this.label1.TabIndex = 0;
			this.label1.Text = "Bluetooth-Connected Bricks";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// flowLayoutPanel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 4);
			this.flowLayoutPanel1.Controls.Add(this.buttonUpdateBricks);
			this.flowLayoutPanel1.Controls.Add(this.spacer1);
			this.flowLayoutPanel1.Controls.Add(this.comboAllBrick);
			this.flowLayoutPanel1.Controls.Add(this.buttonConnectBrick);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 46);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(620, 37);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// buttonUpdateBricks
			// 
			this.buttonUpdateBricks.AutoSize = true;
			this.buttonUpdateBricks.Location = new System.Drawing.Point(3, 3);
			this.buttonUpdateBricks.Name = "buttonUpdateBricks";
			this.buttonUpdateBricks.Size = new System.Drawing.Size(95, 27);
			this.buttonUpdateBricks.TabIndex = 1;
			this.buttonUpdateBricks.Text = "Update bricks";
			this.buttonUpdateBricks.UseVisualStyleBackColor = true;
			this.buttonUpdateBricks.Click += new System.EventHandler(this.buttonUpdateBricks_Click);
			// 
			// spacer1
			// 
			this.spacer1.AutoSize = true;
			this.spacer1.Location = new System.Drawing.Point(104, 0);
			this.spacer1.Name = "spacer1";
			this.spacer1.Size = new System.Drawing.Size(16, 15);
			this.spacer1.TabIndex = 4;
			this.spacer1.Text = "   ";
			// 
			// comboAllBrick
			// 
			this.comboAllBrick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllBrick.FormattingEnabled = true;
			this.comboAllBrick.Location = new System.Drawing.Point(126, 3);
			this.comboAllBrick.MaxDropDownItems = 20;
			this.comboAllBrick.Name = "comboAllBrick";
			this.comboAllBrick.Size = new System.Drawing.Size(190, 23);
			this.comboAllBrick.Sorted = true;
			this.comboAllBrick.TabIndex = 2;
			this.comboAllBrick.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// buttonConnectBrick
			// 
			this.buttonConnectBrick.AutoSize = true;
			this.buttonConnectBrick.Location = new System.Drawing.Point(322, 3);
			this.buttonConnectBrick.Name = "buttonConnectBrick";
			this.buttonConnectBrick.Size = new System.Drawing.Size(92, 25);
			this.buttonConnectBrick.TabIndex = 3;
			this.buttonConnectBrick.Text = "Connect...";
			this.buttonConnectBrick.UseVisualStyleBackColor = true;
			this.buttonConnectBrick.Click += new System.EventHandler(this.buttonConnectBrick_Click);
			// 
			// guidLabel0
			// 
			this.guidLabel0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.guidLabel0.AutoSize = true;
			this.guidLabel0.BackColor = System.Drawing.Color.Gold;
			this.guidLabel0.Location = new System.Drawing.Point(9, 181);
			this.guidLabel0.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.guidLabel0.Name = "guidLabel0";
			this.guidLabel0.Size = new System.Drawing.Size(138, 25);
			this.guidLabel0.TabIndex = 5;
			this.guidLabel0.Text = "GUID";
			this.guidLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textJoystickAlias0
			// 
			this.textJoystickAlias0.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textJoystickAlias0.Location = new System.Drawing.Point(165, 181);
			this.textJoystickAlias0.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.textJoystickAlias0.Name = "textJoystickAlias0";
			this.textJoystickAlias0.Size = new System.Drawing.Size(138, 23);
			this.textJoystickAlias0.TabIndex = 7;
			this.textJoystickAlias0.Text = "1";
			this.textJoystickAlias0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// comboType0
			// 
			this.comboType0.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboType0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType0.FormattingEnabled = true;
			this.comboType0.Location = new System.Drawing.Point(321, 181);
			this.comboType0.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.comboType0.Name = "comboType0";
			this.comboType0.Size = new System.Drawing.Size(138, 23);
			this.comboType0.TabIndex = 8;
			this.comboType0.SelectedIndexChanged += new System.EventHandler(this.comboType0_SelectedIndexChanged);
			// 
			// buttonUpdateJoysticks
			// 
			this.buttonUpdateJoysticks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUpdateJoysticks.AutoSize = true;
			this.buttonUpdateJoysticks.Location = new System.Drawing.Point(3, 144);
			this.buttonUpdateJoysticks.Name = "buttonUpdateJoysticks";
			this.buttonUpdateJoysticks.Size = new System.Drawing.Size(150, 25);
			this.buttonUpdateJoysticks.TabIndex = 2;
			this.buttonUpdateJoysticks.Text = "Update joysticks";
			this.buttonUpdateJoysticks.UseVisualStyleBackColor = true;
			this.buttonUpdateJoysticks.Click += new System.EventHandler(this.buttonUpdateJoysticks_Click);
			// 
			// buttonClearBricksFromPairs
			// 
			this.buttonClearBricksFromPairs.AutoSize = true;
			this.buttonClearBricksFromPairs.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonClearBricksFromPairs.Location = new System.Drawing.Point(471, 144);
			this.buttonClearBricksFromPairs.Name = "buttonClearBricksFromPairs";
			this.buttonClearBricksFromPairs.Size = new System.Drawing.Size(152, 25);
			this.buttonClearBricksFromPairs.TabIndex = 3;
			this.buttonClearBricksFromPairs.Text = "Clear brick selections";
			this.buttonClearBricksFromPairs.UseVisualStyleBackColor = true;
			this.buttonClearBricksFromPairs.Click += new System.EventHandler(this.buttonClearBricksFromPairs_Click);
			// 
			// tabMatch
			// 
			this.tabMatch.Controls.Add(this.tableLayoutPanel2);
			this.tabMatch.Location = new System.Drawing.Point(4, 24);
			this.tabMatch.Name = "tabMatch";
			this.tabMatch.Padding = new System.Windows.Forms.Padding(3);
			this.tabMatch.Size = new System.Drawing.Size(632, 444);
			this.tabMatch.TabIndex = 1;
			this.tabMatch.Text = "Match";
			this.tabMatch.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Controls.Add(this.checkShowChildWindow, 2, 9);
			this.tableLayoutPanel2.Controls.Add(this.textTeleTime, 2, 8);
			this.tableLayoutPanel2.Controls.Add(this.textAutoTime, 2, 7);
			this.tableLayoutPanel2.Controls.Add(this.checkPauseBetween, 0, 9);
			this.tableLayoutPanel2.Controls.Add(this.checkTeleoperated, 0, 8);
			this.tableLayoutPanel2.Controls.Add(this.checkAutonomous, 0, 7);
			this.tableLayoutPanel2.Controls.Add(this.buttonAbort, 2, 6);
			this.tableLayoutPanel2.Controls.Add(this.buttonStartPause, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.timerLabel, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.modeLabel, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 10;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(626, 438);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// checkShowChildWindow
			// 
			this.checkShowChildWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.checkShowChildWindow.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.checkShowChildWindow, 2);
			this.checkShowChildWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.checkShowChildWindow.Location = new System.Drawing.Point(321, 396);
			this.checkShowChildWindow.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.checkShowChildWindow.Name = "checkShowChildWindow";
			this.checkShowChildWindow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkShowChildWindow.Size = new System.Drawing.Size(296, 33);
			this.checkShowChildWindow.TabIndex = 42;
			this.checkShowChildWindow.Text = "Show/hide 2nd window";
			this.checkShowChildWindow.UseVisualStyleBackColor = true;
			this.checkShowChildWindow.CheckedChanged += new System.EventHandler(this.checkShowChildWindow_CheckedChanged);
			// 
			// textTeleTime
			// 
			this.textTeleTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textTeleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textTeleTime.Location = new System.Drawing.Point(321, 354);
			this.textTeleTime.Margin = new System.Windows.Forms.Padding(9, 9, 70, 9);
			this.textTeleTime.Name = "textTeleTime";
			this.textTeleTime.Size = new System.Drawing.Size(77, 23);
			this.textTeleTime.TabIndex = 41;
			this.textTeleTime.Text = "Y:YY";
			this.textTeleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textTeleTime.TextChanged += new System.EventHandler(this.textTeleTime_TextChanged);
			this.textTeleTime.Leave += new System.EventHandler(this.textTeleTime_Leave);
			// 
			// textAutoTime
			// 
			this.textAutoTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textAutoTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textAutoTime.Location = new System.Drawing.Point(321, 311);
			this.textAutoTime.Margin = new System.Windows.Forms.Padding(9, 9, 70, 9);
			this.textAutoTime.Name = "textAutoTime";
			this.textAutoTime.Size = new System.Drawing.Size(77, 23);
			this.textAutoTime.TabIndex = 40;
			this.textAutoTime.Text = "X:XX";
			this.textAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textAutoTime.TextChanged += new System.EventHandler(this.textAutoTime_TextChanged);
			this.textAutoTime.Leave += new System.EventHandler(this.textAutoTime_Leave);
			// 
			// checkPauseBetween
			// 
			this.checkPauseBetween.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.checkPauseBetween.AutoSize = true;
			this.checkPauseBetween.Checked = true;
			this.checkPauseBetween.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel2.SetColumnSpan(this.checkPauseBetween, 2);
			this.checkPauseBetween.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkPauseBetween.Location = new System.Drawing.Point(9, 396);
			this.checkPauseBetween.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.checkPauseBetween.Name = "checkPauseBetween";
			this.checkPauseBetween.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkPauseBetween.Size = new System.Drawing.Size(294, 33);
			this.checkPauseBetween.TabIndex = 39;
			this.checkPauseBetween.Text = "?Pause between periods";
			this.checkPauseBetween.UseVisualStyleBackColor = true;
			// 
			// checkTeleoperated
			// 
			this.checkTeleoperated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.checkTeleoperated.AutoSize = true;
			this.checkTeleoperated.Checked = true;
			this.checkTeleoperated.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel2.SetColumnSpan(this.checkTeleoperated, 2);
			this.checkTeleoperated.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkTeleoperated.Location = new System.Drawing.Point(9, 353);
			this.checkTeleoperated.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.checkTeleoperated.Name = "checkTeleoperated";
			this.checkTeleoperated.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkTeleoperated.Size = new System.Drawing.Size(294, 25);
			this.checkTeleoperated.TabIndex = 38;
			this.checkTeleoperated.Text = "Tele-operated time period";
			this.checkTeleoperated.UseVisualStyleBackColor = true;
			// 
			// checkAutonomous
			// 
			this.checkAutonomous.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.checkAutonomous.AutoSize = true;
			this.checkAutonomous.Checked = true;
			this.checkAutonomous.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel2.SetColumnSpan(this.checkAutonomous, 2);
			this.checkAutonomous.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkAutonomous.Location = new System.Drawing.Point(9, 310);
			this.checkAutonomous.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.checkAutonomous.Name = "checkAutonomous";
			this.checkAutonomous.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkAutonomous.Size = new System.Drawing.Size(294, 25);
			this.checkAutonomous.TabIndex = 37;
			this.checkAutonomous.Text = "Autonomous time period";
			this.checkAutonomous.UseVisualStyleBackColor = true;
			// 
			// buttonAbort
			// 
			this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAbort.AutoSize = true;
			this.buttonAbort.Location = new System.Drawing.Point(319, 266);
			this.buttonAbort.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.Size = new System.Drawing.Size(142, 27);
			this.buttonAbort.TabIndex = 36;
			this.buttonAbort.Text = "ABORT";
			this.buttonAbort.UseVisualStyleBackColor = true;
			this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
			// 
			// buttonStartPause
			// 
			this.buttonStartPause.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStartPause.AutoSize = true;
			this.buttonStartPause.Location = new System.Drawing.Point(163, 266);
			this.buttonStartPause.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
			this.buttonStartPause.Name = "buttonStartPause";
			this.buttonStartPause.Size = new System.Drawing.Size(142, 27);
			this.buttonStartPause.TabIndex = 35;
			this.buttonStartPause.Text = "Start / Pause";
			this.buttonStartPause.UseVisualStyleBackColor = true;
			this.buttonStartPause.Click += new System.EventHandler(this.buttonStartPause_Click);
			// 
			// timerLabel
			// 
			this.timerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.timerLabel.AutoSize = true;
			this.timerLabel.BackColor = System.Drawing.Color.White;
			this.timerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel2.SetColumnSpan(this.timerLabel, 4);
			this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 84F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.timerLabel.Location = new System.Drawing.Point(44, 95);
			this.timerLabel.Margin = new System.Windows.Forms.Padding(44, 9, 44, 9);
			this.timerLabel.Name = "timerLabel";
			this.timerLabel.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tableLayoutPanel2.SetRowSpan(this.timerLabel, 4);
			this.timerLabel.Size = new System.Drawing.Size(538, 154);
			this.timerLabel.TabIndex = 2;
			this.timerLabel.Text = "0:00";
			this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.label5, 2);
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.label5.Location = new System.Drawing.Point(159, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(306, 43);
			this.label5.TabIndex = 0;
			this.label5.Text = "Current Mode";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// modeLabel
			// 
			this.modeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.modeLabel.AutoSize = true;
			this.modeLabel.BackColor = System.Drawing.Color.DimGray;
			this.tableLayoutPanel2.SetColumnSpan(this.modeLabel, 2);
			this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.modeLabel.Location = new System.Drawing.Point(160, 48);
			this.modeLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.modeLabel.Name = "modeLabel";
			this.modeLabel.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.modeLabel.Size = new System.Drawing.Size(304, 33);
			this.modeLabel.TabIndex = 1;
			this.modeLabel.Text = "MODE";
			this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer
			// 
			this.timer.Interval = 50;
			this.timer.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 472);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainWindow";
			this.Text = "FMS v3 0.014";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabSetup.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tabMatch.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSetup;
		private System.Windows.Forms.TabPage tabMatch;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonUpdateBricks;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label spacer1;
		private System.Windows.Forms.Button buttonConnectBrick;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label guidLabel0;
		private System.Windows.Forms.ComboBox comboType2;
		private System.Windows.Forms.TextBox textJoystickAlias2;
		private System.Windows.Forms.Label guidLabel2;
		private System.Windows.Forms.ComboBox comboBrick1;
		private System.Windows.Forms.ComboBox comboType1;
		private System.Windows.Forms.TextBox textJoystickAlias1;
		private System.Windows.Forms.Label guidLabel1;
		private System.Windows.Forms.ComboBox comboBrick0;
		private System.Windows.Forms.ComboBox comboAllBrick;
		private System.Windows.Forms.Button buttonUpdateJoysticks;
		private System.Windows.Forms.Button buttonClearBricksFromPairs;
		private System.Windows.Forms.TextBox textJoystickAlias0;
		private System.Windows.Forms.ComboBox comboType0;
		private System.Windows.Forms.ComboBox comboBrick3;
		private System.Windows.Forms.ComboBox comboType3;
		private System.Windows.Forms.TextBox textJoystickAlias3;
		private System.Windows.Forms.Label guidLabel3;
		private System.Windows.Forms.ComboBox comboBrick2;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Button buttonToggleFreeDrive;
		private System.Windows.Forms.CheckBox checkBeepForButtonPressed;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label timerLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label modeLabel;
		private System.Windows.Forms.Button buttonAbort;
		private System.Windows.Forms.Button buttonStartPause;
		private System.Windows.Forms.TextBox textTeleTime;
		private System.Windows.Forms.TextBox textAutoTime;
		private System.Windows.Forms.CheckBox checkPauseBetween;
		private System.Windows.Forms.CheckBox checkTeleoperated;
		private System.Windows.Forms.CheckBox checkAutonomous;
		private System.Windows.Forms.CheckBox checkShowChildWindow;
	}
}

