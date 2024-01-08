using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace RegexCustomAPIs.Plugins
{
    public class RegexMatch : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.MessageName.Equals("rca_RegexMatch") && context.Stage.Equals(30))
            {
                string input = context.InputParameters["Input"] as string;
                string pattern = context.InputParameters["Pattern"] as string;

                bool success = false;
                int index = 0;
                string value = "";
                bool error = false;
                string errorMessage = "";
                try
                {
                    Match match = Regex.Match(input, pattern);
                    success = match.Success;
                    index = match.Index;
                    value = match.Value;
                }
                catch (Exception ex)
                {
                    error = true;
                    errorMessage = ex.Message;
                }

                context.OutputParameters["Success"] = success;
                context.OutputParameters["Index"] = index;
                context.OutputParameters["Value"] = value;
                context.OutputParameters["Error"] = error;
                context.OutputParameters["ErrorMessage"] = errorMessage;
            }
        }
    }
}
