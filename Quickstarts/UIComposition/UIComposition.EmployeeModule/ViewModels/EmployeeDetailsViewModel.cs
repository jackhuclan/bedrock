using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee Details view.
    /// </summary>
    public class EmployeeDetailsViewModel : INotifyPropertyChanged
    {
        public EmployeeDetailsViewModel(
            IEmployeeDataService dataService,
            IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            Employees = dataService.GetEmployees();
            eventAggregator.GetEvent<EmployeeSelectedEvent>().Subscribe(this.EmployeeSelected, true);
        }

        private void EmployeeSelected(string obj)
        {
            CurrentEmployee = Employees.FirstOrDefault(x => x.Id == obj);
        }

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                NotifyPropertyChanged("CurrentEmployee");
            }
        }

        public string ViewName
        {
            get { return "Employee Details"; }
        }

        public List<Employee> Employees { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private readonly IEmployeeDataService _dataService;
        private Employee _currentEmployee;
    }
}