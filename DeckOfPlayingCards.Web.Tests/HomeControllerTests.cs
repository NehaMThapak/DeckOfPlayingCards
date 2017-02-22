using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using DeckOfPlayingCards.Core;
using DeckOfPlayingCards.Core.Enums;
using DeckOfPlayingCards.Core.Models;
using DeckOfPlayingCards.Web.Controllers;
using DeckOfPlayingCards.Web.ViewModels;
using Xunit;
using Moq;

namespace DeckOfPlayingCards.Web.Tests
{
    /// <summary>Tests for the <see cref="HomeController" />.</summary>
    public class HomeControllerTests
    {
        private readonly Mock<IDeck> _deck;
        private readonly HomeController _controller;

        /// <summary>Initializes a new <see cref="HomeControllerTests" />.</summary>
        public HomeControllerTests()
        {
            _deck = new Mock<IDeck>();
            _controller = new HomeController(_deck.Object);
            var httpContext = MvcMockHelpers.MockHttpContext();
            _controller.SetMockControllerContext("Home","Index", httpContext, null, RouteTable.Routes);
        }

        /// <summary>Constructing the controller with a null argument will fail.</summary>
        [Fact]
        public void Constructor_Requires_NonNull_Values()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new HomeController(null));
        }

        /// <summary>Requesting Index Get Action should return expected action result and data.</summary>
        [Fact]
        public void Index_Get_Action_Should_Return_Expected_Result()
        {
            // Arrange                           
            _deck.Setup(x => x.Cards).Returns(new List<Card>
            {
                new Card
                {
                    Suits = Suits.Diamond,
                    CardNumbers = CardNumbers.Aces
                },
                new Card
                {
                     Suits = Suits.Heart,
                     CardNumbers = CardNumbers.Queen
                },
                new Card
                {
                    Suits = Suits.Spade,
                    CardNumbers = CardNumbers.Two
                },
                new Card
                {
                     Suits = Suits.Heart,
                     CardNumbers = CardNumbers.Ten
                },
                new Card
                {
                    Suits = Suits.Club,
                    CardNumbers = CardNumbers.Five
                },
                new Card
                {
                     Suits = Suits.Diamond,
                     CardNumbers = CardNumbers.Four
                },
                new Card
                {
                    Suits = Suits.Spade,
                    CardNumbers = CardNumbers.Seven
                },
                new Card
                {
                     Suits = Suits.Heart,
                     CardNumbers = CardNumbers.Six
                },                
                new Card
                {
                    Suits = Suits.Diamond,
                    CardNumbers = CardNumbers.Eight
                },
                new Card
                {
                     Suits = Suits.Spade,
                     CardNumbers = CardNumbers.Five
                },
                new Card
                {
                    Suits = Suits.Club,
                    CardNumbers = CardNumbers.King
                },
                new Card
                {
                     Suits = Suits.Heart,
                     CardNumbers = CardNumbers.Jack
                },
                new Card
                {
                    Suits = Suits.Diamond,
                    CardNumbers = CardNumbers.King
                },
                new Card
                {
                     Suits = Suits.Club,
                     CardNumbers = CardNumbers.Nine
                },
                new Card
                {
                    Suits = Suits.Heart,
                    CardNumbers = CardNumbers.Two
                },
                new Card
                {
                     Suits = Suits.Spade,
                     CardNumbers = CardNumbers.Nine
                },                
                new Card
                {
                    Suits = Suits.Club,
                    CardNumbers = CardNumbers.Seven
                },
                new Card
                {
                     Suits = Suits.Spade,
                     CardNumbers = CardNumbers.Four
                },
                new Card
                {
                    Suits = Suits.Club,
                    CardNumbers = CardNumbers.Two
                },
                new Card
                {
                     Suits = Suits.Heart,
                     CardNumbers = CardNumbers.Aces
                },
                new Card
                {
                    Suits = Suits.Diamond,
                    CardNumbers = CardNumbers.Four
                },
                new Card
                {
                     Suits = Suits.Club,
                     CardNumbers = CardNumbers.Jack
                },
                new Card
                {
                    Suits = Suits.Heart,
                    CardNumbers = CardNumbers.Six
                },
                new Card
                {
                     Suits = Suits.Spade,
                     CardNumbers = CardNumbers.Aces
                },
                new Card
                {
                     Suits = Suits.Diamond,
                     CardNumbers = CardNumbers.Seven
                }
            });

            // Act
            var results = _controller.Index();

            // Assert 
            Assert.NotNull(results);
            var viewResult = Assert.IsType<ViewResult>(results);
            var resultViewModel = Assert.IsAssignableFrom<PlayerCardViewModel>(viewResult.ViewData.Model);
            Assert.Equal(5, resultViewModel.Cards.Count);
        }
    }
}
