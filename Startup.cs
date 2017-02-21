using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeckOfPlayingCards.Web.Startup))]
namespace DeckOfPlayingCards.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
