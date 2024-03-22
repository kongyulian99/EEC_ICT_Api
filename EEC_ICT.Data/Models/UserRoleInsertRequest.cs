namespace EEC_ICT.Data.Models
{
    public class UserRoleInsertRequest
    {
        public string UserId { get; set; }
        public int[] RoleIds { get; set; }
    }
}