using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetUpdateHeaderAutoTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUpdateHeaderAuto_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetUpdateHeaderAuto(IntPtr.Zero, It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetUpdateHeaderAuto_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetUpdateHeaderAuto(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetUpdateHeaderAuto_ShouldThrowExceptionOnGreaterThanOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(2);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetUpdateHeaderAuto(new IntPtr(1), It.IsAny<bool>());
        }

        [Test]
        public void SetUpdateHeaderAuto_ShouldPassOnZeroResult()
        {
            const int AutoUpdate = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(AutoUpdate);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetUpdateHeaderAuto(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(AutoUpdate), retval);
        }

        [Test]
        public void SetUpdateHeaderAuto_ShouldPassOnOneResult()
        {
            const int AutoUpdate = 0;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(AutoUpdate);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetUpdateHeaderAuto(new IntPtr(1), It.IsAny<bool>());

            Assert.AreEqual(Convert.ToBoolean(AutoUpdate), retval);
        }
    }
}