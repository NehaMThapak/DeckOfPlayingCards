using System.ComponentModel;

namespace DeckOfPlayingCards.Core.Enums
{
    /// <summary>
    /// Represents different suits of a deck.
    /// </summary>
    public enum Suits
    {
        /// <summary>
        /// Suit of type Heart.
        /// </summary>
        [Description("Heart")]
        Heart = 1,

        /// <summary>
        /// Suit of type Diamond.
        /// </summary>
        [Description("Diamond")]
        Diamond = 2,

        /// <summary>
        /// Suit of type Spade.
        /// </summary>
        [Description("Spade")]
        Spade = 3,

        /// <summary>
        /// Suit of type Club.
        /// </summary>
        [Description("Club")]
        Club = 4
    }
}
