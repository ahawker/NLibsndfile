using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Command")]
    public class LibsndfileApiCommandTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CommandIntPtr_ShouldThrowExceptionOnZeroHandleWithNonStaticCommand()
        {
            var api = new LibsndfileApi();
            api.Command(IntPtr.Zero, LibsndfileCommand.GetLogInfo, It.IsAny<IntPtr>(), It.IsAny<int>());
        }

        [Test]
        public void CommandIntPtr_ShouldPassOnZeroHandleWithStaticCommand()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, IntPtr.Zero, It.IsAny<int>());

            Assert.Greater(retval, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CommandIntPtr_ShouldThrowExceptionOnInvalidResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), It.IsAny<IntPtr>(), It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CommandInt_ShouldThrowExceptionOnZeroHandleWithNonStaticCommand()
        {
            var intRef = It.IsAny<int>();
            var api = new LibsndfileApi();
            api.Command(IntPtr.Zero, LibsndfileCommand.GetLogInfo, ref intRef, It.IsAny<int>());
        }

        [Test]
        public void CommandInt_ShouldPassOnZeroHandleWithStaticCommand()
        {
            var intRef = It.IsAny<int>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref intRef, It.IsAny<int>())).Returns(1);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, ref intRef, It.IsAny<int>());

            Assert.Greater(retval, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CommandInt_ShouldThrowExceptionOnInvalidResult()
        {
            var intRef = It.IsAny<int>();
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref intRef, It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), ref intRef, It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CommandDouble_ShouldThrowExceptionOnZeroHandleWithNonStaticCommand()
        {
            var doubleRef = It.IsAny<double>();
            var api = new LibsndfileApi();
            api.Command(IntPtr.Zero, LibsndfileCommand.GetLogInfo, ref doubleRef, It.IsAny<int>());
        }

        [Test]
        public void CommandDouble_ShouldPassOnZeroHandleWithStaticCommand()
        {
            var doubleRef = It.IsAny<double>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref doubleRef, It.IsAny<int>())).Returns(1);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, ref doubleRef, It.IsAny<int>());

            Assert.Greater(retval, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CommandDouble_ShouldThrowExceptionOnInvalidResult()
        {
            var doubleRef = It.IsAny<double>();
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref doubleRef, It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), ref doubleRef, It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CommandLong_ShouldThrowExceptionOnZeroHandleWithNonStaticCommand()
        {
            var longRef = It.IsAny<long>();
            var api = new LibsndfileApi();
            api.Command(IntPtr.Zero, LibsndfileCommand.GetLogInfo, ref longRef, It.IsAny<int>());
        }

        [Test]
        public void CommandLong_ShouldPassOnZeroHandleWithStaticCommand()
        {
            var longRef = It.IsAny<long>();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref longRef, It.IsAny<int>())).Returns(1);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, ref longRef, It.IsAny<int>());

            Assert.Greater(retval, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CommandLong_ShouldThrowExceptionOnInvalidResult()
        {
            var longRef = It.IsAny<long>();
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), ref longRef, It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), ref longRef, It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CommandDoubleArray_ShouldThrowExceptionOnZeroHandleWithNonStaticCommand()
        {
            var api = new LibsndfileApi();
            api.Command(IntPtr.Zero, LibsndfileCommand.GetLogInfo, It.IsAny<double[]>(), It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CommandDoubleArray_ShouldThrowExceptionOnNullData()
        {
            var api = new LibsndfileApi();
            double[] data = null;
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), data, It.IsAny<int>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CommandDoubleArray_ShouldThrowExceptionOnEmptyData()
        {
            var api = new LibsndfileApi();
            var data = new double[] { };
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), data, It.IsAny<int>());
        }

        [Test]
        public void CommandDoubleArray_ShouldPassOnZeroHandleWithStaticCommand()
        {
            var data = new double[1];
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), It.IsAny<double[]>(), It.IsAny<int>())).Returns(1);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Command(IntPtr.Zero, LibsndfileCommand.GetLibVersion, data, It.IsAny<int>());

            Assert.Greater(retval, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void CommandDoubleArray_ShouldThrowExceptionOnInvalidResult()
        {
            var data = new double[1];
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Command(It.IsAny<IntPtr>(), It.IsAny<LibsndfileCommand>(), data, It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Command(new IntPtr(1), It.IsAny<LibsndfileCommand>(), data, It.IsAny<int>());
        }
    }
}