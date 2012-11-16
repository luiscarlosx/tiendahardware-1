using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MVCVenta.Models;
using MVCVenta.ViewModels;
namespace MVCVenta.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public readonly VentaDataClassesDataContext _data;

        public AccountController(VentaDataClassesDataContext data)
        {
            _data = data;
        }

        public AccountController()
        {
            _data = new VentaDataClassesDataContext();
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                  List<Usuario> listaUsuarios = null;
                  var usuarios = (from u in _data.TB_Usuarios
                                  join c in _data.TB_Clientes
                                  on u.Fk_eCliente equals c.Pk_eCliente
                                  where u.cUsuario == model.UserName && u.cContrasena== model.Password
                                  select new
                                  {
                                      u.Pk_eUsuario,
                                      u.cUsuario,
                                      u.cContrasena
                                     
                                  }).ToList();

                  listaUsuarios = usuarios.ConvertAll(o => new Usuario(o.Pk_eUsuario, o.cUsuario, o.cContrasena));

                  if (listaUsuarios.Count>0)
                  {
                      FormsService.SignIn(model.UserName, model.RememberMe);
                      if (!String.IsNullOrEmpty(returnUrl))
                      {
                          return Redirect(returnUrl);
                      }
                      else
                      {
                          return RedirectToAction("RealizarCompra", "CarritoCompras");
                      }
                  }
                  else
                  {
                      ModelState.AddModelError("", "El nombre de usuario o clave ingresados no son correctos.");
                  }


                //if (MembershipService.ValidateUser(model.UserName, model.Password))
                //{
                //    FormsService.SignIn(model.UserName, model.RememberMe);
                //    if (!String.IsNullOrEmpty(returnUrl))
                //    {
                //        return Redirect(returnUrl);
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
