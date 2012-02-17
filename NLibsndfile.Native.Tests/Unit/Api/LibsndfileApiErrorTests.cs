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

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorString_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ErrorString(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void ErrorString_ShouldThrowExceptionOnNullStringReturned()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorString(It.IsAny<IntPtr>())).Returns((string)null);

            var api = new LibsndfileApi(mock.Object);
            api.ErrorString(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void ErrorString_ShouldThrowExceptionOnEmptyStringReturned()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorString(It.IsAny<IntPtr>())).Returns(string.Empty);

            var api = new LibsndfileApi(mock.Object);
            api.ErrorString(new IntPtr(1));
        }

        [Test]
        public void ErrorString_ShouldReturnValidErrorString()
        {
            const string ErrorString = "Libsndfile encountered no errors.";

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorString(It.IsAny<IntPtr>())).Returns(ErrorString);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.ErrorString(new IntPtr(1));

            Assert.AreEqual(ErrorString, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ErrorNumber_ShouldThrowExceptionOnNegativeErrorCode()
        {
            var api = new LibsndfileApi();
            api.ErrorNumber(-1);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void ErrorNumber_ShouldThrowExceptionOnNullStringReturned()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorNumber(It.IsAny<int>())).Returns((string)null);

            var api = new LibsndfileApi(mock.Object);
            api.ErrorNumber(1);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void ErrorNumber_ShouldThrowExceptionOnEmptyStringReturned()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorNumber(It.IsAny<int>())).Returns(string.Empty);

            var api = new LibsndfileApi(mock.Object);
            api.ErrorNumber(1);
        }

        [Test]
        public void ErrorNumber_ShouldReturnValidErrorString()
        {
            const string ErrorString = "Libsndfile encountered no errors.";

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ErrorNumber(It.IsAny<int>())).Returns(ErrorString);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.ErrorNumber(1);

            Assert.AreEqual(ErrorString, retval);
        }
    }
}