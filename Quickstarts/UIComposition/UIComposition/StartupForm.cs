using Bedrock.Winform;

namespace UIComposition.Shell
{
    public partial class StartupForm : FormView
    {
        public StartupForm()
        {
            InitializeComponent();
            RegisterRegion("LeftRegion", splitContainer1.Panel1);
            RegisterRegion("MainRegion", splitContainer1.Panel2);
            RegisterRegion("TabRegion1", tabPage1);
            RegisterRegion("TabRegion2", tabPage2);
        }
    }
}
