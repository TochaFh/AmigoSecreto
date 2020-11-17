using AmigoSecreto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Services
{
    public interface IRoomsRepository
    {
        /// <summary>
        /// (GET) - Retorna a room do id especificado ou null caso não exita.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room GetRoom(string id);

        /// <summary>
        /// (GET) - Retorna a room do id especificado ou null caso não exita.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Room> GetRoomAsync(string id);

        /// <summary>
        /// (POST) - Gera um novo id único para a room e adiciona ao repositório.
        /// </summary>
        /// <param name="room"></param>
        public void AddRoom(Room room);

        /// <summary>
        /// (POST) - Gera um novo id único para a room e adiciona ao repositório.
        /// </summary>
        /// <param name="room"></param>
        public Task AddRoomAsync(Room room);

        /*/// <summary>
        /// (PUT) - Salva as mudanças dessa room para uma room já existente no repositório (pelo id).
        /// </summary>
        /// <param name="room"></param>
        public void SaveRoom(Room room);

        /// <summary>
        /// (PUT) - Salva as mudanças dessa room para uma room já existente no repositório (pelo id).
        /// </summary>
        /// <param name="room"></param>
        public Task SaveRoomAsync(Room room);*/

        /// <summary>
        /// (DELETE) - Deleta uma room pelo id do repositório (definitivamente).
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRoom(string id);

        /// <summary>
        /// (DELETE) - Deleta uma room pelo id do repositório (definitivamente).
        /// </summary>
        /// <param name="id"></param>
        public Task DeleteRoomAsync(string id);

        /// <summary>
        /// (PUT) - Salva como true o valor do SecretFriendWasRevealed da pessoa (id) de um room (id).
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="personId"></param>
        public void SaveSecretRevealed(string roomId, int personId);

        /// <summary>
        /// (PUT) - Salva como true o valor do SecretFriendWasRevealed da pessoa (id) de um room (id).
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="personId"></param>
        public Task SaveSecretRevealedAsync(string roomId, int personId);
    }
}
