using Microsoft.AspNetCore.Mvc;
using cw10.Services;

namespace cw10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{accountId:int}")]
    public async Task<IActionResult> GetAccountById(int accountId)
    {
        var account = await _accountService.GetAccountByIdAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }
}
