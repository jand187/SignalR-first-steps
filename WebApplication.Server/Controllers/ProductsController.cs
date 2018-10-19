using System.Web.Http;

namespace WebApplication.Server.Controllers
{
    public class ProductsController : ApiController
    {
        public string[] Get()
        {
            return new[]
            {
                "First",
                "Second"
            };
        }
    }
}
