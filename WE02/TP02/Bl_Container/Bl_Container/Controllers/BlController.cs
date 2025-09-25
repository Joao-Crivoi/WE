using Bl_Container.Models;
using Bl_Container.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bl_Container.Controllers
{
    public class BLController : Controller
    {
        private readonly IBLService _service;

        public BLController(IBLService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var lista = _service.selectAll();
            return View(lista);              
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(BL dtoBL)
        {
            _service.save(dtoBL);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var bl = _service.select(new BL { ID = id });
            return View(bl);
        }

        [HttpPost]
        public IActionResult Edit(BL dtoBL)
        {
            _service.update(dtoBL);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _service.delete(new BL { ID = id });
            return RedirectToAction("Index");
        }
    }
}
