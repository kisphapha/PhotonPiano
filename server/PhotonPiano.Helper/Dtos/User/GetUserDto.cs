using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonPiano.Helper.Dtos.User
{
    public class GetUserDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

    }
}
