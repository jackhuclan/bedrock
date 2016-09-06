using System.Windows.Forms;

namespace Modularity
{
    public partial class ModuleControl : UserControl
    {
        private string _moduleName;
        private string _label2Text;

        public ModuleControl()
        {
            InitializeComponent();
        }

        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                _moduleName = value;
                this.label1.Text = value;
            }
        }

        public string Label2Text
        {
            get { return _label2Text; }
            set
            {
                _label2Text = value;
                label2.Text = value;
            }
        }
    }
}
