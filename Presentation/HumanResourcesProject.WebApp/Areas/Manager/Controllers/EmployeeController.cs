using HumanResourcesProject.Domain.Entities;
using AutoMapper;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Application.Methods;
using HumanResourcesProject.Application.Repositories.EmployeeRepos;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.Email.Services;
using HumanResourcesProject.Application.Email.Models;
using static System.Net.WebRequestMethods;
using HumanResourcesProject.Domain.Enums;

namespace HumanResourcesProject.WebApp.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly HumanResourcesProjectDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<PersonRole> _roleManager;
        private readonly SignInManager<Person> _signInManager;

		public EmployeeController(IWebHostEnvironment webHostEnvironment, IMapper mapper, HumanResourcesProjectDbContext context, UserManager<Person> userManager, RoleManager<PersonRole> roleManager, SignInManager<Person> signInManager, IUnitOfWork unitOfWork, IEmailService emailService)
		{
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_unitOfWork = unitOfWork;
			_emailService = emailService;
		}

		public IActionResult FileViewerNewTab(string fileName)
        {
            if (fileName.EndsWith(".pdf"))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "expensefiles", fileName);
                return File(System.IO.File.ReadAllBytes(filePath), "application/pdf");
            }
            ViewBag.ImagePath = $"~/files/expensefiles/{fileName}";
            return View();
        }

		[HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            Guid companyId = (Guid)manager.CompanyId;
            string companyName = _context.Companies.FirstOrDefault(c => c.Id == companyId).Name;
            var companyList = new List<SelectListItem>();
            companyList.Add(new SelectListItem() { Text = companyName, Value = companyId.ToString() });
            ViewBag.Companies = companyList;

            var jobList = new List<SelectListItem>();
            foreach (Job item in _context.Jobs.ToList())
            {
                jobList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Jobs = jobList;
            var departmentList = new List<SelectListItem>();
            foreach (Department item in _context.Departments.ToList())
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
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employeeDto, IFormFile? photo)
        {
            //CreateEmployeeDtoValidation validation = new CreateEmployeeDtoValidation();
            //FluentValidation.Results.ValidationResult result = validation.Validate(employeeDto);
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            Guid companyId = (Guid)manager.CompanyId;
            string companyName = _context.Companies.FirstOrDefault(c => c.Id == companyId).Name;
            var companyList = new List<SelectListItem>();
            companyList.Add(new SelectListItem() { Text = companyName, Value = companyId.ToString() });
            ViewBag.Companies = companyList;
            var jobList = new List<SelectListItem>();
            foreach (Job item in _context.Jobs.ToList())
            {
                jobList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Jobs = jobList;
            var departmentList = new List<SelectListItem>();
            foreach (Department item in _context.Departments.ToList())
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
                    return View(employeeDto);
                }
            }
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
                Employee createdEmployee = _mapper.Map<Employee>(employeeDto);
                createdEmployee.Email = Methods.GenerateEmail(createdEmployee);
                createdEmployee.CreatedDate = DateTime.Now;
                createdEmployee.UpdatedDate = createdEmployee.CreatedDate;
                createdEmployee.ImagePath = photoPath;

                Person newPerson = _mapper.Map<Person>(createdEmployee);

                await _unitOfWork.EmployeeWriteRepository.AddAsync(createdEmployee);
                await _unitOfWork.EmployeeWriteRepository.SaveAsync();

                newPerson.UserName = Methods.GenerateUsername(newPerson);
                newPerson.Id = createdEmployee.Id.ToString();

                var password = Methods.GenerateTempPassword(createdEmployee);
                await _userManager.CreateAsync(newPerson,password);
                await _userManager.AddToRoleAsync(newPerson, "employee");



                #region Verification Mail Send

                var link = "https://humanresourcesprojectapp.azurewebsites.net/home/changepassword?id=" + newPerson.Id;
                var toBeSendEmail = new MailMessage(new Dictionary<string, string>() { { newPerson.UserName!, newPerson.Email! } }, "Welcome to Technopyre", $"<b>Your username is '{newPerson.UserName}' and your temporary password is {password}</b><br/><b>Please change your temporary password this link</b><br/><a href='{link}'>Change Your Temporary Password</a>");
                _emailService.SendEmail(toBeSendEmail);


                #endregion
                return RedirectToAction(nameof(ListOfEmployee));
                //return RedirectToAction("ChangePassword", "Home", new { area = "" ,id=newPerson.Id});
            }

            return View(employeeDto);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            var personnel = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(id.ToString());
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            return View(personnel);
        }


        public async Task<ActionResult> ListOfEmployee(int page = 1)
        {

			var jobList = new List<SelectListItem>();
			foreach (Job item in _context.Jobs.ToList())
			{
				jobList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Jobs = jobList;
			var departmentList = new List<SelectListItem>();
			foreach (Department item in _context.Departments.ToList())
			{
				departmentList.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}

			ViewBag.Departments = departmentList;


			var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Employee> personelListesi = _unitOfWork.EmployeeReadRepository.Table.Where(e=>e.CompanyId==manager.CompanyId).ToList();
            
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            return View(personelListesi.ToPagedList(page, 8));
        }
        [HttpGet]
        public async Task<ActionResult> UpdateEmployee(Guid id)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            Guid companyId = (Guid)manager.CompanyId;
            string companyName = _context.Companies.FirstOrDefault(c => c.Id == companyId).Name;
            var companyList = new List<SelectListItem>();
            companyList.Add(new SelectListItem() { Text = companyName, Value = companyId.ToString() });
            ViewBag.Companies = companyList;

            var jobList = new List<SelectListItem>();
            foreach (Job item in _context.Jobs.ToList())
            {
                jobList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Jobs = jobList;
            var departmentList = new List<SelectListItem>();
            foreach (Department item in _context.Departments.ToList())
            {
                departmentList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Departments = departmentList;
            var person = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(id.ToString());
            var personDto = _mapper.Map<UpdateEmployeeDto>(person);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            return View(personDto);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDto person, IFormFile? photo)
        {

            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            Guid companyId = (Guid)manager.CompanyId;
            string companyName = _context.Companies.FirstOrDefault(c => c.Id == companyId).Name;
            var companyList = new List<SelectListItem>();
            companyList.Add(new SelectListItem() { Text = companyName, Value = companyId.ToString() });
            ViewBag.Companies = companyList;

            var jobList = new List<SelectListItem>();
            foreach (Job item in _context.Jobs.ToList())
            {
                jobList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Jobs = jobList;
            var departmentList = new List<SelectListItem>();
            foreach (Department item in _context.Departments.ToList())
            {
                departmentList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.Departments = departmentList;
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            Employee toBeUpdated = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == person.Id);
            if (photo != null)
            {
                if (!PhotoControl(photo))
                {
                    person.ImagePath = toBeUpdated.ImagePath;
                    return View(person);
                }
            }
            if (ModelState.IsValid)
            {
                string photoPath = "";
                if (photo != null)
                {
                    photoPath = AddPhoto(photo);
                }
                else
                {
                    photoPath = toBeUpdated.ImagePath;
                }
                Employee updatedEmployee = _mapper.Map<Employee>(person);
                updatedEmployee.CreatedDate = toBeUpdated.CreatedDate;
                updatedEmployee.Email = Methods.GenerateEmail(toBeUpdated);
                updatedEmployee.UpdatedDate = DateTime.Now;
                updatedEmployee.ImagePath = photoPath;
                _unitOfWork.EmployeeWriteRepository.Update(updatedEmployee);
                await _unitOfWork.EmployeeWriteRepository.SaveAsync();
                return RedirectToAction(nameof(ListOfEmployee));
            }
            return View(person);

        }
        private bool PhotoControl(IFormFile photo)
        {
            string[] photoExtensions = { ".jpg", ".png", ".jpeg" };

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
        private string? AddPhoto(IFormFile photo)
        {
            string ext = Path.GetExtension(photo.FileName);
            string photoName = Guid.NewGuid() + ext;
            string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "employeeimages", photoName);
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return photoName;
        }
        public IActionResult Search(string search, int page = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    var employees = _unitOfWork.EmployeeReadRepository.Table.ToList();
                    var pagedEmployees = employees.ToPagedList(page, 8);
                    return PartialView("ListOfEmployee", pagedEmployees);
                }
                else
                {
                    var employees = _unitOfWork.EmployeeReadRepository.Table.Where(e => e.FirstName.ToLower().Contains(search.ToLower()) || e.SecondName.ToLower().Contains(search.ToLower()) || e.Surname.ToLower().Contains(search.ToLower()) || e.SecondSurname.ToLower().Contains(search.ToLower()) || e.Department.Name.ToLower().Contains(search.ToLower()) || e.Job.Name.ToLower().Contains(search.ToLower())).ToList();
                    var pagedEmployees = employees.ToPagedList(page, 8);
                    return PartialView("ListOfEmployee", pagedEmployees);
                }

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListOfAdvances(int page=1)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var advances= _unitOfWork.AdvanceReadRepository.GetWhere(a=>a.Employee.CompanyId==manager.CompanyId).ToList().OrderByDescending(a=>a.DateOfDemand);
            var advancesDto = _mapper.Map<List<ListOfAdvancesDto>>(advances);
            return View(advancesDto.ToPagedList(page,10));
        }
		[HttpGet]
		public async Task<IActionResult> ListOfPermission()
		{
			var manager = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.Photo = manager.ImagePath;
			ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var permission = _unitOfWork.PermissionReadRepository.GetAll().ToList();
			//var permissionDto = _mapper.Map<List<ListOfPermissionDto>>(permission);
			return View(permission);
		}

		[HttpGet]
        public async Task<IActionResult> ApproveAdvance(Guid id)
        {
            var toBeApproved = await _unitOfWork.AdvanceReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.AdvanceStatus = AdvanceStatus.Approved;
            toBeApproved.DateOfResponse = DateTime.Now;
            _unitOfWork.AdvanceWriteRepository.Update(toBeApproved);
            await _unitOfWork.AdvanceWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfAdvances));
        }
        [HttpGet]
        public async Task<IActionResult> ApprovePermission(Guid id)
        {
            var toBeApproved = await _unitOfWork.PermissionReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.ApprovalState = PermissionStatus.Approved;
            toBeApproved.ReplyDate = DateTime.Now;
            _unitOfWork.PermissionWriteRepository.Update(toBeApproved);
            await _unitOfWork.PermissionWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfPermission));
        }

        [HttpGet]
        public async Task<IActionResult> DenyAdvance(Guid id)
        {
            var toBeApproved = await _unitOfWork.AdvanceReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.AdvanceStatus = AdvanceStatus.Denied;
            toBeApproved.DateOfResponse = DateTime.Now;
            _unitOfWork.AdvanceWriteRepository.Update(toBeApproved);
            await _unitOfWork.AdvanceWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfAdvances));
        }
        [HttpGet]
        public async Task<IActionResult> DenyPermission(Guid id)
        {
            var toBeApproved = await _unitOfWork.PermissionReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.ApprovalState = PermissionStatus.Denied;
            toBeApproved.ReplyDate = DateTime.Now;
            _unitOfWork.PermissionWriteRepository.Update(toBeApproved);
            await _unitOfWork.PermissionWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfPermission));
        }
        [HttpGet]
        public async Task<IActionResult> DetailOfAdvance(Guid id)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var detailOfAdvance = await _unitOfWork.AdvanceReadRepository.GetSingleAsync(a => a.Id == id);
            var detailOfAdvanceDto = _mapper.Map<ListOfAdvancesDto>(detailOfAdvance);
            return View(detailOfAdvanceDto);
        }
        [HttpGet]
        public async Task<IActionResult> DetailOfPermission(Guid id)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var detailOfPermission = await _unitOfWork.PermissionReadRepository.GetSingleAsync(a => a.Id == id);
            
            return View(detailOfPermission);
        }

        [HttpGet]
        public async Task<IActionResult> ListOfExpense()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var expense = _unitOfWork.ExpenseReadRepository.GetAll().ToList().OrderByDescending(a => a.DateOfExpense);
            var expenseDto = _mapper.Map<List<ListOfExpenseDto>>(expense);
            return View(expenseDto);
        }
        [HttpGet]
        public async Task<IActionResult> ApproveExpense(Guid id)
        {
            var toBeApproved = await _unitOfWork.ExpenseReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.AdvanceStatus = AdvanceStatus.Approved;
            toBeApproved.DateOfResponse = DateTime.Now;
            _unitOfWork.ExpenseWriteRepository.Update(toBeApproved);
            await _unitOfWork.ExpenseWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfExpense));
        }
        [HttpGet]
        public async Task<IActionResult> DenyExpense(Guid id)
        {
            var toBeApproved = await _unitOfWork.ExpenseReadRepository.GetSingleAsync(a => a.Id == id);
            toBeApproved.AdvanceStatus = AdvanceStatus.Denied;
            toBeApproved.DateOfResponse = DateTime.Now;
            _unitOfWork.ExpenseWriteRepository.Update(toBeApproved);
            await _unitOfWork.ExpenseWriteRepository.SaveAsync();
            return RedirectToAction(nameof(ListOfExpense));
        }
        [HttpGet]
        public async Task<IActionResult> DetailOfExpense(Guid id)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var detailOExpense = await _unitOfWork.ExpenseReadRepository.GetSingleAsync(a => a.Id == id);
            var detailOfExpenseDto = _mapper.Map<ListOfExpenseDto>(detailOExpense);
            return View(detailOfExpenseDto);
        }
    }
}
