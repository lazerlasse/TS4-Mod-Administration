using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private ObservableCollection<ProcessViewOutput> dataGridOutput = new ObservableCollection<ProcessViewOutput>();

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
			Progress<TS4ProgressReporter> importerProgress = new Progress<TS4ProgressReporter>();
			importerProgress.ProgressChanged += ImportProgress_ProgressChanged;
			TS4ProgressReporter importerProgressReporter = new TS4ProgressReporter();

			// Create new instance of ModsImporter and run async...
			ModsImporter modsImporter = new ModsImporter();
			await Task.Run(() => modsImporter.ImportMods(ModsIndexer.Instance.ModsToImportList, importerProgress, importerProgressReporter));
		}

		private void ImportProgress_ProgressChanged(object sender, TS4ProgressReporter e)
		{
			// Set progress text label and DataGrid...
			ProgressStatusText.Content = e.StatusMessage;
			dataGridOutput = new ObservableCollection<ProcessViewOutput>(e.DataGridContent);
			ModImportDataGrid.ItemsSource = dataGridOutput;

			// Set ProgressBar procent or animation...
			ProgressBar1.IsIndeterminate = e.LoadingProgress;

			if (e.LoadingProgress == false)
			{
				ProgressBar1.Value = e.ProgressPercentage;
			}
		}

		// Import browse path button event handler...
		private async void ImportBrowseButton_Click(object sender, RoutedEventArgs e)
		{
			// Create instance of Progress Reporter and Progress Change delegation...
			Progress<TS4ProgressReporter> importBrowserProgress = new Progress<TS4ProgressReporter>();
			TS4ProgressReporter importBrowserProgressReporter = new TS4ProgressReporter();
			importBrowserProgress.ProgressChanged += ImportBrowser_ProgressChanged;

			// Get folder to import from...
			browsePath = FolderBrowser();

			// Set browse path text...
			ImportSourcePatch.Text = browsePath;

			// Index and handle archived files before importing mods...
			await Task.Run(() => HandleArchiveFilesAsync(importBrowserProgress, importBrowserProgressReporter));

			// Index Mod files to import...
			await Task.Run(() => ModsIndexer.Instance.IndexModsToImport(browsePath, importBrowserProgress, importBrowserProgressReporter));

			// set progressbar indeterminate to false...
			ProgressBar1.IsIndeterminate = false;
		}

		// Index and handle archive files before import...
		private void HandleArchiveFilesAsync(Progress<TS4ProgressReporter> progress, TS4ProgressReporter progressReporter)
		{
			// Index files to uncompress...
			UncompressArchive uncompress = new UncompressArchive();
			uncompress.IndexFilesToUncompress(browsePath, progress, progressReporter);

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
						uncompress.UncompressArchiveFiles(progress, progressReporter);

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

		// ImportBrowser Progress Changed Event Handler...
		private void ImportBrowser_ProgressChanged(object sender, TS4ProgressReporter e)
		{
			ProgressStatusText.Content = e.StatusMessage;
			dataGridOutput = new ObservableCollection<ProcessViewOutput>(e.DataGridContent);
			ModImportDataGrid.ItemsSource = dataGridOutput;
			ProgressBar1.IsIndeterminate = e.LoadingProgress;

			if (e.LoadingProgress == false)
			{
				ProgressBar1.Value = e.ProgressPercentage;
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
			try
			{
				//var bindingList = new BindingList<ProcessViewOutput>(processOutput);
				if (dataGridOutput.Contains(processOutput))
				{
					int index = dataGridOutput.IndexOf(processOutput);
					dataGridOutput.Remove(processOutput);
					dataGridOutput.Insert(index, processOutput);
				}

				ModImportDataGrid.ItemsSource = dataGridOutput;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hov!!");
			}
		}

		// Clear DataGridView...
		private void ClearImportDataGridView()
		{
			ModImportDataGrid.ItemsSource = null;
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
