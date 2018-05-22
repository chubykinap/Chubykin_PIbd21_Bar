using BarService.BindingModels;
using BarService.Interfaces;
using System;
using System.Web.Http;

namespace NewRestApi.Controllers
{
    public class CocktailController : ApiController
    {
        private readonly ICocktail _service;

        public CocktailController(ICocktail service)
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
        public void AddElement(CocktailBindModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(CocktailBindModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(CocktailBindModel model)
        {
            _service.DelElement(model.ID);
        }
    }
}
