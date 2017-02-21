using DeckOfPlayingCards.Core.Enums;

namespace DeckOfPlayingCards.Core
{
    /// <summary>
    /// Expose properties to get suits and card numbers.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Different suits of a deck.
        /// </summary>
        Suits Suits { get; set; }

        /// <summary>
        /// Different card number of a deck.
        /// </summary>
        CardNumbers CardNumbers { get; set; }

        /// <summary>
        /// Get suit of this card i.e. one of the value from <see cref="Suits"/>.
        /// </summary>
        /// <returns>Value of <see cref="Suits"/>.</returns>
        int getSuit();

        /// <summary>
        /// Get value of this card i.e. one of the value from <see cref="CardNumbers"/>.
        /// </summary>
        /// <returns>Value of <see cref="CardNumbers"/>.</returns>
        int getValue();
    }
}
