namespace BookApp.Core.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Emailaddress { get; set; }
        public string Password { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(int id, string emailaddress, string password)
        {
            Id = id;
            Emailaddress = emailaddress;
            Password = password;
        }
    }
}