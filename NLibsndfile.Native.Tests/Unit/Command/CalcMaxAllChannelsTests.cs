using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class CalcMaxAllChannelsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcMaxAllChannels(IntPtr.Zero, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnNegativeChannels()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnZeroChannels()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnNullResult()
        {
            const double[] Result = null;

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.CalcMaxAllChannels(It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Result);

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), 1);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnZeroLengthResult()
        {
            var Result = new double[0];

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.CalcMaxAllChannels(It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Result);

            var api = new LibsndfileCommandApi(mock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), 1);
        }

        [Test]
        public void CalcMaxAllChannels_ShouldPassOnValidResult()
        {
            var Result = new[] { 1.0d };

            var mock = new Mock<ILibsndfileCommandApi>();
            mock.Setup(x => x.CalcMaxAllChannels(It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Result);

            var api = new LibsndfileCommandApi(mock.Object);
            var retval = api.CalcMaxAllChannels(new IntPtr(1), 1);

            Assert.AreEqual(Result, retval);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.AllocateArray<double>(It.IsAny<int>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), 1);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CalcMaxAllChannels_ShouldThrowExceptionOnGreaterThanZeroResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.AllocateArray<double>(It.IsAny<int>())).Returns(It.IsAny<IntPtr>());

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.CalcMaxAllChannels(new IntPtr(1), 1);
        }

        [Test]
        public void CalcMaxAllChannels_ShouldPassOnZeroResult()
        {
            var Result = new[] { 1.0 };

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.AllocateArray<double>(It.IsAny<int>())).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleToArray<double>(It.IsAny<UnmanagedMemoryHandle>())).Returns(Result);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.CalcMaxAllChannels(new IntPtr(1), 1);

            Assert.AreEqual(Result, retval);
        }

        [Test]
        public void CalcMaxAllChannels_ShouldReturnArrayLengthEqualToChannelsRequested()
        {
            const int Channels = 2;
            var Result = new[] { 1.0, 2.0 };

            var memoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.AllocateArray<double>(It.IsAny<int>())).Returns(memoryMock.Object);
            marshallerMock.Setup(x => x.MemoryHandleToArray<double>(It.IsAny<UnmanagedMemoryHandle>())).Returns(Result);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.CalcMaxAllChannels(new IntPtr(1), Channels);

            Assert.AreEqual(Result, retval);
            Assert.AreEqual(Channels, retval.Length);
        }
    }
}