using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor //Autofac
    {
        public int Priority { get; set; } //Öncelik

        public virtual void Intercept(IInvocation invocation) //Metodun çalışacağı yer
        {

        }
    }
}
