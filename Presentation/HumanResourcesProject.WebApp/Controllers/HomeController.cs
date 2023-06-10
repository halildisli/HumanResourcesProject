using AutoMapper;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Application.Methods;
using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesProject.WebApp.Controllers
{
	public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<PersonRole> _roleManager;
        private readonly SignInManager<Person> _signInManager;
        public HomeController(UserManager<Person> userManager, RoleManager<PersonRole> roleManager, SignInManager<Person> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignIn(SignInDto signInDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Person user = null;
            if (signInDto.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signInDto.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signInDto.UserNameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username-Email or password wrong!");
                return View();
            }
            if (user.EmailConfirmed!=true)
            {
				ModelState.AddModelError(string.Empty, "Your email address has not been confirmed yet! Please check the link sent to your e-mail.");
				return View();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, signInDto.Password, signInDto.RememberMe, true);
            if (signInResult.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("manager"))
                {
                    return RedirectToAction("HomePage","Home",new {area="Manager"});
                }
                else if (roles.Contains("employee"))
                {
                return RedirectToAction("HomePage", "Home",new {area="Personnel"});
                }
                else if (roles.Contains("admin"))
                {
                    return RedirectToAction("HomePage", "Admin", new { area = "Admin" });
                }
                return View(signInDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username-Email or password wrong!");
                return View();
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.Id = user.Id;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            // Kullanıcının kimliğini doğrula
            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(user.Id);
            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Username-Email or password wrong!");
                return View(changePasswordDto);
            }
            var tempPassword = Methods.GenerateTempPassword(employee);
            // şifre kontrolleri yap 
            if (tempPassword == changePasswordDto.NewPassword)
            {
                ModelState.AddModelError(string.Empty, "The new password cannot be the same as the old password!");
                return View(changePasswordDto);
            }

            var result = await _userManager.ChangePasswordAsync(user, tempPassword, changePasswordDto.NewPassword);

            // Şifre değiştirme işlemi başarılıysa kullanıcıya bir bildirim göster

            if (result.Succeeded)
            {

                user.EmailConfirmed = true;
                user.Status = Status.Active;
                employee.Status = Status.Active;
                await _userManager.UpdateAsync(user);
                TempData["SuccessMessage"] = "Password has been changed succesfully.";
                return RedirectToAction("SignIn");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username-Email or password wrong!");
                return View();
            }
        }
    }
}