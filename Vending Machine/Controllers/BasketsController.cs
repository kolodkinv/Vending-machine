using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Dto;
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
            var basket = _machine.GetBasket(id);
            if (basket != null)
            {
                return Ok(basket);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create()
        {
            var basket = _machine.CreateBasket();
            return CreatedAtAction(nameof(Get), new { id = basket.Id }, basket);
        }

        [HttpPut("[action]")]
        public IActionResult AddProduct([FromBody] ProductInBasket product)
        {
            if (ModelState.IsValid)
            {
                _machine.AddProductToBasket(product.IdBasket, product.Id, product.Count);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("[action]")]
        public IActionResult AddMoney([FromBody] MoneyInBasket money)
        {
            if (ModelState.IsValid)
            {
                _machine.AddMoneyToBasket(money.IdBasket, money.Id, money.Count);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        public IActionResult Sell([FromBody] int id)
        {
            if (ModelState.IsValid)
            {    
                var oddMoney = _machine.Sell(id);
                return Ok(oddMoney);
            }

            return BadRequest(ModelState);
        }
    }
}