using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _22SevenFincancialApp.Models.Context;
using _22SevenFincancialApp.Models.apiContent;
using _22SevenFincancialApp.Models.EntitySets;
using Newtonsoft.Json;

namespace _22SevenFincancialApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FinancialController : ControllerBase
  {
    //collection of contexts for talking to the inmemory database
    private readonly FinancialContext _context;

    public FinancialController(FinancialContext context)
    {
      _context = context;
    }

    private async Task<ActionResult<CustomerData>> GetCustomer(int id)
    {
      if (_context.Customer == null)
      {
        return NotFound();
      }
      var getAccount = await _context.Customer.FindAsync(id);

      if (getAccount == null)
      {
        return NotFound();
      }

      return getAccount;
    }
    private async Task<ActionResult<CustomerAccountData>> GetAccount(int id)
    {
      if (_context.CustomerAccounts == null)
      {
        return NotFound();
      }
      var getAccount = await _context.CustomerAccounts.FindAsync(id);

      if (getAccount == null)
      {
        return NotFound();
      }

      return getAccount;
    }
    //Just added this because it makes sense to have it.
    [Route("CreateUsers")]
    [HttpPost]
    public async Task<ActionResult<CustomerData>> PostCreateCustomer(CustomerData customer) 
    {
      try
      {
        if (CustomerExists(customer.Id))
          return Problem("Account already exists");//this is cool because you can use it to easily return status codes and info about the error

        _context.Customer.Add(new Models.EntitySets.CustomerData
        {
          Id = customer.Id,
          Name = customer.Name,
          Address = customer.Address,
          Age = customer.Age,
          Email = customer.Email,
          CreatedDate = DateTime.Now,
        });//populate the customer DB with basic information
        await _context.SaveChangesAsync();
        return Content(JsonConvert.SerializeObject( new { responseCode = 00, responseMessage = "Customer Created Successfully"}));
      }
      catch (Exception ex) 
      {
        return BadRequest(ex.Message);
      }

    }

    // POST: api/CreateAccounts
    [Route("CreateAccounts")]
    [HttpPost]
    public async Task<ActionResult> PostCreateAccount(CreateAccount createAccount)
    {
      try
      {

        for (int i = 0; i < createAccount?.AccountInfo?.Length; i++)//could use foreach too but this should be a little more efficient.
        {
          Accounts? account = createAccount!.AccountInfo[i];
          _context.CustomerAccounts.Add(new Models.EntitySets.CustomerAccountData//setting values manually to avoid risky errors
          {
            Id = new Random().Next(100),//just mocking some id generation here. normally youd want to use guids
            AccountName = account.accountName,
            AccountType = account.AccountType,
            Balance = account.initialDeposit,
            CreatedDate = DateTime.Now,
            CustomerId = createAccount.customerId//link the account to the customer. 1:m relationship
          });
        }
        await _context.SaveChangesAsync();
        return Content("Account created successfully", "application/json");
      }
      catch (Exception ex) 
      {
        return Problem("Response error : "+ ex.Message);//Good idea to log stacktrace. will see if there is time. NLog is pretty solid choice.
      }

    }
    //check if the customer exists...really complex...
    private bool CustomerExists(int id)
    {
      return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // POST: api/TransferAmounts 
    [Route("TransferAmount")]
    [HttpPost]
    public async Task<ActionResult<TransferAmount>> PostTransferAmount(TransferAmount transferAmount) 
    {
      try
      {
        if (!CustomerExists(transferAmount.CustomerId))
          return Problem("No account associated with with customer ID : " + transferAmount.CustomerId);

        var accountFrom = _context.CustomerAccounts?.Where(x => x.Id == transferAmount.AccountFrom.Id).First();
        if (accountFrom == null)
          return Problem("No account assiciated with Account ID : " + transferAmount.AccountFrom.Id);

        var accountTo = _context.CustomerAccounts?.Where(x => x.Id == transferAmount.AccountTo.Id).First();
        if (accountTo == null)
          return Problem("No account assiciated with Account ID : " + transferAmount.AccountTo.Id);

        accountFrom.Balance -= transferAmount.AmountToTransfer;
        accountTo.Balance += transferAmount.AmountToTransfer;

        var customer = await GetCustomer(transferAmount.CustomerId);
        var guid = Guid.NewGuid().ToString();
        _context.History.Add(new HistoryData
        {
          Id = guid,
          customerName = customer.Value!.Name,
          AccountName = accountFrom.AccountName,
          TransactionAmount = transferAmount.AmountToTransfer,
          DestinationAccount = accountTo.AccountName 
        });

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(TransferAmount), new { id = guid }, new {responseMessage = "Transaction completed succesfully" });
      }
      catch (Exception ex) 
      {
        return Problem(ex.Message);
      }
    }

    // GET: api/RetrieveAmount
    [Route("/RetrieveAmount")]
    [HttpGet]
    public ActionResult<TransferAmount> GetRetrieveAmount(RetrieveBalance retrieveBalance)//brain is going dead at this point.
    {
      if (_context.CustomerAccounts! == null)
      {
        return Problem("No account found for Customer " + retrieveBalance.Name);
      }
      var accountList = _context.CustomerAccounts.Where(x => x.CustomerId == retrieveBalance.Id).ToList();//tolist can be a bit memory extensive. so depending on the situation one might considder writing more code to load it into a list.

      return Content(JsonConvert.SerializeObject(accountList));//i love newtonsoft. 
    }

    // GET: api/RetrieveHistory
    [Route("/RetrieveHistory")]
    [HttpGet]
    public ActionResult<TransferAmount> GetRetrieveHistory(RetrieveTransferHistory retrieveTransferHistory)
    {
      if (_context!.History == null)
      {
        return Problem("No history found for transaction : " + retrieveTransferHistory.transactionId);
      }
      var transactionHistory = _context.History.Where(x => x.transactionId == retrieveTransferHistory.transactionId);

      return Content(JsonConvert.SerializeObject(transactionHistory));
    }
  }
}
