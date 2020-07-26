using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Dtos;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

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
        public Task<IActionResult> GetTransactions([FromBody] GetTransactionQuery query)
        {
            return TryOptionAsync(query
                    .Apply(file => QueryAsync(query)))
                    .MapT(Ok)
                    .Match(Some: (some) =>
                    {
                        return some.Match(unit1 => unit1, error => (IActionResult)BadRequest(error));
                    }, None: () => ((IActionResult)BadRequest("Unknown format")).AsTask(), Fail: (error) => ((IActionResult)BadRequest(error.Message)).AsTask())
                    .Bind(task => task);
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
        public Task<IActionResult> SaveCsv(IFormFile formFile)
        {
            return TryOptionAsync(formFile
                .ToSome()
                .Filter(file => file.ContentType != "application/vnd.ms-excel")
                .Reduce((file, file1) => file)
                .Apply(file => CommandAsync(SaveCsvCommand.CreateInstance(file.OpenReadStream()))
                .MapT(unit1 => Ok())))
                .Match(Some: (some) =>
                {
                    return some.Match(unit1 => unit1, error => (IActionResult) BadRequest(error));
                }, None: () => ((IActionResult) UnprocessableEntity("Unknown format")).AsTask(), Fail: (error) => ((IActionResult) BadRequest(error.Message)).AsTask())
                .Bind(task => task);
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
        public Task<IActionResult> SaveXml(IFormFile formFile)
        {
            return TryOptionAsync(formFile
                    .ToSome()
                    .Filter(file => file.ContentType != "text/xml")
                    .Reduce((file, file1) => file)
                    .Apply(file => CommandAsync(SaveXmlCommand.CreateInstance(file.OpenReadStream()))))
                .MapT(unit1 => Ok())
                .Match(Some: (some) =>
                {
                    return some.Match(unit1 => unit1, error => (IActionResult)BadRequest(error));
                }, None: () => ((IActionResult)UnprocessableEntity("Unknown format")).AsTask(), Fail: (error) => ((IActionResult)BadRequest(error.Message)).AsTask())
                .Bind(task => task);
        }
    }
}
