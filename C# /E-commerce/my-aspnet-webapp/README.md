# README.md

# My ASP.NET Web Application

This is a sample ASP.NET web application created using Visual Studio Code. The application demonstrates the use of MVC architecture and includes basic functionality for handling requests and rendering views.

## Project Structure

- **Controllers**: Contains the controllers that handle user requests.
  - `HomeController.cs`: Manages requests for the home page.
  - `ApiController.cs`: Provides API endpoints for the application.

- **Models**: Contains the data models used in the application.
  - `ErrorViewModel.cs`: Represents error handling properties.

- **Views**: Contains the Razor views for rendering HTML.
  - **Home**: Contains views related to the home page.
    - `Index.cshtml`: The main landing page.
    - `Privacy.cshtml`: The privacy policy page.
  - **Shared**: Contains shared layout views.
    - `_Layout.cshtml`: The layout for the application.

- **wwwroot**: Contains static files such as CSS and JavaScript.
  - **css**: Contains stylesheets.
    - `site.css`: The main stylesheet for the application.
  - **js**: Contains JavaScript files.
    - `site.js`: The main JavaScript file for the application.

- **Program.cs**: The entry point of the application, configuring and running the web host.

- **appsettings.json**: Configuration settings for the application.

- **appsettings.Development.json**: Development-specific configuration settings.

- **my-aspnet-webapp.csproj**: The project file specifying dependencies and build settings.

## Getting Started

To run the application, follow these steps:

1. Clone the repository.
2. Navigate to the project directory.
3. Run the application using the command:
   ```
   dotnet run
   ```
4. Open your web browser and navigate to `http://localhost:5000` to view the application.

## License

This project is licensed under the MIT License.