﻿using System;
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
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Display(Name = "确认新密码")]
        public string NewPasswordConfirm { get; set; }
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
        public user_info GetUserInfo(string userName)
        {
            return db.user_info.FirstOrDefault(r => r.ui_name == userName || r.ui_email == userName);
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

        public void Save()
        {
            db.SaveChanges();
        }
    }

}