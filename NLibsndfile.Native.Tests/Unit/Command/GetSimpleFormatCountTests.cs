using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class GetSimpleFormatCountTests
    {
        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetSimpleFormatCount_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<int>()).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetSimpleFormatCount();
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void GetSimpleFormatCount_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<int>()).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.GetSimpleFormatCount();
        }

        [Test]
        public void GetSimpleFormatCount_ShouldPassOnZeroResult()
        {
            const int Count = 10;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<int>()).Returns(It.IsAny<IntPtr>());
            marshallerMock.Setup(x => x.MemoryHandleTo<int>(It.IsAny<UnmanagedMemoryHandle>())).Returns(Count);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.GetSimpleFormatCount();

            Assert.AreEqual(Count, retval);
        }
    }
}