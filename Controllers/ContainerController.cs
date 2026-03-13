using System.Threading.Tasks;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _service;
        public ContainerController(IContainerService service)
        {
            _service=service;
        }

         public async Task<IActionResult> Index()
        {
            var allcontainter = await _service.GetAllContainers();
            return View(allcontainter);
        }

         [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                await _service.CreateContainer(name);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                await _service.DeleteContainer(name);
            }

            return RedirectToAction("Index");
        }
    }
}