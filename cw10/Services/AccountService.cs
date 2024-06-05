using Microsoft.EntityFrameworkCore; 
using cw10.DTO;
using cw10.Data;

namespace cw10.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;

    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AccountDTO> GetAccountByIdAsync(int accountId)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.ShoppingCarts)
            .ThenInclude(sc => sc.Product)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);

        if (account == null)
        {
            return null;
        }

        return new AccountDTO
        {
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
            Phone = account.Phone,
            Role = account.Role.Name,
            Cart = account.ShoppingCarts.Select(sc => new ShoppingCartDTO
            {
                ProductId = sc.ProductId,
                ProductName = sc.Product.Name,
                Amount = sc.Amount
            }).ToList()
        };
    }
}
