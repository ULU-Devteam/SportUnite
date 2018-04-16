using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportUnite.API.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
    public class BankAccountController : Controller
    {
	    private readonly IBankAccountAccess _bankAccountAccess;
		

	    public BankAccountController(IBankAccountAccess bankAccountAccess)
	    {
		    _bankAccountAccess = bankAccountAccess;
	    }

		/// <summary>
		/// Gets all Bank Accounts.
		/// </summary>
		/// <returns>Returns all Bank Accounts.</returns>
		/// <response code="200">Returns all Bank Accounts.</response>
		[HttpGet]
        public IActionResult Get()
		{
			var response = new List<HALResponse>();
			var accounts = _bankAccountAccess.GetBankAccounts();

			foreach (var a in accounts)
			{
				response.Add(new HALResponse(a).AddSelfLink(Request).AddLinks(
					new Link("update", "/api/BankAccount", null, "PUT")));
			}

			return Ok(response);
		}


		/// <summary>
		/// Gets a single Bank Accounts.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>A single Bank Accounts.</returns>
		/// <response code="200">Returns a Bank Accounts.</response>
		/// <response code="400">Returns if the ID is 0 or a Bank Accounts with given ID does not exist.</response>  
		[HttpGet("{id}")]
        public IActionResult Get(int id)
        {
	        return Ok(new HALResponse(_bankAccountAccess.GetAccount(id)).AddSelfLink(Request).AddLinks(
				new Link("update", "/api/BankAccount", null, "PUT")));
		}

		/// <summary>
		/// Creates a Bank Account.
		/// </summary>
		/// <remarks>	
		/// Sample request:
		///
		///     POST /BankAccount
		///     {
		///		"bankAccountId": 3,
		///	    "name": "Rick Update",
		///	    "accountNumber": 102
		///    }
		///
		/// </remarks>
		/// <param name="BankAccount"></param>
		/// <returns>A newly-created Bank Account</returns>
		/// <response code="201">Returns the newly-created item</response>
		/// <response code="400">If the item is null</response>  
	[HttpPost]
        public IActionResult Post([FromBody]BankAccount bankAccount)
        {
	        if (bankAccount == null)
	        {
		        return BadRequest();
	        }

			_bankAccountAccess.AddBankAccount(bankAccount);
			return Ok(new HALResponse(bankAccount).AddSelfLink(Request).AddLinks(
				new Link("update", "/api/BankAccount/" + bankAccount.BankAccountId, null, "GET")));
		}

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]BankAccount bankAccount)
        {
			_bankAccountAccess.UpdateBankAccount(bankAccount);
	        return Ok(bankAccount);
        }

        
    }
}
