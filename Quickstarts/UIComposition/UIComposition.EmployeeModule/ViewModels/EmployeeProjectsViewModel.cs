// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee Projects view.
    /// </summary>
    public class EmployeeProjectsViewModel : INotifyPropertyChanged
    {
        public EmployeeProjectsViewModel(
            IEmployeeDataService dataService,
            IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            _dataService = dataService;
            eventAggregator.GetEvent<EmployeeSelectedEvent>().Subscribe(this.EmployeeSelected, true);
        }

        private void EmployeeSelected(string obj)
        {
            this._projects = _dataService.GetProjects();
            Projects = _projects.Where(x => x.Id == obj).ToList();
        }

        public string ViewName
        {
            get { return "Employee Projects"; }
        }

        public List<Project> Projects
        {
            get { return _projects; }
            private set
            {
                _projects = value;
                NotifyPropertyChanged("Projects");
            }
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

        private List<Project> _projects;
        private readonly IEmployeeDataService _dataService;
    }
}