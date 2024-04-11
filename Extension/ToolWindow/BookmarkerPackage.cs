using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ToolWindow
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(BookmarkerPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(Bookmarker), Style = VsDockStyle.Tabbed, Window = ToolWindowGuids.SolutionExplorer)]
    [ProvideToolWindowVisibility(typeof(Bookmarker), VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideToolWindowVisibility(typeof(Bookmarker), VSConstants.UICONTEXT.EmptySolution_string)]
    [ProvideToolWindowVisibility(typeof(Bookmarker), VSConstants.UICONTEXT.SolutionHasSingleProject_string)]
    [ProvideToolWindowVisibility(typeof(Bookmarker), VSConstants.UICONTEXT.SolutionHasMultipleProjects_string)]

    public sealed class BookmarkerPackage : AsyncPackage
    {
        public const string PackageGuidString = "553aba0a-ba3c-41d1-9eaa-5e663ac5cb8f";

        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await BookmarkerCommand.InitializeAsync(this);
            await Add_Bookmark.InitializeAsync(this);
        }

        #endregion
    }
}
