using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Api
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class CloseTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Close_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.Close(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void Close_ShouldThrowExceptionOnErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Close(It.IsAny<IntPtr>())).Returns(LibsndfileError.MalformedFile);

            var api = new LibsndfileApi(mock.Object);
            api.Close(new IntPtr(1));
        }

        [Test]
        public void Close_ShouldPassOnNoErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Close(It.IsAny<IntPtr>())).Returns(LibsndfileError.NoError);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Close(new IntPtr(1));

            Assert.AreEqual(LibsndfileError.NoError, retval);
        }
    }
}