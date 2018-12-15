using TenderSearch.Tests.Integration.BaseClasses;
using Eml.EntityBaseClasses;
using Shouldly;
using System;
using Xunit;

namespace TenderSearch.Tests.Integration.Conventions
{
    public class WhenEntity : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(EntityClassData))]
        public void Entity_ShouldInheritEntityBase(Type type)
        {
            var sut = typeof(EntityBaseInt).IsAssignableFrom(type);

            sut.ShouldBeTrue();
        }
    }
}
