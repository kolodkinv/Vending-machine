using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class DrinksController : Controller
    {
        private readonly VendingMachine<Drink, Money, Image> _machine;

        public DrinksController(VendingMachine<Drink, Money, Image> machine)
        {
            _machine = machine;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var drinks = _machine.GetAllProducts();
            return Ok(drinks);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var drink = _machine.GetProduct(id);
            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Drink drink)
        {
            if(ModelState.IsValid)
            {
                var image = _machine.GetImage(drink.Image.Id);
                drink.Image = image;
                _machine.AddProduct(drink);
                return CreatedAtAction(nameof(Get), new { id = drink.Id }, drink);
            }
            return BadRequest(ModelState);
        } 

        [HttpPut]
        public IActionResult Edit([FromBody] Drink drink)
        {
            if(ModelState.IsValid)
            {
                if (drink.Image != null && drink.Image.ProductId == null)
                {
                    var drinkInStore = _machine.GetProduct(drink.Id);
                    var image = _machine.GetImage(drinkInStore.Image.Id);
                    image.NormalImage = drink.Image.NormalImage;
                    _machine.UpdateImage(image);
                    _machine.RemoveImage(drink.Image.Id);
                }
                else
                {
                    _machine.UpdateProduct(drink);     
                }
          
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}