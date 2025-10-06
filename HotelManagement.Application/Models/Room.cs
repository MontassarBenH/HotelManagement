using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Models
{
    public class Room
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }

        public bool BelegtHeute { get; set; }
    }
}

