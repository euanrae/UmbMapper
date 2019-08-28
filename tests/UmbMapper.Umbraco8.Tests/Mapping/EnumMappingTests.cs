﻿using System.Collections.Generic;
using UmbMapper.Umbraco8.Tests.Mapping.Models;
using UmbMapper.Umbraco8.Tests.Mocks;
using UmbMapper.Extensions;
using Xunit;
using Umbraco.Core.Models.PublishedContent;
using Moq;
using Umbraco.Web;
using UmbMapper.Factories;

namespace UmbMapper.Umbraco8.Tests.Mapping
{
    public class EnumMappingTests : IClassFixture<UmbracoSupport>
    {
        private readonly UmbracoSupport support;
        private readonly IUmbMapperRegistry umbMapperRegistry;
        private readonly IUmbMapperService umbMapperService;

        public EnumMappingTests(UmbracoSupport support)
        {
            this.support = support;
            // This is needed to access the culture info
            this.support.SetupUmbracoContext();

            this.umbMapperRegistry = new UmbMapperRegistry(Mock.Of<IUmbracoContextFactory>());
            this.support.InitMappers(this.umbMapperRegistry);

            this.umbMapperService = new UmbMapperService(this.umbMapperRegistry, new MappingProcessorFactory());
        }

        [Fact]
        public void MapperReturnsDefaultEnum()
        {
            var registry = new UmbMapperRegistry(Mock.Of<IUmbracoContextFactory>());
            this.support.InitMappers(registry);

            var mapperService = new UmbMapperService(registry, new MappingProcessorFactory());
            const PlaceOrder placeOrder = PlaceOrder.Fourth;

            MockPublishedContent content = this.support.Content;
            content.Properties = new List<IPublishedProperty>
            {
                Mocks.UmbMapperMockFactory.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), null)
            };

            PublishedItem result = mapperService.MapTo<PublishedItem>(content);

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
                Mocks.UmbMapperMockFactory.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), (int)placeOrder)
            };

            PublishedItem result = this.umbMapperService.MapTo<PublishedItem>(content);

            Assert.Equal(placeOrder, result.PlaceOrder);
        }

        [Fact]
        public void MapperReturnsCorrectEnumFromString()
        {
            const PlaceOrder placeOrder = PlaceOrder.Fourth;

            MockPublishedContent content = this.support.Content;
            content.Properties = new List<IPublishedProperty>
            {
                Mocks.UmbMapperMockFactory.CreateMockPublishedProperty(nameof(PublishedItem.PlaceOrder), placeOrder.ToString())
            };

            PublishedItem result = this.umbMapperService.MapTo<PublishedItem>(content);

            Assert.Equal(placeOrder, result.PlaceOrder);
        }
    }
}