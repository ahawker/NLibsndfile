using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.Tests.Write")]
    public class LibsndfileApiWriteItemsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteShortItems_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteItems(IntPtr.Zero, It.IsAny<short[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShortItems_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            short[] buffer = null;
            api.WriteItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShortItems_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            var buffer = new short[] { };
            api.WriteItems(new IntPtr(1), buffer, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShortItems_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            var buffer = new short[1];
            api.WriteItems(new IntPtr(1), buffer, -1);
        }

        [Test]
        public void WriteShortItems_ShouldReturnSameAsItemsRequested()
        {
            const long Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.ReadItems(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.WriteItems(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }
    }
}