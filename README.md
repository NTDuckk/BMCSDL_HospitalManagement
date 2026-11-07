# Protector_BMCSDL

This guide explains how to install and run the hospital information management
application built on .NET 8 MVC, using SQL Server and Azure Key Vault to protect
connection information.

## 1. System Requirements

- **Operating system:** Windows 10/11, Windows Server 2019/2022, Linux, or
  macOS with .NET 8 support.
- **SQL Server:** SQL Server 2019 or later (Express or Standard Edition).
- **.NET SDK:** Version 8.0 or later.
- **IDE:** Visual Studio 2022 (Community or Professional recommended) or
  Visual Studio Code.
- **Azure CLI:** Required when using Azure Key Vault to store connection
  information.
- **Web browser:** The latest version of Chrome, Edge, or Firefox.

## 2. Set Up the SQL Server Database

### Step 1: Install SQL Server and SSMS

1. Download and install [SQL Server 2019 or later](https://www.microsoft.com/sql-server/sql-server-downloads).
2. Install [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/ssms/install/install).
3. Open SSMS and connect to SQL Server.

### Step 2: Create the Databases

In SSMS, open and run the following scripts in order:

1. `CREATE_DATABASE_ACCOUNT.sql` to create the `QLBenhVien_ACCOUNT` database.
2. `QUANLIBENHVIEN.sql` to create the `QLBenhVien` database.

### Step 3: Create Users and Grant Permissions

Run `USER_SQL.sql` to create the `userBenhVien` and `userAccountBenhVien`
logins and grant them permissions for both databases.

### Step 4: Create the Certificate and Key

Run `TaoCertBS.sql` to create the `BSCert` certificate in the `QLBenhVien`
database.

### Step 5: Create Stored Procedures, Views, and Triggers

Run the scripts in the following directories according to their dependencies:

- `SQL\Scipt_HeThong`
- `SQL\Admin`
- `SQL\BacSi`
- `SQL\DieuPhoi`
- `SQL\TaiVu`

Create views before running stored procedures that use them. Example scripts
include `sp_DangNhap.sql`, `SP_View_AdminQuanLyAccount.sql`, and
`View_BenhNhanTrongNgay.sql`.

### Step 6: Import Sample Data

1. Run `AccountQLBenhVien.sql` to import sample data (accounts and permissions)
   into `QLBenhVien_ACCOUNT`.
2. Run `QLBenhVien.sql` to import sample data (departments, clinics, doctors,
   and so on) into `QLBenhVien`.

### Step 7: Verify the Databases

- Check the tables and data in `QLBenhVien` and `QLBenhVien_ACCOUNT`.
- Try signing in with `userBenhVien` and `userAccountBenhVien` to verify access.

## 3. Set Up the .NET 8 MVC Application

### Step 1: Install the .NET SDK

Download the [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0),
then verify the installation:

```bash
dotnet --version
```

### Step 2: Prepare the Source Code

Extract or open the source code in Visual Studio 2022 or Visual Studio Code.
Open a terminal in the project directory and restore the NuGet packages:

```bash
dotnet restore
```

### Step 3: Configure Azure Key Vault

Azure Key Vault is used to securely store database connection information.

1. Install the [Azure CLI on Windows](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows)
   or see the [installation guide for other operating systems](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli).
2. Sign in with an Azure account that has access to the Key Vault:

   ```bash
   az login
   ```

3. Make sure the account has been granted access to the Key Vault.
4. In the `qlbenhvien-vault` Key Vault, create secrets matching the application
   configuration: SQL server name, username, password, database name, and
   certificate name for `QLBenhVien` and `QLBenhVien_ACCOUNT`.
5. Open `QLBenhVien\QLBenhVien\appsettings.json` and configure the Key Vault URL:

   ```json
   {
     "KeyVault": {
       "VaultUrl": "https://qlbenhvien-vault.vault.azure.net/"
     }
   }
   ```

Do not add real passwords or secrets to the source code or commit them to the
repository.

If Azure Key Vault is not used in a development environment, you can configure
the connection strings directly in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "QLBenhVien": "Server=your_server;Database=QLBenhVien;User ID=userBenhVien;Password=your_password;TrustServerCertificate=True;",
    "QLBenhVien_ACCOUNT": "Server=your_server;Database=QLBenhVien_ACCOUNT;User ID=userAccountBenhVien;Password=your_password;TrustServerCertificate=True;"
  }
}
```

### Step 4: Verify the Application Configuration

Open `Program.cs` and make sure the following services are configured correctly:

- `KeyVaultService`
- `DynamicConnectionProvider`
- `CookieAuthentication`

### Step 5: Build and Run the Application

```bash
dotnet build
dotnet run
```

Open a browser and navigate to the address shown in the terminal (usually
`https://localhost:5195`).

## 4. Sample Accounts

| Role | Username | Password |
|---|---|---|
| Administrator | `admin1` | `123` |
| Doctor | `bs1` | `123` |
| Coordinator | `nvdp1` | `123` |
| Finance staff | `nvtv1` | `123` |

These credentials are for sample data only and must not be used in production.
