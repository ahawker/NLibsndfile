using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Read")]
    public class LibsndfileApiReadItemsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadShortItems_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadItems(IntPtr.Zero, It.IsAny<short[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShortItems_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            short[] buffer = null;
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShortItems_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new short[] { };
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShortItems_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new short[1];
            api.ReadItems(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadShortItems_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadShortItems_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadIntItems_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadItems(IntPtr.Zero, It.IsAny<int[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadIntItems_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            int[] buffer = null;
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadIntItems_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new int[] { };
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadIntItems_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new int[1];
            api.ReadItems(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadIntItems_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new int[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadIntItems_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new int[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<int[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadFloatItems_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadItems(IntPtr.Zero, It.IsAny<float[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFloatItems_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            float[] buffer = null;
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFloatItems_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new float[] { };
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadFloatItems_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new float[1];
            api.ReadItems(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadFloatItems_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new float[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadFloatItems_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new float[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<float[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadDoubleItems_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.ReadItems(IntPtr.Zero, It.IsAny<double[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadDoubleItems_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            double[] buffer = null;
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadDoubleItems_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new double[] { };
            api.ReadItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadDoubleItems_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new double[1];
            api.ReadItems(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void ReadDoubleItems_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new double[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadDoubleItems_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const long Items = 10;
            const long PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new double[1];
            var retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<double[]>(), It.IsAny<long>())).Returns(0);
            retval = api.ReadItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }
    }
}