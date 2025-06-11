using EcommerceApi.Models;
using EcommerceApi.Context;

namespace EcommerceApi.Controllers
{
    public class SchedulesController : BaseController<Schedule, ApplicationDbContext>
    {
        public SchedulesController(ApplicationDbContext context, ILogger<SchedulesController> logger)
            : base(context, logger) { }

        protected override object GetEntityId(Schedule entity) => entity.Id;
    }
}