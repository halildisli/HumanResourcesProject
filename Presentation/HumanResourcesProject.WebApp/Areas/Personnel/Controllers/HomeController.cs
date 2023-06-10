using AutoMapper;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Application.Methods;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesProject.WebApp.Areas.Personnel.Controllers
{
    [Area("Personnel")]
    [Authorize(Roles = "employee")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly HumanResourcesProjectDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<PersonRole> _roleManager;
        private readonly SignInManager<Person> _signInManager;
        public HomeController(IWebHostEnvironment webHostEnvironment, IMapper mapper, HumanResourcesProjectDbContext context, UserManager<Person> userManager, RoleManager<PersonRole> roleManager, SignInManager<Person> signManager, IUnitOfWork unitOfWork)
        {

            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Home", new { area = "" });
        }
        [HttpGet]
        public async Task<IActionResult> ListOfAdvances()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var advances = _unitOfWork.AdvanceReadRepository.GetWhere(a => a.EmployeeId == Guid.Parse(manager.Id)).ToList().OrderByDescending(a => a.DateOfDemand);
            var advancesDto = _mapper.Map<List<ListOfAdvancesDto>>(advances);
            return View(advancesDto);
        }
        public async Task<IActionResult> HomePage()
        {
            var entity = await _userManager.FindByNameAsync(User.Identity.Name).WaitAsync(TimeSpan.FromSeconds(15));
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(entity.Id);

            ViewBag.Photo = entity.ImagePath;
            ViewBag.FullName = entity.FirstName + " " + entity.Surname;


            //Bu kısma bakılacak view'i düzenlenecek.

            List<Advance> advancesOfEmployee = _unitOfWork.AdvanceReadRepository.GetWhere(a => a.EmployeeId == employee.Id).Where(a => a.AdvanceStatus == AdvanceStatus.Pending).ToList();

            if (advancesOfEmployee != null)
            {
                ViewBag.AdvancesOfEmployee = advancesOfEmployee;
            }

            return View(entity);
        }
        public async Task<IActionResult> Detail()
        {
            string username = User.Identity.Name;
            //var person = await _personReadService.GetByIdAspAsync("8C46C58D-9D72-4605-9035-B15BB6B50E00");
            var person = await _userManager.FindByNameAsync(username);
            ViewBag.Photo = person.ImagePath;
            ViewBag.FullName = person.FirstName + " " + person.Surname;

            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            Person person1 = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Department = person1.Department.Name;
            ViewBag.Job = person1.Job.Name;
            string username = User.Identity.Name;
            var person = await _userManager.FindByNameAsync(username);
            //var person1 = await _personReadService.GetByIdAspAsync("8C46C58D-9D72-4605-9035-B15BB6B50E00");
            var entity = _mapper.Map<EditPersonDto>(person);
            //EditPersonDto editPerson = new EditPersonDto() { Id = person.Id, PhoneNumber = person.PhoneNumber, ImagePath = person.ImagePath, Address = person.Address };
            ViewBag.PersonName = person.FirstName + " " + person.SecondName + " " + person.Surname + " " + person.SecondSurname;
            ViewBag.Photo = person.ImagePath;
            ViewBag.FullName = person.FirstName + " " + person.Surname;
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPersonDto personDto, IFormFile? photo)
        {
            Person toBeUpdated1 = await _unitOfWork.PersonReadRepository.GetByIdAspAsync(personDto.Id.ToString());
            ViewBag.Department = toBeUpdated1.Department.Name;
            ViewBag.Job = toBeUpdated1.Job.Name;
            //Person toBeUpdated = await _employeeReadService.GetByIdAspAsync(personDto.Id.ToString());
            Employee toBeUpdated = await _context.Employees.AsNoTracking().Where(p => p.Id == personDto.Id).FirstOrDefaultAsync();
            if (personDto.ImagePath == null)
            {
                personDto.ImagePath = toBeUpdated.ImagePath;
            }
            ViewBag.Photo = toBeUpdated.ImagePath;
            ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
            if (photo != null)
            {
                if (!PhotoControl(photo))
                {
                    personDto.ImagePath = toBeUpdated.ImagePath;
                    return View(personDto);
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
                //Employee updatedEmployee = _mapper.Map<Employee>(personDto);
                Employee updatedEmployee = toBeUpdated;
                updatedEmployee.Address = personDto.Address;
                updatedEmployee.PhoneNumber = personDto.PhoneNumber;
                updatedEmployee.CreatedDate = toBeUpdated.CreatedDate;
                updatedEmployee.Email = Methods.GenerateEmail(toBeUpdated);
                updatedEmployee.UpdatedDate = DateTime.Now;
                updatedEmployee.ImagePath = photoPath;
                updatedEmployee.CompanyId = toBeUpdated.CompanyId;
                updatedEmployee.DepartmentId = toBeUpdated.DepartmentId;
                updatedEmployee.JobId = toBeUpdated.JobId;

                _unitOfWork.EmployeeWriteRepository.Update(updatedEmployee);
                await _unitOfWork.EmployeeWriteRepository.SaveAsync();

                Person newPerson = await _userManager.FindByIdAsync(updatedEmployee.Id.ToString());
                newPerson.ImagePath = photoPath;
                newPerson.Address = updatedEmployee.Address;
                newPerson.PhoneNumber = updatedEmployee.PhoneNumber;

                //_personeWriteService.Update(newPerson);
                //await _personeWriteService.SaveAsync();
                var result = await _userManager.UpdateAsync(newPerson);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomePage));

                }


            }
            //EditPersonDto personDto1 = new() { Id = toBeUpdated.Id, Address = toBeUpdated.Address, PhoneNumber = toBeUpdated.PhoneNumber, ImagePath = toBeUpdated.ImagePath };
            ViewBag.PersonName = toBeUpdated.FirstName + toBeUpdated.SecondName + " " + toBeUpdated.Surname + toBeUpdated.SecondSurname;
            ViewBag.Photo = toBeUpdated.ImagePath;
            ViewBag.FullName = toBeUpdated.FirstName + " " + toBeUpdated.Surname;
            return View(personDto);
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
            string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "employeeimages", photoName);
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return photoName;
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
            identityUser.Email = "nur.duran@bilgeadamboost.com";
            identityUser.UserName = "nur.duran";
            identityUser.FirstName = "Nur";
            identityUser.PlaceOfBirth = "Sakarya";
            identityUser.SecondName = null;
            identityUser.Surname = "Duran";
            identityUser.PhoneNumber = "05555555555";
            identityUser.Salary = 8500m;
            identityUser.Status = Status.Active;
            identityUser.BirthDate = DateTime.Parse("01/01/01");
            identityUser.StartingDate = DateTime.Parse("2020/08/07");
            identityUser.Address = "Ankara/Türkiye";
            identityUser.IdentityNumber = "45745376183";
            identityUser.CompanyId = Guid.Parse("e98f2762-0565-4027-8c63-66555752dea0");
            identityUser.DepartmentId = Guid.Parse("8c46c58d-9d72-4605-9035-b15bb6b50e00");
            identityUser.JobId = Guid.Parse("8c46c58d-9d72-4605-9035-b15bb6b50e00");
            identityUser.AccessFailedCount = 0;
            identityUser.EmailConfirmed = true;
            identityUser.LockoutEnabled = false;
            identityUser.PhoneNumberConfirmed = true;
            identityUser.TwoFactorEnabled = false;


            string password = "Nur123*";
            IdentityResult result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(identityUser, "EMPLOYEE");
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
        [HttpGet]
        public async Task<IActionResult> AdvanceRequest()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;

            var advanceStatus = Enum.GetValues(typeof(AdvanceStatus));
            var selectAdvanceStatus = new SelectList(advanceStatus);
            ViewBag.AdvanceStatus = selectAdvanceStatus;

            var advanceCurrency = Enum.GetValues(typeof(Currency));
            var selectCurrency = new SelectList(advanceCurrency);
            ViewBag.AdvanceCurrency = selectCurrency;

            var advanceType = Enum.GetValues(typeof(AdvanceType));
            var selectType = new SelectList(advanceType);
            ViewBag.AdvanceType = selectType;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdvanceRequest(Advance advance)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var advanceStatus = Enum.GetValues(typeof(AdvanceStatus));
            var selectAdvanceStatus = new SelectList(advanceStatus);
            ViewBag.AdvanceStatus = selectAdvanceStatus;

            var advanceCurrency = Enum.GetValues(typeof(Currency));
            var selectCurrency = new SelectList(advanceCurrency);
            ViewBag.AdvanceCurrency = selectCurrency;

            var advanceType = Enum.GetValues(typeof(AdvanceType));
            var selectType = new SelectList(advanceType);
            ViewBag.AdvanceType = selectType;

            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(person.Id);
            advance.EmployeeId = employee.Id;
            var pendingAdvance = _unitOfWork.AdvanceReadRepository.GetWhere(a => a.EmployeeId == employee.Id).Where(a => a.AdvanceStatus == AdvanceStatus.Pending).FirstOrDefault();
            if (pendingAdvance != null)
            {
                ModelState.AddModelError(string.Empty, "You already have a request!");
                return View(advance);
            }
            if (advance.AdvanceType == AdvanceType.Individual)
            {
                var ListOfAdvance = _unitOfWork.AdvanceReadRepository.Table.Where(x => x.EmployeeId == Guid.Parse(person.Id));
                var ApproveAdvance = ListOfAdvance.Where(x => x.AdvanceStatus == AdvanceStatus.Approved);
                var TlApproveAdvance = ApproveAdvance.Where(x => x.Currency == Currency.TL);
                var EuroApproveAdvance = ApproveAdvance.Where(x => x.Currency == Currency.EURO);
                var USDApproveAdvance = ApproveAdvance.Where(x => x.Currency == Currency.USD);

                var TotalDemand = TlApproveAdvance.Sum(x => x.AmountOfDemand) + EuroApproveAdvance.Sum(x => x.AmountOfDemand) * 22 + USDApproveAdvance.Sum(x => x.AmountOfDemand) * 20;
                if (TotalDemand >= person.Salary * 3)
                {
                    ModelState.AddModelError(string.Empty, "Individual advance request limit exceeded");
                    return View(advance);
                }
                else
                {
                    var factor = 1;
                    if (advance.Currency == Currency.USD)
                    {
                        factor = 20;
                    }
                    else if (advance.Currency == Currency.EURO)
                    {
                        factor = 22;
                    }
                    if (advance.AmountOfDemand * factor <= (person.Salary * 3) - TotalDemand)
                    {
                        if (advance.AmountOfDemand * factor <= person.Salary)
                        {
                            await _unitOfWork.AdvanceWriteRepository.AddAsync(advance);
                            await _unitOfWork.AdvanceWriteRepository.SaveAsync();
                            return RedirectToAction("HomePage");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "You can use up to the maximum amount of your salary in advance at one time.");
                            return View(advance);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Individual advance request limit exceeded");
                        return View(advance);
                    }
                }
            }
            else
            {
                var LimitofAdvance = 1000000m;
                var factor = 1;
                if (advance.Currency == Currency.USD)
                {
                    factor = 20;
                }
                else if (advance.Currency == Currency.EURO)
                {
                    factor = 22;
                }
                if (advance.AmountOfDemand * factor > LimitofAdvance)
                {
                    ModelState.AddModelError(string.Empty, "Individual advance request limit exceeded");
                    return View(advance);
                }
                else
                {
                    await _unitOfWork.AdvanceWriteRepository.AddAsync(advance);
                    await _unitOfWork.AdvanceWriteRepository.SaveAsync();
                    return RedirectToAction("HomePage");
                }
            }

        }
        [HttpGet]
        public async Task<IActionResult> PermissionRequest()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;

            var permissionType = Enum.GetValues(typeof(PermissionType));
            var selectpermissionType = new SelectList(permissionType);
            ViewBag.PermissionType = selectpermissionType;

            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(person.Id);
            ViewBag.Employee = employee.Id;
            ViewBag.Person = person.Id;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PermissionRequest(Permission permission)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;

            var permissionType = Enum.GetValues(typeof(PermissionType));
            var selectpermissionType = new SelectList(permissionType);
            ViewBag.PermissionType = selectpermissionType;

            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(person.Id);
            permission.EmployeeId = employee.Id;

            if (ModelState.IsValid)
            {
                var intersectingPermissions = await _unitOfWork.PermissionReadRepository.GetIntersectingPermissionsByEmployeeIdAsync(employee.Id, permission.StartOfPermissionDate.Value, permission.EndOfPermissionDate.Value);
                if (intersectingPermissions.Any())
                {
                    ModelState.AddModelError("", "There is already a permission request for the selected date range.");
                    return View(permission);
                }


                var countOfUsedDays = await _unitOfWork.PermissionReadRepository.GetCountOfUsedDaysByEmployeeIdAsync(employee.Id);
                var usedDays = countOfUsedDays.TotalDays;
                var permittedDays = (permission.EndOfPermissionDate - permission.StartOfPermissionDate).Value.TotalDays;
                permission.CountOfPermittedDays = Convert.ToInt32(usedDays + permittedDays);


                var validationResult = Validate(permission, employee);

                if (validationResult == ValidationResult.Success)
                {
                    await _unitOfWork.PermissionWriteRepository.AddAsync(permission);
                    await _unitOfWork.PermissionWriteRepository.SaveAsync();
                    return RedirectToAction(nameof(ListOfPermission));
                }
                else
                {
                    ModelState.AddModelError("", validationResult.ToString());
                }


            }
            return View(permission);
        }


        [HttpGet]
        public async Task<IActionResult> ListOfPermission()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            //var permission = _unitOfWork.PermissionReadRepository.GetWhere(a => a.EmployeeId == Guid.Parse(manager.Id)).ToList().OrderByDescending(a => a.StartOfPermissionDate);

            List<Permission> permissions = _unitOfWork.PermissionReadRepository.Table.ToList();
            return View(permissions);
        }

        private object Validate(Permission permission, Employee employee)
        {

            if (permission.PermissionType == PermissionType.AnnualLeave)
            {
                if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
                {
                    double diff = (DateTime.Now - ((DateTime)employee.StartingDate)).TotalDays / 365;

                    if (diff < 1)
                    {
                        return new ValidationResult("Employees with less than 1 user cannot take annual leave.");
                    }
                    if (diff >= 1 && diff < 6)
                    {

                        if (permission.CountOfPermittedDays > 14)
                        {
                            return new ValidationResult("Employees with 1-6 years of experience can only take up to 14 days of yearly permission.");
                        }
                    }
                    else if (diff >= 6)
                    {
                        if (permission.CountOfPermittedDays > 20)
                        {
                            return new ValidationResult("Employees with more than 6 years of experience can take up to 20 days of yearly permission.");
                        }
                    }
                }
            }
            else if (permission.PermissionType == PermissionType.MaternityLeave)
            {
                if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
                {
                    if (permission.CountOfPermittedDays > 180)
                    {
                        return new ValidationResult("Employees on maternity leave can only take up to 180 days of yearly permission.");
                    }
                }
            }
            else if (permission.PermissionType == PermissionType.PaternityLeave)
            {
                if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
                {
                    if (permission.CountOfPermittedDays > 30)
                    {
                        return new ValidationResult("Employees on paternity leave can only take up to 30 days of yearly permission");
                    }
                }
            }
            else if (permission.PermissionType == PermissionType.MarriageLeave)
            {
                if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
                {
                    if (permission.CountOfPermittedDays > 15)
                    {
                        return new ValidationResult("Employees on marriage leave can only take up to 15 days of yearly permission");
                    }
                }
            }
            else if (permission.PermissionType == PermissionType.UnpaidLeave)
            {
                if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
                {
                    if (permission.CountOfPermittedDays > 5)
                    {
                        return new ValidationResult("Employees on unpaid leave can only take up to 5 days of yearly permission");
                    }
                }
            }

            return ValidationResult.Success;
        }
        [HttpGet]
        public async Task<IActionResult> Expense()
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;

            var advanceStatus = Enum.GetValues(typeof(AdvanceStatus));
            var selectAdvanceStatus = new SelectList(advanceStatus);
            ViewBag.AdvanceStatus = selectAdvanceStatus;

            var expenseCurrency = Enum.GetValues(typeof(Currency));
            var selectCurrency = new SelectList(expenseCurrency);
            ViewBag.ExpenseCurrency = selectCurrency;

            var expenseType = Enum.GetValues(typeof(ExpenseType));
            var selectType = new SelectList(expenseType);
            ViewBag.ExpenseType = selectType;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Expense(Expense expense,IFormFile fileMultipleInput)
        {
            var manager = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = manager.ImagePath;
            ViewBag.FullName = manager.FirstName + " " + manager.Surname;
            var advanceStatus = Enum.GetValues(typeof(AdvanceStatus));
            var selectAdvanceStatus = new SelectList(advanceStatus);
            ViewBag.AdvanceStatus = selectAdvanceStatus;

            var expenseCurrency = Enum.GetValues(typeof(Currency));
            var selectCurrency = new SelectList(expenseCurrency);
            ViewBag.ExpenseCurrency = selectCurrency;

            var expenseType = Enum.GetValues(typeof(ExpenseType));
            var selectType = new SelectList(expenseType);
            ViewBag.ExpenseType = selectType;

            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var employee = await _unitOfWork.EmployeeReadRepository.GetByIdAsync(person.Id);
            expense.EmployeeId = employee.Id;
            expense.PersonId = person.Id;
            var pendingExpense = _unitOfWork.ExpenseReadRepository.GetWhere(a => a.EmployeeId == employee.Id).Where(a => a.AdvanceStatus == AdvanceStatus.Pending).FirstOrDefault();
            //if (pendingExpense != null)
            //{
            //    ModelState.AddModelError(string.Empty, "A spending request has already been created !");
            //    return View(expense);
            //}
            if (fileMultipleInput != null)
            {
                if (!FileControl(fileMultipleInput))
                {
					ModelState.AddModelError(string.Empty, "File size is not greater than 4MB!");
					return View(expense);
                }
            }
            if (ModelState.IsValid)
            {
                string filePath = "";
                if (fileMultipleInput != null)
                {
                    filePath = AddFile(fileMultipleInput);
                    expense.FilePath = filePath;
                }
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "File field is required!");
                //    return View(expense);
                //}
            }
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "File field is required");
            //    return View(expense);
            //}
            await _unitOfWork.ExpenseWriteRepository.AddAsync(expense);
            await _unitOfWork.ExpenseWriteRepository.SaveAsync();
            return RedirectToAction("ListOfExpense");

        }
        [HttpGet]
        public async Task<IActionResult> ListOfExpense()
        {
            var employee = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Photo = employee.ImagePath;
            ViewBag.FullName = employee.FirstName + " " + employee.Surname;
            var expense = _unitOfWork.ExpenseReadRepository.GetWhere(a => a.EmployeeId == Guid.Parse(employee.Id)).ToList().OrderByDescending(a => a.DateOfExpense);
            var expenseDto = _mapper.Map<List<ListOfExpenseDto>>(expense);
            string total = _unitOfWork.ExpenseReadRepository.Table.Where(e=>e.EmployeeId==Guid.Parse(employee.Id)).Sum(e => e.AmountOfExpense).ToString();
            ViewBag.Total = total;
            return View(expenseDto);
            //Euro/USD/TL cinsinden bakılacak!
        }
        private bool FileControl(IFormFile file)
        {
            string[] fileExtensions = { ".jpg", ".png", ".jpeg",".pdf" };
            if (file != null)
            {
                string ext = Path.GetExtension(file.FileName);
                if (!fileExtensions.Contains(ext))
                {
                    ModelState.AddModelError("formFile", "Extension must be .jpg, .jpeg, .png, .pdf!");
                    return false;
                }
                else if (file.Length > 2000 * 2000 * 1)//Dört MB'a karşılık geliyor.
                {
                    ModelState.AddModelError("formFile", "Maximum file size 4 MB");
                    return false;
                }
                return true;
            }
            else
            {
                ModelState.AddModelError("formFile", "File is required!");
                return false;
            }
        }
        private string? AddFile(IFormFile photo)
        {

            string ext = Path.GetExtension(photo.FileName);
            string fileName = Guid.NewGuid() + ext;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "expensefiles", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return fileName;
        }

    }
}



