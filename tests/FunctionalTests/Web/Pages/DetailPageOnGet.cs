using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.eShopWeb.FunctionalTests.Web;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.WebRazorPages;

[Collection("Sequential")]
public class DetailPageOnGet : IClassFixture<WebTestFixture>
{
    public DetailPageOnGet(WebTestFixture factory)
    {
        Client = factory.CreateClient();
    }

    public HttpClient Client { get; }

    [Fact]
    public async Task ReturnsHomePageWithProductListing()
    {
        // Arrange & Act
        var response = await Client.GetAsync("/Product/18");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Synthetic Engine Oil", stringResponse);
    }
}
