using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{

    /// <summary>
    /// Provides an abstraction for retrieving the current date and time.
    /// Enables deterministic unit testing by allowing time to be 'frozen' or 'mocked'.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current absolute date and time in UTC.
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}
