using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class Profile : BaseEntity
    {
        public string ProfilePic { get; set; }
        public string Bio { get; set; }
    }
}
