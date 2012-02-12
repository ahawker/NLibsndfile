using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Write")]
    public class LibsndfileApiWriteFramesTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteShortFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteFrames(IntPtr.Zero, It.IsAny<short[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShortFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            short[] buffer = null;
            api.WriteFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShortFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new short[] { };
            api.WriteFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteShortFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new short[1];
            api.WriteFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void WriteShortFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Frames = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.WriteFrames(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(Frames);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.WriteFrames(new IntPtr(1), buffer, Frames);

            Assert.AreEqual(Frames, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteIntFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteFrames(IntPtr.Zero, It.IsAny<int[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteIntFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            int[] buffer = null;
            api.WriteFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteIntFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new int[] { };
            api.WriteFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteIntFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new int[1];
            api.WriteFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void WriteIntFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Frames = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.WriteFrames(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(Frames);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new int[1];
            var retval = api.WriteFrames(new IntPtr(1), buffer, Frames);

            Assert.AreEqual(Frames, retval);
        }
    }
}