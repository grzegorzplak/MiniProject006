using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject006.Models;

namespace MiniProject006.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Context _context;

        public EmployeesController(Context context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string? orderBy, string? howToSort)
        {
            ViewBag.OrderBy = (orderBy != null) ? orderBy : "";
            ViewBag.HowToSort = (howToSort == "desc") ? "asc" : "desc";
            List<Employee>? xresult;
            switch (ViewBag.OrderBy)
            {
                case "myName":
                    if (ViewBag.HowToSort == "asc")
                    {
                        xresult = _context.Employees.OrderBy(o => o.Name).ToList();
                    }
                    else 
                    {
                        xresult = await _context.Employees.OrderByDescending(o => o.Name).ToListAsync();
                    }
                    break;
                case "myDate":
                    if (ViewBag.HowToSort == "asc")
                    {
                        xresult = await _context.Employees.OrderBy(o => o.DateOfEmployment).ToListAsync();
                    }
                    else
                    {
                        xresult = await _context.Employees.OrderByDescending(o => o.DateOfEmployment).ToListAsync();
                    }
                    break;
                case "myYear":
                    if (ViewBag.HowToSort == "asc")
                    {
                        xresult = await _context.Employees.OrderBy(o => o.YearOfBirth).ToListAsync();
                    }
                    else
                    {
                        xresult = await _context.Employees.OrderByDescending(o => o.YearOfBirth).ToListAsync();
                    }
                    break;
                default:
                    xresult = await _context.Employees.ToListAsync();
                    break;
            }
            return View(xresult);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfEmployment,YearOfBirth")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfEmployment,YearOfBirth")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
