using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetLogInfoTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLogInfo_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetLogInfo(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLogInfo_ShouldThrowExceptionOnNullStringReturned()
        {
            const string Log = null;

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.GetLogInfo(It.IsAny<IntPtr>())).Returns(Log);

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetLogInfo(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLogInfo_ShouldThrowExceptionOnEmptyStringReturned()
        {
            const string Log = "";

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.GetLogInfo(It.IsAny<IntPtr>())).Returns(Log);

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetLogInfo(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLogInfo_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<int>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetLogInfo(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLogInfo_ShouldThrowExceptionOnZeroResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<int>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetLogInfo(new IntPtr(1));
        }

        [Test]
        public void GetLogInfo_ShouldPassOnValidResult()
        {
            const string Log = "Hello World. I am log.";

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<int>())).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleToString(It.IsAny<UnmanagedMemoryHandle>())).Returns(Log);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetLibVersion();

            Assert.AreEqual(Log, retval);
        }
    }
}