using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class SeekTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Seek_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.Seek(IntPtr.Zero, 0, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Seek_ShouldThrowExceptionOnZeroCount()
        {
            var api = new LibsndfileApi();
            api.Seek(new IntPtr(1), 0, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Seek_ShouldThrowExceptionOnNegativeWhence()
        {
            var api = new LibsndfileApi();
            api.Seek(new IntPtr(1), 1, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Seek_ShouldThrowExceptionOnHigherThanTwoWhence()
        {
            var api = new LibsndfileApi();
            api.Seek(new IntPtr(1), 1, 3);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void Seek_ShouldThrowExceptionOnNegativeOffset()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Seek(It.IsAny<IntPtr>(), It.IsAny<long>(), It.IsAny<int>())).Returns(-1);

            var api = new LibsndfileApi(mock.Object);
            api.Seek(new IntPtr(1), 1, 1);
        }

        [Test]
        public void Seek_ShouldPassOnCorrectOffsetFromBeginning()
        {
            const int Result = 42;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Seek(It.IsAny<IntPtr>(), It.IsAny<long>(), It.IsAny<int>())).Returns(Result);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Seek(new IntPtr(1), 42, 1);

            Assert.AreEqual(Result, retval);
        }
    }
}