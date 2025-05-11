OpenLog
A lightweight, clean-architecture logging API built with .NET 8, MongoDB, JWT Authentication, and Clean Code practices.

📚 Project Structure
Core: Domain models, services, interfaces

Infrastructure: MongoDB repositories, DTO mappings

API: Controllers, authentication, DI setup

🛠️ Tech Stack
.NET 8

MongoDB (Docker local or Atlas)

JWT Authentication

Swagger (API documentation)

xUnit + Moq (testing)

🚀 Quick Start
bash
Copier
Modifier
git clone https://github.com/your-username/OpenLog.git
cd OpenLog
Run MongoDB (Docker):

bash
Copier
Modifier
docker run -d --rm --name openlog-mongo -p 27017:27017 mongo:6
Configure appsettings.json:

json
Copier
Modifier
"MongoSettings": { "Connection": "mongodb://localhost:27017", "Database": "OpenLogDb" },
"Jwt": { "Key": "your-secure-key" }
Run the API:

bash
Copier
Modifier
dotnet run --project OpenLog.Api
Access Swagger at: http://localhost:5000/swagger

🔒 Authentication
Get JWT token: POST /api/auth/token

Authorize Swagger with:

php-template
Copier
Modifier
Bearer <your-token>
Access protected /api/log endpoints.

🧪 Testing
Run tests:

bash
Copier
Modifier
dotnet test OpenLog.Tests.Unit
dotnet test OpenLog.Tests.Integration
📄 License
MIT License.
