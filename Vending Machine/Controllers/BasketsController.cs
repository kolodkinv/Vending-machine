using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class BasketsController : Controller
    {
        private readonly VendingMachine<Drink, Money> _machine;

        public BasketsController(VendingMachine<Drink, Money> machine)
        {
            _machine = machine;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            return BadRequest();
        }
    }
}