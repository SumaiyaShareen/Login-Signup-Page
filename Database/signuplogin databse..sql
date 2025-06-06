Create Database AUTHENTICATION;

USE AUTHENTICATION;

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

INSERT INTO Users (Username, Email, PasswordHash, FullName)
VALUES ('sumaiya123', 'sumaiya@example.com', 'hashed_password_here', 'Sumaiya');

Select * from Users;
