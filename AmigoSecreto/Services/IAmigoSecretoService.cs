using AmigoSecreto.Models;
using System.Threading.Tasks;

namespace AmigoSecreto.Services
{
    public interface IAmigoSecretoService
    {
        Task<int> CreateRoom(BasicRoom baseData);

        Task<BasicRoom> GetBasicRoom(int id);

        Task<SecretFriendResult> GetSecretFriend(int roomId, int personId);
    }
}
