using System.Linq;
using System.Xml;
using Moq;
using NUnit.Framework;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Services;
using Umbraco.Tests.Cache.PublishedCache;
using Umbraco.Tests.LegacyXmlPublishedCache;
using Umbraco.Tests.TestHelpers;
using Umbraco.Tests.Testing;
using Umbraco.Tests.Testing.Objects.Accessors;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace UmbMapper.Umbraco8Source.Tests
{
    [TestFixture]
    [UmbracoTest(Database = UmbracoTestOptions.Database.NewSchemaPerFixture)]
    public class UmbMapper8TestTest : UmbracoTestBase
    {
        

        protected override void Initialize()
        {
            base.Initialize();

            
        }

        [Test]
        public void TestRuns()
        {
            Assert.IsTrue(true);
        }


        
    }
}