namespace cw10.DTO;

public class AccountDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public List<ShoppingCartDTO> Cart { get; set; }
}