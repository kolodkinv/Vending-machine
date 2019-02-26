using Microsoft.AspNetCore.Mvc;
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
        private readonly VendingMachine<Drink, Money> _machine;
        private readonly IRepository<Image> _imageRepository;

        public DrinksController(VendingMachine<Drink, Money> machine, IRepository<Image> imageRepository)
        {
            _machine = machine;
            _imageRepository = imageRepository;
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
                var image = _imageRepository.Get(drink.Image.Id);
                drink.Image = image;
                _machine.AddNewProductToStorage(drink);
                return CreatedAtAction(nameof(Get), new { id = drink.Id }, drink);
            }
            return BadRequest(ModelState);
        } 

        [HttpPut]
        public IActionResult Edit([FromBody] Drink drink)
        {
            if(ModelState.IsValid)
            {
                _machine.UpdateProduct(drink);                
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}