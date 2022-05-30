using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;
        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems() => repository.GetItems().Select(item => item.AsDto());


        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]

        public ActionResult<ItemDto> CreateItem(CreateItemDto body)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = body.Name,
                Price = body.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto body)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            Item updated = item with
            {
                Name = body.Name,
                Price = body.Price,
            };
            repository.UpdateItem(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }
    }
}