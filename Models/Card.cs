using DeckOfPlayingCards.Core.Enums;
using DeckOfPlayingCards.Core.Utilities;

namespace DeckOfPlayingCards.Core.Models
{  
    /// <summary>
    /// Represents a playing card from a card deck having 52 cards. The card has a 
    /// suit i.e. Heart, Diamond, Spade, Club. Each of the suit has one 
    /// of the 13 values specified in <see cref="CardNumbers"/> where Ace is being as
    /// highest value.
    /// </summary>
    public class Card : ICard
    {
         /// <summary>
         /// This card's suit, one of the value from <see cref="Suits"/> Heart, Diamond, Spade, Club       
         /// </summary>
         private readonly int _suit;
        
        /// <summary>
        /// This card's value. 
        /// </summary>
        private readonly int _value;

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
            _suit = 1;

            // Default value for Aces.
            _value = 13; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> with given arguments. 
        /// </summary>
        public Card (int suit, int value)
        {
            if (suit != (int) Suits.Spade && suit != (int)Suits.Heart && suit != (int)Suits.Diamond &&
                  suit != (int)Suits.Club)
                throw new InvalidArgumentException("Invalid playing card suit");
            if (value < 1 || value > 13)
                throw new InvalidArgumentException("Invalid playing card value");
            _value = value;
            _suit = suit;
        }


        /// <summary>
        /// Get suit of this card i.e. one of the value from <see cref="Suits"/>.
        /// </summary>
        /// <returns>_value of <see cref="Suits"/>.</returns>
        public int GetSuit()
        {
            return _suit;
        }

        /// <summary>
        /// Get value of this card i.e. one of the value from <see cref="CardNumbers"/>.
        /// </summary>
        /// <returns>_value of <see cref="CardNumbers"/>.</returns>
        public int GetValue()
        {
            return _value;
        }
    }
}
