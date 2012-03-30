using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetBroadcastInfoTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBroadcastInfo_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetBroadcastInfo(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetBroadcastInfo_ShouldThrowExceptionOnNegativeResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileBroadcastInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetBroadcastInfo(new IntPtr(1));
        }

        [Test]
        public void GetBroadcastInfo_ShouldReturnNullOnZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileBroadcastInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetBroadcastInfo(new IntPtr(1));

            Assert.IsFalse(retval.HasValue);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetBroadcastInfo_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileBroadcastInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetBroadcastInfo(new IntPtr(1));
        }

        [Test]
        public void GetBroadcastInfo_ShouldPassOnOneResult()
        {
            var broadcastInfo = new LibsndfileBroadcastInfo();

            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileBroadcastInfo>()).Returns(unmanagedMemoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleTo<LibsndfileBroadcastInfo>(unmanagedMemoryMock.Object)).Returns(broadcastInfo);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetBroadcastInfo(new IntPtr(1));

            Assert.AreEqual(broadcastInfo, retval);
        }

    }
}