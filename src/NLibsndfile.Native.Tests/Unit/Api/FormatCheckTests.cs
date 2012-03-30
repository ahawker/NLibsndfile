using System;
using Moq;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Unit.Api
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.UnitTests.Api")]
    public class FormatCheckTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatCheck_ShouldThrowExceptionOnDefaultInfoStruct()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            api.FormatCheck(ref info);
        }

        [Test]
        public void FormatCheck_ShouldFailOnInvalidFormat()
        {
            const bool Result = false;

            var info = new LibsndfileInfo {Format = LibsndfileFormat.Wav, Channels = 1, SampleRate = 1};

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.FormatCheck(ref info)).Returns(Result);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.FormatCheck(ref info);

            Assert.AreEqual(Result, retval);
        }

        [Test]
        public void FormatCheck_ShouldPassOnValidFormat()
        {
            const bool Result = true;

            var info = new LibsndfileInfo { Format = LibsndfileFormat.Wav, Channels = 1, SampleRate = 1 };

            var mock = new Mock<ILibsndfileApi>();
            mock.Setup(x => x.FormatCheck(ref info)).Returns(Result);

            var api = new LibsndfileApi(mock.Object);
            var retval = api.FormatCheck(ref info);

            Assert.AreEqual(Result, retval);
        }
    }
}