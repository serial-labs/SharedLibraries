using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SerialLabs.Web.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SerialLabs.Web.Http.Tests
{
    [TestClass]
    public class RequireHttpsAttributeTest
    {
        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpGet_ReturnsFaultedTask()
        {
            // Arrange
            string actual = "http://somesite/action";
            string expected = "https://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Get;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreNotEqual(response, result.Result);
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.Found);
            Assert.AreEqual(result.Result.Headers.Location, expected);
        }
        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpsGet_ReturnsSuccessTask()
        {
            // Arrange
            string actual = "https://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Get;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreEqual(response, result.Result);
        }

        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpHead_ReturnsFaultedTask()
        {
            // Arrange
            string actual = "http://somesite/action";
            string expected = "https://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Head;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreNotEqual(response, result.Result);
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.Found);
            Assert.AreEqual(result.Result.Headers.Location, expected);
        }

        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpHead_ReturnsSuccessTask()
        {
            // Arrange
            string actual = "https://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Head;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreEqual(response, result.Result);
        }

        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpPost_ReturnsFaultedTask()
        {
            // Arrange
            string actual = "http://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Post;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreNotEqual(response, result.Result);
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ExecuteRequireHttpsFilterAsync_IfHttpPost_ReturnsSuccessTask()
        {
            // Arrange
            string actual = "https://somesite/action";
            HttpActionContext context = ContextUtil.CreateActionContext();
            context.Request.RequestUri = new Uri(actual);
            context.Request.Method = HttpMethod.Post;

            Mock<RequireHttpsAttribute> filterMock = new Mock<RequireHttpsAttribute>()
            {
                CallBase = true,
            };

            var filter = (IAuthorizationFilter)filterMock.Object;
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            var result = filter.ExecuteAuthorizationFilterAsync(context, CancellationToken.None, () => Task.FromResult(response));

            // Assert
            result.WaitUntilCompleted();
            Assert.AreEqual(response, result.Result);            
        }
        
    }
}
