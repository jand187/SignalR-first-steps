using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SelfHost.Server.Controllers
{
    public class ProductController : ApiController
    {
        public IEnumerable<string> Get()
        {
            Console.WriteLine($"Call to {nameof(ProductController)}.Get");
            return new[]
            {
                "One",
                "Two",
                "Three"
            };
        }
    }
}
