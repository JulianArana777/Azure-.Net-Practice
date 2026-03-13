using API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _service;

        public BlobController(IBlobService service)
        {
            _service = service;
        }

        // LISTAR BLOBS DE UN CONTAINER
        public async Task<IActionResult> Index(string containerName)
        {
            var blobs = await _service.GetAllBlobs(containerName);

            ViewBag.ContainerName = containerName;

            return View(blobs);
        }

        // SUBIR ARCHIVO
        [HttpPost]
        public async Task<IActionResult> Upload(string containerName, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _service.CreateBlob(file.FileName, file, containerName, null);
            }

            return RedirectToAction(nameof(Index), new { containerName });
        }

        // ELIMINAR BLOB
        [HttpPost]
        public async Task<IActionResult> Delete(string containerName, string name)
        {
            await _service.DeleteBlob(containerName, name);

            return RedirectToAction(nameof(Index), new { containerName });
        }

        // ABRIR BLOB
        public async Task<IActionResult> Open(string containerName, string name)
        {
            var url = await _service.GetABlob(containerName, name);

            if (url == null)
                return NotFound();

            return Redirect(url);
        }        
    }
}