using System;
using Microsoft.AspNetCore.Hosting;

namespace _2._3.Models
{
    public class QuestionModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Operation { get; set; }

        public int? UserAnswer { get; set; }

        public override string ToString()
        {
            return Operation switch
            {
                "+" => $"{X} + {Y} = ",
                "-" => $"{X} - {Y} = ",
                "*" => $"{X} * {Y} = ",
                "/" when Y != 0 => $"{X} / {Y} = ",
                "/" when Y == 0 => throw new Exception("Can't divide by zero"),
                _ => throw new Exception("Wrong operation")
            };
        }

        public bool CorrectAnswerCheck
        {
            get { return UserAnswer is { } a && a == CorrectAnswer; }
        }

        public int CorrectAnswer
        {
            get
            {
                return Operation switch
                {
                    "+" => X + Y,
                    "-" => X - Y,
                    "*" => X * Y,
                    "/" when Y != 0 => X / Y,
                    "/" when Y == 0 => throw new Exception("Dividing by zero"),
                    _ => throw new Exception("Wrong operation")
                };
            }
        }

        public static QuestionModel RandomQuestion
        {
            get
            {
                var random = new Random();
                return new QuestionModel
                {
                    X = random.Next(0, 10),
                    Y = random.Next(1, 10),
                    Operation = random.Next(4) switch
                    {
                        0 => "+",
                        1 => "-",
                        2 => "*",
                        3 => "/",
                        _ => throw new Exception("Wrong")
                    }
                };
            }
        }
    }
}
