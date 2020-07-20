using System;
using GigHub.Api;
using GigHub.Interfaces;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        public GigsControllerTests()
        {
            var mockUoW = new Mock<IUnitOfWork>();

            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser("1", "user1@domain.com");

        }
        [TestMethod]
        public void Cancel()
        { 
        }
    }
}
