using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeSummaryView : PartialView
    {
        public EmployeeSummaryView(EmployeeSummaryViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
