using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetLoopInfoTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLoopInfo_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetLoopInfo(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLoopInfo_ShouldThrowExceptionOnNegativeResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileLoopInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetLoopInfo(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLoopInfo_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileLoopInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetLoopInfo(new IntPtr(1));
        }

        [Test]
        public void GetLoopInfo_ShouldReturnNullOnZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileLoopInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetLoopInfo(new IntPtr(1));

            Assert.IsFalse(retval.HasValue);
        }

        [Test]
        public void GetLoopInfo_ShouldPassOnOneResult()
        {
            var loopInfo = new LibsndfileLoopInfo();
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileLoopInfo>()).Returns(unmanagedMemoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleTo<LibsndfileLoopInfo>(unmanagedMemoryMock.Object)).Returns(loopInfo);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetLoopInfo(new IntPtr(1));

            Assert.IsTrue(retval.HasValue);
            Assert.AreEqual(loopInfo, retval.Value);
        }
    }
}