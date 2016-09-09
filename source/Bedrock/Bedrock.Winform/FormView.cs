using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;
// ReSharper disable VirtualMemberCallInContructor

namespace Bedrock.Winform
{
    public partial class FormView : Form, IView
    {
        public FormView()
        {
            InitializeComponent();
        }
        
        public object DataContext { get; set; }


        public void RegisterRegion(string regionName, object control)
        {
            var instance = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (instance != null)
            {
                instance.Regions.Add(new Region(regionName, control));
            }
        }
    }
}
