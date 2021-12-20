using Microsoft.AspNetCore.Authorization;

namespace Roster.Web.Security
{
    public static class PolicyFactory
    {
        public static void BuildPolicies(AuthorizationOptions options)
        {
            options.AddPolicyByName(Policy.ViewApplications);
            options.AddPolicyByName(Policy.AcceptMembers);
            options.AddPolicyByName(Policy.ViewMembers);
        }

        private static void AddPolicyByName(this AuthorizationOptions authorizationOptions, string policyName)
        {
            authorizationOptions.AddPolicy(policyName, p => p.RequireClaim(policyName));
        }
    }
}