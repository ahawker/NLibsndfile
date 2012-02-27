using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetAddPeakChunkTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAddPeakChunk_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetAddPeakChunk(IntPtr.Zero, It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetAddPeakChunk_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetAddPeakChunk(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetAddPeakChunk_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetAddPeakChunk(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        public void SetAddPeakChunk_ShouldPassOnZeroResult()
        {
            const int Scaling = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Scaling);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetAddPeakChunk(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Scaling), retval);
        }

        [Test]
        public void SetAddPeakChunk_ShouldPassOnOneResult()
        {
            const int Scaling = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Scaling);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetAddPeakChunk(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Scaling), retval);
        }
    }
}