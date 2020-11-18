using AmigoSecreto.Models;
using AmigoSecreto.Services;
using FastConsole;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;

namespace AmigoSecreto
{
    public class MongoConnectionHelper
    {
        private readonly IMongoClient _client;

        private readonly string _roomsCollectionName;

        public MongoConnectionHelper(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.Value("DB_CONNECTION"));
            _roomsCollectionName = configuration.Value("RoomsCollectionName");
            TestConnection();
        }

        private void TestConnection()
        {
            var db = _client.GetDatabase("MainData");
            var collection = db.GetCollection<Room>(_roomsCollectionName);

            Out.Println($"\nConexão bem sucedida com collection {collection.CollectionNamespace}.\n", FColor.Cyan);
        }

        public IMongoCollection<Room> GetRoomsCollection()
        {
            var db = _client.GetDatabase("MainData");
            return db.GetCollection<Room>(_roomsCollectionName);
        }
    }
}