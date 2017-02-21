using DeckOfPlayingCards.Core.Models;
using System.Collections.Generic;

namespace DeckOfPlayingCards.Core
{
    /// <summary>
    /// Expose properties to get cards and divides equally into different players.
    /// </summary>
    public interface IDeck
    {
        /// <summary>
        /// List of cards.
        /// </summary>
        List<Card> Cards { get; set; }

        /// <summary>
        /// Shuffle randomly all cards of the deck.
        /// </summary>
        void Shuffle();              
    }
}
