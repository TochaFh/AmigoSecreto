using AmigoSecreto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AmigoSecreto.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Person> People { get; set; }

        #region Constructors

        public Room()
        {

        }

        public Room(BasicRoom baseData)
        {
            Title = baseData.Title;
            People = baseData.People?.Select((basicPerson, i) => basicPerson.ToPerson(id: i)).ToList();
        }

        #endregion

        public void Sortear()
        {
            int[] shuffled = People.Select(p => p.Id).Shuffle().ToArray();

            for (int i = 0; i < People.Count; i++)
            {
                var personId = People[i].Id;
                int secret = shuffled[i];

                if (personId == secret)
                {
                    int next = NextInt.Next(i, People.Count);

                    shuffled[i] = shuffled[next];
                    shuffled[next] = secret;

                    secret = shuffled[i];
                }
            }

            for (int i = 0; i < People.Count; i++)
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

        public int Id { get; }

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
            Id = room.Id;
        }

        #endregion

        public Room ToRoom() => new Room(this);

        public override string ToString() => Title;
    }
}
