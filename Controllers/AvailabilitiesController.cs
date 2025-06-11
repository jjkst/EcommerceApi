using EcommerceApi.Models;
using EcommerceApi.Context;

namespace EcommerceApi.Controllers
{
    public class AvailabilitiesController : BaseController<Availability, ApplicationDbContext>
    {
        public AvailabilitiesController(ApplicationDbContext context, ILogger<AvailabilitiesController> logger)
            : base(context, logger) { }

        protected override object GetEntityId(Availability entity) => entity.Id;
    }
}