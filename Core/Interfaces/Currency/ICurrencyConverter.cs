using Domain.ValueObjects;

namespace Domain.Interfaces.Currency
{
    /// <summary>
    /// Provides a mechanism for converting monetary values between different currencies.
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Converts a <see cref="Money"/> object to a target Currency.
        /// This is an I/O-bound operation as it conceptually fetches exchange rates.
        /// </summary>
        /// <param name="amount">The source money object to convert.</param>
        /// <returns>A new <see cref="Money"/> object in the target Currency.</returns>
        /// <exception cref="ArgumentException">Thrown if the conversion rate is unavailable.</exception>
        Task<Money> ConvertToDkkAsync(Money amount);

    }
}
