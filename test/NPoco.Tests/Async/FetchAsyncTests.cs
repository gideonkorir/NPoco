using NPoco.Tests.Common;
using NPoco.Tests.NewMapper.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPoco.Tests.Async
{
    [TestFixture]
    public class FetchAsyncTests : BaseDBFuentTest
    {
        [Test]
        public async Task FetchMultipleAsyncWithSqlAndArgs()
        {
            var model = await Database.FetchMultipleAsync<One, Many, User>(
                @"select * from ones;select * from manys;select *from users")
                .ConfigureAwait(false);
            Assert.IsNotEmpty(model.Item1);
            Assert.IsNotEmpty(model.Item2);
            Assert.IsNotEmpty(model.Item3);
        }
    }
}
