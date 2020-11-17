using AmigoSecreto.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using FastConsole;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Services
{
    public class MongoRoomsRepository : IRoomsRepository
    {
        private IMongoCollection<Room> _collection;

        public MongoRoomsRepository(IMongoClient client)
        {
            var db = client.GetDatabase("MainData");
            _collection = db.GetCollection<Room>("Rooms");

            Out.Println("Conexão aberta com collection Rooms de MainData.", FColor.Cyan);
        }

        public void AddRoom(Room room)
        {
            _collection.InsertOne(room);
        }

        public Task AddRoomAsync(Room room)
        {
            return _collection.InsertOneAsync(room);
        }

        public void DeleteRoom(string id)
        {
            var filter = IdFilter(id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteRoomAsync(string id)
        {
            var filter = IdFilter(id);
            return _collection.FindOneAndDeleteAsync(filter);
        }

        public Room GetRoom(string id)
        {
            var filter = IdFilter(id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public async Task<Room> GetRoomAsync(string id)
        {
            var filter = IdFilter(id);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public void SaveSecretRevealed(string roomId, int personId)
        {
            var filter = Builders<Room>.Filter.And(
                IdFilter(roomId),
                Builders<Room>.Filter.ElemMatch(room => room.People, Builders<Person>.Filter.Eq("Id", personId))
                );

            var update = Builders<Room>.Update.Set(room => room.People[-1].SecretFriendWasRevealed, true);

            _collection.UpdateOne(filter, update);
        }

        public Task SaveSecretRevealedAsync(string roomId, int personId)
        {
            var filter = Builders<Room>.Filter.And(
                IdFilter(roomId),
                Builders<Room>.Filter.ElemMatch(room => room.People, Builders<Person>.Filter.Eq("Id", personId))
                );

            var update = Builders<Room>.Update.Set(room => room.People[-1].SecretFriendWasRevealed, true);

            return _collection.UpdateOneAsync(filter, update);
        }

        private FilterDefinition<Room> IdFilter(string id)
        {
            return Builders<Room>.Filter.Eq("Id", new ObjectId(id));
        }
    }
}
