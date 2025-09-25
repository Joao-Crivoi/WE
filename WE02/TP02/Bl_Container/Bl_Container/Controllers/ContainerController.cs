using Bl_Container.Models;
using Bl_Container.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bl_Container.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _service;
        private readonly IBLService _blService;

        public ContainerController(IContainerService service, IBLService blService)
        {
            _service = service;
            _blService = blService;
        }

        public IActionResult Index()
        {
            var containers = _service.selectAll();
            return View(containers);
        }

        public IActionResult Create()
        {
            var blList = _blService.selectAll(); // pega todos os BLs do banco
            ViewBag.BLs = new SelectList(blList, "ID", "Numero");
            // "ID" -> valor da option
            // "Numero" -> texto exibido

            return View();
        }

        // POST: Container/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Container container)
        {
            if (ModelState.IsValid)
            {
                _service.save(container);
                return RedirectToAction(nameof(Index));
            }

            // se der erro, recarrega o combo
            var blList = _blService.selectAll();
            ViewBag.BLs = new SelectList(blList, "ID", "Numero");
            return View(container);
        }

        public IActionResult Edit(int id)
        {
            var container = _service.select(new Container { ID = id });
            ViewBag.BLs = new SelectList(new List<BL>(), "ID", "Numero", container.IDBl);
            return View(container);
        }

        [HttpPost]
        public IActionResult Edit(Container dtoContainer)
        {
            _service.update(dtoContainer);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _service.delete(new Container { ID = id });
            return RedirectToAction("Index");
        }
    }
}
