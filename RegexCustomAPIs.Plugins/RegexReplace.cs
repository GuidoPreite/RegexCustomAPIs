using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace RegexCustomAPIs.Plugins
{
    public class RegexReplace : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.MessageName.Equals("rca_RegexReplace") && context.Stage.Equals(30))
            {
                string input = context.InputParameters["Input"] as string;
                string pattern = context.InputParameters["Pattern"] as string;
                string replacement = context.InputParameters["Replacement"] as string;

                string result = "";
                bool error = false;
                string errorMessage = "";
                try
                {
                    result = Regex.Replace(input, pattern, replacement);
                }
                catch (Exception ex)
                {
                    error = true;
                    errorMessage = ex.Message;
                }

                context.OutputParameters["Result"] = result;
                context.OutputParameters["Error"] = error;
                context.OutputParameters["ErrorMessage"] = errorMessage;
            }
        }
    }
}
