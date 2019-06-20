// COMMENTED OUT - UmbMapper.PropertyMappers.UmbracoPickerPropertyMapper states that:
// This mapper is only required when using Umbraco prior to version 7.6
using Moq;
using UmbMapper.Extensions;
using UmbMapper.Services;
using UmbMapper.Umbraco8.Tests.Mapping.Models;
using UmbMapper.Umbraco8.Tests.Mocks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Xunit;

namespace UmbMapper.Umbraco8.Tests.Mapping
{
    public class UmbracoPickerMappingTests : BaseUmbracoMappingTest, IClassFixture<UmbracoSupport>
    {
        private readonly UmbracoSupport support;
        protected static IUmbracoContextFactory _umbracoContextFactory;

        private readonly IUmbMapperService _umbMapperService;

        public UmbracoPickerMappingTests(UmbracoSupport support)
        {
            this.support = support;

            
            //// Set up the content cache to return our mocked object
            //var contentCacheMock = new Mock<IPublishedContentCache>();
            //contentCacheMock.Setup(cache => cache.GetById(1000)).Returns(new MockPublishedContent());

            //// Set up the Umbraco context, which will return the content cache
            //var umbCtxMock = new Mock<UmbracoContext>();
            //umbCtxMock.Setup(m => m.ContentCache).Returns(contentCacheMock.Object);

            //// Set up the Umbraco Contex Reference
            //var ctxRef = new Mock<UmbracoContextReference>();
            //ctxRef.Setup(r => r.UmbracoContext).Returns(umbCtxMock.Object);

            //// Mock the context factory
            //var ctxMock = new Mock<IUmbracoContextFactory>();
            //ctxMock.Setup(m => m.EnsureUmbracoContext(null)).Returns(ctxRef.Object);

            //// Instantiate our service with the context
            //_umbMapperService = new UmbMapperService(ctxMock.Object);
        }

        [Fact]
        public void UmbracoPickerProcessesIPublishedContent()
        {
            PublishedItem serviceResult = _umbMapperService.MapTo<PublishedItem>(this.support.Content);
            PublishedItem result = this.support.Content.MapTo<PublishedItem>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IPublishedContent>(result.PublishedInterfaceContent);
        }

        [Fact]
        public void UmbracoPickerProcessesPublishedContent()
        {
            PublishedItem result = this.support.Content.MapTo<PublishedItem>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<MockPublishedContent>(result.PublishedContent);
        }

        [Fact]
        public void UmbracoPickerProcessesMappedContent()
        {
            PublishedItem result = this.support.Content.MapTo<PublishedItem>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PublishedItem>(result.Child);
        }
    }
}
