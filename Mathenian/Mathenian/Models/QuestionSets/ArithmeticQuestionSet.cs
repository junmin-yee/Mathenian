using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class ArithmeticQuestionSet : AbstractQuestionSet
    {
        private static readonly string[] _questionTemplates = { "{0} + {1}", "{0} - {1}", "{0} * {1}", "{0} / {1}" };

        public ArithmeticQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                double firstValue = 0;
                double secondValue = 1;
                switch (_mastery)
                {
                    case Mastery.Bronze:
                        firstValue = random.Next(1, 11);
                        secondValue = random.Next(1, 11);
                        break;
                    case Mastery.Silver:
                        firstValue = random.Next(-10, 0);
                        secondValue = random.Next(-10, 0);
                        break;
                    case Mastery.Gold:
                        firstValue = random.Next(1, 4) / random.Next(1, 6);
                        secondValue = random.Next(1, 4) / random.Next(1, 6);
                        break;
                    case Mastery.Platinum:
                        firstValue = random.NextDouble();
                        secondValue = random.NextDouble();
                        break;
                }

                switch (random.Next(_questionTemplates.Length))
                {
                    case 0:
                        return Tuple.Create(string.Format(_questionTemplates[0], firstValue, secondValue),
                            (firstValue + secondValue).ToString());
                    case 1:
                        return Tuple.Create(string.Format(_questionTemplates[1], firstValue, secondValue),
                            (firstValue - secondValue).ToString());
                    case 2:
                        return Tuple.Create(string.Format(_questionTemplates[2], firstValue, secondValue),
                            (firstValue * secondValue).ToString());
                    case 3:
                        firstValue = secondValue * random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplates[3], firstValue, secondValue),
                            (firstValue / secondValue).ToString());
                }
            }
            throw new ArithmeticException();
        }
    }
}
