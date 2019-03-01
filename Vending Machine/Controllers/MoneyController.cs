using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class MoneyController : Controller
    {
        private readonly VendingMachine<Drink, Money, Image> _machine;
        
        public MoneyController(VendingMachine<Drink, Money, Image> machine)
        {
            _machine = machine;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var moneis = _machine.GetAllMoneis();
            return Ok(moneis);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var money = _machine.GetMoney(id);
            if (money == null)
            {
                return NotFound();
            }

            return Ok(money);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Money money)
        {
            if(ModelState.IsValid)
            {
                _machine.AddMoney(money);
                return CreatedAtAction(nameof(Get), new { id = money.Id }, money);
            }
            return BadRequest(ModelState);
        } 

        [HttpPut]
        public IActionResult Edit([FromBody] Money money)
        {
            if(ModelState.IsValid)
            {
                _machine.UpdateMoney(money);                
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}