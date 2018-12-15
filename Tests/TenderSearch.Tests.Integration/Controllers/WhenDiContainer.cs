using TenderSearch.Tests.Integration.BaseClasses;
using Eml.Extensions;
using Eml.MefExtensions;
using Shouldly;
using System;
using System.ComponentModel.Composition;
using Xunit;

namespace TenderSearch.Tests.Integration.Controllers
{
    public class WhenDiContainer : IntegrationTestDiBase
    {
        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldBeExportable(Type type)
        {
            var sut = classFactory.Container.GetExportedValueByType(type);

            sut.ShouldNotBeNull();
        }

        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldHaveExportAttributes(Type type)
        {
            var sut = type.GetClassAttribute<ExportAttribute>();

            sut.ShouldNotBeNull();
        }
    }
}