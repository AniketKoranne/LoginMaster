CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(100) NOT NULL, -- Store hashed passwords
    PasswordSalt NVARCHAR(100) NOT NULL, -- Store password salts
    Organization NVARCHAR(100) NULL,
    Department NVARCHAR(100) NULL,
    IsAdmin BIT NOT NULL DEFAULT 0, -- Indicates if user is an administrator
    IsActive BIT NOT NULL DEFAULT 1, -- Indicates if user account is active
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Timestamp for account creation
    CONSTRAINT CHK_IsAdmin CHECK (IsAdmin IN (0, 1)), -- Validate IsAdmin values
    CONSTRAINT CHK_IsActive CHECK (IsActive IN (0, 1)) -- Validate IsActive values
);

-- checking table
select * from Users