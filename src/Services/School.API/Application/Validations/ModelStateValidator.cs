using API.Controllers.Base;
using API.Infrastructure;
using Domain.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Net;

namespace API.Application.Validations
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
            string errorSerialized = entry.Errors.First().ErrorMessage;

            Error error = Error.Deserialize(errorSerialized);
            Envelope envelope = Envelope.Error(error, fieldName);
            var envelopeResult = new EnvelopeResult(envelope, HttpStatusCode.BadRequest);

            return envelopeResult;
        }
    }
}
