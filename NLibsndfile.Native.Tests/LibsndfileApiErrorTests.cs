using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Error")]
    public class LibsndfileApiErrorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Error_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.Error(IntPtr.Zero);
        }

        [Test]
        public void Error_ShouldReturnSameErrorCode()
        {
            const LibsndfileError ErrorCode = LibsndfileError.System;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Error(It.IsAny<IntPtr>())).Returns(ErrorCode);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Error(new IntPtr(1));

            Assert.AreEqual(ErrorCode, retval);
        }

        [Test]
        public void Error_ShouldPassOnNoErrorCode()
        {
            const LibsndfileError ErrorCode = LibsndfileError.NoError;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Error(It.IsAny<IntPtr>())).Returns(ErrorCode);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Error(new IntPtr(1));

            Assert.AreEqual(ErrorCode, retval);
        }
    }
}