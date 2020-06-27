using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SourceCode.Web.Controllers;
using SourceCode.Web.Domain.Entities;
using SourceCode.Web.Models;
using SourceCode.Web.Options;
using SourceCode.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SourceCode.Tests
{
    public class MvcControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithClients()
        {
            // Arrange
            var clientService = new Mock<IClientService<Client>>();
            var config = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("testvalue");
            var env = new Mock<IWebHostEnvironment>();
            var pagingOptions = new PagingOptions
            {
                PageLinkCount = 5,
                PageSize = 5
            };
            clientService.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(GetTestClients())
                .Verifiable();

            var controller = new HomeController(clientService.Object, config.Object, pagingOptions, env.Object);

            // Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ClientsViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.ItemCount);
        }

        private List<Client> GetTestClients()
        {
            var clients = new List<Client>();
            clients.Add(new Client
            {
                Id = Guid.Empty,
                Name = "Justin",
                WebSite = "http://www.google.com",
                DirectorName = "Justin Reeves",
                EmailAddress = "reeves999999@hotmail.com",
                ClientProjects = new List<ClientProject>
                {
                    new ClientProject
                    {
                        Id = Guid.Empty,
                        Name = "Project 1"
                    }
                }
            });

            clients.Add(new Client
            {
                Id = Guid.Empty,
                Name = "Fay",
                WebSite = "http://www.apple.com",
                DirectorName = "Justin Reeves",
                EmailAddress = "fay999999@hotmail.com",
                ClientProjects = new List<ClientProject>
                {
                    new ClientProject
                    {
                        Id = Guid.Empty,
                        Name = "Project 2"
                    }
                }
            });

            return clients;
        }
    }
}
