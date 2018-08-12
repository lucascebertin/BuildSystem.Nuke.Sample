using BuildSystem.Nuke.Sample.App;
using FluentAssertions;
using Xunit;

namespace BuildSystem.Nuke.Sample.Tests.Integration
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Program.Main(null).Should().Be(0);
            Program.Main(new string[] { }).Should().Be(0);
        }
    }
}
