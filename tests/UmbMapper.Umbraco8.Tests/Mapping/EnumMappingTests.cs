﻿using System.Collections.Generic;
using System.Linq;
using UmbMapper.Umbraco8.Tests.Mapping.Models;
using UmbMapper.Umbraco8.Tests.Mocks;
using UmbMapper.Extensions;
using Xunit;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using System.Reflection;
using Moq;
using Umbraco.Tests.TestHelpers;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Web.PublishedCache;
using Umbraco.Core.Configuration;
using Umbraco.Web.Security;
using Umbraco.Core.Services;
using Umbraco.Web.Routing;
using Umbraco.Tests.Testing.Objects.Accessors;

namespace UmbMapper.Umbraco8.Tests.Mapping
{
    public class EnumMappingTests : BaseUmbracoMappingTest, IClassFixture<UmbracoSupport>
    {
        private readonly UmbracoSupport support;

        public EnumMappingTests(UmbracoSupport support)
        {
            this.support = support;
        }

        [Fact]
        public void MapperReturnsDefaultEnum()
        {
            const PlaceOrder placeOrder = PlaceOrder.Fourth;

            MockPublishedContent content = this.support.Content;
            content.Properties = new List<IPublishedProperty>
            {
                MockHelper.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), null)
            };

            PublishedItem result = content.MapTo<PublishedItem>();

            Assert.NotEqual(placeOrder, result.PlaceOrder);
            Assert.Equal(default(PlaceOrder), result.PlaceOrder);
        }

        [Fact]
        public void MapperReturnsCorrectEnumFromInt()
        {
            const PlaceOrder placeOrder = PlaceOrder.Fourth;

            MockPublishedContent content = this.support.Content;
            content.Properties = new List<IPublishedProperty>
            {
                MockHelper.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), (int)placeOrder)
            };

            PublishedItem result = content.MapTo<PublishedItem>();

            Assert.Equal(placeOrder, result.PlaceOrder);
        }

        [Fact]
        public void MapperReturnsCorrectEnumFromString()
        {
            const PlaceOrder placeOrder = PlaceOrder.Fourth;

            MockPublishedContent content = this.support.Content;
            content.Properties = new List<IPublishedProperty>
            {
                MockHelper.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), placeOrder.ToString())
            };

            PublishedItem result = content.MapTo<PublishedItem>();

            Assert.Equal(placeOrder, result.PlaceOrder);
        }

        [Fact]
        public void UmbracoContextNotNull()
        {
            // Get the internal constructor
            var constructor = typeof(UmbracoContext)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();

            var ctorParams = constructor.GetParameters();

            // build required parameters
            var _httpContextFactory = new FakeHttpContextFactory("~/Home");
            var umbracoSettings = Mock.Of<IUmbracoSettingsSection>();
            var globalSettings = Mock.Of<IGlobalSettings>();
            var publishedSnapshotService = new Mock<IPublishedSnapshotService>();
            publishedSnapshotService.Setup(x => x.CreatePublishedSnapshot(It.IsAny<string>())).Returns(Mock.Of<IPublishedSnapshot>());
            var ctxMock = new Mock<UmbracoContext>();

            var instance =
                constructor.Invoke(
                    new object[] {
                        _httpContextFactory.HttpContext,
                        publishedSnapshotService.Object,
                        new WebSecurity(_httpContextFactory.HttpContext, Mock.Of<IUserService>(), globalSettings),
                        umbracoSettings,
                        Enumerable.Empty<IUrlProvider>(),
                        globalSettings,
                        new TestVariationContextAccessor()
                    }
                );


        }
    }
}