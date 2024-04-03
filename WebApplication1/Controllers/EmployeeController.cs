using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee

            {
                Name = viewModel.Name,
                BloodGroup = viewModel.BloodGroup,
                MobileNumber = viewModel.MobileNumber,
                Email = viewModel.Email,
                Address = viewModel.Address,
                Projects = viewModel.Projects,

            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var employess = await dbContext.Employees.ToListAsync();
            return View(employess);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id) 
        {
            var employee = await dbContext.Employees.FindAsync(id);
            
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Employee viewModel)
        {
            var employee = await dbContext.Employees.FindAsync(viewModel.Id);
               
            if (employee != null)
            {
                employee.Name = viewModel.Name;
                employee.BloodGroup = viewModel.BloodGroup;
                employee.MobileNumber = viewModel.MobileNumber;
                employee.Email = viewModel.Email;
                employee.Address = viewModel.Address;
                employee.Projects = viewModel.Projects;
                
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Employee");
        }

        [HttpPost] 
        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (employee != null)
            {
                dbContext.Employees.Remove(viewModel);
                await dbContext.SaveChangesAsync();

            }

            return RedirectToAction("List", "Employee");
        }

    }
}
