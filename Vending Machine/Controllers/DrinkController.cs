using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class DrinkController : Controller
    {
        private readonly VendingMachine<Drink, Money> _machine;

        public DrinkController(VendingMachine<Drink, Money> machine)
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