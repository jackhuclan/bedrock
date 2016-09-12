using Microsoft.Practices.Prism.PubSubEvents;

namespace UIComposition.EmployeeModule.Events
{
    /// <summary>
    /// Defines an event to communicate that an employee has been selected.
    /// The event payload is the employee id.
    /// </summary>
    public class EmployeeSelectedEvent : PubSubEvent<string>
    {
    }
}