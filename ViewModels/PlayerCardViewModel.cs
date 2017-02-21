using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfPlayingCards.Web.ViewModels
{
    /// <summary>
    /// Represents playing cards contains in each player hand.
    /// </summary>
    public class PlayerCardViewModel
    {
        /// <summary>
        /// Cards given to each hand.
        /// </summary>
        public List<CardBucket> Cards { get; set; } = new List<CardBucket>();

        /// <summary>
        /// A bucket of the card contains suits and card numbers.
        /// </summary>
        public class CardBucket
        {
            /// <summary>
            /// Display cards in hand in the below format.
            /// ValueSuit - ValueSuit - ValueSuit- ValueSuit where
            /// Values = 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A
            /// and Suits = H, D, S, C(Heart, Diamond, Spade, Club).
            /// </summary>
            public string CardsInHand { get; set; }
        }

    }
}