using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace ResetToolWindows
{
    public static class DockHelpers
    {
        public static void DockToolWindows(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            dte.ExecuteCommand("Window.AutoHideAll");

            ToolWindows windows = dte.ToolWindows;
            GeneralOptions options = GeneralOptions.Instance;

            Dock(windows.CommandWindow.Parent, options.ShowCommandWindow);
            Dock(windows.ErrorList.Parent, options.ShowErrorList);
            Dock(windows.OutputWindow.Parent, options.ShowOutputWindow);
            Dock(windows.SolutionExplorer.Parent, options.ShowSolutionExplorer);
            Dock(windows.TaskList.Parent, options.ShowTaskList);
            Dock(windows.ToolBox.Parent, options.ShowToolbox);
        }

        private static void Dock(Window windowToDock, bool shouldDock)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (shouldDock)
            {
                // This docks a window to its last docked location
                windowToDock.IsFloating = false;
            }
        }
    }
}
