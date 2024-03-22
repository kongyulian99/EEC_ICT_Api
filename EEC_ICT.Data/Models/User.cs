using System;

namespace EEC_ICT.Data.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        //public Function Functions { get; set; }

        public int TotalRows { get; set; }
    }
}