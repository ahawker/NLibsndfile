using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetFormatMajorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatMajor_ShouldThrowExceptionOnSubmaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatMajor(LibsndfileFormat.Submask);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatMajor_ShouldThrowExceptionOnTypemaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatMajor(LibsndfileFormat.Typemask);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormatMajor_ShouldThrowExceptionOnEndmaskFormat()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetFormatMajor(LibsndfileFormat.Endmask);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetFormatMajor_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileFormatInfo>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetFormatMajor(LibsndfileFormat.Wav);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetFormatMajor_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<LibsndfileFormatInfo>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetFormatMajor(LibsndfileFormat.Wav);
        }

        [Test]
        public void GetFormatMajor_ShouldPassOnZeroResult()
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
            var retval = api.GetFormatMajor(LibsndfileFormat.Wav);

            Assert.AreEqual(formatInfo, retval);
        }
    }
}