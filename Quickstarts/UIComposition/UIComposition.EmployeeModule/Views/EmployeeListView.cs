using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeListView : PartialView
    {
        public EmployeeListView(EmployeeListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
