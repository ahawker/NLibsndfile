using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetVbrEncodingQualityTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetVbrEncodingQuality_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetVbrEncodingQuality(IntPtr.Zero, It.IsAny<double>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetVbrEncodingQuality_ShouldThrowExceptionOnNegativeValue()
        {
            const double Value = -1.0;

            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), Value);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetVbrEncodingQuality_ShouldThrowExceptionOnGreaterThanOneValue()
        {
            const double Value = 2.0;

            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), Value);
        }

        [Test]
        public void SetVbrEncodingQuality_ShouldPassOnValidValue()
        {
            const double Value = 0.5;

            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), Value);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetVbrEncodingQuality_ShouldThrowExceptionOnNegativeResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<double>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), It.IsAny<double>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetVbrEncodingQuality_ShouldThrowExceptionOnGreaterThanZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<double>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), It.IsAny<double>());
        }

        [Test]
        public void SetVbrEncodingQuality_ShouldPassOnZeroResult()
        {
            var unmanagedMemoryMock = new Mock<UnmanagedMemoryHandle>();

            var marshallerMock = new Mock<ILibsndfileMarshaller>();
            marshallerMock.Setup(x => x.Allocate(It.IsAny<double>())).Returns(unmanagedMemoryMock.Object);

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(0);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetVbrEncodingQuality(new IntPtr(1), It.IsAny<double>());
        }
    }
}