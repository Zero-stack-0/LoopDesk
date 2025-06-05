

using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Data.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Dto.User;
using Service.Helper;
using Service.Interface;
using static Entities.Constants;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;
        private readonly string AES_KEY;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
            this.AES_KEY = configuration["AESKEY"] ?? "";
        }

        public async Task<CommonResponse> Create(CreateRequest request)
        {
            try
            {
                var erroListing = new List<(bool Condition, string ErrorMessage)>
                {
                    (string.IsNullOrWhiteSpace(request.Email), string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, Keys.EMAIL)),
                    (string.IsNullOrWhiteSpace(request.Name), string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, Keys.NAME)),
                    (string.IsNullOrWhiteSpace(request.Password), string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, Keys.PASSWORD))
                };

                foreach (var (condition, errorMessage) in erroListing)
                {
                    if (condition)
                    {
                        return new CommonResponse(StatusCodes.Status400BadRequest, null, errorMessage);
                    }
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

                var roleByName = await roleRepository.GetByName(ROLE.COMPANY_ADMIN);
                if (roleByName is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, USER_VALIDATIONS.ROLE_NOT_FOUND);
                }

                var user = new Users(request.Name, EncryptString(request.Password, AES_KEY), request.Email, roleByName.Id);

                var createdUser = await userRepository.Create(user);

                return new CommonResponse(StatusCodes.Status201Created, mapper.Map<UserResponse>(createdUser), USER_VALIDATIONS.USER_CREATED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        public async Task<CommonResponse> Login(LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Email"));
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Password"));
                }

                if (string.IsNullOrWhiteSpace(AES_KEY))
                {
                    return new CommonResponse().Error(StatusCodes.Status500InternalServerError, null, SOMETHING_WENT_WRONG);
                }

                var user = await userRepository.GetByEmailAndPassword(request.Email, EncryptString(request.Password, AES_KEY));
                if (user is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, USER_VALIDATIONS.USER_NOT_FOUND);
                }

                await userRepository.Update(user);

                return new CommonResponse(StatusCodes.Status200OK, mapper.Map<UserResponse>(user), "User logged in successfully");
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        public async Task<CommonResponse> GetByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, string.Format(USER_VALIDATIONS.CANNOT_BE_EMPTY, "Email"));
                }

                var user = await userRepository.GetByEmail(email);
                if (user is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, USER_VALIDATIONS.USER_NOT_FOUND);
                }
                return new CommonResponse(StatusCodes.Status200OK, mapper.Map<UserResponse>(user), "User found successfully");
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
            aes.IV = new byte[16]; // All zeros (not secure)
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}