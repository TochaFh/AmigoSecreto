using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Models
{
    public class RoomViewModel
    {
        public RoomViewModel(BasicRoom room)
        {
            Room = room;
        }
        public RoomViewModel(BasicRoom room, int? alreadyRevealed) : this(room)
        {
            AlreadyRevealed = alreadyRevealed;
        }

        public BasicRoom Room { get; }
        public int? AlreadyRevealed { get; }
    }
}
