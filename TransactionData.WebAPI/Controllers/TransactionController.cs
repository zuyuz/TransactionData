using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
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
        [Route("save-csv")]
        public async Task<IActionResult> SaveCsv(IFormFile formFile)
        {
            var stream = formFile.OpenReadStream();
            var a = (await CommandAsync(SaveCsvCommand.CreateInstance(stream)));
            return a.Match<Unit, IActionResult, string>(unit => Ok(), BadRequest);
        }

        [HttpPost]
        [Route("save-xml")]
        public async Task<IActionResult> SaveXml(IFormFile formFile)
        {
            var stream = formFile.OpenReadStream();
            var a = (await CommandAsync(SaveXmlCommand.CreateInstance(stream)));
            return a.Match<Unit, IActionResult, string>(unit => Ok(), BadRequest);
        }
    }
}
