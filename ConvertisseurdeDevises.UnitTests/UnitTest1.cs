using Xunit;
using System.Threading.Tasks;
using ConvertisseurdeDevises.Services;
using System.Net.Http;

namespace ConvertisseurdeDevises.UnitTests
{
    public class ExchangeRateServiceTest
    {
        [Fact]
        public async Task GetLatestRatesAsync_ReturnsData()
        {
            // Arrange
            var httpClient = new HttpClient();
            var service = new ExchangeRateService(httpClient);
            
            // Act
            var result = await service.GetLatestRatesAsync("USD");
            
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Conversion_Rates);
        }
    }
}