using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extentions
    {
        public static ItemDto AsDto(this Item item) => new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreatedDate = item.CreatedDate
        };
    }
}