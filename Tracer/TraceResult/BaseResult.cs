using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public abstract class BaseResult
    {

        protected List<MethodResult> _methods;

        public void AddMethod(StackTrace stackTrace, MethodResult method)
        {
            foreach (MethodResult methodResult in _methods)
            {
                for (int i = 0; i < stackTrace.FrameCount; i++)
                {
                    StackFrame frame = stackTrace.GetFrame(i);
                    MethodBase methodInfo = frame.GetMethod();
                    string methodName = methodInfo.Name;
                    string className = methodInfo.DeclaringType.Name;
                    if (methodResult.MethodName.Equals(methodName) && methodResult.ClassName.Equals(className))
                    {
                        methodResult.AddMethod(stackTrace, method);
                        return;
                    }
                }
            }
            _methods.Add(method);
        }
    }
}
