using Microsoft.Playwright;

namespace PlaywrightExperiments;

public class PlaywrightFixture : IAsyncLifetime
{
    public IBrowser Browser { get; set; } = null!;
    private IPlaywright PlaywrightInstance { get; set; } = null!;

    public async Task InitializeAsync()
    {
        PlaywrightInstance = await Playwright.CreateAsync();

        BrowserTypeLaunchOptions options = null;

        // Uncomment lines below to run in headful mode (to view browser UI).
        //options = new()
        //{
        //    Headless = false,
        //    SlowMo = 50,
        //};

        Browser = await PlaywrightInstance.Chromium.LaunchAsync(options);
    }

    public async Task DisposeAsync()
    {
        await Browser.DisposeAsync();
        PlaywrightInstance.Dispose();
    }
}