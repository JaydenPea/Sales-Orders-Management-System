# Sales Orders Management System

## Project Overview

This project is a comprehensive Sales Orders Management System with built-in authentication, user role management, and integrated analytics dashboard. The system allows for managing sales orders, user roles, and user authentication using JWT tokens. It is built using Blazor WebAssembly, with backend APIs to support the frontend operations, and integrates with external APIs for additional functionalities like analytics.

### Key Features:

1. **Custom Authentication Management (JWT):**
   - Built-in custom authentication state provider that manages user authentication with JWT tokens.
   - Users can register, log in, and the system handles token-based authentication across the frontend and backend.
   - JWT tokens are stored in local storage and used for secure API calls between the frontend and backend.

2. **Sales Order Management (CRUD):**
   - Full functionality for creating, reading, updating, and deleting (CRUD) sales orders.
   - Admin users can manage all sales orders, while other users have limited access based on their roles.

3. **User Role Management:**
   - Admins can add new users and assign roles (Admin, User, etc.).
   - Role-based authorization ensures that only users with the appropriate permissions can perform certain tasks.

4. **JWT-Based Authorization Between Frontend and Backend:**
   - Secured interaction between the Blazor WebAssembly frontend and backend APIs through JWT tokens.
   - Only authenticated users can access certain features, and role-based permissions limit access to sensitive actions.

5. **External API Integration:**
   - The application integrates with an external API to fetch data and display it on an analytics dashboard.
   - This API-driven approach allows users to view charts and insights generated from the sales data.

6. **Analytics Dashboard (JavaScript & API Integration):**
   - The system includes an analytics dashboard built using JavaScript libraries.
   - Data for the dashboard is fetched from external APIs and presented as charts using ApexCharts.
   - Users can track performance, view sales trends, and analyze data visually.

7. **API Logging with Serilog:**
   - All API activities are logged using Serilog, with logs written to a local file.
   - This enables effective monitoring and troubleshooting of backend operations.

8. **Unit Testing with XUnit:**
   - The system is tested using XUnit, with built-in tests to ensure the reliability and correctness of various features.
   - Unit tests are used for both frontend and backend components, ensuring robustness.

### Technology Stack:

- **Frontend:**
  - Blazor WebAssembly
  - JavaScript (for chart rendering)
  - ApexCharts (for analytics)
  - MudBlazor (UI components)
  - CSS, HTML
  
- **Backend:**
  - C# .NET Web API
  - Entity Framework (EF) Core
  - SQL Server (data storage)
  
- **Authentication & Authorization:**
  - JWT Tokens (for securing APIs)
  - Custom `AuthenticationStateProvider` (Blazor)
  
- **Logging:**
  - Serilog (for structured logging to local files)

### Custom Authentication Implementation:

The authentication system is built using Blazored.LocalStorage and a custom `AuthenticationStateProvider` that manages JWT tokens. The system parses the JWT token, extracts claims, and manages the authentication state for the Blazor application.


### External Libraries Used:

1. **Blazored.LocalStorage:** 
   - For handling local storage, particularly for storing JWT tokens.
   
2. **MudBlazor:**
   - For building a modern UI with prebuilt Blazor components.
   
3. **ApexCharts:**
   - Used for rendering interactive charts and graphs in the analytics dashboard.

### How to Run:

1. Clone the repository.
2. Install the required dependencies (Blazor WebAssembly, .NET SDK).
3. Set up the backend SQL Server database.
4. Run the backend Web API and configure it to use JWT for authentication.
5. Run the frontend Blazor WebAssembly project.
6. Access the application, register a new user, log in, and manage sales orders and user roles.

### Future Improvements:

- Additional analytics features to provide more insights into sales trends.
- More extensive role-based management with customizable permissions.
- Additional API integrations for extending the system's capabilities.
- Custom exporting(csv) based on selected setpoint.

---

This system demonstrates a full-fledged sales order management application with authentication, role management, and analytics, built using modern web technologies and development best practices.
