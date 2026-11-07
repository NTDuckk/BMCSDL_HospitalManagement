using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLBenhVien.Services
{
    public class DynamicConnectionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly KeyVaultService _keyVaultService;

        public DynamicConnectionProvider(IHttpContextAccessor httpContextAccessor, KeyVaultService keyVaultService)
        {
            _httpContextAccessor = httpContextAccessor;
            _keyVaultService = keyVaultService;
        }

        /// 
        ///  QLBenhVien
        /// 
        public async Task<string> GetDataConnectionStringAsync()
        {
            var server = await _keyVaultService.GetSecretAsync("serverName");
            var dbUser = await _keyVaultService.GetSecretAsync("DbUser-BV");
            var dbPass = await _keyVaultService.GetSecretAsync("DbPass-BV");
            var dbName = await _keyVaultService.GetSecretAsync("databaseData");

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(dbUser) ||
                string.IsNullOrEmpty(dbPass) || string.IsNullOrEmpty(dbName))
            {
                throw new Exception("Thông tin kết nối dữ liệu không hợp lệ.");
            }
 
            return $"Server={server};Database={dbName};User ID={dbUser};Password={dbPass};TrustServerCertificate=True;";
        }


        /// 
        /// QLBenhVien_ACCOUNT
        /// 
        public async Task<string> GetAccountConnectionStringAsync()
        {
            var server = await _keyVaultService.GetSecretAsync("serverName");
            var dbUser = await _keyVaultService.GetSecretAsync("DbUser-AccountBV");
            var dbPass = await _keyVaultService.GetSecretAsync("DbPass-AccountBV");
            var dbName = await _keyVaultService.GetSecretAsync("databaseAccount");

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(dbUser) ||
                string.IsNullOrEmpty(dbPass) || string.IsNullOrEmpty(dbName))
            {
                throw new Exception("Thông tin kết nối tài khoản không hợp lệ.");
            }

            return $"Server={server};Database={dbName};User ID={dbUser};Password={dbPass};TrustServerCertificate=True;";
        }
    }
}
