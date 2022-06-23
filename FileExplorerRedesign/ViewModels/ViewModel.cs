using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using FileExplorerRedesign.Annotations;
using FileExplorerRedesign.Commands;
using FileExplorerRedesign.Models;
using FileExplorerRedesign.Views;
using FileExplorerRedesign.Views.Clase;
using Microsoft.VisualBasic.FileIO;
using Syroot.Windows.IO;
using SearchOption = System.IO.SearchOption;

namespace FileExplorerRedesign.ViewModels
{
    public sealed class ViewModel : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        readonly ResourceDictionary _iconDictionary =
            Application.LoadComponent(new Uri("/FileExplorerRedesign;component/Resources/Icons.xaml",
                UriKind.RelativeOrAbsolute)) as ResourceDictionary;

        public string ParentDirectory { get; set; }
        public string PreviousDirectory { get; set; }
        public string CurrentDirectory { get; set; }
        public string NextDirectory { get; set; }
        public string SelectedDriveSize { get; set; }
        public string SelectedFolderDetails { get; set; }
        public string NewFolderName { get; set; }
        public bool IsListView { get; set; }
        public string DriveSize { get; set; }
        public bool IsInSearchMode { get; set; }

        public ObservableCollection<FileDetailsModel> FavoriteFolders { get; set; }
        public ObservableCollection<FileDetailsModel> RemoteFolders { get; set; }
        public ObservableCollection<FileDetailsModel> LibraryFolders { get; set; }
        public ObservableCollection<FileDetailsModel> ConnectedDevices { get; set; }
        public ObservableCollection<FileDetailsModel> NavigatedFolderFiles { get; set; }
        public ObservableCollection<SubMenuItemDetails> HomeTabSubMenuCollection { get; set; }
        public ObservableCollection<SubMenuItemDetails> ViewTabSubMenuCollection { get; set; }
        public ObservableCollection<FileDetailsModel> ClipBoardCollection { get; set; }


