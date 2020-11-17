using AmigoSecreto.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AmigoSecreto.Models
{
    public class Room
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        public Person[] People { get; set; }

        public DateTime CreationDate { get; set; }

        #region Constructors

        public Room()
        {
            
        }

        public Room(BasicRoom baseData)
        {
            Title = baseData.Title;
            People = baseData.People?.Select((basicPerson, i) => basicPerson.ToPerson(id: i)).ToArray();
        }

        #endregion

        public void Sortear()
        {
            int[] shuffled = People.Select(p => p.Id).Shuffle().ToArray();

            for (int i = 0; i < People.Length; i++)
            {
                var personId = People[i].Id;
                int secret = shuffled[i];

                if (personId == secret)
                {
                    int next = NextInt.Next(i, People.Length);

                    shuffled[i] = shuffled[next];
                    shuffled[next] = secret;

                    secret = shuffled[i];
                }
            }

            for (int i = 0; i < People.Length; i++)
            {
                People[i].SecretFriendId = shuffled[i];
            }
        }

        #region Json

        public string ToJson(JsonSerializerOptions opt = null)
        {
            return JsonSerializer.Serialize(this, opt);
        }

        public override string ToString() => ToJson();

        public static Room FromJson(string json, JsonSerializerOptions opt = null)
        {
            return JsonSerializer.Deserialize<Room>(json, opt);
        }

        #endregion

        public BasicRoom ToBasic() => new BasicRoom(this);
    }

    public class BasicRoom
    {
        public string Title { get; set; }

        public string Id { get; }

        public List<BasicPerson> People { get; set; }

        #region Constructors

        public BasicRoom()
        {

        }

        public BasicRoom(string title) : this()
        {
            Title = title;
        }

        public BasicRoom(Room room)
        {
            Title = room.Title;
            People = room.People?.Select(p => p.ToBasic()).ToList();
            Id = room.Id.ToString();
        }

        #endregion
        
        public override string ToString() => Title;
    }
}
