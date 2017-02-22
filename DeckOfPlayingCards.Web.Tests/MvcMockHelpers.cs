using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace DeckOfPlayingCards.Web.Tests
{
    /// <summary>Helper class to mock HttpContext for controller.</summary>
    public static class MvcMockHelpers
    {
        /// <summary>Create Default Mock of HttpContext.</summary>
        /// <returns>HttpContextBase object with mock data.</returns>
        public static HttpContextBase MockHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            // Set up Request.
            request.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns("/");
            request.Setup(r => r.ApplicationPath).Returns("/");

            // Set up Response.
            response.Setup(s => s.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            response.SetupProperty(res => res.StatusCode, (int) HttpStatusCode.OK);

            // Set up context.
            context.Setup(h => h.Request).Returns(request.Object);
            context.Setup(h => h.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            // Setup identity.
            // User identity always needs to be part of HTTP Context since
            // we use it at the BaseNavigationViewModelService level.
            var identity = new GenericIdentity("testUser");
            var principal = new GenericPrincipal(identity, new[] { "testRole" });
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()));
            context.Setup(x => x.User).Returns(principal);

            return context.Object;
        }

        /// <summary>Create Mock HttpContext with url and/or HTTP method. Use this method when testing anything related to URL and/or HTTP method.</summary>
        /// <param name="url">Specific url for Request.</param>
        /// <param name="httpMethod">Specific HTTP method for the request.</param>
        /// <returns>HttpContextBase with mock data.</returns>
        public static HttpContextBase MockHttpContext(string url, string httpMethod = null)
        {
            var context = MvcMockHelpers.MockHttpContext();

            if (url != null)
                context.Request.SetupRequestUrl(url);

            if (httpMethod != null)
                context.Request.SetHttpMethodResult(httpMethod);

            return context;
        }

        #region Private

        /// <summary>Set up HttpMethod for Request. (GET, POST, etc.).</summary>
        /// <param name="request">Current HttpRequestBase object.</param>
        /// <param name="httpMethod">HttpMethod: GET, POST, etc.</param>
        private static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            if (string.IsNullOrEmpty(httpMethod))
                throw new ArgumentNullException(nameof(httpMethod));
            Mock.Get(request).Setup(req => req.HttpMethod).Returns(httpMethod);
        }

        /// <summary>Return url without query string.</summary>
        /// <param name="url">Input url.</param>
        /// <returns>Url without query string.</returns>
        private static string GetUrlWithoutQueryString(string url)
        {
            return (url.Contains("?")) ? url.Substring(0, url.IndexOf("?")) : url;
        }

        /// <summary>Return the list of query string parameters from url.</summary>
        /// <param name="url">Input url.</param>
        /// <returns>Collection of parameters extracted from query string.</returns>
        private static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                var parameters = new NameValueCollection();
                var parts = url.Split("?".ToCharArray());
                var keys = parts[1].Split("&".ToCharArray());

                foreach (var key in keys)
                {
                    var part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            return null;
        }

        #endregion

        #region Extension methods

        /// <summary>Create Context for controller.</summary>
        /// <param name="controller">Current controller.</param>
        /// <param name="httpContext">HttpContextBase if don't want to get from default.</param>
        /// <param name="routeData">RouteData if any.</param>
        /// <param name="routes">Route table if any.</param>
        public static void SetMockControllerContext(this Controller controller,
            HttpContextBase httpContext = null,
            RouteData routeData = null,
            RouteCollection routes = null)
        {
            // Initialize data if NULL.
            routeData = routeData ?? new RouteData();
            routes = routes ?? RouteTable.Routes;
            httpContext = httpContext ?? MvcMockHelpers.MockHttpContext();

            var requestContext = new RequestContext(httpContext, routeData);
            var context = new ControllerContext(requestContext, controller);

            // Set context for controller.
            controller.Url = new UrlHelper(requestContext, routes);
            controller.ControllerContext = context;
        }

        /// <summary>Create Context for controller CAUTION: Use this method only if you want to execute ActionExecutingContext event.</summary>
        /// <param name="controller">Current controller.</param>
        /// <param name="controllerName">Name of the controller need to call (to pass to BaseNavigationController.</param>
        /// <param name="actionName">Name of the action need to call (to pass to BaseNavigationController).</param>
        /// <param name="httpContext">HttpContextBase if don't want to get from default.</param>
        /// <param name="routeData">RouteData if any.</param>
        /// <param name="routes">Route table if any.</param>
        /// <returns>Current controller context.</returns>
        public static ControllerContext SetMockControllerContext(this Controller controller,
            string controllerName,
            string actionName,
            HttpContextBase httpContext = null,
            RouteData routeData = null,
            RouteCollection routes = null)
        {
            // If values not passed then initialize.
            routeData = routeData ?? new RouteData();
            routeData.Values.Add("controller", controllerName);
            routeData.Values.Add("action", actionName);
            routes = routes ?? RouteTable.Routes;
            httpContext = httpContext ?? MvcMockHelpers.MockHttpContext();

            // Set up controller context.
            var requestContext = new RequestContext(httpContext, routeData);
            var context = new ControllerContext(requestContext, controller);
            controller.Url = new UrlHelper(requestContext, routes);
            controller.ControllerContext = context;

            // Set up action context.
            var actionDescriptor = new Mock<ActionDescriptor>();
            var actionContext = new ActionExecutingContext(controller.ControllerContext,
                actionDescriptor.Object,
                new Dictionary<string, object>());
            actionDescriptor.SetupGet(x => x.ActionName).Returns(actionName);

            // Call Action Executing.
            ((IActionFilter) controller).OnActionExecuting(actionContext);

            return context;
        }

        /// <summary>Set up Request Url for HttpRequest.</summary>
        /// <param name="request">Current HttpRequestBase request.</param>
        /// <param name="url">Url needs to pass to HttpRequest. Should begin with "~".</param>
        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            Uri uri = null;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
                throw new ArgumentException($"'{nameof(url)}' is not in a correct format.");

            if (!url.StartsWith("~/"))
                throw new ArgumentException($"'{nameof(url)}' should start with \"~/\".");

            // Setup the uri object for the request context.
            var mock = Mock.Get(request);
            mock.Setup(req => req.Url).Returns(uri);
            mock.Setup(req => req.QueryString).Returns(MvcMockHelpers.GetQueryStringParameters(url));
            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath).Returns(MvcMockHelpers.GetUrlWithoutQueryString(url));
            mock.Setup(req => req.PathInfo).Returns(string.Empty);
        }

        /// <summary>Get value of a property from an json object. This is for testing the value returned in Json result.</summary>
        /// <param name="obj">Object to get the value of a property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Value of input property if exists.</returns>
        public static object GetJsonProperty(this object obj, string propertyName)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var property = obj.GetType().GetProperty(propertyName);
            return property?.GetValue(obj, null);
        }

        /// <summary>Mock the view engine and set up for find view/partial view method.</summary>
        /// <param name="controllerContext">The current controller context.</param>
        public static void MockViewEngine(this ControllerContext controllerContext)
        {
            var viewHtmlContent = string.Empty;
            var view = new Mock<IView>();
            var engine = new Mock<IViewEngine>();

            view.Setup(x => x.Render(It.IsAny<ViewContext>(), It.IsAny<TextWriter>()))
                .Callback<ViewContext, TextWriter>((v, t) => { t.Write(viewHtmlContent); });
            var viewEngineResult = new ViewEngineResult(view.Object, engine.Object);

            engine.Setup(x => x.FindPartialView(controllerContext, It.IsAny<string>(), It.IsAny<bool>())).Returns(viewEngineResult);
            engine.Setup(x => x.FindView(controllerContext, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(viewEngineResult);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine.Object);
        }

        #endregion
    }
}