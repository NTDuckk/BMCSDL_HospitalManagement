using System.ComponentModel.DataAnnotations;

namespace QLBenhVien.Models
{
    public class AccountCreate
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Mật khẩu phải dài ít nhất 8 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
       ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt.")]
        public string Password { get; set; } = null!;
        [Required]
        public string MaNhanVien { get; set; } = null!;
        [Required]
        public string TypeID { get; set; } = null!;
    }
}
