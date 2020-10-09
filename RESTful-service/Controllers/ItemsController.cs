using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTful_service.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        public static List<Item> items = new List<Item>()
        {
            new Item(1, "bread", "Low", 33),
            new Item(2, "Bread", "Middle", 21),
            new Item(3, "Beer", "low", 70.5),
            new Item(4, "Soda", "High", 21.4),
            new Item(5, "Milk", "Low", 55.8)
        };

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET api/<ItemsController>/5
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return items.Find(i => i.Id == id);
        }

        // POST api/<ItemsController>
        [HttpPost]
        [DisableCors]
        public void Post([FromBody] Item value)
        {
            items.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut]
        [Route("{id}")]
        [EnableCors("AllowPostGet")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {

                IEnumerable<Item> checkList = Get();
                if (!checkList.Any(x => x.Id == item.Id))
                {
                    item.Id = value.Id;
                    item.Name = value.Name;
                    item.Quality = value.Quality;
                    item.Quantity = value.Quantity;
                }
            }

            if (item == null)
            {
                items.Add(new Item()
                {
                    Id = value.Id,
                    Name = value.Name,
                    Quality = value.Quality,
                    Quantity = value.Quantity
            });
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            items.Remove(item);
        }

        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(string substring)
        {
            var list =  items.FindAll(i => i.Name.Contains(substring, StringComparison.CurrentCultureIgnoreCase));
            return list;
        }
        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetFromQuality(string substring)
        {
            var list = items.FindAll(i => i.Quality.Contains(substring, StringComparison.CurrentCultureIgnoreCase));
            return list;
        }

        [HttpGet]
        [Route("search")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {
            var list = items.FindAll(i=>i.Quantity >= filter.LowQuantity && i.Quantity <= filter.HighQuantity);
            return list;
        }
    }
}
