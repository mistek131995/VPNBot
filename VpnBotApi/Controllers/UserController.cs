﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.ControllerService.Common;

using GetIndex = Service.ControllerService.Service.GetIndex;

using Register = Service.ControllerService.Service.User.Register;
using Activation = Service.ControllerService.Service.ActivateUser;
using AuthByLogin = Service.ControllerService.Service.User.AuthByLogin;
using AuthWithGoogle = Service.ControllerService.Service.User.AuthWithGoogle;
using CreateResetPasswordLink = Service.ControllerService.Service.User.RestorePassword.CreateResetPasswordLink;
using SetNewPassword = Service.ControllerService.Service.User.RestorePassword.SetNewPassword;
using GetSettings = Service.ControllerService.Service.User.GetSettings;
using SaveNotificationSettings = Service.ControllerService.Service.User.SaveNotificationSettings;
using SaveTelegramId = Service.ControllerService.Service.User.SaveTelegramId;

using AddChangePasswordRequest = Service.ControllerService.Service.User.ChangePassword.AddChangePasswordRequest;
using ConfirmChangePassword = Service.ControllerService.Service.User.ChangePassword.ConfirmChangePassword;

using AddChangeEmailRequest = Service.ControllerService.Service.User.ChangeEmail.AddChangeEmailRequest;
using ConfirmChangeEmail = Service.ControllerService.Service.User.ChangeEmail.ConfirmChangeEmail;

using ReferralIndex = Service.ControllerService.Service.ReferralIndex;


namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(ControllerServiceDispatcher dispatcher) : Controller
    {

        [HttpGet]
        public async Task<JsonResult> Activation(Guid guid)
        {
            var response = await dispatcher.GetService<bool, Activation.Request>(new Activation.Request()
            {
                Guid = guid
            });

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody] Register.Request request)
        {
            var response = await dispatcher.GetService<bool, Register.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetIndex()
        {
            var query = new GetIndex.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            };

            var response = await dispatcher.GetService<GetIndex.Result, GetIndex.Request>(query);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] AuthByLogin.Request request)
        {
            request.Ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var response = await dispatcher.GetService<string, AuthByLogin.Request>(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AuthWithGoogle([FromBody] AuthWithGoogle.Request request)
        {
            var response = await dispatcher.GetService<string, AuthWithGoogle.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddChangePasswordRequest([FromBody] AddChangePasswordRequest.Request command)
        {
            command.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, AddChangePasswordRequest.Request>(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ConfirmChangePassword([FromBody] ConfirmChangePassword.Request command)
        {
            var response = await dispatcher.GetService<bool, ConfirmChangePassword.Request>(command);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddChangeEmailRequest([FromBody] AddChangeEmailRequest.Request command)
        {
            command.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, AddChangeEmailRequest.Request>(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ConfirmChangeEmail([FromBody] ConfirmChangeEmail.Request command)
        {
            var response = await dispatcher.GetService<bool, ConfirmChangeEmail.Request>(command);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetReferralIndex()
        {
            var response = await dispatcher.GetService<ReferralIndex.Result, ReferralIndex.Request>(new ReferralIndex.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> CreateResetPasswordLink([FromBody] CreateResetPasswordLink.Request request)
        {
            var response = await dispatcher.GetService<bool, CreateResetPasswordLink.Request>(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SetNewPassword([FromBody] SetNewPassword.Request request)
        {
            var response = await dispatcher.GetService<bool, SetNewPassword.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetSettings()
        {
            var response = await dispatcher.GetService<GetSettings.Result, GetSettings.Request>(new GetSettings.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> SaveNotificationSettings([FromBody] SaveNotificationSettings.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, SaveNotificationSettings.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> SaveTelegramId([FromBody] SaveTelegramId.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, SaveTelegramId.Request>(request);

            return Json(response);
        }
    }
}
