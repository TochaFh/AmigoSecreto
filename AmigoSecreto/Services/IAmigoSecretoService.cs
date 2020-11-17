using AmigoSecreto.Models;
using System.Threading.Tasks;

namespace AmigoSecreto.Services
{
    public interface IAmigoSecretoService
    {
        Task<string> CreateRoom(BasicRoom baseData);

        Task<BasicRoom> GetBasicRoom(string id);

        Task<SecretFriendResult> GetSecretFriend(string roomId, int personId);
    }
}
