using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter email")]
        [Display(Name = "Email")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
        
        public void DecryptAndModifyPassword()
        {
            string[] AsciiPassword = Password.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            string decodedPassword = "";
            foreach (string asciikey in AsciiPassword)
            {
                decodedPassword += (char)(int.Parse(asciikey));
            }
            Password = decodedPassword;
        }
    }
}
