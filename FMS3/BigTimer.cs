using System;
using System.Drawing;
using System.Windows.Forms;

namespace FMS3
{
	/*
	 * Child window to main FMS3 pane. Designed to be dragged to a 2nd monitor & maximized
	 * (i.e., providing an audience-facing panel of game information - mode, timer, etc.)
	 * 
	 * Special functionality resizes the fonts/etc. to match the size of the window
	 */
	public partial class BigTimer : Form
	{
		private float baseWinXsize;
		private float baseWinYsize;
		private float baseXyRatio;

		private float baseModeFontSize;
		private float baseTimerFontSize;

		public BigTimer()
		{
			InitializeComponent();

			baseWinXsize = ClientRectangle.Width;
			baseWinYsize = ClientRectangle.Height;
			baseXyRatio = baseWinXsize / baseWinYsize;

			baseModeFontSize = modeLabel.Font.Size;
			baseTimerFontSize = timerLabel.Font.Size;

			//Console.WriteLine("DEBUG: BigTimer() - " + baseWinXsize + "," + baseWinYsize + "~" + baseXyRatio + ";" + baseModeFontSize + "," + baseTimerFontSize);
		}

		// DNGN methods
		private void buttonMaximize_Click(object sender, EventArgs e) { }
		private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
		private void BigTimer_ResizeEnd(object sender, EventArgs e) { }

		// Change window state to/from maximized when check box is toggled
		private void checkMaximize_CheckedChanged(object sender, EventArgs e)
		{
			if (checkMaximize.Checked)
				this.WindowState = FormWindowState.Maximized;
			else
				this.WindowState = FormWindowState.Normal;
		}

		// Uses the window size to calculate what the various font sizes should be
		public void resizeFontsToWindow()
		{
			float newWinXsize = ClientRectangle.Width;
			float newWinYsize = ClientRectangle.Height;
			float newXyRatio = newWinXsize / newWinYsize;
			//Console.WriteLine("DEBUG: BigTimer_ResizeEnd() - " + newWinXsize + "," + newWinYsize + "~" + newXyRatio);

			// Use either the X-size or the Y-size to figure out the correct font size
			// (depending on if the window is tall but skinny, or if the window is very wide but short, etc.)
			float modRatio = 1;
			if (newXyRatio > baseXyRatio)
			{
				modRatio = newWinYsize / baseWinYsize;
				//Console.WriteLine("DEBUG: BigTimer_ResizeEnd() - using Y sizes, mod=" + modRatio);
			}
			else
			{
				modRatio = newWinXsize / baseWinXsize;
				//Console.WriteLine("DEBUG: BigTimer_ResizeEnd() - using X sizes, mod=" + modRatio);
			}

			// 2014-01-20, M.O'C: Need this check here for Windows 7 startup
			float modeEmSize = baseModeFontSize * modRatio;
			float timerEmSize = baseTimerFontSize * modRatio;
			if (float.IsNaN(modeEmSize) || float.IsNaN(timerEmSize))
				return;

			// Adjust the fonts to new sizes
			modeLabel.Font = new Font(modeLabel.Font.FontFamily, modeEmSize, modeLabel.Font.Style);
			timerLabel.Font = new Font(timerLabel.Font.FontFamily, timerEmSize, timerLabel.Font.Style);
		}

		// If the window size gets changed, this triggers resizing the fonts
		private void BigTimer_SizeChanged(object sender, EventArgs e)
		{
			resizeFontsToWindow();
		}
	}
}
