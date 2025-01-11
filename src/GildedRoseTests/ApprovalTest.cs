using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GildedRoseKata;
using VerifyXunit;
using Xunit;

namespace GildedRoseTests
{
    [ExcludeFromCodeCoverage]
    public class ApprovalTest
    {
        [Fact]
        public Task ProgramMain_ThirtyDays_OutputVerified()
        {
            // Arrange
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Console.SetIn(new StringReader("a\n"));

            // Act
            Program.Main(["30"]);
            var output = fakeoutput.ToString();

            // Assert
            return Verifier.Verify(output);
        }
    }
}
