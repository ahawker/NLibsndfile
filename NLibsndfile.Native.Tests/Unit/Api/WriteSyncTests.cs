using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class WriteSyncTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteSync_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteSync(IntPtr.Zero);
        }
    }
}
