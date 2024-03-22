using System.Collections.Generic;

namespace EEC_ICT.Data.Models
{
    public class PermissionsInsertRequest
    {
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}