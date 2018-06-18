using AbstractShopRestApi.Services;
using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.Http;

namespace NewRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrder(OrderBindModel model)
        {
            _service.CreateOrder(model);
        }

        [HttpPost]
        public void TakeOrderInWork(OrderBindModel model)
        {
            _service.TakeOrderInWork(model);
        }

        [HttpPost]
        public void FinishOrder(OrderBindModel model)
        {
            _service.FinishOrder(model.ID);
        }

        [HttpPost]
        public void PayOrder(OrderBindModel model)
        {
            _service.PayOrder(model.ID);
        }

        [HttpPost]
        public void PutElementOnStorage(ElementStorageBindModel model)
        {
            _service.PutElementOnStorage(model);
        }

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var List = service.GetInfoByAssembly();
            if(List == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(List);
        }
    }
}
