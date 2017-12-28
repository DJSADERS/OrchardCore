using System;
using System.Collections.Generic;
using OrchardCore.Scripting;
using OrchardCore.Workflows.Models;

namespace OrchardCore.Workflows.Scripting
{
    public class WorkflowMethodProvider : IGlobalMethodProvider
    {
        private readonly GlobalMethod _workflowMethod;
        private readonly GlobalMethod _inputMethod;
        private readonly GlobalMethod _outputMethod;
        private readonly GlobalMethod _variableMethod;
        private readonly GlobalMethod _resultMethod;

        public WorkflowMethodProvider(WorkflowContext workflowContext)
        {
            _workflowMethod = new GlobalMethod
            {
                Name = "workflow",
                Method = serviceProvider => (Func<object>)(() => workflowContext)
            };

            _inputMethod = new GlobalMethod
            {
                Name = "input",
                Method = serviceProvider => (Func<string, object>)(name => workflowContext.Input[name])
            };

            _outputMethod = new GlobalMethod
            {
                Name = "output",
                Method = serviceProvider => (Action<string, object>)((name, value) => workflowContext.Output[name] = value)
            };

            _variableMethod = new GlobalMethod
            {
                Name = "variable",
                Method = serviceProvider => (Func<string, object>)((name) => workflowContext.Variables[name])
            };

            _resultMethod = new GlobalMethod
            {
                Name = "result",
                Method = serviceProvider => (Func<object>)(() => workflowContext.Stack.Pop())
            };
        }

        public IEnumerable<GlobalMethod> GetMethods()
        {
            return new[] { _workflowMethod, _inputMethod, _outputMethod, _variableMethod, _resultMethod };
        }
    }
}
