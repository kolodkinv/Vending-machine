using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Repositories;
using Vending_Machine.Seller;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class MoneyController : Controller
    {
        private readonly VendingMachine _machine;
        
        public MoneyController(VendingMachine machine)
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
                _machine.AddNewMoneyToStorage(money);
                return CreatedAtAction(nameof(Get), new { id = money.Id }, money);
            }
            return BadRequest(ModelState);
        }  
        
        [HttpPut("[action]")]
        public IActionResult Increase([FromBody] Money money)
        {
            if(ModelState.IsValid)
            {
                _machine.IncreaseMoneyInStorage(money.Id, money.Count);
                return Ok();
            }
            return BadRequest(ModelState);
        } 
    }
}