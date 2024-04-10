using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.ComponentModel.Design;
using System.Windows;
using Shared.Models;
using Task = System.Threading.Tasks.Task;

namespace ToolWindow
{
    internal sealed class Add_Bookmark
    {
        public const int CommandId = 4129;

        public static readonly Guid CommandSet = new Guid("b90beadf-0c5b-43d4-8026-1c54f260a10b");

        private readonly AsyncPackage package;
        private Add_Bookmark(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.CreateBookmark, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static Add_Bookmark Instance
        {
            get;
            private set;
        }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Add_Bookmark's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Add_Bookmark(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
        }

        private void CreateBookmark(object sender, EventArgs e)
        {
            GetSelectedText getSelectedText = new GetSelectedText();
            getSelectedText.GetCurrentViewHost();
            ITextSelection selection = getSelectedText.GetSelection(getSelectedText.GetCurrentViewHost());
            int startLine = selection.StreamSelectionSpan.SnapshotSpan.Start.GetContainingLine().LineNumber + 1; // 1-based
            string filePath = getSelectedText.GetFilePath(getSelectedText.GetCurrentViewHost().TextView);
            DateTime dateCreated = DateTime.Now;

            string result = DialogLauncher.ShowDialog();
            if (result == null)
            {
                return;
            }



            MessageBox.Show(result);


            Route route = new Route
            {
                FilePath = filePath,
                LineNumber = startLine,
            };

            Bookmark bookmark = new Bookmark
            {
                // UserId = 0;
                // RouteId = route.id,
                // Name = result,
                // DateCreaetd = dateCreated,
                // Route = route,
            };

            // Add the bookmark to the list of bookmarks

        }
    }
}
