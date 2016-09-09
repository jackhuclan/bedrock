using Bedrock.Winform;

namespace HelloWorld
{
    public partial class Form1 : FormView
    {
        public Form1()
        {
            InitializeComponent();
            RegisterRegion(this.panel1.Name, this.panel1);
        }
    }
}
