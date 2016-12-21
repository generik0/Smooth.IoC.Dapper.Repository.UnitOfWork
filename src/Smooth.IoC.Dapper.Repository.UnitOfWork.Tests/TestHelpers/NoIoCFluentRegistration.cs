using System;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    [NoIoCFluentRegistration]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class NoIoCFluentRegistration : Attribute
    {
    }
}
