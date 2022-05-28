using Catalog.Entities;

namespace Catalog.Repositories
{


    public class InMemItemRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item {Id= Guid.NewGuid(), Name="Posion", Price=10, CreatedDate=DateTimeOffset.Now},
            new Item {Id= Guid.NewGuid(), Name="Sword", Price=20, CreatedDate=DateTimeOffset.Now},
            new Item {Id= Guid.NewGuid(), Name="Gun", Price=100, CreatedDate=DateTimeOffset.Now},
        };

        public IEnumerable<Item> GetItems() => items;

        public Item GetItem(Guid Id) => items.Where(item => item.Id == Id).SingleOrDefault();

        public void CreateItem(Item item) => items.Add(item);

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(i => i.Id == item.Id);
            items[index] = item;
        }
    }
}