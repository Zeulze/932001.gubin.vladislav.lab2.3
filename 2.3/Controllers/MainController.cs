using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using _2._3.Tools;
using _2._3.Models;


namespace _2._3.Controllers
{
    public class MainController : Controller
    {

        private void toSaveAnswer(int? answer)
        {
            var lastQuest = LastQuestion;
            lastQuest.UserAnswer = answer;
            HttpContext.Session.Set($"Question{QuestionCount - 1}", lastQuest);
        }

        private int QuestionCount
        {
            get
            {
                return HttpContext.Session.Get<int>(nameof(QuestionCount)) switch
                {
                    < 0 => throw new Exception("Wrong count"), { } count => count
                };
            }
        }

        private QuestionModel NextQuestion
        {
            get
            {
                var question = QuestionModel.RandomQuestion;

                var count = QuestionCount;
                HttpContext.Session.Set($"Question{count}", question);
                count += 1;
                HttpContext.Session.Set(nameof(QuestionCount), count);

                return question;
            }
        }
        private QuestionModel LastQuestion
        {
            get
            {
                var count = QuestionCount - 1;
                return HttpContext.Session.Get<QuestionModel>($"Question{count}");
            }
        }

        private ResultModel Result
        {
            get
            {
                var result = new ResultModel { Questions = new() };
                for (var i = 0; i < QuestionCount; i++)
                {
                    var question = HttpContext.Session.Get<QuestionModel>($"Question{i}");
                    result.Questions.Add(question);
                }

                return result;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            var question = QuestionCount switch
            {
                0 => NextQuestion,
                _ => LastQuestion
            };
            ViewBag.Question = question.ToString();
            return View();
        }

        [HttpPost]
        public IActionResult Quiz(AnswerModel answerModel, string action)
        {
            if (ModelState.IsValid)
            {
                if (answerModel.Answer < -10)
                {
                    ModelState.AddModelError("Answer", $"  {answerModel.Answer} слишком маленькое число для этого сайта");
                    ViewBag.Question = LastQuestion;
                    return View();
                }
                toSaveAnswer(answerModel.Answer);

                if (action == "Next")
                {
                    ViewBag.Question = NextQuestion;
                    return RedirectToAction("Quiz");
                }
                else
                {
                    return RedirectToAction("QuizResult");
                }
            }
            else
            {
                ViewBag.Question = LastQuestion;
                return View();
            }
        }
        public IActionResult QuizResult()
        {
            var result = Result;

            HttpContext.Session.Clear();

            return View(result);
        }

    }
}
