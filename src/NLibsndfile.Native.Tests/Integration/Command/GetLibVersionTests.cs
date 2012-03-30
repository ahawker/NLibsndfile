using System;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Integration.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.IntegrationTests.CommandApi")]
    public class GetLibVersionTests
    {
        [Test]
        public void GetLibVersion_ReturnsCorrectVersion()
        {
            var api = new LibsndfileApi();

            var version = api.Commands.GetLibVersion();
            Assert.That(!string.IsNullOrEmpty(version));
        }
    }
}