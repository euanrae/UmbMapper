using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace UmbMapper.Services
{
    public interface IUmbMapperService
    {
        T MapTo<T>(IPublishedContent content)
            where T : class;
    }
}
