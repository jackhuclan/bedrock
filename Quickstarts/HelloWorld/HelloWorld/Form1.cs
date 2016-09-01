using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;

namespace HelloWorld
{
    public partial class Form1 : Form, IStartupView
    {
        public Form1()
        {
            InitializeComponent();
        }

        public object DataContext { get; set; }
        public IRegionManager RegionManager { get; set; }
    }
}
