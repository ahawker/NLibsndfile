using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class CalcSignalMaxTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalcSignalMax_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcSignalMax(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcSignalMax_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<double>()).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.CalcSignalMax(new IntPtr(1));
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcSignalMax_ShouldThrowExceptionOnGreaterThanZeroResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<double>()).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.CalcSignalMax(new IntPtr(1));
        }

        [Test]
        public void CalcSignalMax_ShouldPassOnZeroResult()
        {
            const double SignalMax = 1.0;

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate<double>()).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleTo<double>(It.IsAny<UnmanagedMemoryHandle>())).Returns(SignalMax);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.CalcSignalMax(new IntPtr(1));

            Assert.AreEqual(SignalMax, retval);
        }
    }
}