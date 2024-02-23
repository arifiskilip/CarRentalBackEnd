using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validaton;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Bu bir doğrulama sınıf değildir!");
            }
            _validatorType = validatorType;
        }

        protected override void OnAfter(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //ProductValidator
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  //Entity
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
