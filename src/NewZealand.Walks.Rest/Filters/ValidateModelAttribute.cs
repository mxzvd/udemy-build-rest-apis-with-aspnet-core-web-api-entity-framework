using Microsoft.AspNetCore.Mvc.Filters;

namespace NewZealand.Walks.Rest;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ModelState.IsValid)
            context.Result = new BadRequestResult();
    }
}
