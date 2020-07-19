﻿using System;
using System.IO;
using System.Xml.Serialization;
using CSharpFunctionalExtensions;
using CsvHelper;
using MediatR;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Events;
using TransactionData.Domain.Models;

namespace TransactionData.Service.ExtensionMethods
{
    public static class SaveXmlCommandExtensionMethods
    {
        public static Result<XmlTransactionModel> GetXmlTransactionModel(this SaveXmlCommand command)
        {
            try
            {
                using TextReader reader = new StreamReader(command.Stream);
                XmlSerializer serializer = new XmlSerializer(typeof(XmlTransactionModel));
                return (XmlTransactionModel) serializer.Deserialize(reader);
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Failure<XmlTransactionModel>(e.Message);
            }
            catch (FieldValidationException e)
            {
                return Result.Failure<XmlTransactionModel>(e.Message);
            }
            catch (CsvHelperException e)
            {
                return Result.Failure<XmlTransactionModel>(e.Message);
            }
            catch (Exception e)
            {
                return Result.Failure<XmlTransactionModel>(e.Message);
            }
        }
    }
}