        public ObservableCollection<string> PathHistoryCollection { get; set; }
        internal int position = 0;
        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }
        public bool IsAtRootDirectory { get; set; }
        public bool IsMoveOperation { get; set; }

        internal bool _pathDisrupted;

        public bool PathDisrupted
        {
            get => _pathDisrupted;
            set
            {
                _pathDisrupted = value;
                if (_pathDisrupted)
                {
                    var tempCollection = new ObservableCollection<string>();
                    for (int i = position; i < PathHistoryCollection.Count - 1; i++)
                    {
                        tempCollection.Add(PathHistoryCollection[i]);
                    }

                    foreach (var path in tempCollection)
                    {
                        PathHistoryCollection.Remove(path);
                    }

                    OnPropertyChanged(nameof(PathHistoryCollection));
                    _pathDisrupted = false;
                }
            }
        }

        internal ReadOnlyCollection<string> tempFolderCollection;

        private BackgroundWorker bgGetFilesBackgroundWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        private BackgroundWorker bgGetSizeBackgroundWorker = new BackgroundWorker()
        {
            WorkerSupportsCancellation = true
        };

        internal BackgroundWorker bgGetFoundFilesWorker = new BackgroundWorker()
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true
        };

        #endregion

        #region Functions

        internal bool IsFileHidden(string fileName)
        {
            var attr = FileAttributes.Normal;
            try
            {
                attr = File.GetAttributes(fileName);
            }
            catch
            {
                // ignored
            }

            return attr.HasFlag(FileAttributes.Hidden);
        }

        internal bool IsFileReadOnly(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return (FileSystem.GetDirectoryInfo(path).Attributes & FileAttributes.ReadOnly) != 0;
                return (FileSystem.GetFileInfo(path).Attributes & FileAttributes.ReadOnly) != 0;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
        }

        internal bool IsDirectory(string fileName)
        {
            var attr = FileAttributes.Normal;
            try
            {
                attr = File.GetAttributes(fileName);
            }
            catch
            {
                // ignored
            }

            return attr.HasFlag(FileAttributes.Directory);
        }

        internal string GetFileExtension(string fileName)
        {
            if (fileName == null) return string.Empty;
            var extension = Path.GetExtension(fileName);
            var CultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = CultureInfo.TextInfo;
            var data = textInfo.ToTitleCase(extension.Replace(".", string.Empty));
            return data;
        }

        internal static readonly List<string> ImageExtensions = new List<string>()
        {
            ".jpg",
            ".jpeg",
            ".bmp",
            ".gif",
            ".png"
        };

        internal static readonly List<string> VideoExtensions = new List<string>()
        {
            ".mp4",
            ".m4v",
            ".mov",
            ".wmv",
            ".avi",
            ".avchd",
            ".flv",
            ".f4v",
            ".swf",
            ".mkv",
            ".webm",
        };

        internal PathGeometry GetImageForExtension(FileDetailsModel file)
        {
            var fileExtension = file.FileExtension;
            if (Directory.Exists(file.Path))
                return (PathGeometry) _iconDictionary["Folder"];

            if (file.IsImage)
                return (PathGeometry) _iconDictionary["ImageFile"];

            if (file.IsVideo)
                return (PathGeometry) _iconDictionary["VideoFile"];

            if ((PathGeometry) _iconDictionary[$"{fileExtension}File"] == null)
                return (PathGeometry) _iconDictionary["File"];

            return (PathGeometry) _iconDictionary[$"{fileExtension}File"];
        }

        void LoadDirectory(FileDetailsModel fileDetailsModel)
        {
            CanGoBack = position != 0;
            OnPropertyChanged(nameof(CanGoBack));

            NavigatedFolderFiles.Clear();
            tempFolderCollection = null;

            DriveSize = CalculateSize(new DriveInfo(fileDetailsModel.Path).TotalSize);
            OnPropertyChanged(nameof(DriveSize));

            if (PathHistoryCollection != null && position > 0)
            {
                if (PathHistoryCollection.ElementAt(position) != fileDetailsModel.Path)
                    PathDisrupted = true;
            }

            if (bgGetFilesBackgroundWorker.IsBusy)
                bgGetFilesBackgroundWorker.CancelAsync();

            bgGetFilesBackgroundWorker.RunWorkerAsync(fileDetailsModel);
        }

        private void BgGetFilesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var fileOrFolder = (FileDetailsModel) e.Argument;

            tempFolderCollection =
                new ReadOnlyCollectionBuilder<string>(FileSystem.GetDirectories(fileOrFolder.Path)
                    .Concat(FileSystem.GetFiles(fileOrFolder.Path))).ToReadOnlyCollection();

            foreach (var filename in tempFolderCollection)
                bgGetFilesBackgroundWorker.ReportProgress(1, filename);

            CurrentDirectory = fileOrFolder.Path;
            OnPropertyChanged(nameof(CurrentDirectory));

            var root = Path.GetPathRoot(fileOrFolder.Path);
            if (string.IsNullOrWhiteSpace(CurrentDirectory)
                || CurrentDirectory == root)
            {
                IsAtRootDirectory = true;
                OnPropertyChanged(nameof(IsAtRootDirectory));
            }
            else
            {
                IsAtRootDirectory = false;
                OnPropertyChanged(nameof(IsAtRootDirectory));
            }
        }

        private void BgGetFilesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var fileName = e.UserState.ToString();
            var file = new FileDetailsModel();
            file.Name = Path.GetFileName(fileName);
            file.Path = fileName;
            file.CreatedOn = GetCreatedOn(fileName);
            file.DateModified = GetDateModified(fileName);
            file.AccessedOn = GetLastAccessedOn(fileName);
            file.IsHidden = IsFileHidden(fileName);
            file.IsReadOnly = IsFileReadOnly(fileName);
            file.IsDirectory = IsDirectory(fileName);
            file.FileExtension = GetFileExtension(fileName);
            file.IsImage = ImageExtensions.Contains(file.FileExtension.ToLower());
            file.IsVideo = VideoExtensions.Contains(file.FileExtension.ToLower());
            file.FileIcon = GetImageForExtension(file);

            NavigatedFolderFiles.Add(file);
            OnPropertyChanged(nameof(NavigatedFolderFiles));
        }

        private void BgGetFilesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var file in NavigatedFolderFiles)
            {
                var subWorker = new BackgroundWorker();
                subWorker.DoWork += (o, args) => { file.Size = CalculateSize(GetDirectorySize(file.Path)); };
                subWorker.RunWorkerCompleted += (o, args) =>
                {
                    subWorker.Dispose();
                    CollectionViewSource.GetDefaultView(NavigatedFolderFiles).Refresh();
                };
                subWorker.RunWorkerAsync();
            }
        }

        internal string CalculateSize(long bytes)
        {
            var suffix = new[] {"B", "KB", "MB", "GB", "TB"};
            float byteNumber = bytes;
            for (var i = 0; i < suffix.Length; i++)
            {
                if (byteNumber < 1000)
                {
                    if (i == 0)
                        return $"{byteNumber} {suffix[i]}";
                    else
                        return $"{byteNumber:0.#0} {suffix[i]}";
                }
                else
                {
                    byteNumber /= 1024;
                }
            }

            return $"{byteNumber:N} {suffix[suffix.Length - 1]}";
        }

        internal static long GetDirectorySize(string directoryPath)
        {
            try
            {
                if (FileSystem.DirectoryExists(directoryPath))
                {
                    var d = new DirectoryInfo(directoryPath);
                    return d.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                }

                return new FileInfo(directoryPath).Length;
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }
            catch (FileNotFoundException)
            {
                return 0;
            }
            catch (DirectoryNotFoundException)
            {
                return 0;
            }
        }

        private void BgGetSizeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var Size = NavigatedFolderFiles.Where(File => File.IsSelected && !File.IsDirectory)
                .Sum(x => new FileInfo(x.Path).Length);

            SelectedFolderDetails = CalculateSize(Size);
            OnPropertyChanged(nameof(SelectedFolderDetails));

            var Directories = NavigatedFolderFiles.Where(directory => directory.IsSelected && directory.IsDirectory);
            try
            {
                foreach (var directory in Directories)
                {
                    Size += GetDirectorySize(directory.Path);
                    SelectedFolderDetails = CalculateSize(Size);
                    OnPropertyChanged(nameof(SelectedFolderDetails));
                }
            }
            catch (InvalidOperationException)
            {
                //ignored
            }
        }

        internal void UpdatePathHistory(string path)
        {
            if (PathHistoryCollection != null && !string.IsNullOrEmpty(path))
            {
                PathHistoryCollection.Add(path);
                position++;
                OnPropertyChanged(nameof(PathHistoryCollection));
            }
        }

        internal void PinFolder()
        {
            if (FavoriteFolders == null)
                FavoriteFolders = new ObservableCollection<FileDetailsModel>();

            try
            {
                var selectedFile =
                    NavigatedFolderFiles.Where(folder => folder.IsSelected && !folder.IsPinned && folder.IsDirectory);

                foreach (var directory in selectedFile)
                {
                    directory.IsPinned = true;
                    FavoriteFolders.Add(directory);
                    OnPropertyChanged(nameof(FavoriteFolders));
                }
            }
            catch
            {
                //ignored
            }
        }

        internal void Copy()
        {
            if (ClipBoardCollection == null)
                ClipBoardCollection = new ObservableCollection<FileDetailsModel>();
            ClipBoardCollection.Clear();

            var selectedFiles = NavigatedFolderFiles.Where(file => file.IsSelected);
            foreach (var file in selectedFiles)
            {
                if (!ClipBoardCollection.Contains(file))
                    ClipBoardCollection.Add(file);
            }

            OnPropertyChanged(nameof(ClipBoardCollection));
            IsMoveOperation = false;
        }

        internal void Cut()
        {
            if (ClipBoardCollection == null)
                ClipBoardCollection = new ObservableCollection<FileDetailsModel>();
            ClipBoardCollection.Clear();

            var selectedFiles = NavigatedFolderFiles.Where(file => file.IsSelected);
            foreach (var file in selectedFiles)
            {
                if (!ClipBoardCollection.Contains(file))
                    ClipBoardCollection.Add(file);
            }

            OnPropertyChanged(nameof(ClipBoardCollection));
            IsMoveOperation = true;
        }

        internal void Paste(bool IsMoveOperation)
        {
            if (ClipBoardCollection != null && ClipBoardCollection.Count > 0)
            {
                var destinationPath = CurrentDirectory;
                if (!IsMoveOperation)
                {
                    foreach (var file in ClipBoardCollection)
                    {
                        var sourcePath = file.Path;
                        var destPath = CurrentDirectory + "\\" + file.Name;
                        destPath = Path.Combine(sourcePath, destPath);
                        var temp = Path.GetExtension(file.Path);

                        if (string.IsNullOrWhiteSpace(temp))
                            FileSystem.CopyDirectory(file.Path, destPath, UIOption.AllDialogs,
                                UICancelOption.DoNothing);
                        else
                            FileSystem.CopyFile(file.Path, destPath, UIOption.AllDialogs, UICancelOption.DoNothing);
                    }
                }
                else
                {
                    foreach (var file in ClipBoardCollection)
                    {
                        var sourcePath = file.Path;
                        var destPath = CurrentDirectory + "\\" + file.Name;
                        destPath = Path.Combine(sourcePath, destPath);
                        var temp = Path.GetExtension(file.Path);

                        if (string.IsNullOrWhiteSpace(temp))
                            FileSystem.MoveDirectory(file.Path, destPath, UIOption.AllDialogs,
                                UICancelOption.DoNothing);
                        else
                            FileSystem.MoveFile(file.Path, destPath, UIOption.AllDialogs, UICancelOption.DoNothing);
                    }
                }

                LoadDirectory(new FileDetailsModel()
                {
                    Path = destinationPath
                });
                IsMoveOperation = false;
            }
        }

        internal void Delete()
        {
            var selectedFiles = NavigatedFolderFiles.Where(file => file.IsSelected);
            if (selectedFiles.Count() > 1)
            {
                if (MessageBoxResult.Yes ==
                    MessageBox.Show(
                        $"Are you sure that you want to move these {selectedFiles.Count()} items to the Recycle Bin?",
                        "Delete Multiple Items", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
                {
                    foreach (var file in selectedFiles)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(Path.GetExtension(file.Path)))
                            {
                                FileSystem.DeleteDirectory(file.Path ?? string.Empty, UIOption.OnlyErrorDialogs,
                                    RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                            }
                            else
                            {
                                FileSystem.DeleteFile(file.Path, UIOption.OnlyErrorDialogs,
                                    RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                            }
                        }
                        catch
                        {
                            //Ignored
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(selectedFiles.ElementAt(0).Path))
                {
                    FileSystem.DeleteDirectory(selectedFiles.ElementAt(0).Path, UIOption.OnlyErrorDialogs,
                        RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                }
                else
                {
                    FileSystem.DeleteFile(selectedFiles.ElementAt(0).Path, UIOption.OnlyErrorDialogs,
                        RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                }
            }

            LoadDirectory(new FileDetailsModel() {Path = CurrentDirectory});
        }


        
        public void Rename()
        {
            var selectedFiles =
                new ObservableCollection<FileDetailsModel>(NavigatedFolderFiles.Where(x => x.IsSelected));
            foreach (var file in selectedFiles)
            {
                if (file.IsSelected)
                {
                    restart:
                    try
                    {
                        new RenameDialog()
                        {
                            DataContext = this,
                            Owner = Application.Current.MainWindow,
                            ShowActivated = true,
                            ShowInTaskbar = false,
                            Topmost = true,
                            OldFolderName = $"Renaming: {file.Name}"
                        }.ShowDialog();
                        
                        if (!string.IsNullOrWhiteSpace(NewFolderName))
                        {
                            if (file.IsDirectory)
                                FileSystem.RenameDirectory(file.Path, NewFolderName);
                            else
                                FileSystem.RenameFile(file.Path, $"{NewFolderName}.{file.FileExtension.ToLower()}");
                            file.Name = NewFolderName;
                            file.IsSelected = false;

                            NavigatedFolderFiles.Remove(file);
                            OnPropertyChanged(nameof(NavigatedFolderFiles));
                            NavigatedFolderFiles.Add(file);
                            OnPropertyChanged(nameof(NavigatedFolderFiles));

                            NewFolderName = string.Empty;
                            OnPropertyChanged(nameof(NewFolderName));
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        goto restart;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.Source);
                    }
                }
            }
        }

        internal void CreateNewFolder()
        {
            CreateNewFolderCommand.Execute(null);
        }

        internal static string GetCreatedOn(string path)
        {
            try
            {
                if (FileSystem.DirectoryExists(path))
                {
                    return
                        $"{FileSystem.GetDirectoryInfo(path).CreationTime.ToShortDateString()} {FileSystem.GetDirectoryInfo(path).CreationTime.ToShortTimeString()}";
                }

                return
                    $"{FileSystem.GetFileInfo(path).CreationTime.ToShortDateString()} {FileSystem.GetFileInfo(path).CreationTime.ToShortTimeString()}";
            }
            catch (UnauthorizedAccessException)
            {
                return String.Empty;
            }
            catch (FileNotFoundException)
            {
                return String.Empty;
            }
            catch (DirectoryNotFoundException)
            {
                return String.Empty;
            }
        }

        internal static string GetDateModified(string path)
        {
            try
            {
                if (FileSystem.DirectoryExists(path))
                {
                    return
                        $"{FileSystem.GetDirectoryInfo(path).LastWriteTime.ToShortDateString()} {FileSystem.GetDirectoryInfo(path).LastWriteTime.ToShortTimeString()}";
                }

                return
                    $"{FileSystem.GetFileInfo(path).LastWriteTime.ToShortDateString()} {FileSystem.GetFileInfo(path).LastWriteTime.ToShortTimeString()}";
            }
            catch (UnauthorizedAccessException)
            {
                return String.Empty;
            }
            catch (FileNotFoundException)
            {
                return String.Empty;
            }
            catch (DirectoryNotFoundException)
            {
                return String.Empty;
            }
        }

        internal static string GetLastAccessedOn(string path)
        {
            try
            {
                if (FileSystem.DirectoryExists(path))
                {
                    return
                        $"{FileSystem.GetDirectoryInfo(path).LastAccessTime.ToShortDateString()} {FileSystem.GetDirectoryInfo(path).LastAccessTime.ToShortTimeString()}";
                }

                return
                    $"{FileSystem.GetFileInfo(path).LastAccessTime.ToShortDateString()} {FileSystem.GetFileInfo(path).LastAccessTime.ToShortTimeString()}";
            }
            catch (UnauthorizedAccessException)
            {
                return String.Empty;
            }
            catch (FileNotFoundException)
            {
                return String.Empty;
            }
            catch (DirectoryNotFoundException)
            {
                return String.Empty;
            }
        }

        internal void ShowProperties()
        {
            if (NavigatedFolderFiles.Count(file => file.IsSelected) == 1)
            {
                var f = NavigatedFolderFiles.Where(file => file.IsSelected).ToArray();
                new PropertiesDialog()
                {
                    FileName = f[0].Name,
                    Icon = f[0].FileIcon,
                    FileExtension = f[0].FileExtension,
                    FullPath = f[0].Path,
                    Size = CalculateSize(GetDirectorySize(f[0].Path)),
                    CreatedOn = GetCreatedOn(f[0].Path),
                    DateModified = GetDateModified(f[0].Path),
                    AccessedOn = GetLastAccessedOn(f[0].Path),
                    IsReadOnly = f[0].IsReadOnly,
                    IsHidden = f[0].IsHidden,
                    Owner = Application.Current.MainWindow,
                    ShowInTaskbar = false,
                    Topmost = true
                }.ShowDialog();
            }
        }

        internal static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern,
            SearchOption searchOption)
        {
            try
            {
                var directories = Enumerable.Empty<string>();
                if (searchOption == SearchOption.AllDirectories)
                    directories = Directory.EnumerateDirectories(parentDirectory)
                        .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOption));
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
            catch (FileNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }

        internal static IEnumerable<string> EnumerateFiles(string path, string searchPattern,
            SearchOption searchOption)
        {
            try
            {
                var dirFiles = Enumerable.Empty<string>();
                if (searchOption == SearchOption.AllDirectories)
                    dirFiles = Directory.EnumerateDirectories(path)
                        .SelectMany(x => EnumerateFiles(x, searchPattern, searchOption));
                return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
            catch (FileNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }

        public ViewModel()
        {
            RemoteFolders = new ObservableCollection<FileDetailsModel>()
            {
                new FileDetailsModel()
                {
                    Name = "OneDrive",
                    IsDirectory = true,
                    Path = Environment.GetEnvironmentVariable("OneDriveConsumer"),
                    FileIcon = (PathGeometry) _iconDictionary["OneDrive"]
                },
                new FileDetailsModel()
                {
                    Name = "Google Drive",
                    IsDirectory = true,
                    Path = "",
                    FileIcon = (PathGeometry) _iconDictionary["GoogleDrive"]
                },
                new FileDetailsModel()
                {
                    Name = "Dropbox",
                    IsDirectory = true,
                    Path = "",
                    FileIcon = (PathGeometry) _iconDictionary["Dropbox"]
                }
            };

            LibraryFolders = new ObservableCollection<FileDetailsModel>()
            {
                new FileDetailsModel()
                {
                    Name = "Desktop",
                    IsDirectory = true,
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    FileIcon = (PathGeometry) _iconDictionary["DesktopFolder"]
                },
                new FileDetailsModel()
                {
                    Name = "Documents",
                    IsDirectory = true,
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    FileIcon = (PathGeometry) _iconDictionary["DocumentsFolder"]
                },

                new FileDetailsModel()
                {
                    Name = "Downloads",
                    IsDirectory = true,
                    Path = new KnownFolder(KnownFolderType.Downloads).Path,
                    FileIcon = (PathGeometry) _iconDictionary["DownloadsFolder"]
                },
                new FileDetailsModel()
                {
                    Name = "Pictures",
                    IsDirectory = true,
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    FileIcon = (PathGeometry) _iconDictionary["PicturesFolder"]
                },
                new FileDetailsModel()
                {
                    Name = "Videos",
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                    IsDirectory = true,
                    FileIcon = (PathGeometry) _iconDictionary["VideosFolder"]
                },
                new FileDetailsModel()
                {
                    Name = "Music",
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                    IsDirectory = true,
                    FileIcon = (PathGeometry) _iconDictionary["MusicsFolder"]
                }
            };

            ConnectedDevices = new ObservableCollection<FileDetailsModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                var name = string.IsNullOrEmpty(drive.VolumeLabel) ? "Local Disk" : drive.VolumeLabel;
                ConnectedDevices.Add(new FileDetailsModel()
                {
                    Name = $"{name}({drive.Name.Replace(@"\", "")})",
                    Path = drive.RootDirectory.FullName,
                    IsDirectory = true,
                    FileIcon = drive.Name.Contains("C:")
                        ? (PathGeometry) _iconDictionary["CDrive"]
                        : (PathGeometry) _iconDictionary["NormalDrive"]
                });
            }

            LoadSubMenuCollectionsCommand.Execute(null);

            CurrentDirectory = @"C:\";
            OnPropertyChanged(nameof(CurrentDirectory));

            NavigatedFolderFiles = new ObservableCollection<FileDetailsModel>();
            bgGetFilesBackgroundWorker.DoWork += BgGetFilesBackgroundWorker_DoWork;
            bgGetFilesBackgroundWorker.ProgressChanged += BgGetFilesBackgroundWorker_ProgressChanged;
            bgGetFilesBackgroundWorker.RunWorkerCompleted += BgGetFilesBackgroundWorker_RunWorkerCompleted;

            LoadDirectory(new FileDetailsModel()
            {
                Path = CurrentDirectory
            });

            PathHistoryCollection = new ObservableCollection<string>();
            PathHistoryCollection.Add(CurrentDirectory);

            CanGoBack = position != 0;
            OnPropertyChanged(nameof(CanGoBack));
        }

        #endregion

        #region Commands

        private ICommand _openSettingsCommand;

        public ICommand openSettingsCommand
        {
            get
            {
                return _openSettingsCommand ??
                       (_openSettingsCommand = new Command(() => Process.Start("ms-settings:home")));
            }
        }

        private ICommand _openUserProfileSettingsCommand;

        public ICommand openUserProfileSettingsCommand
        {
            get
            {
                return _openUserProfileSettingsCommand ??
                       (_openUserProfileSettingsCommand = new Command(() => Process.Start("ms-settings:yourinfo")));
            }
        }

        private ICommand _loadSubMenuCollectionsCommand;

        public ICommand LoadSubMenuCollectionsCommand =>
            _loadSubMenuCollectionsCommand ??
            (_loadSubMenuCollectionsCommand = new Command(() =>
            {
                HomeTabSubMenuCollection = new ObservableCollection<SubMenuItemDetails>
                {
                    new SubMenuItemDetails()
                    {
                        Name = "Pin",
                        Icon = (PathGeometry) _iconDictionary["Pin"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Copy",
                        Icon = (PathGeometry) _iconDictionary["Copy"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Cut",
                        Icon = (PathGeometry) _iconDictionary["Cut"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Paste",
                        Icon = (PathGeometry) _iconDictionary["Paste"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Delete",
                        Icon = (PathGeometry) _iconDictionary["Delete"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Rename", Icon = (PathGeometry) _iconDictionary["Rename"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "New Folder",
                        Icon = (PathGeometry) _iconDictionary["NewFolder"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Properties",
                        Icon = (PathGeometry) _iconDictionary["FileSettings"]
                    }
                };

                ViewTabSubMenuCollection = new ObservableCollection<SubMenuItemDetails>
                {
                    new SubMenuItemDetails()
                    {
                        Name = "List",
                        Icon = (PathGeometry) _iconDictionary["ListView"]
                    },
                    new SubMenuItemDetails()
                    {
                        Name = "Tile",
                        Icon = (PathGeometry) _iconDictionary["TileView"]
                    }
                };
            }));

        protected ICommand _getFilesListCommand;

        public ICommand GetFilesListCommand =>
            _getFilesListCommand ?? (_getFilesListCommand = new RelayCommand(parameter =>
            {
                var file = parameter as FileDetailsModel;
                if (file == null) return;

                SelectedFolderDetails = string.Empty;
                OnPropertyChanged(nameof(SelectedFolderDetails));

                if (Directory.Exists(file.Path))
                {
                    UpdatePathHistory(file.Path);
                    LoadDirectory(file);
                }

                else
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(file.Path));
                    }
                    catch (Win32Exception w3Ex)
                    {
                        MessageBox.Show(w3Ex.Message, w3Ex.Source);
                    }
                    catch (InvalidOperationException ioEx)
                    {
                        MessageBox.Show($"{file.Name} is not installed on this computer.", ioEx.Source);
                    }
                }
            }));

        protected ICommand _getFilesSizeCommand;

        public ICommand GetFilesSizeCommand =>
            _getFilesSizeCommand ?? (_getFilesSizeCommand = new RelayCommand(parameter =>
            {
                var file = parameter as FileDetailsModel;
                if (file == null) return;

                SelectedFolderDetails = "Calculating size...";
                OnPropertyChanged(nameof(SelectedFolderDetails));
                bgGetSizeBackgroundWorker.DoWork -= BgGetSizeBackgroundWorker_DoWork;
                bgGetSizeBackgroundWorker.DoWork += BgGetSizeBackgroundWorker_DoWork;

                if (bgGetSizeBackgroundWorker.IsBusy)
                    bgGetSizeBackgroundWorker.CancelAsync();

                if (bgGetSizeBackgroundWorker.CancellationPending)
                {
                    bgGetSizeBackgroundWorker.Dispose();
                    bgGetSizeBackgroundWorker = new BackgroundWorker()
                    {
                        WorkerSupportsCancellation = true
                    };
                }

                bgGetSizeBackgroundWorker.RunWorkerAsync();
            }));

                                                    protected ICommand _goToPreviousDirectoryCommand;

                                                    public ICommand GoToPreviousDirectoryCommand => _goToPreviousDirectoryCommand ??
                                                        (_goToPreviousDirectoryCommand = new Command(() =>
                                                        {
                                                            if (position >= 1)
                                                            {
                                                                position--;
                                                                LoadDirectory(new FileDetailsModel()
                                                                {
                                                                    Path = PathHistoryCollection.ElementAt(position)
                                                                });

                                                                CanGoForward = true;
                                                                OnPropertyChanged(nameof(CanGoForward));

                                                                PathDisrupted = false;
                                                                OnPropertyChanged(nameof(PathDisrupted));
                                                            }
                                                        }));

                                                    protected ICommand _goToForwardDirectoryCommand;

                                                    public ICommand GoToForwardDirectoryCommand => _goToForwardDirectoryCommand ??
                                                       (_goToForwardDirectoryCommand = new Command(() =>
                                                       {
                                                           if (position < PathHistoryCollection.Count - 1)
                                                           {
                                                               position++;
                                                               LoadDirectory(new FileDetailsModel()
                                                               {
                                                                   Path = PathHistoryCollection.ElementAt(position)
                                                               });

                                                               CanGoForward =
                                                                   position < PathHistoryCollection.Count - 1 &&
                                                                   position != PathHistoryCollection.Count - 1;
                                                               OnPropertyChanged(nameof(CanGoForward));
                                                           }
                                                       }));

                                                    protected ICommand _goToParentDirectoryCommand;

                                                    public ICommand GoToParentDirectoryCommand => _goToParentDirectoryCommand ??
                                                      (_goToParentDirectoryCommand = new Command(() =>
                                                      {
                                                          var ParentDirectory = string.Empty;
                                                          PathDisrupted = true;

                                                          var d = new DirectoryInfo(CurrentDirectory);

                                                          if (d.Parent != null)
                                                          {
                                                              ParentDirectory = d.Parent.FullName;
                                                              IsAtRootDirectory = false;
                                                              OnPropertyChanged(nameof(IsAtRootDirectory));
                                                          }
                                                          else if (d.Parent == null)
                                                          {
                                                              IsAtRootDirectory = true;
                                                              OnPropertyChanged(nameof(IsAtRootDirectory));
                                                              return;
                                                          }

                                                          else
                                                          {
                                                              ParentDirectory = d.Root.ToString()
                                                                  .Split(Path.DirectorySeparatorChar)[1];
                                                          }

                                                          GetFilesListCommand.Execute(new FileDetailsModel()
                                                          {
                                                              Path = ParentDirectory
                                                          });
                                                      }));

                                                protected ICommand _navigateToPathCommand;

                                                public ICommand NavigateToPathCommand => _navigateToPathCommand ??
                                                 (_navigateToPathCommand = new RelayCommand((parameter) =>
                                                 {
                                                     var path = parameter as string;
                                                     if (!string.IsNullOrEmpty(path))
                                                         GetFilesListCommand.Execute(new FileDetailsModel()
                                                         {
                                                             Path = path
                                                         });
                                                 }));

                                                    protected ICommand _subMenuFileOperationCommand;

                                                    protected ICommand _unPinFavoriteFolderCommand;

                                                    public ICommand UnPinFavoriteFolderCommand => _unPinFavoriteFolderCommand ??
                                                      (_unPinFavoriteFolderCommand = new RelayCommand((parameter) =>
                                                      {
                                                          var folder = parameter as FileDetailsModel;
                                                          if (folder == null) return;

                                                          folder.IsPinned = false;
                                                          FavoriteFolders.Remove(folder);
                                                          OnPropertyChanged(nameof(FavoriteFolders));
                                                      }));

                                                    public ICommand SubMenuFileOperationCommand => _subMenuFileOperationCommand ??
                                                       (_subMenuFileOperationCommand = new RelayCommand((parameter) =>
                                                       {
                                                           var subMenuItem = parameter as SubMenuItemDetails;
                                                           if (subMenuItem == null) return;
                                                           try
                                                           {
                                                               switch (subMenuItem.Name)
                                                               {
                                                                   case "Pin":
                                                                       PinFolder();
                                                                       break;
                                                                   case "Copy":
                                                                       Copy();
                                                                       break;
                                                                   case "Cut":
                                                                       Cut();
                                                                       break;
                                                                   case "Paste":
                                                                       Paste(IsMoveOperation);
                                                                       break;
                                                                   case "Delete":
                                                                       Delete();
                                                                       break;
                                                                   case "Rename":
                                                                       Rename();
                                                                       break;
                                                                   case "New Folder":
                                                                       CreateNewFolder();
                                                                       break;
                                                                   case "Properties":
                                                                       ShowProperties();
                                                                       break;
                                                                   case "List":
                                                                       IsListView = false;
                                                                       OnPropertyChanged(nameof(IsListView));
                                                                       break;
                                                                   case "Tile":
                                                                       IsListView = true;
                                                                       OnPropertyChanged(nameof(IsListView));
                                                                       break;
                                                                   default:
                                                                       return;
                                                               }
                                                           }
                                                           catch
                                                           {
                                                           }
                                                       }));

                                                protected ICommand _createNewFolderCommand;

                                                public ICommand CreateNewFolderCommand => _createNewFolderCommand ??
                                                  (_createNewFolderCommand = new Command(() =>
                                                  {
                                                      foreach (var folder in NavigatedFolderFiles.Where(f =>
                                                          f.IsSelected))
                                                          folder.IsSelected = false;
                                                      OnPropertyChanged(nameof(NavigatedFolderFiles));

                                                      var i = FileSystem.GetDirectories(CurrentDirectory)
                                                          .Count(x => x.Contains("New Folder"));
                                                      var path = i == 0
                                                          ? $"{CurrentDirectory}\\New Folder"
                                                          : $"{CurrentDirectory}\\New Folder{i}";
                                                      Directory.CreateDirectory(path);

                                                      var file = new FileDetailsModel();
                                                      file.Name = Path.GetFileName(path);
                                                      file.Path = path;
                                                      file.IsDirectory = true;
                                                      file.FileExtension = string.Empty;
                                                      file.IsImage = false;
                                                      file.IsVideo = false;
                                                      file.FileIcon = GetImageForExtension(file);
                                                      file.IsSelected = true;

                                                      NavigatedFolderFiles.Add(file);
                                                      OnPropertyChanged(nameof(NavigatedFolderFiles));

                                                      Rename();
                                                  }));

                                                    protected ICommand _searchFileOrFolderCommand;

                                                    public ICommand SearchFileOrFolderCommand => _searchFileOrFolderCommand ??
                                                     (_searchFileOrFolderCommand = new RelayCommand((parameter) =>
                                                     {
                                                         var searchQuery = parameter as string;
                                                         if (string.IsNullOrWhiteSpace(searchQuery)) return;

                                                         NavigatedFolderFiles.Clear();

                                                         if (bgGetFoundFilesWorker != null &&
                                                             bgGetFoundFilesWorker.IsBusy)
                                                             bgGetFoundFilesWorker.CancelAsync();

                                                         bgGetFoundFilesWorker.DoWork += (o, args) =>
                                                         {
                                                             try
                                                             {
                                                                 IsInSearchMode = true;
                                                                 OnPropertyChanged(nameof(IsInSearchMode));
                                                                 var directories =
                                                                     EnumerateDirectories(CurrentDirectory, searchQuery,
                                                                         SearchOption.AllDirectories);
                                                                 foreach (var directory in directories)
                                                                 {
                                                                     bgGetFoundFilesWorker.ReportProgress(1,
                                                                         directory);
                                                                 }

                                                                 var files = EnumerateFiles(CurrentDirectory,
                                                                     searchQuery, SearchOption.AllDirectories);
                                                                 foreach (var file in files)
                                                                 {
                                                                     bgGetFoundFilesWorker.ReportProgress(1, file);
                                                                 }

                                                                 IsInSearchMode = false;
                                                                 OnPropertyChanged(nameof(IsInSearchMode));
                                                             }
                                                             catch (Exception ex)
                                                             {
                                                                 //ignored
                                                             }
                                                         };

                                                         bgGetFoundFilesWorker.ProgressChanged +=
                                                             BgGetFilesBackgroundWorker_ProgressChanged;
                                                         bgGetFoundFilesWorker.RunWorkerCompleted +=
                                                             BgGetFilesBackgroundWorker_RunWorkerCompleted;

                                                         if (!bgGetFoundFilesWorker.IsBusy)
                                                             bgGetFoundFilesWorker.RunWorkerAsync();
                                                     }));

                                                        protected ICommand _cancelSearchFileOrFolderCommand;

                                                        public ICommand CancelSearchFileOrFolderCommand => _cancelSearchFileOrFolderCommand ??
                                                           (_cancelSearchFileOrFolderCommand = new Command(() =>
                                                           {
                                                               if (bgGetFoundFilesWorker.IsBusy)
                                                                   bgGetFoundFilesWorker.CancelAsync();

                                                               if (bgGetFoundFilesWorker.CancellationPending)
                                                                   bgGetFoundFilesWorker.Dispose();

                                                               IsInSearchMode = false;
                                                               OnPropertyChanged(nameof(IsInSearchMode));
                                                           }));

                                        protected ICommand _SortFilesCommand;

                                        private bool SortedByAscending { get; set; }
                                        public string SortedBy { get; set; }

                                        public ICommand SortFilesCommand => _SortFilesCommand ??
                                            (_SortFilesCommand = new RelayCommand((parameter) =>
                                            {
                                                var header = parameter as GridViewColumnHeader;
                                                if (header == null ||
                                                    string.IsNullOrWhiteSpace(header.Content.ToString())) return;

                                                SortedByAscending = !SortedByAscending;
                                                OnPropertyChanged(nameof(SortedByAscending));

                                                CollectionViewSource.GetDefaultView(NavigatedFolderFiles)
                                                    .SortDescriptions.Clear();
                                                if (SortedByAscending)
                                                {
                                                    CollectionViewSource.GetDefaultView(NavigatedFolderFiles)
                                                        .SortDescriptions
                                                        .Add(new SortDescription(
                                                            header.Content.ToString().Replace(" ", ""),
                                                            ListSortDirection.Descending));
                                                }
                                                else
                                                {
                                                    CollectionViewSource.GetDefaultView(NavigatedFolderFiles)
                                                        .SortDescriptions
                                                        .Add(new SortDescription(
                                                            header.Content.ToString().Replace(" ", ""),
                                                            ListSortDirection.Ascending));
                                                }

                                                SortedBy = (string) header.Content;
                                                OnPropertyChanged(nameof(SortedBy));
                                            }));

        

        

        #endregion
    }
}