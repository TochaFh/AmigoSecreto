using AmigoSecreto.Models;
using FastConsole;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmigoSecreto.Services
{
    public class RoomsRepository : IRoomsRepository
    {
        DirectoryInfo _roomsDirectory;

        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions() { WriteIndented = true };

        #region Constructors

        public RoomsRepository(string name = "rooms")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server data", name);
            _roomsDirectory = new DirectoryInfo(path);
            if (!_roomsDirectory.Exists) _roomsDirectory.Create();

            Out.Println(("\nRoomsRepository aberto para " + name.I(FColor.DarkYellow) + ":").I(FColor.Cyan));
            Out.Println(_roomsDirectory.FullName + "\n");
        }

        #endregion

        #region IRoomsRepository

        public void SaveRoom(Room room)
        {
            if (!File.Exists(FilePath(room))) throw new Exception($"Uma sala com id {room.Id} NÃO existe");

            WriteFile(room);
        }

        public Task SaveRoomAsync(Room room)
        {
            if (!File.Exists(FilePath(room))) throw new Exception($"Uma sala com id {room.Id} NÃO existe");

            return WriteFileAsync(room);
        }

        public void AddRoom(Room room)
        {
            if (room.Id != 0) throw new Exception($"O id da sala deve ser igual a zero no 'Add' pois será gerado um novo id único");

            room.Id = GenerateId();
            
            WriteFile(room);
        }

        public Task AddRoomAsync(Room room)
        {
            if (room.Id != 0) throw new Exception($"O id da sala deve ser igual a zero no 'Add' pois será gerado um novo id único");

            room.Id = GenerateId();

            return WriteFileAsync(room);
        }

        public Room GetRoom(int id)
        {
            if (id == 0) throw new Exception("É preciso o id do cliente! (não pode ser 0)");

            string path = FilePath(id);

            if (!File.Exists(path))
            {
                return null;
            }

            string json = File.ReadAllText(path);
            return Room.FromJson(json, _jsonOptions);
        }

        public async Task<Room> GetRoomAsync(int id)
        {
            if (id == 0) throw new Exception("É preciso o id do cliente! (não pode ser 0)");

            string path = FilePath(id);

            if (!File.Exists(path))
            {
                return null;
            }

            string json = await File.ReadAllTextAsync(path);
            return Room.FromJson(json, _jsonOptions);
        }

        public void DeleteRoom(int id)
        {
            string path = FilePath(id);

            if (File.Exists(path)) File.Delete(path);
            else throw new Exception($"Uma sala com id {id} NÃO existe");
        }

        public Task DeleteRoomAsync(int id)
        {
            return Task.Run(() => DeleteRoom(id));
        }

        #endregion

        private string FilePath(Room room) => FilePath(room.Id);
        private string FilePath(int roomId) => _roomsDirectory.GetFile($"room_{roomId}.json");

        private void WriteFile(Room room)
        {
            string path = FilePath(room);
            string json = room.ToJson(_jsonOptions);

            File.WriteAllText(path, json);
        }

        private async Task WriteFileAsync(Room room)
        {
            string path = FilePath(room);
            string json = room.ToJson(_jsonOptions);

            await File.WriteAllTextAsync(path, json);
        }

        private int GenerateId()
        {
            int guid = Math.Abs(Guid.NewGuid().GetHashCode());
            return File.Exists(FilePath(guid)) ? Repeat() : guid;

            int Repeat()
            {
                Out.Println("Repeating id generation", FColor.Yellow);
                return GenerateId();
            }
        }
    }
}
