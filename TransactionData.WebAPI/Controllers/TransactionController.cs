using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Dtos;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactions([FromBody] GetTransactionQuery query)
        {
            var a = await QueryAsync(query);
            return a.Match<List<GetTransactionDto>, IActionResult, string>(dto => Ok(dto), dto => BadRequest(dto));
        }

        [HttpPost]
        [Route("save-csv")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> SaveCsv(IFormFile formFile)
        {
            if (formFile == null || formFile.ContentType != "application/vnd.ms-excel")
                return UnprocessableEntity("Unknown format");

            return (await CommandAsync(SaveCsvCommand.CreateInstance(formFile.OpenReadStream())))
                .Match<IActionResult, Unit>(unit => Ok(), BadRequest);
        }

        [HttpPost]
        [Route("save-xml")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> SaveXml(IFormFile formFile)
        {
            if (formFile == null || formFile.ContentType != "text/xml")
                return UnprocessableEntity("Unknown format");

            return (await CommandAsync(SaveXmlCommand.CreateInstance(formFile.OpenReadStream())))
                .Match<IActionResult, Unit>(unit => Ok(), BadRequest);
        }
    }
}
