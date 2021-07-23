using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ImGuiFileDialog {
    [Flags]
    public enum ImGuiFileDialogFlags {
        None = 0,
        ConfirmOverwrite = 1,
        SelectOnly = 2,
        DontShowHiddenFiles = 3,
        DisableCreateDirectoryButton = 4,
        HideColumnType = 5,
        HideColumnSize = 6,
        HideColumnDate = 7,
        HideSideBar = 8
    }

    public partial class FileDialog {
        private bool Visible;

        private readonly string Title;
        private readonly int SelectionCountMax;
        private readonly ImGuiFileDialogFlags Flags;
        private readonly string Id;
        private readonly string DefaultExtension;
        private readonly string DefaultFileName;

        private string CurrentPath;
        private string FileNameBuffer = "";

        private List<string> PathDecomposition = new();
        private bool PathClicked = true;
        private bool PathInputActivated = false;
        private string PathInputBuffer = "";

        private bool IsModal = false;
        private bool OkResultToConfirm = false;
        private bool IsOk;
        private bool WantsToQuit;

        private bool CreateDirectoryMode = false;
        private string CreateDirectoryBuffer = "";

        private string SearchBuffer = "";

        private string LastSelectedFileName = "";
        private List<string> SelectedFileNames = new();

        private float FooterHeight = 0;

        private string SelectedSideBar = "";
        private List<SideBarItem> Drives = new();
        private List<SideBarItem> QuickAccess = new();
        private struct SideBarItem {
            public char Icon;
            public string Text;
            public string Location;
        }

        public FileDialog(
            string id,
            string title,
            string filters,
            string path,
            string defaultFileName,
            string defaultExtension,
            int selectionCountMax,
            bool isModal,
            ImGuiFileDialogFlags flags
         ) {
            Id = id;
            Title = title;
            Flags = flags;
            SelectionCountMax = selectionCountMax;
            IsModal = isModal;

            CurrentPath = path;
            DefaultExtension = defaultExtension;
            DefaultFileName = defaultFileName;

            ParseFilters( filters );
            SetSelectedFilterWithExt( DefaultExtension );
            SetDefaultFileName();
            SetPath( CurrentPath );

            SetupSideBar();
        }

        public void Show() {
            Visible = true;
        }

        public void Hide() {
            Visible = false;
        }

        public bool GetIsOk() {
            return IsOk;
        }

        public string GetResult() {
            if( !Flags.HasFlag( ImGuiFileDialogFlags.SelectOnly ) ) return GetFilePathName();
            if(IsDirectoryMode() && SelectedFileNames.Count == 0) {
                return GetFilePathName(); // current directory
            }

            var fullPaths = SelectedFileNames.Where( x => !string.IsNullOrEmpty( x ) ).Select( x => Path.Combine( CurrentPath, x ) );
            return string.Join( ",", fullPaths.ToArray() );
        }

        // the full path, specified by the text input box and the current path
        private string GetFilePathName() {
            var path = GetCurrentPath();
            var fileName = GetCurrentFileName();

            if(!string.IsNullOrEmpty(fileName)) {
                return Path.Combine( path, fileName );
            }

            return path;
        }

        // the current path. In directory mode, this takes into account the text input box
        public string GetCurrentPath() {
            if(IsDirectoryMode()) { // combine path file with directory input
                var selectedDirectory = FileNameBuffer;
                if(!string.IsNullOrEmpty(selectedDirectory) && selectedDirectory != ".") {
                    return string.IsNullOrEmpty( CurrentPath ) ? selectedDirectory : Path.Combine( CurrentPath, selectedDirectory );
                }
            }

            return CurrentPath;
        }

        // the current filename, taking into account the current filter applied. In directory mod, this is alway empty
        private string GetCurrentFileName() {
            if( IsDirectoryMode() ) return "";

            var result = FileNameBuffer;
            // a collection like {.cpp, .h}, so can't decide on an extension
            if( SelectedFilter.CollectionFilters != null && SelectedFilter.CollectionFilters.Count > 0 ) {
                return result;
            }

            // a single one, like .cpp
            if( !SelectedFilter.Filter.Contains( '*' ) && result != SelectedFilter.Filter ) {
                var lastPoint = result.LastIndexOf( '.' );
                if(lastPoint != -1) {
                    result = result.Substring( 0, lastPoint );
                }
                result += SelectedFilter.Filter;
            }
            return result;
        }

        private void SetDefaultFileName() {
            FileNameBuffer = DefaultFileName;
        }

        private void SetPath(string path) {
            SelectedSideBar = "";
            CurrentPath = path;
            Files.Clear();
            PathDecomposition.Clear();
            SelectedFileNames.Clear();
            if( IsDirectoryMode() ) {
                SetDefaultFileName();
            }
            ScanDir( CurrentPath );
        }

        private void SetCurrentDir(string path) {
            var dir = new DirectoryInfo( path );
            CurrentPath = dir.FullName;
            if(CurrentPath[CurrentPath.Length - 1] == Path.DirectorySeparatorChar) { // handle selecting a drive, like C: -> C:\
                CurrentPath = CurrentPath.Substring( 0, CurrentPath.Length - 1 );
            }

            PathInputBuffer = CurrentPath;
            PathDecomposition = new List<string>( CurrentPath.Split( Path.DirectorySeparatorChar ) );
        }

        private bool IsDirectoryMode() {
            return Filters.Count == 0;
        }

        private void ResetEvents() {
            PathClicked = false;
        }
    }
}
