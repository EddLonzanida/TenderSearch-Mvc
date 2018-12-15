using Eml.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace TenderSearch.Tests.Integration.Controllers
{
    public class ControllerClassData : TheoryData<Type>
    {
        public ControllerClassData()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var moduleDirectory = new DirectoryInfo(path);
            var concreteClasses = moduleDirectory.GetAssembliesFromDirectory("TenderSearch.Web.dll")
                .First()
                .GetClasses(type => !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));

            foreach (var type in concreteClasses)
            {
                Add(type);
            }
        }
    }
    //public class ControllerClassData : IEnumerable<object[]>
    //{
    //    public IEnumerator<object[]> GetEnumerator()
    //    {
    //        var path = AppDomain.CurrentDomain.BaseDirectory;
    //        var moduleDirectory = new DirectoryInfo(path);
    //        var concreteClasses = moduleDirectory.GetAssembliesFromDirectory("TenderSearch.Web.dll")
    //            .ToList()
    //            .First()
    //            .GetClasses(type => !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));


    //        return concreteClasses.;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}
