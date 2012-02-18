using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class StringTests
    {
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
        public void GetString_ShouldReturnNullStringIfTagNotAvailable()
        {
            const string Tag = null;

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.GetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>())).Returns(Tag);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.GetString(new IntPtr(1), It.IsAny<LibsndfileStringType>());

            Assert.AreEqual(Tag, retval);
        }

        [Test]
        public void GetString_ShouldReturnEmptyStringIfTagUnset()
        {
            const string Tag = "";

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.GetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>())).Returns(Tag);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.GetString(new IntPtr(1), It.IsAny<LibsndfileStringType>());

            Assert.AreEqual(Tag, string.Empty);

        }

        [Test]
        public void GetString_ShouldReturnValidString()
        {
            const string Tag = "AlbumTag";

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.GetString(It.IsAny<IntPtr>(), It.IsAny<LibsndfileStringType>())).Returns(Tag);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.GetString(new IntPtr(1), It.IsAny<LibsndfileStringType>());

            Assert.AreEqual(Tag, retval);
        }
    }
}