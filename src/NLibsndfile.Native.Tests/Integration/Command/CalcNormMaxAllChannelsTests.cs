using System;
using NUnit.Framework;

namespace NLibsndfile.Native.Tests.Integration.Command
{
    [TestFixture]
    [Category("NLibsndfileApi.Native.IntegrationTests.CommandApi")]
    public class CalcNormMaxAllChannelsTests
    {
        [Test]
        public void CalcNormMaxAllChannels_ReturnsValidData()
        {
            var api = new LibsndfileApi();
            var info = new LibsndfileInfo();

            var sndfile = api.Open(TestConfiguration.ValidWavFile, LibsndfileMode.Read, ref info);
            Assert.That(sndfile != IntPtr.Zero);

            var max = api.Commands.CalcNormMaxAllChannels(sndfile, info.Channels);
            Assert.That(max != null);
            Assert.That(max.Length == info.Channels);

            var close = api.Close(sndfile);
            Assert.That(close == LibsndfileError.NoError);
        }
    }
}