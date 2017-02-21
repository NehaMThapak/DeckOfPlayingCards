using DeckOfPlayingCards.Core.Enums;
using DeckOfPlayingCards.Core.Utilities;

namespace DeckOfPlayingCards.Core.Models
{  
    /// <summary>
    /// Represents a playing card from a card deck having 52 cards. The card has a 
    /// suit i.e. Heart, Diamond, Spade, Club. Each of the suit has one 
    /// of the 13 values specified in <see cref="CardNumbers"/> where Ace is considered as
    /// highest value.
    /// </summary>
    public class Card : ICard
    {
         /// <summary>
         /// This card's suit, one of the value from <see cref="Suits"/> Heart, Diamond, Spade, Club       
         /// </summary>
         private readonly int Suit;
        
        /// <summary>
        /// This card's value. 
        /// </summary>
        private readonly int Value;

        /// <summary>
        /// Different suits of a deck.
        /// </summary>
        public Suits Suits { get; set; }

        /// <summary>
        /// Different card number of a deck.
        /// </summary>
        public CardNumbers CardNumbers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/>. 
        /// </summary>
        public Card()
        {
            Suit = 1;
            Value = 13; // Default value for Aces.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> with given arguments. 
        /// </summary>
        public Card (int value, int suit)
        {
            if (suit != (int) Suits.Spade && suit != (int)Suits.Heart && suit != (int)Suits.Diamond &&
                  suit != (int)Suits.Club)
                throw new InvalidArgumentException("Illegal playing card suit");
            if (value < 1 || value > 13)
                throw new InvalidArgumentException("Illegal playing card value");
            Value = value;
            Suit = suit;
        }


        /// <summary>
        /// Get suit of this card i.e. one of the value from <see cref="Suits"/>.
        /// </summary>
        /// <returns>Value of <see cref="Suits"/>.</returns>
        public int getSuit()
        {
            return Suit;
        }

        /// <summary>
        /// Get value of this card i.e. one of the value from <see cref="CardNumbers"/>.
        /// </summary>
        /// <returns>Value of <see cref="CardNumbers"/>.</returns>
        public int getValue()
        {
            return Value;
        }
    }
}
