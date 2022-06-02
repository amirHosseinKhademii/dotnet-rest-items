
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MDBItemsRepo : IItemsRepository
    {

        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MDBItemsRepo(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid Id)
        {
            var filter = filterBuilder.Eq(ex => ex.Id, Id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid Id)

        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(ex => ex.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}