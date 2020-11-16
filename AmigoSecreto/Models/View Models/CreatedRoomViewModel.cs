using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Models
{
    public class CreatedRoomViewModel
    {
        public string Url { get; }
        public string Name { get; }
        
        public CreatedRoomViewModel(string roomUrl, string roomName)
        {
            Url = roomUrl;
            Name = roomName;
        }
    }
}
