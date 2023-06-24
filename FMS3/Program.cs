using System;
using System.Windows.Forms;

namespace FMS3
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// M.O'C - Code to pop a dialog box with a stack trace if the app encounters a fatal unhandled exception [part 1]
			AppDomain.CurrentDomain.UnhandledException +=
				new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			BrickManager.getInstance().getBrickByName("COM3", false, true);

			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				// M.O'C - Code to pop a dialog box with a stack trace if the app encounters a fatal unhandled exception [part 2]
				Exception ex = (Exception)e.ExceptionObject;

				MessageBox.Show("Whoops! Please contact the developers with the following"
					  + " information:\n\n" + ex.Message + ex.StackTrace,
					  "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			finally
			{
				Application.Exit();
			}
		}
	}
}
