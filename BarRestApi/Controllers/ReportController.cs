using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.Http;

namespace NewRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerOrders(ReportBindModel model)
        {
            var list = _service.GetCustomerOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveCocktailPrice(ReportBindModel model)
        {
            _service.SaveCocktailPrice(model);
        }

        [HttpPost]
        public void SaveStoragesLoad(ReportBindModel model)
        {
            _service.SaveStoragesLoad(model);
        }

        [HttpPost]
        public void SaveCustomerOrders(ReportBindModel model)
        {
            _service.SaveCustomerOrders(model);
        }
    }
}
