namespace FMS3
{
	partial class BigTimer
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.checkMaximize = new System.Windows.Forms.CheckBox();
			this.timerLabel = new System.Windows.Forms.Label();
			this.modeLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.Controls.Add(this.checkMaximize, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.timerLabel, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.modeLabel, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 498);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// checkMaximize
			// 
			this.checkMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkMaximize.AutoSize = true;
			this.checkMaximize.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.tableLayoutPanel1.SetColumnSpan(this.checkMaximize, 2);
			this.checkMaximize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkMaximize.Location = new System.Drawing.Point(592, 3);
			this.checkMaximize.Name = "checkMaximize";
			this.checkMaximize.Size = new System.Drawing.Size(87, 18);
			this.checkMaximize.TabIndex = 5;
			this.checkMaximize.Text = "Maximize";
			this.checkMaximize.UseVisualStyleBackColor = true;
			this.checkMaximize.CheckedChanged += new System.EventHandler(this.checkMaximize_CheckedChanged);
			// 
			// timerLabel
			// 
			this.timerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.timerLabel.AutoSize = true;
			this.timerLabel.BackColor = System.Drawing.Color.White;
			this.timerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.timerLabel, 2);
			this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 116F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerLabel.Location = new System.Drawing.Point(68, 122);
			this.timerLabel.Margin = new System.Windows.Forms.Padding(0);
			this.timerLabel.Name = "timerLabel";
			this.timerLabel.Padding = new System.Windows.Forms.Padding(5);
			this.timerLabel.Size = new System.Drawing.Size(544, 249);
			this.timerLabel.TabIndex = 3;
			this.timerLabel.Text = "0:00";
			this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// modeLabel
			// 
			this.modeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.modeLabel.AutoSize = true;
			this.modeLabel.BackColor = System.Drawing.Color.DimGray;
			this.tableLayoutPanel1.SetColumnSpan(this.modeLabel, 2);
			this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.modeLabel.Location = new System.Drawing.Point(73, 29);
			this.modeLabel.Margin = new System.Windows.Forms.Padding(5);
			this.modeLabel.Name = "modeLabel";
			this.modeLabel.Padding = new System.Windows.Forms.Padding(5);
			this.modeLabel.Size = new System.Drawing.Size(534, 64);
			this.modeLabel.TabIndex = 2;
			this.modeLabel.Text = "MODE";
			this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BigTimer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 498);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "BigTimer";
			this.ShowIcon = false;
			this.Text = "Match Information";
			this.ResizeEnd += new System.EventHandler(this.BigTimer_ResizeEnd);
			this.SizeChanged += new System.EventHandler(this.BigTimer_SizeChanged);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		public System.Windows.Forms.Label modeLabel;
		public System.Windows.Forms.Label timerLabel;
		private System.Windows.Forms.CheckBox checkMaximize;

	}
}