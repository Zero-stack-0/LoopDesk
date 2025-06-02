

using System.Security.Cryptography;
using System.Text;
using Data.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Service.Dto.User;
using Service.Helper;
using Service.Interface;
using static Entities.Constants;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly string AES_KEY;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.AES_KEY = configuration["AESKEY"] ?? "";
        }

        public async Task<CommonResponse> Create(CreateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Email"));
                }

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Name"));
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Password"));
                }

                if (string.IsNullOrWhiteSpace(AES_KEY))
                {
                    return new CommonResponse().Error(StatusCodes.Status500InternalServerError, null, SOMETHING_WENT_WRONG);
                }

                var doesUserExistsByEmail = await userRepository.GetByEmail(request.Email);
                if (doesUserExistsByEmail is not null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, USER_VALIDATIONS.USER_ALREADY_EXISTS_WITH_EMAIL);
                }

                var user = new Users(request.Name, EncryptString(request.Password, AES_KEY), request.Email);

                var createdUser = await userRepository.Create(user);

                return new CommonResponse(StatusCodes.Status201Created, createdUser, "User created succuesfully");
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        public static string EncryptString(string plainText, string key)
        {
            using var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.Key = keyBytes;
            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            using var ms = new MemoryStream();
            ms.Write(iv, 0, iv.Length);
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string cipherText, string key)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.Key = keyBytes;
            var iv = new byte[16];
            Array.Copy(fullCipher, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(fullCipher, 16, fullCipher.Length - 16);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}