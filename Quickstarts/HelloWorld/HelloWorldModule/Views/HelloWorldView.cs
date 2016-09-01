using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;

namespace HelloWorldModule.Views
{
    public partial class HelloWorldView : UserControl,IView
    {
        public HelloWorldView()
        {
            InitializeComponent();
        }

        public object DataContext { get; set; }
        public IRegionManager RegionManager { get; set; }
    }
}
