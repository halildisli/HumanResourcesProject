using AutoMapper;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HumanResourcesProject.WebApp.Areas.Manager.Controllers
{
	[Area("Manager")]
    [Authorize(Roles ="manager")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly HumanResourcesProjectDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<PersonRole> _roleManager;
        private readonly SignInManager<Person> _signInManager;
		public HomeController(IWebHostEnvironment webHostEnvironment, IMapper mapper, HumanResourcesProjectDbContext context, UserManager<Person> userManager, RoleManager<PersonRole> roleManager, SignInManager<Person> signInManager, IUnitOfWork unitOfWork)
		{
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> SignOut()
		{
            await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Home", new { area = "" });
		}
		public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            string entityId = _unitOfWork.PersonReadRepository.GetWhere(p => p.FirstName == "Halil").FirstOrDefault().Id.ToString();
            var entity = await _unitOfWork.PersonReadRepository.GetByIdAspAsync(entityId);
            return Ok(entity);
        }
        public async Task<IActionResult> Detail()
        {
            string username = User.Identity.Name;
            var person = await _userManager.FindByNameAsync(username);
            ViewBag.Photo = person.ImagePath;
            ViewBag.FullName = person.FirstName + " " + person.Surname;
            return View(person);
        }
        public async Task<IActionResult> HomePage()
        {
            var entity = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = entity.ImagePath;
            ViewBag.FullName = entity.FirstName + " " + entity.Surname;
            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string username = User.Identity.Name;
            var person = await _userManager.FindByNameAsync(username);
            var entity = _mapper.Map<EditPersonDto>(person);
            ViewBag.Department = person.Department.Name;
            ViewBag.Job = person.Job.Name;
            ViewBag.PersonName = person.FirstName + " " + person.SecondName + " " + person.Surname + " " + person.SecondSurname;
            ViewBag.Photo = person.ImagePath;
            ViewBag.FullName = person.FirstName + " " + person.Surname;
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPersonDto personDto, IFormFile? photo)
        {
            Person toBeUpdated = await _unitOfWork.PersonReadRepository.GetByIdAspAsync(personDto.Id.ToString());
			ViewBag.Department = toBeUpdated.Department.Name;
			ViewBag.Job = toBeUpdated.Job.Name;
			ViewBag.Photo = toBeUpdated.ImagePath;
            ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
            if (personDto.ImagePath==null)
            {
                personDto.ImagePath = toBeUpdated.ImagePath;
            }
            if (photo!=null)
            {
				if (!PhotoControl(photo))
				{
					return View(personDto);
				}
			}
            if (ModelState.IsValid)
            {
                //var personDto = _mapper.Map<EditPersonDto>(person);
                toBeUpdated.Address = personDto.Address;
                toBeUpdated.PhoneNumber = personDto.PhoneNumber;
                if (photo != null)
                {
                    string photoPath = AddPhoto(photo);
                    toBeUpdated.ImagePath = photoPath;
                }
                _unitOfWork.PersonWriteRepository.Update(toBeUpdated);
                await _unitOfWork.PersonWriteRepository.SaveAsync();
                ViewBag.Photo = toBeUpdated.ImagePath;
                ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
                return RedirectToAction(nameof(HomePage));
            }
            //EditPersonDto personDto1 = new() { Id = toBeUpdated.Id, Address = toBeUpdated.Address, PhoneNumber = toBeUpdated.PhoneNumber, ImagePath = toBeUpdated.ImagePath };
            ViewBag.PersonName = toBeUpdated.FirstName + toBeUpdated.SecondName + " " + toBeUpdated.Surname + toBeUpdated.SecondSurname;
            ViewBag.Photo = toBeUpdated.ImagePath;
            ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
            return View(personDto);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            Person identityUser = new Person();
            identityUser.Email = "admin@technopyre.com";
            identityUser.UserName = "admin";
            identityUser.FirstName = "Halil";
            identityUser.PlaceOfBirth = "Ankara";
            identityUser.SecondName = null;
            identityUser.Surname = "Disli";
            identityUser.PhoneNumber = "******";
            identityUser.Salary = 250000m;
            identityUser.Status = Status.Active;
            identityUser.BirthDate = DateTime.Parse("0001/01/01");
            identityUser.StartingDate = DateTime.Parse("0001/01/01");
            identityUser.Address = "Ankara/Türkiye";
            identityUser.IdentityNumber = "111111111111";
            identityUser.CompanyId = Guid.Parse("e98f2762-0565-4027-8c63-66555752dea0");
            identityUser.DepartmentId = Guid.Parse("8c46c58d-9d72-4605-9035-b15bb6b50e00");
            identityUser.JobId = Guid.Parse("8c46c58d-9d72-4605-9035-b15bb6b50e00");
            identityUser.AccessFailedCount = 0;
            identityUser.EmailConfirmed = true;
            identityUser.LockoutEnabled = false;
            identityUser.PhoneNumberConfirmed = true;
            identityUser.TwoFactorEnabled = false;

            string password = "Admin123*";
            IdentityResult result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(identityUser, "ADMIN");
                if (resultRole.Succeeded)
                {
                    TempData["Message"] = "Sign Up successfully! Please click on the activation link sent to your e-mail. ";
                    return RedirectToAction("SignIn", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Sign Up unsuccessfully!";
                return View();
            }
            return View();
        }

		private bool PhotoControl(IFormFile photo)
        {
            string[] photoExtensions = { ".jpg", ".png", ".jpeg" };
            if (photo != null)
            {
                string ext = Path.GetExtension(photo.FileName);
                if (!photoExtensions.Contains(ext))
                {
                    ModelState.AddModelError("formFile", "Extension must be .jpg, .jpeg, .png!");
                    return false;
                }
                else if (photo.Length > 1000 * 1000 * 1)//Bir MB'a karşılık geliyor.
                {
                    ModelState.AddModelError("formFile", "Maximum file size 1 MB");
                    return false;
                }
                return true;
            }
            else
            {
                ModelState.AddModelError("formFile", "Photo is required!");
                return false;
            }
        }
        private string? AddPhoto(IFormFile photo)
        {

            string ext = Path.GetExtension(photo.FileName);
            string photoName = Guid.NewGuid() + ext;
            string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "personimages", photoName);
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return photoName;
        }
    }
}
