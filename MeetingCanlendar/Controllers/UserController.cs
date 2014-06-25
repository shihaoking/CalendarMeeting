using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Tools;
using DBEntity;
using System.Collections;
using System.Drawing;
using System.IO;
using MeetingCanlendar.Models;

namespace MeetingCanlendar.Controllers
{

    public class UserController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            UserModel userModel = new UserModel();
            user_info_detail userInfo = userModel.GetUserInfoDetail(User.Identity.Name);
            ViewBag.PageName = "home";
            return View(userInfo);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerInfo, string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("index", "meeting");

            if (ModelState.IsValid)
            {
                if (Session["ValidateCode"] == null || registerInfo.ValidateCode.ToLower() != Session["ValidateCode"].ToString().ToLower())
                {
                    ModelState.AddModelError("ValidateCode", "验证码错误");
                    return View(registerInfo);
                }

                UserModel userModel = new UserModel();
                user_info userInfo = new user_info();

                userInfo.ui_name = registerInfo.UserName;
                userInfo.ui_password = userModel.PasswordEncrypt(registerInfo.Password);
                userInfo.ui_email = registerInfo.Email;
                userInfo.ui_grade_id = 1;
                userInfo.ui_create_time = DateTime.Now;
                userInfo.ui_status = "A";

                userModel.Add(userInfo);

                //userModel.SignIn(userInfo, false);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Meeting");
        }

        [MCAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [MCAuthorize]
        public ActionResult ChangePassword(ChangePassword pwdForm)
        {
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);
            
            userInfo.ui_password = userModel.PasswordEncrypt(pwdForm.NewPassword);
            userModel.Save();

            userModel.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult FindPassword(int step = 1)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index");

            if (step == 2)
                return View("FindPasswordStep2");

            return View();
        }

        [HttpPost]
        public ActionResult FindPassword(FindPassword formValues)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index");

            if (ModelState.IsValid)
            {
                var emailReg = new System.Text.RegularExpressions.Regex("^(\\w)+(\\.\\w+)*@[\\w\\d]+((\\.\\w+)+)$");
                if (formValues.UserEmail == null || !emailReg.IsMatch(formValues.UserEmail))
                {
                    ModelState.AddModelError("UserEmail", "邮箱格式不正确");
                    return View(formValues);
                }
                else if (Session["ValidateCode"] == null
                   || formValues.ValidateCode == null
                   || formValues.ValidateCode.ToLower() != Session["ValidateCode"].ToString().ToLower())
                {
                    ModelState.AddModelError("ValidateCode", "验证码错误");
                    return View(formValues);
                }

                UserModel userModel = new UserModel();
                user_info userInfo = userModel.GetUserInfo(formValues.UserEmail);
                string newPwd = ImageValidate.CreateCode(6);//生生6位数的随机密码
                userInfo.ui_password = userModel.PasswordEncrypt(newPwd);
                userModel.Save();
                //发送新密码
                MeetingCanlendar.Tools.SendEmail.ResetPassword(formValues.UserEmail, newPwd);
            }

            return RedirectToAction("FindPassword", new { step = 2 });

        }

        public ActionResult Login()
        {
            //如果用户已经被验证,但是进入到某些页面后由于权限不足,被系统引导到登陆界面.则强制将用户引导到主页面
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Meeting");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index");

            UserModel accountModel = new UserModel();
            if (ModelState.IsValid)
            {
                var userInfo = accountModel.ValidateUser(model.UserName, model.Password);
                if (userInfo != null)
                {
                    //设置验证用户的cookie信息
                    accountModel.SignIn(userInfo, model.RememberMe);
                    
                    if (Url.IsLocalUrl(returnUrl))
                        return Json(new { key = 1, value = returnUrl }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { key = 1, value = "/" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { key = 0, value = "账号或密码错误" }, JsonRequestBehavior.AllowGet);
                }
            }

            return View(model);
        }

        public ActionResult LogOut(string returnUrl)
        {
            UserModel accountModel = new UserModel();
            accountModel.SignOut();

            if (!string.IsNullOrWhiteSpace(returnUrl) && !returnUrl.ToLower().Contains("/user"))
                return Redirect(returnUrl);

            return Redirect("/");;
        }


        [HttpPost]
        public ActionResult CheckUserInfo(string type, string value)
        {
            UserModel userModel = new UserModel();
            int returnKey = 0;
            string returnValue = "";

            if (type == "name" && userModel.CheckUserName(value) != null)
                returnValue = "用户名已存在";
            else if (type == "email" && userModel.CheckUserEmail(value) != null)
                returnValue = "邮箱已存在";
            else
                returnKey = 1;

            return Json(new { key = returnKey, value = returnValue }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MCAuthorize]
        public ActionResult ChangeUserInfo(bool gender, string birthDay)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Json(new { key = 0, value = "用户不合法！" }, JsonRequestBehavior.AllowGet);

                UserModel userModel = new UserModel();
                var userInfo = userModel.GetUserInfo(User.Identity.Name);
                userInfo.ui_gender = gender;
                userModel.Save();
                return Json(new { key = 1, value = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { key = 0, value = "网站内部错误!请刷新页面重试。" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult GetImageValidate()
        {
            string validateStr = ImageValidate.CreateCode(4);
            Session["ValidateCode"] = validateStr;

            Response.ContentType = "image/jpeg";
            return new FileStreamResult(ImageValidate.CreateImage(validateStr), Response.ContentType);
        }
    }
}
