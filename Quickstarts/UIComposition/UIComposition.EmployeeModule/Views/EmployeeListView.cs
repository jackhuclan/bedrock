using Bedrock.Winform;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeListView : PartialView
    {
        public EmployeeListView(EmployeeListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _employeeListViewModel = viewModel;
            BindGridView();
        }

        private void BindGridView()
        {
            this.dataGridView1.DataSource = _employeeListViewModel.Employees;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = dataGridView1.SelectedRows[0].Cells["Id"].Value;
                _employeeListViewModel.SelectedEmployeeChanged(id.ToString());
            }
        }

        private readonly EmployeeListViewModel _employeeListViewModel;
    }
}
