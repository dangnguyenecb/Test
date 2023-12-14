using System;

namespace ECBIM.APIUI
{
    /// <summary>
    /// Interface that defines if the object instance is active
    /// and notifies when the activity changes.
    /// </summary>
    public interface IActiveAware
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>True if the object is active; otherwise False.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Notifies that the value for IsActive property has changed.
        /// </summary>
        event EventHandler IsActiveChanged;
    }
}
