using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionData.Domain.Commands;

namespace TransactionData.WebAPI.Controllers
{
    [ApiController]
    [Route("transactions")]
    [Produces("application/json")]
    public class TransactionController : ApiControllerBase
    {
        public TransactionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> SaveCsv(IFormFile formFile)
        {
            if (formFile != null)
            {
                var stream = formFile.OpenReadStream();

                return Ok(await CommandAsync(SaveCsvCommand.CreateInstance(stream)));
            }

            return Ok();
        }
    }
}
