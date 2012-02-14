using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Write")]
    public class LibsndfileApiWriteRawTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteRaw_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteRaw(IntPtr.Zero, It.IsAny<byte[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteRaw_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            byte[] buffer = null;
            api.WriteRaw(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteRaw_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new byte[] { };
            api.WriteRaw(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteRaw_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new byte[1];
            api.WriteRaw(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void WriteRaw_ShouldReturnSameAsItemsRequested()
        {
            const long Bytes = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.WriteRaw(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<long>())).Returns(Bytes);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new byte[1];
            var retval = api.WriteRaw(new IntPtr(1), buffer, Bytes);

            Assert.AreEqual(Bytes, retval);
        }
    }
}