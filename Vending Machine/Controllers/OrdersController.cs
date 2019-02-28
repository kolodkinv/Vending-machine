using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController: Controller
    {
        private readonly VendingMachine<Drink, Money, Image> _machine;

        public OrdersController(VendingMachine<Drink, Money, Image> machine)
        {
            _machine = machine;
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]Order order)
        {
            if(ModelState.IsValid)
            {
                var oddMonies = _machine.Sell(order);
               
                return Ok(oddMonies);
            }
            return BadRequest(ModelState);
        } 
    }
}