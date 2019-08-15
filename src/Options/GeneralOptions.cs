using System.ComponentModel;

namespace CodeCleanupOnSave
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("Visible Tool Windows")]
        [DisplayName("Command Window")]
        [Description("Docks the Command Window if it was already open.")]
        [DefaultValue(false)]
        public bool ShowCommandWindow { get; set; }

        [Category("Visible Tool Windows")]
        [DisplayName("Error List")]
        [Description("Docks the Error List if it was already open.")]
        [DefaultValue(true)]
        public bool ShowErrorList { get; set; } = true;

        [Category("Visible Tool Windows")]
        [DisplayName("OutputWindow")]
        [Description("Docks the OutputWindow if it was already open.")]
        [DefaultValue(false)]
        public bool ShowOutputWindow { get; set; }

        [Category("Visible Tool Windows")]
        [DisplayName("Solution Explorer")]
        [Description("Docks the Solution Explorer if it was already open.")]
        [DefaultValue(true)]
        public bool ShowSolutionExplorer { get; set; } = true;

        [Category("Visible Tool Windows")]
        [DisplayName("Toolbox")]
        [Description("Docks the Toolbox if it was already open.")]
        [DefaultValue(false)]
        public bool ShowToolbox { get; set; }

        [Category("Visible Tool Windows")]
        [DisplayName("Task List")]
        [Description("Docks the Task List if it was already open.")]
        [DefaultValue(false)]
        public bool ShowTaskList { get; set; }
    }
}
