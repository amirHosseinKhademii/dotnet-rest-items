using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemRepository repository;
        public ItemsController()
        {
            repository = new InMemItemRepository();
        }

        [HttpGet]
        public IEnumerable<Item> GetItems() => repository.GetItems();

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}