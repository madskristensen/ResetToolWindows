using System;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using static Microsoft.VisualStudio.VSConstants;

namespace ResetToolWindows
{
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "Environment\\Startup", "Reset Tool Windows", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "Environment\\Startup", "Reset Tool Windows", 0, 0, true)]
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
                ex.LogAsync().ConfigureAwait(false);
            }

            return base.QueryClose(out canClose);
        }
    }
}
