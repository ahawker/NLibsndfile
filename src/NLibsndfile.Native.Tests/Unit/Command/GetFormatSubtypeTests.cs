using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetFormatSubtypeTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatSubtype_ShouldThrowExceptionOnSubmaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatSubtype(LibsndfileFormat.Submask);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatSubtype_ShouldThrowExceptionOnTypemaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatSubtype(LibsndfileFormat.Typemask);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatSubtype_ShouldThrowExceptionOnEndmaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatSubtype(LibsndfileFormat.Endmask);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetFormatSubtype_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileFormatInfo>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetFormatSubtype(LibsndfileFormat.Wav);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetFormatSubtype_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileFormatInfo>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetFormatSubtype(LibsndfileFormat.Wav);
        }

        [Test]
        public void GetFormatSubtype_ShouldPassOnZeroResult()
        {
            var formatInfo = new LibsndfileFormatInfo { Format = LibsndfileFormat.Wav, Name = "Wav", Extension = ".wav" };

            var allocatedFormatInfo = new LibsndfileFormatInfo { Format = LibsndfileFormat.Wav };

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(allocatedFormatInfo)).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleTo<LibsndfileFormatInfo>(It.IsAny<UnmanagedMemoryHandle>())).Returns(formatInfo);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetFormatSubtype(LibsndfileFormat.Wav);

            Assert.AreEqual(formatInfo, retval);
        }
    }
}