using System;
using DeckOfPlayingCards.Core.Models;
using DeckOfPlayingCards.Web.ViewModels;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using DeckOfPlayingCards.Core;
using DeckOfPlayingCards.Core.Utilities;
using IO = System.IO;

namespace DeckOfPlayingCards.Web.Controllers
{   
    public class HomeController : Controller
    {
        private readonly IDeck _deck;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/>. 
        /// </summary>
        /// <param name="deck">An instance of a deck.</param>
        public HomeController(IDeck deck)
        {
            if (deck == null)
                throw new ArgumentNullException(nameof(deck));           

            // Create a shuffled Deck of 52 cards.
            _deck = deck;           
        }

        /// <summary>
        /// Get action to display home page.
        /// </summary>
        /// <returns>View for the home page.</returns>
        [HttpGet, Route("~/", Name = "Index")]
        public ActionResult Index()
        {         
            return View(GetCardsInHand());
        }        

        /// <summary>
        /// Get all cards detail assigned to each hand.
        /// </summary>
        /// <returns>Player card view model data.</returns>
        private PlayerCardViewModel GetCardsInHand()
        {                                  
            // Creates hands of 5 players.
            var hands = new Hand[5];

            // Iterate through card collection for equal distribution of cards to the hands.
            for (var i = 0; i <= 24; i++)
            {
                // Iterate to assign cards to five players.
                for (var j = 0; j <= 4; j++)
                {
                    if (hands[j] == null)
                        hands[j] = new Hand();

                    if (i < _deck.Cards.Count)                  
                        hands[j].AddCard(_deck.Cards[i]);
                   
                    ++i;
                }
            }

            var viewModel = new PlayerCardViewModel();
            var cardBuckets = new List<PlayerCardViewModel.CardBucket>();

            // Counter to display player number.
            var counter = 1;    
            
            // Iterate through hands and sort the values to get sorted cards in hand.             
            foreach (var player in hands)
            {
                var sb = new StringBuilder();
                sb.Append($"Player #{counter}: ");
                player.SortByValue();
                foreach (var item in player.hand)
                {                    
                    sb.Append($"{item.CardNumbers.GetDescription()}{item.Suits.GetDescription()}-");
                }
                cardBuckets.Add(new PlayerCardViewModel.CardBucket
                {
                    // Remove last extra dash from the string.
                    CardsInHand = sb.Remove(sb.Length-1, 1).ToString()
                });

                counter++;
            }

            viewModel.Cards = cardBuckets;
            WriteOutputToTextFile(viewModel);

            return viewModel;
        }

        /// <summary>
        /// Writes output to the text file on every shuffle or run of the application.
        /// </summary>
        /// <param name="viewModel">View model to display player card information.</param>
        private void WriteOutputToTextFile(PlayerCardViewModel viewModel)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Ouput.txt";

            // Check for file existence, If not exist already then create new file.
            if (!IO.File.Exists(path))
            {
                // Creates a file to write to.
                using (var sw = IO.File.CreateText(path))
                {
                    foreach (var player in viewModel.Cards)
                    {
                        sw.WriteLine(player.CardsInHand);
                    }                   
                }
            }
            else
            {
                // Clears out all text before append.
                IO.File.WriteAllText(path, string.Empty);

                // Append text if file already exists.
                using (var sw = IO.File.AppendText(path))
                {                    
                    foreach (var player in viewModel.Cards)
                    {
                        sw.WriteLine(player.CardsInHand);
                    }
                }
            }
        }
    }
}