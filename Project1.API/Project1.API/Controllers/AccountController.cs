using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project1.API.DAL;
using Project1.API.ViewModels;
using Project1.Data.Models;
using Project1.Common;
using Project1.API.Utility;
using System.Net.Http.Headers;

namespace Project1.API.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        DataContext context = new DataContext();

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginViewModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToUpdate = context.ApplicationUsers.Where(u => u.PhoneNumber == loginInfo.PhoneNumber).FirstOrDefault();

                if (userToUpdate != null)
                {
                    bool result = userToUpdate.Password == UserManager.GeneratePassword(userToUpdate, loginInfo.Password);

                    if (result)
                    {
                        string newAccessToken = UserManager.GenerateAccessToken(userToUpdate);

                        var accessToken = context.AccessTokens.Where(s => s.UserID == userToUpdate.UserID).FirstOrDefault();

                        if (accessToken != null)
                        {
                            accessToken.AccessToken = newAccessToken;
                            accessToken.ExpiresOn = DateTime.Now.AddDays(7);
                            accessToken.UpdatedTS = DateTime.Now;

                            context.Entry<UserAccessToken>(accessToken).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            accessToken = new UserAccessToken();
                            accessToken.AccessToken = newAccessToken;
                            accessToken.ExpiresOn = DateTime.Now.AddDays(7);
                            accessToken.UpdatedTS = DateTime.Now;
                            accessToken.UserID = userToUpdate.UserID;

                            context.AccessTokens.Add(accessToken);
                            context.SaveChanges();
                        }

                        return Ok(
                            new
                            {
                                access_token = newAccessToken,
                                token_type = "bearer"
                            }
                        );
                    }
                }

                return BadRequest("Phone Number and Password didn't match.");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateUser(ApplicationUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToSave = new ApplicationUser
                {
                    Name = user.Name,
                    Password = "_",
                    SecurityHash = string.Empty,
                    PhoneNumber = user.PhoneNumber,
                    JoinTS = DateTime.Now,
                    Active = true
                };

                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    context.ApplicationUsers.Add(userToSave);

                    context.SaveChanges();

                    userToSave.SecurityHash = UserManager.GenerateSecurityHash(userToSave, user.Password);

                    userToSave.Password = UserManager.GeneratePassword(userToSave, user.Password);

                    context.Entry<ApplicationUser>(userToSave).State = EntityState.Modified;

                    context.SaveChanges();

                    transaction.Commit();
                }

                return Ok(new ResponseModel
                {
                    Success = true,
                    Message = "User Created Successfully.",
                    Data = new { userId = userToSave.UserID, url = ConfigurationManager.AppSettings["app:ApiUrl"] + "account/" + userToSave.UserID }
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("{id:guid}")]
        [HttpGet]
        public IHttpActionResult Get([FromUri]Guid id)
        {
            ApplicationUser user = context.ApplicationUsers.Where(u => u.UserID == id).FirstOrDefault();

            if (user != null)
            {
                return Ok(new ResponseModel
                {
                    Success = true,
                    Message = string.Empty,
                    Data = user
                });
            }
            else
            {
                return NotFound();
            }
        }

        [Route("password/update/{id:guid}")]
        [HttpPut]
        public IHttpActionResult ChangePassword([FromUri]Guid id, [FromBody]ChangePasswordViewModel request)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToUpdate = context.ApplicationUsers.Where(u => u.UserID == id).FirstOrDefault();
                bool result = userToUpdate.Password == UserManager.GeneratePassword(userToUpdate, request.OldPassword);

                if (userToUpdate != null && result)
                {
                    userToUpdate.Password = UserManager.GeneratePassword(userToUpdate, request.NewPassword);

                    context.Entry<ApplicationUser>(userToUpdate).State = EntityState.Modified;
                    context.SaveChanges();

                    return Ok(new ResponseModel
                    {
                        Success = true,
                        Message = "Password updated successfully.",
                        Data = null
                    });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("GenerateConfirmationCode/{id:guid}")]
        [HttpGet]
        public IHttpActionResult GeneratePhoneNumberConfirmationCode([FromUri]Guid id)
        {
            string code = "865485";

            //SMS Sending Code here.

            return Ok(code);
        }

        [Route("phone/confirm/{id:guid}")]
        [HttpPut]
        public IHttpActionResult ConfirmPhoneNumber([FromUri]Guid id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userToUpdate = context.ApplicationUsers.Where(u => u.UserID == id).FirstOrDefault();

                if (userToUpdate != null)
                {
                    userToUpdate.IsPhoneConfirmed = true;

                    context.Entry<ApplicationUser>(userToUpdate).State = EntityState.Modified;
                    context.SaveChanges();

                    return Ok(new ResponseModel
                    {
                        Success = true,
                        Message = "Phone Number confirmed successfully.",
                        Data = null
                    });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
