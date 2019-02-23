using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vending_Machine.Models;
using Vending_Machine.Repositories;

namespace Vending_Machine.Controllers
{
    [Route("api/[controller]")]
    public class MoneyController : Controller
    {
        private readonly IRepository<Money> _moneyRepository;
        
        public MoneyController(IRepository<Money> moneyRepository)
        {
            _moneyRepository = moneyRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var moneis = _moneyRepository.GetAll();
            return Ok(new
            {
                Count = moneis.Count(),
                Moneis = moneis
            });
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var money = _moneyRepository.Get(id);
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
                _moneyRepository.Create(money);
                return CreatedAtAction(nameof(Get), new { id = money.Id }, money);
            }
            return BadRequest(ModelState);
        }   
    }
}