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

        /// <summary>
        /// Endpoint processes CSV file, parses and stores data to database.
        /// </summary>
        /// <param name="formFile">Form file, that should contain CSV file.</param>
        /// <returns>
        /// Returns <see cref="OkResult"/> if success.
        /// Returns <see cref="BadRequestResult"/> if file is corrupted or values, with provided keys, already exist.
        /// Returns <see cref="UnprocessableEntityResult"/> if user sent NOT XML file.
        /// </returns>
        [HttpPost]
        [Route("save-csv")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> SaveCsv(IFormFile formFile)
        {
            if (formFile == null || formFile.ContentType != "application/vnd.ms-excel")
                return UnprocessableEntity("Unknown format");

            return (await CommandAsync(SaveCsvCommand.CreateInstance(formFile.OpenReadStream())))
                .Match<IActionResult, Unit>(unit => Ok(), BadRequest);
        }

        /// <summary>
        /// Endpoint processes XML file, parses and stores data to database.
        /// </summary>
        /// <param name="formFile">Form file, that should contain XML file.</param>
        /// <returns>
        /// Returns <see cref="OkResult"/> if success.
        /// Returns <see cref="BadRequestResult"/> if file is corrupted or values, with provided keys, already exist.
        /// Returns <see cref="UnprocessableEntityResult"/> if user sent NOT XML file.
        /// </returns>
        [HttpPost]
        [Route("save-xml")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
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
