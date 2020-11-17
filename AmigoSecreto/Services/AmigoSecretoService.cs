using AmigoSecreto.Models;
using System;
using FastConsole;
using System.Threading.Tasks;
using System.Linq;

namespace AmigoSecreto.Services
{
    public class AmigoSecretoService : IAmigoSecretoService
    {
        IRoomsRepository _repo;

        public AmigoSecretoService(IRoomsRepository repo)
        {
            _repo = repo;
        }

        #region IAmigoSecretoService

        public async Task<string> CreateRoom(BasicRoom baseData)
        {
            if (baseData?.Title.IsNullOrWhite() ?? true || baseData.People?.Count < 4) return "error";

            Room room = new Room(baseData);
            room.Sortear();

            await _repo.AddRoomAsync(room);
            return room.Id.ToString();
        }

        public async Task<BasicRoom> GetBasicRoom(string id)
        {
            Room room = await _repo.GetRoomAsync(id);
            return room?.ToBasic();
        }

        public async Task<SecretFriendResult> GetSecretFriend(string roomId, int personId)
        {
            Room room = await _repo.GetRoomAsync(roomId);

            if (room == null) return new(Sfr.NotFound);

            Person person = room.People.ElementAtOrDefault(personId);

            if (person == null) return new(Sfr.NotFound);

            if (person.SecretFriendWasRevealed) return new(Sfr.AlreadyRevelead) { Person = person.ToBasic() };

            Person secret = room.People.ElementAtOrDefault(person.SecretFriendId);

            if (secret == null) return new(Sfr.NotFound) { Person = person.ToBasic() };

            person.SecretFriendWasRevealed = true;

            await _repo.SaveSecretRevealedAsync(roomId, personId);

            return new(Sfr.Success) { Person = person.ToBasic(), SecretFriend = secret.Name };
        }

        #endregion
    }
}
