using System.Web.Http;

namespace WebApplication.Server.Controllers
{
    public class ProductController : ApiController
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
