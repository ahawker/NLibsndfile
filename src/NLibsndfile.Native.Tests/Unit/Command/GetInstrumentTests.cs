using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetInstrumentTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInstrument_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetInstrument(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetInstrument_ShouldThrowExceptionOnNegativeResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileInstrumentInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetInstrument(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetInstrument_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileInstrumentInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetInstrument(new IntPtr(1));
        }

        [Test]
        public void GetInstrument_ShouldReturnNullOnZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileInstrumentInfo>()).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetInstrument(new IntPtr(1));

            Assert.IsFalse(retval.HasValue);
        }

        [Test]
        public void GetLoopInfo_ShouldPassOnOneResult()
        {
            var instrumentInfo = new LibsndfileInstrumentInfo();
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<LibsndfileInstrumentInfo>()).Returns(unmanagedMemoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleTo<LibsndfileInstrumentInfo>(unmanagedMemoryMock.Object)).Returns(instrumentInfo);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetInstrument(new IntPtr(1));

            Assert.IsTrue(retval.HasValue);
            Assert.AreEqual(instrumentInfo, retval.Value);
        }
    }
}