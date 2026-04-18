using Domain.Enums;
using Domain.Interfaces.Currency;
using Domain.ValueObjects;

namespace Infrastructure.Services
{
    /// <summary>
    /// A static implementation of <see cref="ICurrencyConverter"/> using predefined exchange rates.
    /// Used to handle internal DKK conversions for the clinic's local pricing.
    /// </summary>
    public class CurrencyConverter : ICurrencyConverter
    {

        /// <inheritdoc/>
        /// <remarks>
        /// Current implementation uses a hard-coded rate (1 EUR = 7.47 DKK etc.) 
        /// as specified for the current business context.
        /// </remarks>
        public Task<Money> ConvertToDkkAsync(Money amount)
        {
            decimal rateToDkk = amount.Currency switch
            {
                Currency.DKK => 1.0M,
                Currency.NOK => 0.67M,
                Currency.SEK => 0.69M,
                Currency.EUR => 7.47M,
                _ => throw new ArgumentException($"Currency {amount.Currency} is not supported for Conversion")
            };

            decimal amountInDkk = amount.Amount * rateToDkk;

            return Task.FromResult(new Money(amountInDkk, Currency.DKK));


        }
    }
}
