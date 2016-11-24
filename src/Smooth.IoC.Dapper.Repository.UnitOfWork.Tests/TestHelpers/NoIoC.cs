using System;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    [NoIoC]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class NoIoC : Attribute
    {
    }
}
