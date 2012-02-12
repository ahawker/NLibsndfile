using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Read")]
    public class LibsndfileApiReadFramesTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadShortFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadFrames(IntPtr.Zero, It.IsAny<short[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShortFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            short[] buffer = null;
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShortFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new short[] { };
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShortFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new short[1];
            api.ReadFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadShortFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadShortFrames_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadIntFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadFrames(IntPtr.Zero, It.IsAny<int[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadIntFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            int[] buffer = null;
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadIntFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new int[] { };
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadIntFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new int[1];
            api.ReadFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadIntFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new int[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadIntFrames_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new int[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadFloatFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadFrames(IntPtr.Zero, It.IsAny<float[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFloatFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            float[] buffer = null;
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFloatFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new float[] { };
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadFloatFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new float[1];
            api.ReadFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadFloatFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new float[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadFloatFrames_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new float[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadDoubleFrames_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadFrames(IntPtr.Zero, It.IsAny<double[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadDoubleFrames_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            double[] buffer = null;
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadDoubleFrames_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new double[] { };
            api.ReadFrames(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadDoubleFrames_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new double[1];
            api.ReadFrames(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadDoubleFrames_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new double[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadDoubleFrames_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new double[1];
            var retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadFrames(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadFrames(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }
    }
}