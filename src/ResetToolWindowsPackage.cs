using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using static Microsoft.VisualStudio.VSConstants;
using Task = System.Threading.Tasks.Task;

namespace ResetToolWindows
{
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(DialogPageProvider.General), "Environment\\Startup", "Reset Tool Windows", 0, 0, true, new[] { Vsix.Name }, ProvidesLocalizedCategoryName = false)]
    [Guid("f2156abf-b775-442c-ab74-92c77551c474")]
    public sealed class ResetToolWindowsPackage : AsyncPackage
    {
        private DTE2 _dte;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            _dte = await GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(_dte);
        }

        protected override int QueryClose(out bool canClose)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                _dte.ExecuteCommand("Window.AutoHideAll");

                DockWindow(GeneralOptions.Instance.ShowCommandWindow, _dte.ToolWindows.CommandWindow.Parent);
                DockWindow(GeneralOptions.Instance.ShowErrorList, _dte.ToolWindows.ErrorList.Parent);
                DockWindow(GeneralOptions.Instance.ShowOutputWindow, _dte.ToolWindows.OutputWindow.Parent);
                DockWindow(GeneralOptions.Instance.ShowSolutionExplorer, _dte.ToolWindows.SolutionExplorer.Parent);
                DockWindow(GeneralOptions.Instance.ShowTaskList, _dte.ToolWindows.TaskList.Parent);
                DockWindow(GeneralOptions.Instance.ShowToolbox, _dte.ToolWindows.ToolBox.Parent);
            }
            catch (Exception ex)
            {
                VsShellUtilities.LogError(ex.Source, ex.ToString());
            }

            return base.QueryClose(out canClose);
        }

        private void DockWindow(bool shouldShow, Window windowToDock)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (shouldShow && windowToDock.Visible)
            {
                windowToDock.IsFloating = false;
                windowToDock.Visible = true;
            }
        }
    }
}
