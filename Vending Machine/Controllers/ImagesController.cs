using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly UnitOfWorkEF _db;

        public ImagesController(UnitOfWorkEF db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var image = _db.Images.Get(id);
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

                var image = new Image { NormalImage = imageData };
                _db.Images.Create(image);
                return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
            }
            
            return BadRequest();
        }
    }
}