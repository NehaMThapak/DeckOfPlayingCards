using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfPlayingCards.Core.Models
{
    /// <summary>
    /// Represents a hand class which contains methods to add, remove cards and sort it by value.
    /// </summary>
    public class Hand
    {      
        // The cards in the hand.
        public List<Card> hand;   

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/>. 
        /// </summary>
        public Hand()
        {            
            hand = new List<Card>();
        }
       
        /// <summary>
        /// Add the card to the hand. Card should not be null.    
        /// </summary>
        /// <param name="card">Card to add.</param>
        public void AddCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException($"Can't add a null '{nameof(card)}' to a hand.");
            hand.Add(card);
        }       

        /// <summary>
        /// Sorts the cards in the hand so that cards are sorted into ascending order.
        /// Cards with the same value are sorted by suit. Aces are considered
        /// to have the highest value.
        /// </summary>
        public void SortByValue()
        {
            var newHand = new List<Card>();

            // Iterate through till card exists in the hand.
            while (hand.Any())
            {
                // Lowest card position.
                var pos = 0;

                // Lowest card.
                var c = hand[0];  
                for (var i = 1; i < hand.Count; i++)
                {
                    var c1 = hand[i];
                    if (c1.GetValue() >= c.GetValue() && (c1.GetValue() != c.GetValue() || c1.GetSuit() >= c.GetSuit()))
                        continue;
                    pos = i;
                    c = c1;
                }
                hand.RemoveAt(pos);
                newHand.Add(c);
            }
            hand = newHand;
        }
    }
}
