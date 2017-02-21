using DeckOfPlayingCards.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckOfPlayingCards.Core.Models
{
    /// <summary>
    /// Represents a hand class which contains methods to add, remove cards from the hand,
    /// get total cards in the hand etc.
    /// </summary>
    public class Hand
    {
        // The cards in the hand.
        private List<Card> hand;   

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
                throw new ArgumentNullException("Can't add a null card to a hand.", nameof(card));
            hand.Add(card);
        }       

        /// <summary>
        /// Sorts the cards in the hand so that cards are sorted into order of increasing value.
        /// Cards with the same value are sorted by suit. Note that aces are considered
        /// to have the lowest value.
        /// </summary>
        public void SortByValue()
        {
            List<Card> newHand = new List<Card>();
            while (hand.Count() > 0)
            {
                int pos = 0;  // Position of minimal card.
                Card c = hand[0];  // Minimal card.
                for (int i = 1; i < hand.Count(); i++)
                {
                    Card c1 = hand[i];
                    if (c1.getValue() < c.getValue() ||
                            (c1.getValue() == c.getValue() && c1.getSuit() < c.getSuit()))
                    {
                        pos = i;
                        c = c1;
                    }
                }
                hand.RemoveAt(pos);
                newHand.Add(c);
            }
            hand = newHand;
        }
    }
}
