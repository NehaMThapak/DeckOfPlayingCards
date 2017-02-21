using DeckOfPlayingCards.Core.Models;
using DeckOfPlayingCards.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DeckOfPlayingCards.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Action to display home page.
        /// </summary>
        /// <returns>View for the home page.</returns>
        [HttpGet]
        public ActionResult Index()
        {         
            return View(GetCardsInHand());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DisplayCardsInHand()
        {           
            return View(GetCardsInHand());
        }

        private PlayerCardViewModel GetCardsInHand()
        {           
            var cardList = new List<PlayerCardViewModel.CardBucket>();

            // Create a shuffled Deck of 52 cards.
            Deck cards = new Deck();

            // Creates hands of 5 players.
            Hand[] hands = new Hand[5];

            // Iterate through card collection for equal distribution of cards to the hands.
            for (var i = 0; i <= 24; i++)
            {
                // Iterate through to assign cards to five players.
                for (var j = 0; j <= 4; j++)
                {
                    if (hands[j] == null)
                        hands[j] = new Hand();

                    hands[j].AddCard(cards.Cards[i]);
                    ++i;
                }
            }

            var viewModel = new PlayerCardViewModel();
            var cardBuckets = new List<PlayerCardViewModel.CardBucket>();
            var counter = 1;          
            foreach (var player in hands)
            {
                cardBuckets.Add(new PlayerCardViewModel.CardBucket
                {
                    CardsInHand = $"Player #{counter}: {}{}-"
                });
            }

            return viewModel;
        }
    }
}