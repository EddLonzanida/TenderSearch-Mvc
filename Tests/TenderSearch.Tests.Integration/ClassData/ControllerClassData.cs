using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eml.Extensions;
using TenderSearch.Web;
using Xunit;

namespace TenderSearch.Tests.Integration.ClassData
{
    public class ControllerClassData : TheoryData<Type>
    {
        private static List<Type> _concreteClasses;

        public ControllerClassData()
        {
            if (_concreteClasses == null)
            {
                _concreteClasses = typeof(MvcApplication).Assembly
                    .GetClasses(type => !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));
            }

            foreach (var type in _concreteClasses)
            {
                Add(type);
            }
        }
    }
}
