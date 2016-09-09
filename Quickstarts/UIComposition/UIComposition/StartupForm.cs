using Bedrock.Winform;

namespace UIComposition.Shell
{
    public partial class StartupForm : FormView
    {
        public StartupForm()
        {
            InitializeComponent();
            AddRegion("LeftRegion", splitContainer1.Panel1);
            AddRegion("MainRegion", splitContainer1.Panel2);
            AddRegion("TabRegion1", tabPage1);
            AddRegion("TabRegion2", tabPage2);
        }
    }
}
