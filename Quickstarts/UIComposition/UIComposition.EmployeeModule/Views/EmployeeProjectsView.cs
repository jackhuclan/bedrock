using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeProjectsView : PartialView
    {
        public EmployeeProjectsView(EmployeeProjectsViewModel employeeProjectsViewModel, EmployeeProjectsViewModel employeeProjectsViewModel1)
        {
            InitializeComponent();
            this.DataContext = employeeProjectsViewModel;
            this.employeeProjectsViewModel = employeeProjectsViewModel1;
            this.employeeProjectsViewModel.PropertyChanged += EmployeeProjectsViewModel_PropertyChanged;
        }

        private void EmployeeProjectsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Projects")
            {
                this.dataGridView1.DataSource = this.employeeProjectsViewModel.Projects;
            }
        }

        private EmployeeProjectsViewModel employeeProjectsViewModel;
    }
}
