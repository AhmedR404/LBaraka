-- =====================================================
-- AlBaraka POS - SQL Server Database Script
-- Full Schema + Seed Data
-- =====================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'AlBarakaDB')
    DROP DATABASE AlBarakaDB;
GO

CREATE DATABASE AlBarakaDB
    COLLATE Arabic_CI_AS;
GO

USE AlBarakaDB;
GO

-- =====================================================
-- 1. BRANCHES
-- =====================================================
CREATE TABLE Branches (
    BranchId    INT IDENTITY(1,1) PRIMARY KEY,
    BranchName  NVARCHAR(100) NOT NULL,
    Address     NVARCHAR(255),
    Phone       NVARCHAR(30),
    IsActive    BIT NOT NULL DEFAULT 1,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 2. ROLES
-- =====================================================
CREATE TABLE Roles (
    RoleId      INT IDENTITY(1,1) PRIMARY KEY,
    RoleName    NVARCHAR(50) NOT NULL UNIQUE,   -- Admin / Manager / Cashier / Agent
    Description NVARCHAR(255)
);
GO

INSERT INTO Roles (RoleName, Description) VALUES
    (N'Admin',   N'مدير النظام - صلاحيات كاملة'),
    (N'Manager', N'مدير الفرع - يرى تقارير الفرع'),
    (N'Cashier', N'كاشير - نقطة البيع فقط'),
    (N'Agent',   N'مندوب - دليفاري وعملاء');
GO

-- =====================================================
-- 3. USERS
-- =====================================================
CREATE TABLE Users (
    UserId          INT IDENTITY(1,1) PRIMARY KEY,
    BranchId        INT NOT NULL REFERENCES Branches(BranchId),
    RoleId          INT NOT NULL REFERENCES Roles(RoleId),
    FullName        NVARCHAR(100) NOT NULL,
    Username        NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash    NVARCHAR(256) NOT NULL,   -- BCrypt hash
    PasswordSalt    NVARCHAR(128) NOT NULL,
    Email           NVARCHAR(150),
    Phone           NVARCHAR(30),
    IsActive        BIT NOT NULL DEFAULT 1,
    MustChangePass  BIT NOT NULL DEFAULT 0,
    LastLoginAt     DATETIME2,
    CreatedAt       DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy       INT REFERENCES Users(UserId)
);
GO

-- =====================================================
-- 4. PASSWORD RESET TOKENS
-- =====================================================
CREATE TABLE PasswordResetTokens (
    TokenId     INT IDENTITY(1,1) PRIMARY KEY,
    UserId      INT NOT NULL REFERENCES Users(UserId),
    Token       NVARCHAR(128) NOT NULL UNIQUE,
    ExpiresAt   DATETIME2 NOT NULL,
    UsedAt      DATETIME2,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 5. LOGIN AUDIT LOG
-- =====================================================
CREATE TABLE LoginAuditLog (
    LogId       INT IDENTITY(1,1) PRIMARY KEY,
    UserId      INT REFERENCES Users(UserId),
    Username    NVARCHAR(50) NOT NULL,
    BranchId    INT REFERENCES Branches(BranchId),
    Success     BIT NOT NULL,
    FailReason  NVARCHAR(100),
    IpAddress   NVARCHAR(45),
    MachineName NVARCHAR(100),
    AttemptedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 6. SHIFTS
-- =====================================================
CREATE TABLE Shifts (
    ShiftId         INT IDENTITY(1,1) PRIMARY KEY,
    BranchId        INT NOT NULL REFERENCES Branches(BranchId),
    UserId          INT NOT NULL REFERENCES Users(UserId),   -- Cashier who opened
    StartTime       DATETIME2 NOT NULL DEFAULT GETDATE(),
    EndTime         DATETIME2,
    OpeningCash     DECIMAL(12,2) NOT NULL DEFAULT 0,
    ClosingCash     DECIMAL(12,2),
    Notes           NVARCHAR(500),
    IsOpen          BIT NOT NULL DEFAULT 1
);
GO

-- =====================================================
-- 7. CATEGORIES
-- =====================================================
CREATE TABLE Categories (
    CategoryId   INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    SortOrder    INT NOT NULL DEFAULT 0,
    IsActive     BIT NOT NULL DEFAULT 1
);
GO

INSERT INTO Categories (CategoryName, SortOrder) VALUES
    (N'الكل',          0),
    (N'كيدة',          1),
    (N'مخ',            2),
    (N'جمبري',         3),
    (N'سمك فليه',      4),
    (N'فراخ بانيه',    5),
    (N'مشكل',          6),
    (N'مراع مشوية',    7),
    (N'محاشي',         8),
    (N'أطباق',         9),
    (N'إضافات',       10);
GO

-- =====================================================
-- 8. PRODUCTS
-- =====================================================
CREATE TABLE Products (
    ProductId    INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId   INT NOT NULL REFERENCES Categories(CategoryId),
    ProductName  NVARCHAR(150) NOT NULL,
    Price        DECIMAL(12,2) NOT NULL,
    BranchId     INT REFERENCES Branches(BranchId),  -- NULL = all branches
    IsActive     BIT NOT NULL DEFAULT 1,
    SortOrder    INT NOT NULL DEFAULT 0,
    CreatedAt    DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Seed products from the screenshot
INSERT INTO Products (CategoryId, ProductName, Price, SortOrder) VALUES
-- كيدة
(2, N'كيلو كيدة',    300.00, 1),
(2, N'3/4 كيدة',    230.00, 2),
(2, N'1/2 كيدة',    160.00, 3),
(2, N'1/4 كيدة',     90.00, 4),
(2, N'1/8 كيدة',     50.00, 5),
-- مخ
(3, N'كيلو مخ',      500.00, 1),
(3, N'3/4 مخ',       330.00, 2),
(3, N'1/2 مخ',       260.00, 3),
(3, N'1/4 مخ',       100.00, 4),
(3, N'1/8 مخ',        60.00, 5),
-- جمبري
(4, N'كيلو جمبري',   500.00, 1),
(4, N'3/4 جمبري',    330.00, 2),
(4, N'1/2 جمبري',    260.00, 3),
(4, N'1/4 جمبري',    100.00, 4),
(4, N'1/8 جمبري',     60.00, 5),
-- سمك فليه
(5, N'كيلو سمك فليه',400.00, 1),
(5, N'3/4 سمك فليه', 310.00, 2),
(5, N'1/2 سمك فليه', 210.00, 3),
(5, N'1/4 سمك فليه', 110.00, 4),
(5, N'1/8 سمك فليه',  60.00, 5),
-- فراخ بانيه
(6, N'كيلو فراخ بانيه',400.00, 1),
(6, N'3/4 فراخ بانيه',310.00, 2),
(6, N'1/2 فراخ بانيه',210.00, 3),
(6, N'1/4 فراخ بانيه',110.00, 4),
(6, N'1/8 فراخ بانيه', 60.00, 5),
-- مشكل
(7, N'كيلو مشكل (كيدة-مخ-جمبري)',       500.00, 1),
(7, N'3/4 مشكل (كيدة-مخ-جمبري)',        410.00, 2),
(7, N'1/2 مشكل (كيدة-مخ-جمبري)',        260.00, 3),
(7, N'1/4 مشكل (كيدة-مخ-جمبري)',        140.00, 4),
(7, N'1/8 مشكل (كيدة-مخ-جمبري)',         70.00, 5),
(7, N'كيلو مشكل (كيدة-بانيه-فليه)',      400.00, 6),
(7, N'3/4 مشكل (كيدة-بانيه-فليه)',       310.00, 7),
(7, N'1/2 مشكل (كيدة-بانيه-فليه)',       210.00, 8),
(7, N'1/4 مشكل (كيدة-بانيه-فليه)',       110.00, 9),
(7, N'1/8 مشكل (كيدة-بانيه-فليه)',        60.00, 10),
-- مراع مشوية
(8, N'فرخة مشوية',        500.00, 1),
(8, N'3/4 فرخة مشوية',    400.00, 2),
(8, N'1/2 فرخة مشوية',    300.00, 3),
(8, N'1/4 فرخة مشوية',    200.00, 4),
-- محاشي
(9, N'محشي مميار',        70.00, 1),
(9, N'محشي ورق عنب',      70.00, 2),
(9, N'محشي بادنجان',      70.00, 3),
(9, N'محشي كرنب',         70.00, 4),
-- إضافات
(10, N'أرز جمبري صغير',   75.00, 1);
GO

-- =====================================================
-- 9. CUSTOMERS
-- =====================================================
CREATE TABLE Customers (
    CustomerId   INT IDENTITY(1,1) PRIMARY KEY,
    FullName     NVARCHAR(100) NOT NULL,
    Phone        NVARCHAR(30),
    Address      NVARCHAR(255),
    Notes        NVARCHAR(500),
    BranchId     INT REFERENCES Branches(BranchId),
    CreatedAt    DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 10. ORDERS
-- =====================================================
CREATE TABLE Orders (
    OrderId      INT IDENTITY(1,1) PRIMARY KEY,
    BranchId     INT NOT NULL REFERENCES Branches(BranchId),
    ShiftId      INT NOT NULL REFERENCES Shifts(ShiftId),
    UserId       INT NOT NULL REFERENCES Users(UserId),
    CustomerId   INT REFERENCES Customers(CustomerId),
    OrderType    NVARCHAR(20) NOT NULL CHECK (OrderType IN (N'DineIn', N'TakeAway', N'Delivery')),
    TableNumber  NVARCHAR(20),              -- for DineIn
    DeliveryAddress NVARCHAR(255),          -- for Delivery
    DeliveryAgent   INT REFERENCES Users(UserId),  -- Agent role
    Subtotal     DECIMAL(12,2) NOT NULL DEFAULT 0,
    Discount     DECIMAL(12,2) NOT NULL DEFAULT 0,
    ServiceFee   DECIMAL(12,2) NOT NULL DEFAULT 0,
    Total        DECIMAL(12,2) NOT NULL DEFAULT 0,
    PaymentMethod NVARCHAR(30) NOT NULL DEFAULT N'Cash'
                  CHECK (PaymentMethod IN (N'Cash', N'Card', N'Instapay', N'Vodafone')),
    Status       NVARCHAR(20) NOT NULL DEFAULT N'Pending'
                  CHECK (Status IN (N'Pending', N'Confirmed', N'Cancelled')),
    Notes        NVARCHAR(500),
    CreatedAt    DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 11. ORDER ITEMS
-- =====================================================
CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId     INT NOT NULL REFERENCES Orders(OrderId) ON DELETE CASCADE,
    ProductId   INT NOT NULL REFERENCES Products(ProductId),
    ProductName NVARCHAR(150) NOT NULL,   -- snapshot at order time
    UnitPrice   DECIMAL(12,2) NOT NULL,
    Quantity    INT NOT NULL DEFAULT 1,
    LineTotal   AS (UnitPrice * Quantity) PERSISTED
);
GO

-- =====================================================
-- 12. DAILY EXPENSES
-- =====================================================
CREATE TABLE DailyExpenses (
    ExpenseId    INT IDENTITY(1,1) PRIMARY KEY,
    BranchId     INT NOT NULL REFERENCES Branches(BranchId),
    ShiftId      INT NOT NULL REFERENCES Shifts(ShiftId),
    UserId       INT NOT NULL REFERENCES Users(UserId),
    Description  NVARCHAR(255) NOT NULL,
    Amount       DECIMAL(12,2) NOT NULL,
    ExpenseDate  DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    CreatedAt    DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 13. STORED PROCEDURES
-- =====================================================

-- sp_Login: validate credentials, log attempt, return user info
CREATE OR ALTER PROCEDURE sp_Login
    @Username   NVARCHAR(50),
    @MachineName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.UserId, u.FullName, u.Username,
        u.PasswordHash, u.PasswordSalt,
        u.IsActive, u.MustChangePass,
        u.BranchId, b.BranchName,
        u.RoleId, r.RoleName
    FROM Users u
    JOIN Branches b ON b.BranchId = u.BranchId
    JOIN Roles    r ON r.RoleId   = u.RoleId
    WHERE u.Username = @Username;
END
GO

-- sp_LogLoginAttempt
CREATE OR ALTER PROCEDURE sp_LogLoginAttempt
    @UserId     INT = NULL,
    @Username   NVARCHAR(50),
    @BranchId   INT = NULL,
    @Success    BIT,
    @FailReason NVARCHAR(100) = NULL,
    @MachineName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LoginAuditLog (UserId, Username, BranchId, Success, FailReason, MachineName)
    VALUES (@UserId, @Username, @BranchId, @Success, @FailReason, @MachineName);

    IF @Success = 1 AND @UserId IS NOT NULL
        UPDATE Users SET LastLoginAt = GETDATE() WHERE UserId = @UserId;
END
GO

-- sp_ChangePassword
CREATE OR ALTER PROCEDURE sp_ChangePassword
    @UserId      INT,
    @NewHash     NVARCHAR(256),
    @NewSalt     NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET PasswordHash   = @NewHash,
        PasswordSalt   = @NewSalt,
        MustChangePass = 0
    WHERE UserId = @UserId;
END
GO

-- sp_CreateResetToken
CREATE OR ALTER PROCEDURE sp_CreateResetToken
    @UserId  INT,
    @Token   NVARCHAR(128),
    @Hours   INT = 2
AS
BEGIN
    SET NOCOUNT ON;
    -- Invalidate old tokens
    UPDATE PasswordResetTokens SET UsedAt = GETDATE()
    WHERE UserId = @UserId AND UsedAt IS NULL;

    INSERT INTO PasswordResetTokens (UserId, Token, ExpiresAt)
    VALUES (@UserId, @Token, DATEADD(HOUR, @Hours, GETDATE()));

    SELECT @Token AS Token;
END
GO

-- sp_ValidateResetToken
CREATE OR ALTER PROCEDURE sp_ValidateResetToken
    @Token NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TokenId, t.UserId, u.Username, u.FullName
    FROM PasswordResetTokens t
    JOIN Users u ON u.UserId = t.UserId
    WHERE t.Token = @Token
      AND t.UsedAt IS NULL
      AND t.ExpiresAt > GETDATE();
END
GO

-- sp_OpenShift
CREATE OR ALTER PROCEDURE sp_OpenShift
    @BranchId    INT,
    @UserId      INT,
    @OpeningCash DECIMAL(12,2) = 0
AS
BEGIN
    SET NOCOUNT ON;
    -- Only one open shift per branch per cashier
    IF EXISTS (SELECT 1 FROM Shifts WHERE BranchId = @BranchId AND UserId = @UserId AND IsOpen = 1)
    BEGIN
        SELECT ShiftId FROM Shifts WHERE BranchId = @BranchId AND UserId = @UserId AND IsOpen = 1;
        RETURN;
    END
    INSERT INTO Shifts (BranchId, UserId, OpeningCash)
    VALUES (@BranchId, @UserId, @OpeningCash);
    SELECT SCOPE_IDENTITY() AS ShiftId;
END
GO

-- sp_CloseShift
CREATE OR ALTER PROCEDURE sp_CloseShift
    @ShiftId     INT,
    @ClosingCash DECIMAL(12,2),
    @Notes       NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Shifts
    SET IsOpen = 0, EndTime = GETDATE(), ClosingCash = @ClosingCash, Notes = @Notes
    WHERE ShiftId = @ShiftId AND IsOpen = 1;
END
GO

-- sp_SaveOrder
CREATE OR ALTER PROCEDURE sp_SaveOrder
    @BranchId        INT,
    @ShiftId         INT,
    @UserId          INT,
    @CustomerId      INT = NULL,
    @OrderType       NVARCHAR(20),
    @TableNumber     NVARCHAR(20) = NULL,
    @DeliveryAddress NVARCHAR(255) = NULL,
    @DeliveryAgent   INT = NULL,
    @Subtotal        DECIMAL(12,2),
    @Discount        DECIMAL(12,2),
    @ServiceFee      DECIMAL(12,2),
    @Total           DECIMAL(12,2),
    @PaymentMethod   NVARCHAR(30),
    @Notes           NVARCHAR(500) = NULL,
    @ItemsXml        XML   -- <Items><Item ProductId="" ProductName="" UnitPrice="" Quantity=""/></Items>
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Orders (BranchId, ShiftId, UserId, CustomerId, OrderType,
                            TableNumber, DeliveryAddress, DeliveryAgent,
                            Subtotal, Discount, ServiceFee, Total,
                            PaymentMethod, Status, Notes)
        VALUES (@BranchId, @ShiftId, @UserId, @CustomerId, @OrderType,
                @TableNumber, @DeliveryAddress, @DeliveryAgent,
                @Subtotal, @Discount, @ServiceFee, @Total,
                @PaymentMethod, N'Confirmed', @Notes);

        DECLARE @OrderId INT = SCOPE_IDENTITY();

        INSERT INTO OrderItems (OrderId, ProductId, ProductName, UnitPrice, Quantity)
        SELECT 
            @OrderId,
            x.Item.value('@ProductId',   'INT'),
            x.Item.value('@ProductName', 'NVARCHAR(150)'),
            x.Item.value('@UnitPrice',   'DECIMAL(12,2)'),
            x.Item.value('@Quantity',    'INT')
        FROM @ItemsXml.nodes('/Items/Item') AS x(Item);

        COMMIT;
        SELECT @OrderId AS OrderId;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END
GO

-- sp_GetProducts (for loading product buttons)
CREATE OR ALTER PROCEDURE sp_GetProducts
    @CategoryId INT = NULL,
    @BranchId   INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.ProductId, p.ProductName, p.Price, p.CategoryId, c.CategoryName, p.SortOrder
    FROM Products p
    JOIN Categories c ON c.CategoryId = p.CategoryId
    WHERE p.IsActive = 1
      AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
      AND (p.BranchId IS NULL OR p.BranchId = @BranchId)
    ORDER BY p.CategoryId, p.SortOrder;
END
GO

-- sp_GetDailySummary (for reports)
CREATE OR ALTER PROCEDURE sp_GetDailySummary
    @BranchId INT,
    @Date     DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @Date IS NULL SET @Date = CAST(GETDATE() AS DATE);

    SELECT
        COUNT(*)                        AS TotalOrders,
        SUM(o.Total)                    AS TotalRevenue,
        SUM(o.Discount)                 AS TotalDiscounts,
        SUM(o.ServiceFee)               AS TotalService,
        SUM(CASE WHEN o.OrderType = N'DineIn'   THEN 1 ELSE 0 END) AS DineInCount,
        SUM(CASE WHEN o.OrderType = N'TakeAway' THEN 1 ELSE 0 END) AS TakeAwayCount,
        SUM(CASE WHEN o.OrderType = N'Delivery' THEN 1 ELSE 0 END) AS DeliveryCount,
        SUM(CASE WHEN o.PaymentMethod = N'Cash'      THEN o.Total ELSE 0 END) AS CashTotal,
        SUM(CASE WHEN o.PaymentMethod = N'Card'      THEN o.Total ELSE 0 END) AS CardTotal,
        SUM(CASE WHEN o.PaymentMethod = N'Instapay'  THEN o.Total ELSE 0 END) AS InstapayTotal,
        SUM(CASE WHEN o.PaymentMethod = N'Vodafone'  THEN o.Total ELSE 0 END) AS VodafoneTotal
    FROM Orders o
    WHERE o.BranchId = @BranchId
      AND CAST(o.CreatedAt AS DATE) = @Date
      AND o.Status = N'Confirmed';
END
GO

-- sp_GetUsers (Admin only)
CREATE OR ALTER PROCEDURE sp_GetUsers
    @BranchId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.UserId, u.FullName, u.Username, u.Email, u.Phone,
           u.IsActive, u.MustChangePass, u.LastLoginAt, u.CreatedAt,
           b.BranchName, r.RoleName
    FROM Users u
    JOIN Branches b ON b.BranchId = u.BranchId
    JOIN Roles    r ON r.RoleId   = u.RoleId
    WHERE (@BranchId IS NULL OR u.BranchId = @BranchId)
    ORDER BY u.CreatedAt DESC;
END
GO

-- sp_CreateUser
CREATE OR ALTER PROCEDURE sp_CreateUser
    @BranchId      INT,
    @RoleId        INT,
    @FullName      NVARCHAR(100),
    @Username      NVARCHAR(50),
    @PasswordHash  NVARCHAR(256),
    @PasswordSalt  NVARCHAR(128),
    @Email         NVARCHAR(150) = NULL,
    @Phone         NVARCHAR(30)  = NULL,
    @CreatedBy     INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
        RAISERROR(N'اسم المستخدم موجود بالفعل', 16, 1);
        RETURN;
    END
    INSERT INTO Users (BranchId, RoleId, FullName, Username, PasswordHash, PasswordSalt,
                       Email, Phone, MustChangePass, CreatedBy)
    VALUES (@BranchId, @RoleId, @FullName, @Username, @PasswordHash, @PasswordSalt,
            @Email, @Phone, 1, @CreatedBy);

    SELECT SCOPE_IDENTITY() AS UserId;
END
GO

-- =====================================================
-- 14. SEED DATA - Default Branch & Admin User
-- =====================================================

-- Default branch
INSERT INTO Branches (BranchName, Address, Phone) VALUES
    (N'الفرع الرئيسي', N'القاهرة', N'01000000000'),
    (N'فرع المعادي',   N'المعادي', N'01000000001'),
    (N'فرع مدينة نصر', N'مدينة نصر', N'01000000002');
GO

-- Admin user: username=admin / password=Admin@1234
-- Hash below is BCrypt of "Admin@1234" - will be replaced at runtime
-- Leave as placeholder; the C# code will hash on first run if using sp_CreateUser
-- For direct DB seed we store a known BCrypt hash:
-- BCrypt("Admin@1234", salt="$2a$12$AlBarakaSaltXXXXXXXXXX") - generated below as placeholder
-- In production, use the C# UserService.CreateUser() to seed properly.

-- Placeholder: app will detect no admin and prompt setup on first launch
-- OR run this after generating hash from C#:
-- EXEC sp_CreateUser 1,1,N'مدير النظام',N'admin',N'<hash>',N'<salt>',N'admin@albaraka.com',NULL,NULL

-- =====================================================
-- 15. INDEXES FOR PERFORMANCE
-- =====================================================
CREATE INDEX IX_Orders_BranchDate   ON Orders(BranchId, CreatedAt);
CREATE INDEX IX_Orders_ShiftId      ON Orders(ShiftId);
CREATE INDEX IX_Orders_Status       ON Orders(Status);
CREATE INDEX IX_OrderItems_OrderId  ON OrderItems(OrderId);
CREATE INDEX IX_Products_Category   ON Products(CategoryId, IsActive);
CREATE INDEX IX_Users_Username      ON Users(Username);
CREATE INDEX IX_Shifts_Branch       ON Shifts(BranchId, IsOpen);
GO

PRINT N'AlBarakaDB created successfully.';
GO
