using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace UmbMapper.Services
{
    public class UmbMapperService : IUmbMapperService
    {
        private readonly IUmbracoContextFactory _ctxFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbMapperService"/> class.
        /// Umbraco Context Factory which is needed to ensure umbraco context
        /// before doing any mapping
        /// </summary>
        /// <param name="ctxFactory"></param>
        public UmbMapperService(IUmbracoContextFactory ctxFactory)
        {
            _ctxFactory = ctxFactory;
            _ctxFactory.EnsureUmbracoContext();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public T MapTo<T>(IPublishedContent content) where T : class
        {
            Type type = typeof(T);
            return (T)MapTo(content, type);
        }

        public object MapTo(IPublishedContent content, Type type)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            UmbMapperRegistry.Mappers.TryGetValue(type, out IUmbMapperConfig mapper);

            if (mapper is null)
            {
                throw new InvalidOperationException($"No mapper for the given type {type} has been registered.");
            }

            return mapper.Map(content);
        }
    }
}
