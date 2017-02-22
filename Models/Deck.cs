using DeckOfPlayingCards.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfPlayingCards.Core.Models
{
    /// <summary>
    /// Represents deck of the playing cards.
    /// </summary>
    public class Deck : IDeck
    {                      
        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/>. 
        /// </summary>
        public Deck()
        {          
            CreateCardDeck();
            Shuffle();         
        }

        /// <summary>
        /// List of cards.
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Shuffle randomly all cards of the deck.
        /// </summary>
        public void Shuffle()
        {
            Cards = Cards.OrderBy(x => Guid.NewGuid()).ToList();
        }              

        /// <summary>
        /// Creates a deck of card.
        /// </summary>
        private void CreateCardDeck()
        {
            // Creating a card deck iterating through the range of (1 - 4) suits. Then
            // use range (1 - 13) for the cards, by selecting a new card based on the
            // suits and card numbers. 
            Cards = Enumerable.Range(1, 4).SelectMany(x => Enumerable.Range(1, 13).Select(y => new Card(x, y)
            {
                Suits = (Suits)x,
                CardNumbers = (CardNumbers)y
            })).ToList();
        }
    }
}
