using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.Linq;

namespace ResetToolWindows
{
    public static class DockHelpers
    {
        public static void DockToolWindows(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            dte.ExecuteCommand("Window.AutoHideAll");

            ToolWindows toolWindows = dte.ToolWindows;            
            GeneralOptions options = GeneralOptions.Instance;
            
            Dock(toolWindows.CommandWindow.Parent, options.ShowCommandWindow);
            Dock(toolWindows.ErrorList.Parent, options.ShowErrorList);
            Dock(toolWindows.OutputWindow.Parent, options.ShowOutputWindow);
            Dock(toolWindows.SolutionExplorer.Parent, options.ShowSolutionExplorer);
            Dock(toolWindows.TaskList.Parent, options.ShowTaskList);
            Dock(toolWindows.ToolBox.Parent, options.ShowToolbox);

            var customWindowCaptions = options.CustomWindowCaptionList.Split(';').ToHashSet();

            Windows windows = dte.Windows;
            foreach (Window window in windows)
            {
                if (customWindowCaptions.Contains(window.Caption))
                {
                    Dock(window, true);
                }
            }
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
