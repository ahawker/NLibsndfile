using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetLibVersionTests
    {
        [Test]
        public void GetLibVersion_ShouldReturnValidString()
        {
            const string Version = "1.0";

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.GetLibVersion()).Returns(Version);

            var api = new LibsndfileCommandApi(mock.Object);
            var retval = api.GetLibVersion();

            Assert.AreEqual(Version, retval);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLibVersion_ShouldThrowExceptionOnNullStringReturned()
        {
            const string Version = null;

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.GetLibVersion()).Returns(Version);

            var api = new LibsndfileCommandApi(mock.Object);
            api.GetLibVersion();
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetLibVersion_ShouldThrowExceptionOnInvalidResult()
        {
            var marshallerMock = new Mock<ILibsndfileCommandMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<int>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetLibVersion();
        }

        [Test]
        public void GetLibVersion_ShouldPassOnValidResult()
        {
            const string Version = "1.0";

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileCommandMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<int>())).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleToString(It.IsAny<UnmanagedMemoryHandle>())).Returns(Version);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetLibVersion();

            Assert.AreEqual(Version, retval);
        }
    }
}