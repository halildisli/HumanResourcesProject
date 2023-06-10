using AutoMapper;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Application.Email.Models;
using HumanResourcesProject.Application.Email.Services;
using HumanResourcesProject.Application.Methods;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HumanResourcesProject.WebApp.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
		private readonly IEmailService _emailService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<Person> _userManager;
		private readonly SignInManager<Person> _signInManager;
		public AdminController(UserManager<Person> userManager, IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, SignInManager<Person> signInManager, IEmailService emailService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_signInManager = signInManager;
			_emailService = emailService;
		}
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Home", new { area = "" });
		}
		[HttpGet]
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
		public async Task<IActionResult> Edit(EditPersonDto personDto, IFormFile photo)
		{
			Person toBeUpdated = await _unitOfWork.PersonReadRepository.GetByIdAspAsync(personDto.Id.ToString());
			ViewBag.Department = toBeUpdated.Department.Name;
			ViewBag.Job = toBeUpdated.Job.Name;
			ViewBag.Photo = toBeUpdated.ImagePath;
			ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
			if (personDto.ImagePath == null)
			{
				personDto.ImagePath = toBeUpdated.ImagePath;
			}
			if (photo != null)
			{
				if (!PhotoControl(photo))
				{
					return View(personDto);
				}
			}
			if (ModelState.IsValid)
			{
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
			ViewBag.PersonName = toBeUpdated.FirstName + toBeUpdated.SecondName + " " + toBeUpdated.Surname + toBeUpdated.SecondSurname;
			ViewBag.Photo = toBeUpdated.ImagePath;
			ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
			return View(personDto);
		}
		[HttpGet]
		public async Task<IActionResult> Detail()
		{
			string username = User.Identity.Name;
			var person = await _userManager.FindByNameAsync(username);
			ViewBag.Photo = person.ImagePath;
			ViewBag.FullName = person.FirstName + " " + person.Surname;
			return View(person);
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
		private string? AddLogo(IFormFile photo)
		{

			string ext = Path.GetExtension(photo.FileName);
			string photoName = Guid.NewGuid() + ext;
			string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "companyimages", photoName);
			using (var stream = new FileStream(photoPath, FileMode.Create))
			{
				photo.CopyTo(stream);
			}
			return photoName;
		}
		[HttpGet]
		public async Task<IActionResult> CreateManager()
		{
			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;


			var companyList = new List<SelectListItem>();
			foreach (Company item in _unitOfWork.CompanyReadRepository.Table.ToList())
			{
				companyList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}
			ViewBag.Companies = companyList;

			var jobList = new List<SelectListItem>();
			foreach (Job item in _unitOfWork.JobReadRepository.Table.ToList())
			{
				jobList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Jobs = jobList;
			var departmentList = new List<SelectListItem>();
			foreach (Department item in _unitOfWork.DepartmentReadRepository.Table.ToList())
			{
				departmentList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Departments = departmentList;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateManager(CreateManagerDto createManagerDto, IFormFile? photo)
		{
			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;


			var companyList = new List<SelectListItem>();
			foreach (Company item in _unitOfWork.CompanyReadRepository.Table.ToList())
			{
				companyList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}
			ViewBag.Companies = companyList;

			var jobList = new List<SelectListItem>();
			foreach (Job item in _unitOfWork.JobReadRepository.Table.ToList())
			{
				jobList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Jobs = jobList;
			var departmentList = new List<SelectListItem>();
			foreach (Department item in _unitOfWork.DepartmentReadRepository.Table.ToList())
			{
				departmentList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Departments = departmentList;
			if (photo != null)
			{
				if (!PhotoControl(photo))
				{
					return View(createManagerDto);
				}
			}
			createManagerDto.Id = Guid.NewGuid();
			if (ModelState.IsValid)
			{
				string photoPath = "";
				if (photo != null)
				{
					photoPath = AddPhoto(photo);
				}
				else
				{
					photoPath = "default-profile-image.png";
				}
				Person createdManager = _mapper.Map<Person>(createManagerDto);
				createdManager.Email = Methods.GenerateEmailManager(createdManager);
				createdManager.CreatedDate = DateTime.Now;
				createdManager.UpdatedDate = createdManager.CreatedDate;
				createdManager.ImagePath = photoPath;

				Person newPerson = _mapper.Map<Person>(createdManager);

				newPerson.UserName = Methods.GenerateUsername(newPerson);
				newPerson.Id = createdManager.Id.ToString();
				newPerson.Status = Status.Active;
				newPerson.EmailConfirmed = true;

				var password = Methods.GenerateTempPasswordManager(createdManager);
				await _userManager.CreateAsync(newPerson, password);
				await _userManager.AddToRoleAsync(newPerson, "manager");



				#region Verification Mail Send

				//var link = "https://humanresourcesprojectapp.azurewebsites.net/home/changepassword?id=" + newPerson.Id;
				var toBeSendEmail = new MailMessage(new Dictionary<string, string>() { { newPerson.UserName!, newPerson.Email! } }, "Welcome to Technopyre", $"<b>Your username is '{newPerson.UserName}' and your password is {password}</b><br/>");
				_emailService.SendEmail(toBeSendEmail);

				#endregion
				return RedirectToAction(nameof(ListOfManagers));
				//return RedirectToAction("ChangePassword", "Home", new { area = "" ,id=newPerson.Id});
			}

			return View(createManagerDto);
		}
		public async Task<IActionResult> ListOfManagers(int page = 1)
		{
			var jobList = new List<SelectListItem>();
			foreach (Job item in _unitOfWork.JobReadRepository.Table.ToList())
			{
				jobList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Jobs = jobList;
			var departmentList = new List<SelectListItem>();
			foreach (Department item in _unitOfWork.DepartmentReadRepository.Table.ToList())
			{
				departmentList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Departments = departmentList;


			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			IList<Person> personList = await _userManager.GetUsersInRoleAsync("manager");

			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;
			return View(personList.ToPagedList(page, 8));
		}
		public async Task<IActionResult> Search(string search, int page = 1)
		{
			try
			{
				if (string.IsNullOrEmpty(search))
				{
					var managers = await _userManager.GetUsersInRoleAsync("manager");
					var pagedManagers = managers.ToPagedList(page, 8);
					return PartialView("ListOfManagers", pagedManagers);
				}
				else
				{
					IList<Person> managers = await _userManager.GetUsersInRoleAsync("manager");
					var managers1 = managers.Where(e => e.FirstName.ToLower().Contains(search.ToLower()) || e.SecondName.ToLower().Contains(search.ToLower()) || e.Surname.ToLower().Contains(search.ToLower()) || e.SecondSurname.ToLower().Contains(search.ToLower()) || e.Department.Name.ToLower().Contains(search.ToLower()) || e.Job.Name.ToLower().Contains(search.ToLower())).ToList();
					var pagedManagers = managers1.ToPagedList(page, 8);
					return PartialView("ListOfManagers", pagedManagers);
				}

			}
			catch (Exception ex)
			{

			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> CreateCompany()
		{
			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateCompany(CreateCompanyDto createCompanyDto, IFormFile? photo)
		{
			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;
			if (photo != null)
			{
				if (!PhotoControl(photo))
				{
					return View(createCompanyDto);
				}
			}
			createCompanyDto.Id = Guid.NewGuid();
			if (ModelState.IsValid)
			{
				if (!createCompanyDto.MersisNo.Contains(createCompanyDto.TaxNo) && createCompanyDto.MersisNo[0]!='0')
				{
					ModelState.AddModelError(string.Empty, "Mersis No must be start '0' zero and it contains Tax No!");
					return View(createCompanyDto);
				}
				if (createCompanyDto.ContractStartDate>=createCompanyDto.ContractEndDate)
				{
					ModelState.AddModelError(string.Empty, "The contract start date cannot be equal or later than the contract end date.");
					return View(createCompanyDto);
				}
				if (createCompanyDto.FoundedDate>=createCompanyDto.ContractStartDate)
				{
                    ModelState.AddModelError(string.Empty, "The contract founded date cannot be equal or later than the contract start date.");
                    return View(createCompanyDto);
                }
				if (createCompanyDto.ContractEndDate>=DateTime.Today)
				{
					createCompanyDto.Status = Status.Active;
				}
				string photoPath = "";
				if (photo != null)
				{
					photoPath = AddLogo(photo);
				}
				else
				{
					photoPath = "default-company-image.png";
				}
				Company createdCompany = _mapper.Map<Company>(createCompanyDto);
				createdCompany.CreatedDate = DateTime.Now;
				createdCompany.UpdatedDate = createdCompany.CreatedDate;
				createdCompany.Logo = photoPath;


				await _unitOfWork.CompanyWriteRepository.AddAsync(createdCompany);
				await _unitOfWork.CompanyWriteRepository.SaveAsync();

				return RedirectToAction(nameof(ListOfCompanies));
			}
			return View(createCompanyDto);
		}
		[HttpGet]
		public async Task<IActionResult> ListOfCompanies(int page=1)
		{
			List<Company> companies = _unitOfWork.CompanyReadRepository.Table.ToList();

			var admin = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = admin.ImagePath;
			ViewBag.FullName = admin.FirstName + " " + admin.Surname;
			return View(companies.ToPagedList(page, 8));
		}
		public async Task<IActionResult> SearchCompanies(string search, int page = 1)
		{

			if (string.IsNullOrEmpty(search))
			{
				var companies = _unitOfWork.CompanyReadRepository.GetAll();
				var pagedCompanies = companies.ToPagedList(page, 8);
				return PartialView("ListOfCompanies", pagedCompanies);
			}
			else
			{
				var companies = _unitOfWork.CompanyReadRepository.GetAll();
				var companies1 = companies.Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Title.ToLower().Contains(search.ToLower()) || e.Address.ToLower().Contains(search.ToLower()) || e.Email.ToLower().Contains(search.ToLower())).ToList();
				var pagedCompanies = companies1.ToPagedList(page, 8);
				return PartialView("ListOfCompanies", pagedCompanies);
			}
		}
		[HttpGet]
		public async Task<IActionResult> DetailOfCompany(Guid id)
		{
			var company = _unitOfWork.CompanyReadRepository.GetWhere(c => c.Id == id).FirstOrDefault();
			return View(company);
		}
	}
}
