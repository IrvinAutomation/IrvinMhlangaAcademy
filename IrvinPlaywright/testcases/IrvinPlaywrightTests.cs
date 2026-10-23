using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrvinPlaywright.testcases
{
    public class IrvinPlaywrightTests
    {
        static async Task Main(string[] args)
        {
            //Creating a playwright instance
           var playwright = await Playwright.CreateAsync();

            //launch a new browser instance
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false});

            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://gmail.com/");

            var title = await page.TitleAsync();

            Console.WriteLine(title);

            await page.Locator("#identifierId").FillAsync("Irvin@gmail.com");

            await page.GetByRole(AriaRole.Button, new() {Name = "Next" }).ClickAsync();

            await Task.Delay(5000);


        }
    }
}
