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
		#region Attributes and Main Section

		// Attributes...
		private string browsePath;

		// Initialize main window...
		public MainWindow()
		{
			InitializeComponent();
			ConflictSourcePatch.Text = SimsFolder.ModsFolderPath;
		}

		#endregion Attributes and Main Section

		#region Import Mods Section

		// Start importing mods button click event...
		private void ImportModsButton_Click(object sender, RoutedEventArgs e)
		{

		}

		// Import browse path button event handler...
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

			// Index and handle archived files before importing mods...
			HandleArchiveFiles();
		}

		// Index and handle archive files before import...
		private void HandleArchiveFiles()
		{
			// Index files to uncompress...
			UncompressArchive uncompress = new UncompressArchive();
			uncompress.IndexFilesToUncompress(browsePath);

			// Check if files are added...
			if (uncompress.FilesToUncompressCounter != 0)
			{
				// Show indexed files in DataGrid...
				foreach (FileInfo file in uncompress.FilesToUncompress)
				{
					// Create DataGrid output...
					UpdateImportDataGridView(new ProcessViewOutput
					{
						Package_Name = file.Name.Split('.')[0],
						Package_Type = file.Extension,
						Package_CreatedDate = file.CreationTime,
						Package_EditedDate = file.LastWriteTime
					});
				}

				// Show messagebox and get answer...
				MessageBoxResult result = MessageBox.Show(
						"Der blev fundet "
						+ uncompress.FilesToUncompressCounter
						+ " filer som kan udpakkes!!\nØnsker du at udpakke disse filer?",
						"Der blev fundet filer som kan udpakkes!",
						MessageBoxButton.YesNo);

				// If user answered yes unpack files...
				if (result == MessageBoxResult.Yes)
				{
					try
					{
						// Uncompress the files...
						uncompress.UncompressArchiveFiles();
					}
					catch (Exception ex)
					{
						MessageBox.Show("Fejlmeddelelse " + ex.Message, "Der opstod en fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
					}

					// Confirm unpacking succeded...
					MessageBox.Show("Filerne blev udpakket med succes.", "Færdig", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
		}

		#endregion Import Mods Section

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
	}
}
