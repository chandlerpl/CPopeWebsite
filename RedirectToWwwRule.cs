using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Rewrite;
    using System;

    public class RedirectToWwwRule : IRule
    {
        public virtual void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;

            if (req.Host.Value.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            var wwwHost = new HostString($"www.{req.Host.Value}");
            var newUrl = UriHelper.BuildAbsolute(req.Scheme, wwwHost, req.PathBase, req.Path, req.QueryString);
            var response = context.HttpContext.Response;
            response.StatusCode = 301;
            response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] = newUrl;
            context.Result = RuleResult.EndResponse;
        }
    }
}
