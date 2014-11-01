using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DBEntity;
using System.Web.Security;
using System.Web;

namespace MeetingCanlendar.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="账号不能为空")]
        [Display(Name = "账号")]
        public string UserName { get; set; }

        [Required(ErrorMessage="密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "保持登录状态")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "账号不能为空")]
        [Display(Name = "账号")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage="邮箱不能为空")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "验证码")]
        public string ValidateCode { get; set; }
    }

    public class ChangePassword
    {
        public int UserId { get; set; }

        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Display(Name = "确认密码")]
        public string NewPasswordConfirm { get; set; }

        [Display(Name = "是否可用")]
        public bool Status { get; set; }

    }

    public class FindPassword
    {
        [Display(Name = "邮箱")]
        public string UserEmail { get; set; }

        [Display(Name = "验证码")]
        public string ValidateCode { get; set; }
    }


    public class UserModel : MySqlDBModel
    {
        public IQueryable<user_info_detail> GetUserInfoDetails()
        {
            return db.user_info_detail;
        }

        public IQueryable<user_info> GetUserInfos()
        {
            return db.user_info;
        }

        public IQueryable<user_info> GetUserInfos(bool status)
        {
            return db.user_info.Where(r => r.ui_status == status);
        }

        public IQueryable<user_info_detail> GetAvaliableUserInfos()
        {
            return db.user_info_detail.Where(r => r.ui_status);
        }

        public static int GetUsersCount(bool showAll = true)
        {
            if(showAll)
            {
                return staticDb.user_info.Count();
            }
            else
            {
                return staticDb.user_info.Count(r => r.ui_status);
            }
        }

        public user_info GetUserInfo(string userName)
        {
            return db.user_info.FirstOrDefault(r => r.ui_name == userName || r.ui_email == userName);
        }

        public user_info GetUserInfo(int userId)
        {
            return db.user_info.FirstOrDefault(r => r.id == userId);
        }

        public user_info_detail GetUserInfoDetail(string userName)
        {
            return db.user_info_detail.FirstOrDefault(r => r.ui_name == userName);
        }

        public user_info CheckUserName(string userName)
        {
            return db.user_info.FirstOrDefault(r => r.ui_name == userName);
        }

        public user_info CheckUserEmail(string email)
        {
            return db.user_info.FirstOrDefault(r => r.ui_email == email);
        }

        public user_grade_catg GetUserGrade(int userId)
        {
            return db.user_info.FirstOrDefault(r => r.id == userId).user_grade_catg;
        }

        public user_grade_catg GetUserGrade(string userName)
        {
            return db.user_info.FirstOrDefault(r => r.ui_name == userName).user_grade_catg;
        }

        public void SignIn(user_info user, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(user.ui_name, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public user_info ValidateUser(string userName, string password)
        {
            string pwd = PasswordEncrypt(password);
            return db.user_info.FirstOrDefault(r => (r.ui_name == userName || r.ui_email == userName) && r.ui_password == pwd);
        }

        public string PasswordEncrypt(string str)
        {
            //先SHA1编码再MD5编码
            string sha = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sha, "MD5");
        }

        public void Add(user_info userInfo)
        {
            db.user_info.AddObject(userInfo);
            Save();
        }

        public int UpdateUserStatus(string ids, short status)
        {
            return db.ExecuteStoreCommand("Update user_info Set ui_status = " + status + " Where id In(" + ids + ")");
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }

}