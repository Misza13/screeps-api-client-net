namespace ScreepsApiClient
{
    using System.Collections.Generic;
    using System.Linq;
    using RestSharp;

    public static class ParameterExtensions
    {
        public static string Get(this IList<Parameter> parameters, string parameterName)
        {
            var parameter = parameters.First(param => param.Name == parameterName);
            return (string) parameter.Value;
        }
    }
}