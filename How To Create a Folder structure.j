The Automation projects are plain class libraries, so you create them with the Class Library template. Create them before the Tests projects so the Tests projects can reference them.

Step 1: Create the API_Automation project
In Visual Studio, open your solution (or create a blank one: File → New → Project → Blank Solution, named ASEC.IdentityPlatform.Automation).
In Solution Explorer, right-click the Solution → Add → New Project.
Search for Class Library and choose the C# one (not “.NET Framework” unless your team uses it). Click Next.
Set:
Project name: ASEC_Identity_Platform_API_Automation
Location: the src folder inside the solution folder (create src first if it doesn’t exist)
Click Next, choose the Framework (e.g. .NET 8.0), and click Create.
Delete the default Class1.cs: right-click it → Delete.
Step 2: Create the Web_Automation project

Repeat the same steps:

Right-click the Solution → Add → New Project → Class Library → Next.
Project name: ASEC_Identity_Platform_Web_Automation, Location: the same src folder.
Choose the same framework as API_Automation (.NET 8.0) → Create.
Delete Class1.cs.
Step 3: Add the NuGet packages each library needs

API_Automation (HTTP calls and JSON handling):

Right-click the project → Manage NuGet Packages → Browse.
Install what your code needs. Typical choices are RestSharp or just the built-in HttpClient (no package), and Newtonsoft.Json (or built-in System.Text.Json).

Web_Automation (browser automation):

Right-click the project → Manage NuGet Packages → Browse.
Install Microsoft.Playwright.

If the restore fails with a 401 error, your private NuGet feed needs sign-in. See the earlier fix with dotnet restore --interactive, or sign in under Tools → NuGet Package Manager → Package Manager Settings → Package Sources.

Step 4: Add your first classes

Right-click each project → Add → Class:

In API_Automation: e.g. IdentityApiClient.cs, plus a Models folder for request/response classes.
In Web_Automation: e.g. BrowserFactory.cs, plus a Pages folder for page objects such as LoginPage.cs.

Keep these projects free of [Test] attributes. They only hold reusable code.

Step 5: Build the libraries

Press Ctrl+Shift+B and confirm the build succeeds before moving on. Fixing errors now is easier than after the Tests projects reference them.

Step 6: Then create the Tests projects

Now the libraries exist, so follow the earlier Option B steps:

Add NUnit Test Project named ASEC_Identity_Platform_API_Tests (same src folder, same framework).
Add NUnit Test Project named ASEC_Identity_Platform_Web_Tests.
Right-click API_Tests → Add → Project Reference → tick API_Automation → OK.
Right-click Web_Tests → Add → Project Reference → tick Web_Automation → OK.
Right-click Web_Tests → Manage NuGet Packages → install Microsoft.Playwright.NUnit.
Final structure
Solution 'ASEC.IdentityPlatform.Automation'
└── src
    ├── ASEC_Identity_Platform_API_Automation   (Class Library)
    ├── ASEC_Identity_Platform_Web_Automation   (Class Library)
    ├── ASEC_Identity_Platform_API_Tests        (NUnit, references API_Automation)
    └── ASEC_Identity_Platform_Web_Tests        (NUnit, references Web_Automation)
Tips
Solution folder vs disk folder: a src folder you create in Solution Explorer is virtual. To have a real src directory on disk, create it in File Explorer first and use it as the Location in each wizard.
Same framework everywhere: a mismatch between a library and the project referencing it causes build errors.
Order matters only for references: a Tests project can’t reference a library that doesn’t exist yet, which is why you create the Automation projects first.


