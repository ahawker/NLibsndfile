using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class ReadRawTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadRaw_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadRaw(IntPtr.Zero, It.IsAny<byte[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadRaw_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            byte[] buffer = null;
            api.ReadRaw(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadRaw_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new byte[] { };
            api.ReadRaw(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadRaw_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new byte[1];
            api.ReadRaw(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadRaw_ShouldReturnSameAsItemsRequested()
        {
            const long Bytes = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadRaw(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<long>())).Returns(Bytes);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new byte[1];
            var retval = api.ReadRaw(new IntPtr(1), buffer, Bytes);

            Assert.AreEqual(Bytes, retval);
        }

        [Test]
        public void ReadRaw_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Bytes = 10;
            const long PartialBytes = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadRaw(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<long>())).Returns(PartialBytes);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new byte[1];
            var retval = api.ReadRaw(new IntPtr(1), buffer, Bytes);

            Assert.AreEqual(PartialBytes, retval);

            mock.Setup(x => x.ReadRaw(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadRaw(new IntPtr(1), buffer, Bytes);

            Assert.AreEqual(0, retval);
        }
    }
}