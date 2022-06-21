# WorkoutGlobal

## How run app?

1. Open an IDE that supports running .NET 6 projects (Visual Studio 2022);
2. Open in Visual Studio 2022 Solution Explorer -> Set Startup Projects;
3. Select checkbox `Multiple startup projects` in this order and click on option `Start`:
- WorkoutGlobal.Api;
- WorkoutGlobal.UI;
- WorkoutGlobal.Monitoring.
4. Set all the projects specified in the fourth paragraph from IIS Express to Kestrel. To do this, expand the project launch button and select the appropriate project name, this is Kestrel;
5. Open in Visual Studio 2022 Solution Explorer. Select WorkoutGlobal.Api project. Open file "appsettings.json" and update "ConnectionStrings" with your database connection string.
6. Open in Visual Studio 2022 top menu item "View" -> Other Windows -> Package Maneger Console. In the command line that opens, type "Update-Database" and press the "Enter" key;
7. Run projects with `Start` button in top menu of Visual Studio 2022.
