using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetInstrumentTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetInstrument_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetInstrument(IntPtr.Zero, It.IsAny<LibsndfileInstrumentInfo>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetInstrument_ShouldThrowExceptionOnNegativeResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileInstrumentInfo>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetInstrument(IntPtr.Zero, It.IsAny<LibsndfileInstrumentInfo>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetInstrument_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileInstrumentInfo>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetInstrument(IntPtr.Zero, It.IsAny<LibsndfileInstrumentInfo>());
        }

        [Test]
        public void SetInstrument_ShouldReturnFalseOnZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileInstrumentInfo>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetInstrument(IntPtr.Zero, It.IsAny<LibsndfileInstrumentInfo>());

            Assert.IsFalse(retval);
        }

        [Test]
        public void SetInstrument_ShouldReturnTrueOnOneResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileInstrumentInfo>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetInstrument(IntPtr.Zero, It.IsAny<LibsndfileInstrumentInfo>());

            Assert.IsTrue(retval);
        }
    }
}