using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetScaleFloatIntReadTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetScaleFloatIntRead_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetScaleFloatIntRead(IntPtr.Zero, It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetScaleFloatIntRead_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetScaleFloatIntRead(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetScaleFloatIntRead_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetScaleFloatIntRead(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        public void SetScaleFloatIntRead_ShouldPassOnZeroResult()
        {
            const int Scaling = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Scaling);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetScaleFloatIntRead(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Scaling), retval);
        }

        [Test]
        public void SetScaleFloatIntRead_ShouldPassOnOneResult()
        {
            const int Scaling = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Scaling);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetScaleFloatIntRead(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Scaling), retval);
        }
    }
}