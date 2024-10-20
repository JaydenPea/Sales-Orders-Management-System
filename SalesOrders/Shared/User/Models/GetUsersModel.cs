using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.User.Models
{
    public class GetUsersModel
    {
        public string email { get; set; }
        public string role { get; set; }
        public DateTime dateCreated { get; set; }

        [NotMapped]
        public bool isEditing { get; set; } = false;
        [NotMapped]
        public bool isNew { get; set; } = false;
    }
}
