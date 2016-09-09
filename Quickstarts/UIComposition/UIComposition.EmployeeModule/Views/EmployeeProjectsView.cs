using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeProjectsView : PartialView
    {
        public EmployeeProjectsView(EmployeeProjectsViewModel employeeProjectsViewModel)
        {
            InitializeComponent();
            this.DataContext = employeeProjectsViewModel;
        }
    }
}
