using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetNormDoubleTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetNormDouble_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetNormDouble(IntPtr.Zero, It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetNormDouble_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetNormDouble(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetNormDouble_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetNormDouble(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        public void SetNormDouble_ShouldPassOnZeroNormalization()
        {
            const int Normalization = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Normalization);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetNormDouble(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Normalization), retval);
        }

        [Test]
        public void SetNormDouble_ShouldPassOnOneNormalization()
        {
            const int Normalization = 1;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Normalization);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetNormDouble(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(Normalization), retval);
        } 
    }
}