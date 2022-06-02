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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync() =>
            (await repository.GetItemsAsync()).Select(item => item.AsDto());


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]

        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto body)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = body.Name,
                Price = body.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };
            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto body)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            Item updated = item with
            {
                Name = body.Name,
                Price = body.Price,
            };
            await repository.UpdateItemAsync(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAync(id);
            return NoContent();
        }
    }
}