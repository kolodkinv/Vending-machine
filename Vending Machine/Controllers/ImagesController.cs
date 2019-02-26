using System.IO;
using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Repositories;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IRepository<Image> _repository;

        public ImagesController(IRepository<Image> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var images = _repository.GetAll();
            return Ok(images);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var image = _repository.Get(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
        
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            var file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
            if (file != null)
            {
                byte[] imageData;
         
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)file.Length);
                }

                var image = new Image
                {
                    NormalImage = imageData
                };
                
                _repository.Create(image);
                return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
            }
            
            return BadRequest();
        }
    }
}