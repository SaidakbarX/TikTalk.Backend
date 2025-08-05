# TikTalk - TikTok Clone Backend API

A modern TikTok clone backend built with .NET 8 Minimal API, Entity Framework Core, and SQL Server.

## 🚀 Features

- **User Management**: Registration, authentication, profile management
- **Video Management**: Upload, view, like, comment on videos
- **Social Features**: Follow/unfollow users, repost videos
- **Real-time Features**: Notifications, messaging
- **Analytics**: Video analytics and statistics
- **Admin Panel**: Role-based access control
- **Security**: JWT authentication, password hashing with BCrypt

## 🏗️ Architecture

The project follows Clean Architecture principles:

- **Domain Layer**: Entities and core business logic
- **Application Layer**: Services, DTOs, interfaces, and validators
- **Infrastructure Layer**: Data access, repositories, DbContext
- **API Layer**: Minimal API endpoints and configuration

## 🛠️ Technologies Used

- **.NET 8**: Latest .NET framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Primary database
- **JWT**: Authentication and authorization
- **BCrypt**: Password hashing
- **FluentValidation**: Input validation
- **Swagger**: API documentation

## 📦 Project Structure

```
TikTalk.Backend/
├── TikTalk.Backend.Core/
│   └── Exceptions/           # Custom exceptions
├── TikTalk.Backend.Domain/
│   └── Entities/            # Domain entities
├── TikTalk.Backend.Application/
│   ├── Dtos/               # Data transfer objects
│   ├── Interfaces/         # Service and repository interfaces
│   ├── Services/           # Business logic services
│   ├── Helpers/            # Utility classes
│   └── Validator/          # FluentValidation validators
├── TikTalk.Backend.Infrastructure/
│   └── Persistence/        # Database configurations and repositories
└── Program.cs              # Minimal API configuration
```

## 🔧 Setup Instructions

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd TikTalk.Backend
   ```

2. **Update connection string**
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your-SQL-Server-Connection-String"
     }
   }
   ```

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Create database migration**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7000` (HTTPS) or `http://localhost:5000` (HTTP).

## 📚 API Endpoints

### Authentication
- `POST /api/auth/signup` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh JWT token
- `POST /api/auth/logout` - User logout

### Users
- `GET /api/users/{id}` - Get user by ID
- `PUT /api/users/profile` - Update user profile

### Videos
- `GET /api/videos` - Get trending videos
- `GET /api/videos/{id}` - Get video by ID
- `POST /api/videos` - Upload new video
- `POST /api/videos/{id}/like` - Like a video
- `DELETE /api/videos/{id}/like` - Unlike a video

### Social Features
- `POST /api/users/{id}/follow` - Follow a user
- `DELETE /api/users/{id}/follow` - Unfollow a user

### Comments
- `GET /api/videos/{id}/comments` - Get video comments
- `POST /api/videos/{id}/comments` - Add comment to video

### Notifications
- `GET /api/notifications` - Get user notifications
- `PUT /api/notifications/{id}/read` - Mark notification as read

### Analytics
- `GET /api/videos/{id}/analytics` - Get video analytics

## 🔒 Authentication

The API uses JWT Bearer tokens for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## 🗄️ Database Schema

The application uses a single SQL Server database with the following main entities:

- **Users**: User accounts and profiles
- **Videos**: Video content and metadata
- **VideoLikes**: Video likes relationship
- **VideoComments**: Comments on videos
- **Follows**: User follow relationships
- **Notifications**: User notifications
- **Messages**: Direct messages between users
- **Hashtags**: Video hashtags
- **Analytics**: Video statistics

## 🔧 Configuration

### JWT Settings
```json
{
  "Jwt": {
    "Issuer": "TikTalk",
    "Audience": "TikTalk",
    "SecurityKey": "YourSuperSecretKeyThatIsAtLeast32Characters!",
    "Lifetime": "24"
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TikTalkDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

## 🚀 Deployment

### For Free Hosting (Railway, Heroku, etc.)

1. **Update connection string** for your cloud database
2. **Set environment variables** for JWT settings
3. **Build and deploy** the application

### Docker Deployment
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TikTalk.Backend.csproj", "."]
RUN dotnet restore "./TikTalk.Backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TikTalk.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TikTalk.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TikTalk.Backend.dll"]
```

## 🛡️ Security Features

- **Password Hashing**: BCrypt with salt
- **JWT Authentication**: Secure token-based auth
- **Input Validation**: FluentValidation for all inputs
- **CORS**: Configurable cross-origin requests
- **Exception Handling**: Global exception middleware
- **Authorization**: Role-based access control

## 🧪 Testing

Run tests with:
```bash
dotnet test
```

## 📝 API Documentation

When running in development mode, Swagger UI is available at:
- `https://localhost:7000/swagger`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if needed
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For support, please create an issue on GitHub or contact the development team.

## 🔄 Version History

- **v1.0.0**: Initial release with core features
  - User authentication and management
  - Video upload and management
  - Social features (follow, like, comment)
  - Real-time notifications
  - Admin panel functionality

---

**Built with ❤️ using .NET 8 and Entity Framework Core**