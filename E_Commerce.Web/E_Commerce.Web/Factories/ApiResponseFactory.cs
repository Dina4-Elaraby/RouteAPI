using Microsoft.AspNetCore.Mvc;
using Shared_DTOs_.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new ValidationErrors()
            {
                Field = m.Key,
                Errors = m.Value.Errors.Select(e => e.ErrorMessage)
            });
            var ErrorResponse = new ValidationErrorToReturn()
            {
                validationErrors = Errors,
            };
            return new BadRequestObjectResult(ErrorResponse);
        }
    }
}
