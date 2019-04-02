using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CET322_HW2.Domain;
using CET322_HW2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CET322_HW2.Controllers
{
    public class StudentsController : Controller
    {
        SchoolContext _context;

        #region Ctor
        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        private IList<SelectListItem> GetAvailableDepartments(IList<Department> departments)
        {
            var availableDepartments = new List<SelectListItem>();
            foreach (var department in departments)
            {
                availableDepartments.Add(new SelectListItem
                {
                    Value = department.Id.ToString(),
                    Text = department.Name
                });
               
            }
            availableDepartments.Insert(0,new SelectListItem
            {
                Value = "0",
                Text = "Please select a department"
            });
            return availableDepartments;
        }
        #endregion

        #region List
        public IActionResult StudentList()
        {
            var students = _context.Students.Include(x => x.Department).ToList();
            var studentsmodel = new List<StudentModel>();
            foreach (var item in students)
            {
                var model = new StudentModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    SchoolNumber = item.SchoolNumber,
                    Surname = item.Surname,
                    SelectedDepartmentId = item.DepartmentId,
                    Department = item.Department

                };

                studentsmodel.Add(model);
            }

            return View(studentsmodel);
        }
        #endregion

        #region Detail
        public IActionResult Detail(int id)
        {
            var student = _context.Students.Include(x=>x.Department).Where(x => x.Id == id).FirstOrDefault();
            if (student != null)
            {
                StudentModel studentModel = new StudentModel();
                studentModel.Name = student.Name;
                studentModel.Surname = student.Surname;
                studentModel.Email = student.Email;
                studentModel.SchoolNumber = student.SchoolNumber;
                studentModel.SelectedDepartmentId = student.DepartmentId;
                studentModel.Department = student.Department;
                return View(studentModel);

            }
            else
                return NotFound();

        }
        #endregion
        #region Create
        public IActionResult Create()
        {
            StudentModel studentModel = new StudentModel();
            var departments = _context.Departments.OrderBy(x => x.Name).ToList();
            studentModel.AvailableDepartments = GetAvailableDepartments(departments);
            return View(studentModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel model)
        {
            var existingStudent = _context.Students.Where(x => x.SchoolNumber == model.SchoolNumber).FirstOrDefault();
            var department = _context.Departments.Where(x => x.Id == model.SelectedDepartmentId).FirstOrDefault();
            if (ModelState.IsValid && model.SelectedDepartmentId != 0)
            {
                if (existingStudent == null)
                {
                    Student newstudent = new Student
                    {
                        Email = model.Email,
                        Name = model.Name,
                        SchoolNumber = model.SchoolNumber,
                        Surname = model.Surname,
                        DepartmentId = model.SelectedDepartmentId,
                        Department = department

                    };
                    _context.Students.Add(newstudent);
                    _context.SaveChanges();

                }
                return RedirectToAction("StudentList");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();

            if (!id.HasValue)
            {
                return BadRequest();
            }

            if (student == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.OrderBy(x => x.Name).ToList();
            var model = new StudentModel
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
                SchoolNumber = student.SchoolNumber,
                Surname = student.Surname,
                SelectedDepartmentId = student.DepartmentId,
                AvailableDepartments = GetAvailableDepartments(departments)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, StudentModel model)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            if (model == null)
            {
                return NotFound();
            }
            var student = _context.Students.Where(x => x.Id == model.Id).FirstOrDefault();
            if (id != student.Id)
                return BadRequest();
            if (ModelState.IsValid && student != null && model.SelectedDepartmentId != 0)
            {
                student.Name = model.Name;
                student.Surname = model.Surname;
                student.SchoolNumber = model.SchoolNumber;
                student.Email = model.Email;
                student.DepartmentId = model.SelectedDepartmentId;
                student.Department = _context.Departments.Where(x => x.Id == model.SelectedDepartmentId).FirstOrDefault();
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Edit");
            }
            else
            {
                return View(student);

            }


        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();

                return RedirectToAction("StudentList");
            }
            return NotFound();
        }

        #endregion


    }

}
