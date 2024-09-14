use users
-- Assign the 'Admin' role to a user with UserId = 1
INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'adminUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'Admin'));

-- Assign the 'User' role to a regular user
INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'regularUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'User'));



CREATE TABLE AsginRoles (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId),
   
);
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE EmailConfirmations (
    ConfirmationId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    Token NVARCHAR(256),
    ExpiryDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE PasswordResets (
    ResetId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    Token NVARCHAR(256),
    ExpiryDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
INSERT INTO Roles (RoleName) VALUES ('Admin');
INSERT INTO Roles (RoleName) VALUES ('User');
-- Assign the 'Admin' role to a user
INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'adminUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'Admin'));

-- Assign the 'User' role to another user
INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'regularUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'User'));

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    UserEmail NVARCHAR(50) NOT NULL,
    PassWord NVARCHAR(256) NOT NULL
);

CREATE TABLE UserRoles (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
CREATE TABLE UserRoles (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

ALTER TABLE AsginRoles
ADD CONSTRAINT FK_AsginRoles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId);



INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'adminUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'Admin'));

-- Assign the 'User' role to another user
INSERT INTO UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM Users WHERE UserName = 'regularUser'), (SELECT RoleId FROM Roles WHERE RoleName = 'User'));

SELECT * FROM Dept

SELECT 
    Dept.DeptID, 
    COUNT(Emp.EmpID) AS [No. of Emp], 
    SUM(Emp.EmpSal) AS Salaries
FROM 
    Dept
LEFT JOIN 
    Emp ON Dept.DeptID = Emp.DeptID
GROUP BY 
    Dept.DeptID;

	SELECT 
    Dept.DeptID, 
    COUNT(Emp.EmpID) AS [No. of Emp], 
    SUM(Emp.EmpSal) AS Salaries
FROM 
    Dept
LEFT JOIN 
    Emp ON Dept.DeptID = Emp.DeptID
GROUP BY 
    Dept.DeptID
HAVING 
    COUNT(Emp.EmpID) > 1;


	SELECT 
    EmpID, 
    EmpName, 
    EmpSal, 
    HireDate, 
    DeptID
FROM 
    Emp
WHERE 
    DATEDIFF(YEAR, HireDate, GETDATE()) > 10;


SELECT 
    Emp.EmpID, 
    Emp.EmpName, 
    Emp.EmpSal, 
    Emp.HireDate, 
    Dept.DeptName
FROM 
    Emp
JOIN 
    Dept ON Emp.DeptID = Dept.DeptID;


	SELECT 
    Dept.DeptID, 
    Dept.DeptName, 
    MAX(Emp.EmpSal) AS HighestSalary
FROM 
    Emp
JOIN 
    Dept ON Emp.DeptID = Dept.DeptID
GROUP BY 
    Dept.DeptID, Dept.DeptName;
