using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
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
		List<ProcessViewOutput> processViews = new List<ProcessViewOutput>();

		// Initialize main window...
		public MainWindow()
		{
			InitializeComponent();
			ConflictSourcePatch.Text = SimsFolder.ModsFolderPath;
		}

		#endregion Attributes and Main Section

		#region Import Mods Section

		// Start importing mods button async click event...
		private async void ImportModsButton_Click(object sender, RoutedEventArgs e)
		{
			// Create new instance of progress reporter functions...
			Progress<TS4ProgressReporter> importProgress = new Progress<TS4ProgressReporter>();
			importProgress.ProgressChanged += ImportProgress_ProgressChanged;
			TS4ProgressReporter importProgressReporter = new TS4ProgressReporter();

			// Create new instance of ModsIndexer and run async...
			ModsIndexer modsIndexer = new ModsIndexer();
			await Task.Run(() => modsIndexer.IndexModsToImport(browsePath, importProgress, importProgressReporter));

			// Create new instance of ModsImporter and run async...
			ModsImporter modsImporter = new ModsImporter();
			await Task.Run(() => modsImporter.ImportMods(modsIndexer.ModsToImport, importProgress, importProgressReporter));
		}

		private void ImportProgress_ProgressChanged(object sender, TS4ProgressReporter e)
		{
			// Set progress text label and DataGrid...
			UpdateProgressTextLabel(e.StatusMessage);
			UpdateImportDataGridView(e.DataGridContent);

			// Set ProgressBar procent or animation...
			if (e.ProgressPercentage < 1)
			{
				ProgressBar1.IsIndeterminate = true;
			}
			else
			{
				ProgressBar1.Value = e.ProgressPercentage;
			}
		}

		// Import browse path button event handler...
		private void ImportBrowseButton_Click(object sender, RoutedEventArgs e)
		{
			// Clear DataGrid...
			processViews.Clear();

			// Get folder to import from...
			browsePath = FolderBrowser();

			// Set browse path text and enable import button...
			ImportSourcePatch.Text = browsePath;
			ImportModsButton.IsEnabled = true;

			// Index and handle archived files before importing mods...
			HandleArchiveFilesAsync();
		}

		// Index and handle archive files before import...
		private async void HandleArchiveFilesAsync()
		{
			// Create instance of Progress Reporter and Progress Change delegation...
			Progress<TS4ProgressReporter> uncompressProgress = new Progress<TS4ProgressReporter>();
			TS4ProgressReporter uncompressProgressReporter = new TS4ProgressReporter();
			uncompressProgress.ProgressChanged += Uncompress_ProgressChanged;

			// Index files to uncompress...
			UncompressArchive uncompress = new UncompressArchive();
			await Task.Run(() => uncompress.IndexFilesToUncompress(browsePath, uncompressProgress, uncompressProgressReporter));

			// Check if files are added...
			if (uncompress.FilesToUncompressCounter != 0)
			{
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
						await Task.Run(() => uncompress.UncompressArchiveFiles(uncompressProgress, uncompressProgressReporter));

						// Confirm unpacking succeded...
						MessageBox.Show("Filerne blev udpakket med succes.", "Færdig", MessageBoxButton.OK, MessageBoxImage.Information);
					}

					catch (Exception ex)
					{
						MessageBox.Show("Fejlmeddelelse " + ex.Message, "Der opstod en fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		private void Uncompress_ProgressChanged(object sender, TS4ProgressReporter e)
		{
			UpdateProgressBar1(e.ProgressPercentage);
			UpdateProgressTextLabel(e.StatusMessage);
			UpdateImportDataGridView(e.DataGridContent);
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
		private void UpdateImportDataGridView(List<ProcessViewOutput> processOutput)
		{
			var bindingList = new BindingList<ProcessViewOutput>(processOutput);
			ModImportDataGrid.ItemsSource = bindingList;
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

		#region Extensions and features (FolderBrowser and more)

		private string FolderBrowser()
		{
			// Create directory-browser dialog...
			System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
			System.Windows.Forms.DialogResult result = dialog.ShowDialog();

			// Check browser dialog result..
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				// Get source patch and update patch textbox...
				 return dialog.SelectedPath;
			}
			else
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
		}

		#endregion Extensions and features (FolderBrowser and more)
	}
}
