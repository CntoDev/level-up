using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Roster.Web.Utilities
{
    public static class ResultExtensions
    {
        public static void AddResultErrors(this ModelStateDictionary modelState, Result result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Message);
            }
        }
    }
}