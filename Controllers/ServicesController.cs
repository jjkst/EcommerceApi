using EcommerceApi.Models;
using EcommerceApi.Context;

namespace EcommerceApi.Controllers
{
    public class ServicesController : BaseController<Service, ApplicationDbContext>
    {
        public ServicesController(ApplicationDbContext context, ILogger<ServicesController> logger)
            : base(context, logger) { }

        protected override object GetEntityId(Service entity) => entity.Id;
    }
}