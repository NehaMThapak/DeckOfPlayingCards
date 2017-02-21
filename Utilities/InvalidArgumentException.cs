using System;

namespace DeckOfPlayingCards.Core.Utilities
{
    /// <summary>
    /// An exception representing a invalid argument error.
    /// </summary>
    public class InvalidArgumentException :  Exception
    {
        /// <summary>
        /// Instantiates a new InvalidArgumentException.
        /// </summary>
        /// <param name="message">The message for the exception.</param>        
        public InvalidArgumentException(string message) : base(message)
        {
        }
    }
}
