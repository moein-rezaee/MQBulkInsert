# MQ Bulk Insert With EF & MassTransit

## Description

This project is a robust application designed to efficiently handle bulk data processing and management through a message-driven architecture. It incorporates features for uploading, validating, and processing Excel files while ensuring the integrity and scalability of data operations. The application follows the CQRS (Command Query Responsibility Segregation) pattern, enhancing the separation of read and write operations.

## Technologies Used

- **C#**: The primary programming language for backend development.
- **ASP.NET Core**: A framework for building web applications and APIs.
- **Entity Framework Core**: An Object-Relational Mapper (ORM) for seamless data access.
- **MediatR**: A simple mediator implementation for .NET to facilitate the CQRS (Command Query Responsibility Segregation) pattern.
- **CQRS**: A design pattern that separates read and write operations, enhancing scalability and maintainability.
- **MassTransit**: A distributed application framework for .NET, enabling message-based communication.
- **RabbitMQ**: A message broker that handles message queues, ensuring reliable message delivery.
- **EPPlus**: A library for reading and writing Excel files.
- **Clean Architecture**: A software design pattern that separates concerns and promotes testability.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- A database server (e.g., SQL Server)
- [RabbitMQ](https://www.rabbitmq.com/download.html) installed and running

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/moein-rezaee/MQBulkInsert.git
   cd MQBulkInsert
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json` to match your database settings.

4. Run the RabbitMQ server, ensuring it is configured correctly.

5. Run the application:
   ```bash
   dotnet run
   ```

### Monitoring the Application

Consider using monitoring tools like **Serilog** or **Application Insights** to track application performance and log errors.

### Testing the API

You can test the API using tools like **Postman** or **cURL**. Below are some example endpoints:

- **Import a File**:  
  `POST /User/Import`  
  Upload an Excel file for processing.

- **Check Import Status**:  
  `GET /User/CheckImportState?trackingId=...`  
  Retrieve the status of the import process.

## License

This project is licensed under the MIT License. You are free to use, modify, and distribute this software, provided that all copies include the original license and attribution to the authors.

---

Feel free to customize further if needed!
