using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeDetailsView : PartialView
    {
        public EmployeeDetailsView(EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            InitializeComponent();
            this.DataContext = employeeDetailsViewModel;
        }
    }
}
