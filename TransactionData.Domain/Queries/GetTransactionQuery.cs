﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using LanguageExt;
using MediatR;
using TransactionData.Domain.Dtos;

namespace TransactionData.Domain.Commands
{
    public class GetTransactionQuery : IRequest<TryAsync<List<GetTransactionDto>>>
    {
        public GetTransactionQuery()
        {
            
        }

        public GetTransactionQuery(string currency = null, 
            DateTimeOffset? from = null,
            DateTimeOffset? to = null,
            List<GetTransactionStatusEnumQuery> status = null)
        {
            Currency = currency;
            From = from;
            To = to;
            Status = status;
        }

        public string Currency { get; set; }
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public List<GetTransactionStatusEnumQuery> Status { get; set; }
    }
}