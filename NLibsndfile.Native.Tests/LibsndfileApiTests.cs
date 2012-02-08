using System;
using Moq;
using NUnit;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    public class LibsndfileApiTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Open_ShouldThrowExceptionOnEmptyPath()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            api.Open(string.Empty, LibsndfileMode.Read, ref info);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Open_ShouldThrowExceptionOnNullPath()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            api.Open(null, LibsndfileMode.Read, ref info);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void Open_ShouldThrowExceptionOnEmptyFileHandle()
        {
            var info = new LibsndfileInfo();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Open(It.IsAny<string>(), It.IsAny<LibsndfileMode>(), ref info)).Returns(IntPtr.Zero);

            var api = new LibsndfileApi(mock.Object);
            api.Open("junk.txt", LibsndfileMode.Read, ref info);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OpenFileDescriptor_ShouldThrowOnZeroHandle()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            api.OpenFileDescriptor(0, LibsndfileMode.Read, ref info, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OpenFileDescriptor_ShouldThrowOnNegativeOneHandle()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            api.OpenFileDescriptor(-1, LibsndfileMode.Read, ref info, 0);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void OpenFileDescriptor_ShouldThrowExceptionOnZeroFileHandle()
        {
            var info = new LibsndfileInfo();

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.OpenFileDescriptor(It.IsAny<int>(), It.IsAny<LibsndfileMode>(), ref info, It.IsAny<int>())).Returns(IntPtr.Zero);

            var api = new LibsndfileApi(mock.Object);
            api.OpenFileDescriptor(20, LibsndfileMode.Read, ref info, 0);
        }

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
    }
}
