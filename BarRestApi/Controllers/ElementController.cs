using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.Http;

namespace NewRestApi.Controllers
{
    public class ElementController : ApiController
    {
        private readonly IElement _service;

        public ElementController(IElement service)
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

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(ElementBindModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(ElementBindModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(ElementBindModel model)
        {
            _service.DelElement(model.ID);
        }
    }
}
