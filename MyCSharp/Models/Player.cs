using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Models
{
    [Table("Player")]
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Comments { get; set; }
        public bool? IsSingle { get; set; }
        public RoleType? RoleType { get; set; }
    }

    public enum RoleType
    {
        Type1 = 1,
        Type2 = 2,
        Type3 = 3
    }
}
