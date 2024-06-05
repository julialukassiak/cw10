using cw10.DTO;
namespace cw10.Services;

public interface IAccountService
{
    Task<AccountDTO> GetAccountByIdAsync(int accountId);
   
}