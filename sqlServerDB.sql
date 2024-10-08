USE [master]
GO
/****** Object:  Database [ShopDB]    Script Date: 9/2/2024 10:49:33 PM ******/
CREATE DATABASE [ShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopDB', FILENAME = N'/var/opt/mssql/data/ShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopDB_log', FILENAME = N'/var/opt/mssql/data/ShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [ShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopDB', N'ON'
GO
ALTER DATABASE [ShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ShopDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryId] [int] NULL,
	[Available] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Rating] [numeric](4, 2) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](11) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[HashedPassword] [nvarchar](255) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 9/2/2024 10:49:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_PhoneNumber]    Script Date: 9/2/2024 10:49:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_PhoneNumber] ON [dbo].[Users]
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_available]  DEFAULT ((0)) FOR [Available]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Rating]  DEFAULT ((0)) FOR [Rating]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_UpdatedAt]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RoleID]  DEFAULT ((1)) FOR [RoleID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[PRC_CreateCategory]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_CreateCategory]
@Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Categories (Name, Deleted)
    VALUES (@Name, 0);

    SELECT SCOPE_IDENTITY() AS CategoryId;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_CreateProduct]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_CreateProduct]
@Name NVARCHAR(100),
@Description NVARCHAR(MAX),
@Price DECIMAL(18, 2),
@CategoryId INT,
@Available BIT,
@Rating numeric(4, 2)
AS
BEGIN
    INSERT INTO Products (Name, Description, Price, CategoryId, Available, Deleted ,Rating)
    VALUES (@Name, @Description, @Price, @CategoryId, @Available, 0 ,@Rating);

    SELECT SCOPE_IDENTITY() AS ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_CreateUser]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_CreateUser]
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Email NVARCHAR(100),
    @HashedPassword NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, PhoneNumber, Email, HashedPassword, RoleID, Deleted)
    VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @HashedPassword, 1, 0);

    SELECT Id, FirstName, LastName, PhoneNumber, Email, RoleID
    FROM Users
    WHERE Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_DeleteCategory]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_DeleteCategory]
@Id INT
AS
BEGIN
    UPDATE Categories
    SET Deleted = 1
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_DeleteProduct]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_DeleteProduct]
@Id INT
AS
BEGIN
    UPDATE Products
    SET Deleted = 1
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_DeleteUser]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_DeleteUser]
@Id INT
AS
BEGIN
    UPDATE Users
    SET Deleted = 1
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetCategories]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetCategories]
AS
BEGIN
    SELECT Id, Name
    FROM Categories
    WHERE Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetCategoryById]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetCategoryById]
@Id INT
AS
BEGIN
    SELECT Id, Name
    FROM Categories
    WHERE Id = @Id AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetProductById]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetProductById]
@Id INT
AS
BEGIN
    SELECT Id, Name, Description, Price, CategoryId, Available ,Rating
    FROM Products
    WHERE Id = @Id AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetProducts]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetProducts]
AS
BEGIN
    SELECT Id, Name, Description, Price, CategoryId, Available ,Rating
    FROM Products
    WHERE Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetProductsByCategoryName]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetProductsByCategoryName]
    @CategoryName nvarchar(100)
AS
BEGIN
    SELECT 
        p.Id, p.Name, p.Description, p.Price, p.Available , p.Rating
    FROM 
        Products p
    INNER JOIN 
        Categories c ON p.CategoryId = c.Id
    WHERE 
        c.Name = @CategoryName AND p.Deleted =0 ;
END;
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetProductsByName]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetProductsByName]
    @SearchTerm NVARCHAR(255)
  
AS
BEGIN
 

    SELECT *
    FROM Products
    WHERE Name LIKE '%' + @SearchTerm + '%' AND Deleted=0
 
 
    SELECT COUNT(*)
    FROM Products
    WHERE Name LIKE '%' + @SearchTerm + '%'  AND Deleted=0;
END;
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetUserByEmail]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetUserByEmail]
@Email nvarchar(100)
AS
BEGIN
    SELECT Id, FirstName, LastName, PhoneNumber, Email ,RoleID , HashedPassword
    FROM Users
    WHERE Email = @Email AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetUserById]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetUserById]
@Id INT
AS
BEGIN
    SELECT Id, FirstName, LastName, PhoneNumber, Email
    FROM Users
    WHERE Id = @Id AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetUsers]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetUsers]
AS
BEGIN
    SELECT Id, FirstName, LastName, PhoneNumber, Email
    FROM Users
    WHERE Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_UpdateCategory]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_UpdateCategory]
@Id INT,
@Name NVARCHAR(100)
AS
BEGIN
    UPDATE Categories
    SET Name = @Name
    WHERE Id = @Id AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_UpdateProduct]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_UpdateProduct]
@Id INT,
@Name NVARCHAR(100),
@Description NVARCHAR(MAX),
@Price DECIMAL(18, 2),
@CategoryId INT,
@Available BIT,
@Rating numeric(4, 2)
AS
BEGIN
    UPDATE Products
    SET Name = @Name,
        Description = @Description,
        Price = @Price,
        CategoryId = @CategoryId,
        Available = @Available,
		Rating=@Rating
    WHERE Id = @Id AND Deleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_UpdateUser]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_UpdateUser]
    @Id INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Email NVARCHAR(100),
    @HashedPassword NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE Users
    SET FirstName = @FirstName,
        LastName = @LastName,
        PhoneNumber = @PhoneNumber,
        Email = @Email,
        HashedPassword = COALESCE(@HashedPassword, HashedPassword)  
    WHERE Id = @Id AND Deleted = 0;

    SELECT Id, FirstName, LastName, PhoneNumber, Email
    FROM Users
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_UpdateUserRoleByEmail]    Script Date: 9/2/2024 10:49:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_UpdateUserRoleByEmail]
	@RoleID INT,
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET RoleID = @RoleID
    WHERE Email = @Email AND Deleted = 0;

END
GO
USE [master]
GO
ALTER DATABASE [ShopDB] SET  READ_WRITE 
GO
