using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeDetailsView : PartialView
    {
        public EmployeeDetailsView(EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            InitializeComponent();
            DataContext = employeeDetailsViewModel;
            _employeeDetailsViewModel = employeeDetailsViewModel;
            employeeDetailsViewModel.PropertyChanged += EmployeeDetailsViewModel_PropertyChanged;
        }

        private void EmployeeDetailsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentEmployee")
            {
                txtFirstName.Text = _employeeDetailsViewModel.CurrentEmployee.FirstName;
                txtLastName.Text = _employeeDetailsViewModel.CurrentEmployee.LastName;
                txtPhone.Text = _employeeDetailsViewModel.CurrentEmployee.Phone;
                txtEmail.Text = _employeeDetailsViewModel.CurrentEmployee.Email;
            }
        }

        private readonly EmployeeDetailsViewModel _employeeDetailsViewModel;
    }
}
