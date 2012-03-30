using System;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Integration.Api
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.IntegrationTests.Api")]
    public class ReadFramesTests
    {
        [Test]
        public void ReadFramesShort_ReturnsValidData()
        {
            const long Items = 50;

            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            var open = api.Open(TestConfiguration.ValidWavFile, LibsndfileMode.Read, ref info);
            Assert.That(open != IntPtr.Zero);

            var buffer = new short[Items];
            var read = api.ReadFrames(open, buffer, Items);
            Assert.That(Items == read);
            Assert.That(buffer != null);
            Assert.That(buffer.Length == Items);

            var close = api.Close(open);
            Assert.That(close == LibsndfileError.NoError);
        }

        [Test]
        public void ReadFramesInt_ReturnsValidData()
        {
            const long Items = 50;

            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            var open = api.Open(TestConfiguration.ValidWavFile, LibsndfileMode.Read, ref info);
            Assert.That(open != IntPtr.Zero);

            var buffer = new int[Items];
            var read = api.ReadFrames(open, buffer, Items);
            Assert.That(Items == read);
            Assert.That(buffer != null);
            Assert.That(buffer.Length == Items);

            var close = api.Close(open);
            Assert.That(close == LibsndfileError.NoError);
        }

        [Test]
        public void ReadFramesFloat_ReturnsValidData()
        {
            const long Items = 50;

            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            var open = api.Open(TestConfiguration.ValidWavFile, LibsndfileMode.Read, ref info);
            Assert.That(open != IntPtr.Zero);

            var buffer = new float[Items];
            var read = api.ReadFrames(open, buffer, Items);
            Assert.That(Items == read);
            Assert.That(buffer != null);
            Assert.That(buffer.Length == Items);

            var close = api.Close(open);
            Assert.That(close == LibsndfileError.NoError);
        }

        [Test]
        public void ReadFramesDouble_ReturnsValidData()
        {
            const long Items = 50;

            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            var open = api.Open(TestConfiguration.ValidWavFile, LibsndfileMode.Read, ref info);
            Assert.That(open != IntPtr.Zero);

            var buffer = new double[Items];
            var read = api.ReadFrames(open, buffer, Items);
            Assert.That(Items == read);
            Assert.That(buffer != null);
            Assert.That(buffer.Length == Items);

            var close = api.Close(open);
            Assert.That(close == LibsndfileError.NoError);
        }
    }
}