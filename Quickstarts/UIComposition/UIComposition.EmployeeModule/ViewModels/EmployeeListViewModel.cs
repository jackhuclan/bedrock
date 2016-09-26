using System;
using System.Collections.Generic;
using System.ComponentModel;
using Bedrock.Events;
using UIComposition.EmployeeModule.Events;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee List view.
    /// </summary>
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        public EmployeeListViewModel(
            IEmployeeDataService dataService,
            IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this._eventAggregator = eventAggregator;
            this.Employees = dataService.GetEmployees();
        }

        public List<Employee> Employees { get; private set; }

        public void SelectedEmployeeChanged(string id)
        {
            this._eventAggregator.GetEvent<EmployeeSelectedEvent>().Publish(id);
        }

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

        private readonly IEventAggregator _eventAggregator;
    }
}