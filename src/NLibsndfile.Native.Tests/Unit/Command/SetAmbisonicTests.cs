using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.CommandApi")]
    public class SetAmbisonicTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAmbisonic_ShouldThrowExceptionOnZeroHandle()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetAmbisonic(IntPtr.Zero, It.IsAny<LibsndfileMode>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAmbisonic_ShouldThrowExceptionOnInvalidMode()
        {
            var mock = new Mock<ILibsndfileCommandApi>();

            var api = new LibsndfileCommandApi(mock.Object);
            api.SetAmbisonic(new IntPtr(1), It.IsAny<LibsndfileMode>());
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetAmbisonic_ShouldThrowExceptionOnNegativeResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetAmbisonic(new IntPtr(1), LibsndfileMode.AmbisonicBFormat);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetAmbisonic_ShouldThrowExceptionOnOneResult()
        {
            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            api.SetAmbisonic(new IntPtr(1), LibsndfileMode.AmbisonicBFormat);
        }

        [Test]
        public void SetAmbisonic_ShouldReturnTrueOnModeMatch()
        {
            const int Result = 0x40;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Result);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetAmbisonic(new IntPtr(1), LibsndfileMode.AmbisonicNone);

            Assert.IsTrue(retval);
        }

        [Test]
        public void SetAmbisonic_ShouldReturnFalseOnModeMismatch()
        {
            const int Result = 0x40;
            const int Mismatch = 0x41;

            var marshallerMock = new Mock<ILibsndfileMarshaller>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(Mismatch);

            var api = new LibsndfileCommandApiNativeWrapper(mock.Object, marshallerMock.Object);
            var retval = api.SetAmbisonic(new IntPtr(1), (LibsndfileMode)Result);

            Assert.IsFalse(retval);
        }
    }
}