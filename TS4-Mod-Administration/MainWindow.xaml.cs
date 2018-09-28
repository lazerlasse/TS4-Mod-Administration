using System;
using System.IO;
using System.Windows;

namespace TS4_Mod_Administration
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string modsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\The Sims 4\Mods";
		private bool modsFolderÉxist = false;
		private string browsePath;

		public MainWindow()
		{
			InitializeComponent();
			ConflictSourcePatch.Text = modsFolderPath;
		}
         
		#region Import Buttons

		// Start importing mods button click event...
		private void ImportModsButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ImportBrowseButton_Click(object sender, RoutedEventArgs e)
		{
			// Create directory-browser dialog...
			System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
			System.Windows.Forms.DialogResult result = dialog.ShowDialog();

			// Check browser dialog result..
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				// Get source patch and update patch textbox...
				browsePath = dialog.SelectedPath;
				ImportSourcePatch.Text = browsePath;
				ImportModsButton.IsEnabled = true;
			}
		}

		#endregion Import Buttons

		#region Conflict Detection Buttons

		// Start scanning for conflicts button click event...
		private void ConflictScanButton_Click(object sender, RoutedEventArgs e)
		{

		}

		// Browse folder for conflict scanning button click event...
		private void ConflictBrowseButton_Click(object sender, RoutedEventArgs e)
		{

		}

		#endregion Conflict Detection Buttons

		#region Update GUI Text, Datagrid and Progressbar.

		// Update log viewer text.
		private void UpdateImportDataGridView(ProcessViewOutput processOutput)
		{
			ModImportDataGrid.Items.Add(processOutput);
		}

		// Update ProgressBar1...
		private void UpdateProgressBar1(double Value)
		{
			ProgressBar1.Value = Value;
		}

		// Update Progress Text Label...
		private void UpdateProgressTextLabel(string ProgressText)
		{
			ProgressTextBox.Content = ProgressText;
		}

		#endregion Update GUI Text, Datagrid and Progressbar.

		#region Validation section

		private void CheckModsFolderExist()
		{
			if (Directory.Exists(modsFolderPath))
			{
				modsFolderÉxist = true;
			}
			else
			{
				modsFolderÉxist = false;
				modsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
		}

		#endregion Validation section
	}
}
