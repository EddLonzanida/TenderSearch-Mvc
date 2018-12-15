using Eml.Extensions;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace TenderSearch.Tests.Integration.Conventions
{
    public class EntityClassData : TheoryData<Type>
    {
        public EntityClassData()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var moduleDirectory = new DirectoryInfo(path);
            var concreteClasses = moduleDirectory.GetAssembliesFromDirectory("TenderSearch.Business.Common.dll")
                .First()
                .GetClasses(type => !type.IsAbstract && !type.IsEnum && type.Namespace == "TenderSearch.Business.Common.Entities")
                .ToList();

            foreach (var type in concreteClasses)
            {
                Add(type);
            }
        }
    }
}



//var concreteClasses = moduleDirectory.GetAssembliesFromDirectory("TenderSearch.Business.Common.dll")
//.ToList()
//.First()
//.GetClasses(type => !type.IsAbstract
//&& type.Namespace == "TenderSearch.Business.Common.Entities")
////.Select(type =>
////{
////    Type[] typeArgs = { type };

////    return repository.MakeGenericType(type);
////})
//.ToList();
