namespace BookApp.Core.DTO;

public class UserDTO
{
    private int Id { get; set; }
    private string Emailaddress { get; set; }
    private string Password { get; set; }

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