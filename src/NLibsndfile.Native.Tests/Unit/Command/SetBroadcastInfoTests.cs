using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetBroadcastInfoTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetBroadcastInfo_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetBroadcastInfo(IntPtr.Zero, It.IsAny<LibsndfileBroadcastInfo>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetBroadcastInfo_ShouldThrowExceptionOnNegativeResult()
        {
            var broadcastInfo = new LibsndfileBroadcastInfo();

            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(broadcastInfo)).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetBroadcastInfo(new IntPtr(1), It.IsAny<LibsndfileBroadcastInfo>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetBroadcastInfo_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var broadcastInfo = new LibsndfileBroadcastInfo();

            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(broadcastInfo)).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetBroadcastInfo(new IntPtr(1), It.IsAny<LibsndfileBroadcastInfo>());
        }

        [Test]
        public void SetBroadcastInfo_ShouldReturnFalseOnZeroResult()
        {
            var broadcastInfo = new LibsndfileBroadcastInfo();

            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileBroadcastInfo>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetBroadcastInfo(new IntPtr(1), It.IsAny<LibsndfileBroadcastInfo>());

            Assert.IsFalse(retval);
        }

        [Test]
        public void SetBroadcastInfo_ShouldReturnTrueOnOneResult()
        {
            var broadcastInfo = new LibsndfileBroadcastInfo();

            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(broadcastInfo)).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetBroadcastInfo(new IntPtr(1), It.IsAny<LibsndfileBroadcastInfo>());

            Assert.IsTrue(retval);
        }
    }
}