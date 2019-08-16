using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using static Microsoft.VisualStudio.VSConstants;

namespace ResetToolWindows
{
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(DialogPageProvider.General), "Environment\\Startup", "Reset Tool Windows", 0, 0, true, new[] { Vsix.Name }, ProvidesLocalizedCategoryName = false)]
    [Guid("f2156abf-b775-442c-ab74-92c77551c474")]
    public sealed class ResetToolWindowsPackage : AsyncPackage
    {
        protected override int QueryClose(out bool canClose)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                if (GeneralOptions.Instance.EnableAutoHide)
                {
                    var dte = GetService(typeof(DTE)) as DTE2;
                    DockHelpers.DockToolWindows(dte);
                }
            }
            catch (Exception ex)
            {
                VsShellUtilities.LogError(ex.Source, ex.ToString());
            }

            return base.QueryClose(out canClose);
        }
    }
}
