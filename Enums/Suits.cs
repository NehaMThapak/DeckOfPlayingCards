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
        [Description("H")]
        Heart = 1,

        /// <summary>
        /// Suit of type Diamond.
        /// </summary>
        [Description("D")]
        Diamond = 2,

        /// <summary>
        /// Suit of type Spade.
        /// </summary>
        [Description("S")]
        Spade = 3,

        /// <summary>
        /// Suit of type Club.
        /// </summary>
        [Description("C")]
        Club = 4
    }
}
