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

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Close_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.Close(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void Close_ShouldThrowExceptionOnErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Close(It.IsAny<IntPtr>())).Returns(LibsndfileError.MalformedFile);

            var api = new LibsndfileApi(mock.Object);
            api.Close(new IntPtr(1));
        }

        [Test]
        public void Close_ShouldPassOnNoErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Close(It.IsAny<IntPtr>())).Returns(LibsndfileError.NoError);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.Close(new IntPtr(1));

            Assert.AreEqual(LibsndfileError.NoError, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteSync_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.WriteSync(IntPtr.Zero);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetString_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.SetString(IntPtr.Zero, It.IsAny<LibsndfileStringType>(), It.IsAny<string>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetString_ShouldThrowExceptionOnEmptyValue()
        {
            var api = new LibsndfileApi();
            api.SetString(new IntPtr(1), It.IsAny<LibsndfileStringType>(), string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetString_ShouldThrowExceptionOnNullValue()
        {
            var api = new LibsndfileApi();
            api.SetString(new IntPtr(1), It.IsAny<LibsndfileStringType>(), null);
        }

        [Test]
        [ExpectedException(typeof(LibsndfileException))]
        public void SetString_ShouldThrowExceptionOnErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.SetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>(), It.IsAny<string>())).Returns(LibsndfileError.MalformedFile);

            var api = new LibsndfileApi(mock.Object);
            api.SetString(new IntPtr(1), LibsndfileStringType.Album, "album");
        }

        [Test]
        public void SetString_ShouldPassOnNoErrorResult()
        {
            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.SetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>(), It.IsAny<string>())).Returns(LibsndfileError.NoError);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.SetString(new IntPtr(1), LibsndfileStringType.Album, "album");

            Assert.AreEqual(LibsndfileError.NoError, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetString_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.GetString(IntPtr.Zero, It.IsAny<LibsndfileStringType>());
        }

        [Test]
        public void GetString_ShouldReturnValidString()
        {
            const string Tag = "AlbumTag";

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.GetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>())).Returns(Tag);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.GetString(new IntPtr(1), LibsndfileStringType.Album);

            Assert.AreEqual(Tag, retval);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadShort_ShouldThrowExceptionOnZeroHandle()
        {
            var api = new LibsndfileApi();
            api.Read(IntPtr.Zero, It.IsAny<short[]>(), It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShort_ShouldThrowExceptionOnNullBuffer()
        {
            var api = new LibsndfileApi();
            api.Read(new IntPtr(1), null, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShort_ShouldThrowExceptionOnEmptyBuffer()
        {
            var api = new LibsndfileApi();
            api.Read(new IntPtr(1), new short[] { }, It.IsAny<long>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShort_ShouldThrowExceptionOnLessThanZeroItems()
        {
            var api = new LibsndfileApi();
            api.Read(new IntPtr(1), new short[] { }, -1); 
        }

        [Test]
        public void ReadShort_ShouldReturnSameAsItemsRequested()
        {
            const int Items = 10;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Read(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(Items);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.Read(new IntPtr(1), buffer, Items);

            Assert.AreEqual(Items, retval);
        }

        [Test]
        public void ReadShort_ShouldReturnLessThanItemsRequestedThenZeroOnNextRead()
        {
            const int Items = 10;
            const int PartialItems = 10 - 5;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.Read(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(PartialItems);

            var api = new LibsndfileApi(mock.Object);
            var buffer = new short[1];
            var retval = api.Read(new IntPtr(1), buffer, Items);

            Assert.AreEqual(PartialItems, retval);

            mock.Setup(x => x.Read(It.IsAny<IntPtr>(), It.IsAny<short[]>(), It.IsAny<long>())).Returns(0);
            retval = api.Read(new IntPtr(1), buffer, Items);

            Assert.AreEqual(0, retval);
        }
    }
}
