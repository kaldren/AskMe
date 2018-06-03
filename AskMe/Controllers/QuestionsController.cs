using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AskMe.Data;
using AskMe.Models;
using Microsoft.AspNetCore.Identity;
using AskMe.Models.QuestionViewModels;
using AskMe.Repository.Interfaces;

namespace AskMe.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IQuestionRepository _questionRepository;


        public QuestionsController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context,
            IQuestionRepository questionRepository)
        {
            _userManager = userManager;
            _context = context;
            _questionRepository = questionRepository;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Question.Include(q => q.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionVM = new QuestionDetailsViewModel
            {
                Answers = await _questionRepository.GetAllAnswersById(id),
                Question = await _questionRepository.GetQuestionDetails(id),
                Users = await _questionRepository.GetAllAnswerAuthors(await _questionRepository.GetAllAnswersById(id))
            };

            return View(questionVM);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserId = _userManager.GetUserId(User);

                if (question.UserId == null)
                {
                    return View(question);
                }

                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", question.UserId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.Id == id && m.UserId == _userManager.GetUserId(User));
            if (question == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", question.UserId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,UserId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", question.UserId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.User)
                .SingleOrDefaultAsync(m => m.Id == id && m.UserId == _userManager.GetUserId(User));
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.SingleOrDefaultAsync(m => m.Id == id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> All(string id)
        {
            var allQuestionsVM = new AllQuestionsViewModel
            {
                AllQuestions = await _context.Question.Where(p => p.User.NickName == id).ToListAsync(),
                User = await _context.Users.SingleOrDefaultAsync(p => p.NickName == id),
            };

            ViewData["UserProvided"] = id;

            return View(allQuestionsVM);
        }


        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}
