using System.ComponentModel;

namespace DeckOfPlayingCards.Core.Enums
{
    /// <summary>
    /// Represents different values of the card belongs to the deck.
    /// </summary>
    public enum CardNumbers
    {
        /// <summary>
        /// Card number Two being the lowest value in the deck.
        /// </summary>
        [Description("2")]
        Two = 1,

        /// <summary>
        /// Card number three value.
        /// </summary>
        [Description("3")]
        Three = 2,

        /// <summary>
        /// Card number four value.
        /// </summary>
        [Description("4")]
        Four = 3,

        /// <summary>
        /// Card number five value.
        /// </summary>
        [Description("5")]
        Five = 4,

        /// <summary>
        /// Card number six value.
        /// </summary>
        [Description("6")]
        Six = 5,

        /// <summary>
        /// Card number seven value.
        /// </summary>
        [Description("7")]
        Seven = 6,

        /// <summary>
        /// Card number eight value.
        /// </summary>
        [Description("8")]
        Eight = 7,

        /// <summary>
        /// Card number nine value.
        /// </summary>
        [Description("9")]
        Nine = 8,

        /// <summary>
        /// Card number ten value.
        /// </summary>
        [Description("10")]
        Ten = 9,

        /// <summary>
        /// Card Jack value.
        /// </summary>
        [Description("J")]
        Jack = 10,

        /// <summary>
        /// Card Queen value.
        /// </summary>
        [Description("Q")]
        Queen = 11,

        /// <summary>
        /// Card King value.
        /// </summary>
        [Description("K")]
        King = 12,

        /// <summary>
        /// Card Aces being the highest value in the deck.
        /// </summary>
        [Description("A")]
        Aces = 13
    }
}
