using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.Http;

namespace NewRestApi.Controllers
{
    public class StorageController : ApiController
    {
        private readonly IStorage _service;

        public StorageController(IStorage service)
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
        public void AddElement(StorageBindModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(StorageBindModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(StorageBindModel model)
        {
            _service.DelElement(model.ID);
        }
    }
}
