using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace PlaywrightExperiments;

public class UnitTest1 : IClassFixture<PlaywrightFixture>
{
    private readonly IBrowser _browser;

    public UnitTest1(PlaywrightFixture fixture)
    {
        _browser = fixture.Browser;
    }

    [Fact]
    public async Task Playwright_HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        var page = await _browser.NewPageAsync();
        await page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Assertions.Expect(page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

        // Expect an attribute "to be strictly equal" to the value.
        await Assertions.Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Assertions.Expect(page).ToHaveURLAsync(new Regex(".*intro"));
    }

    [Fact]
    public async Task Github_Incorrect_Login()
    {
        var page = await _browser.NewPageAsync();
        await page.GotoAsync("https://github.com/login");

        await page.GetByLabel("Username or email address").FillAsync("user01");
        await page.GetByLabel("Password").FillAsync("password01");

        var signInButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign in", Exact = true });
        await signInButton.ClickAsync();

        await Assertions.Expect(page.GetByText("Incorrect username or password")).ToBeVisibleAsync();
    }
}