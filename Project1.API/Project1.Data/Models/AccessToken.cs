using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data.Models
{
    [Table("AccessToken", Schema="USER")]
    public class UserAccessToken
    {
        [Key, ForeignKey("ApplicationUser")]
        public Guid UserID { get; set; }

        [StringLength(1000)]
        public string AccessToken { get; set; }

        public DateTime ExpiresOn { get; set; }

        public DateTime UpdatedTS { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
