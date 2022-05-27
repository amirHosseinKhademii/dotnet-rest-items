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
    }
}