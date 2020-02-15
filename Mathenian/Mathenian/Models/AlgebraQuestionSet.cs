using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class AlgebraQuestionSet : AbstractQuestionSet
    {
        private static readonly string[] _questionTemplateBr = { "{0}x = {1}", "{0}/x = {1}" };
        private static readonly string _questionTemplateSi = "{0}x + {1} = {2}";
        //private static readonly string _questionTemplateGo = "{0}x + {1} = {2}x + {3}";
        //private static readonly string _questionTemplatePl = "{0}({1}x + {2}) = {3}({4}x + {5})";

        public AlgebraQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock(syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                        {
                            int firstValue = random.Next(1, 11);
                            int secondValue = firstValue * random.Next(1, 11);
                            switch (random.Next(_questionTemplateBr.Length))
                            {
                                case 0:
                                    return Tuple.Create(string.Format(_questionTemplateBr[0], firstValue, secondValue),
                                        (secondValue / firstValue).ToString());
                                case 1:
                                    return Tuple.Create(string.Format(_questionTemplateBr[1], secondValue, firstValue),
                                        (firstValue / secondValue).ToString());
                            }
                            break;
                        }
                    case Mastery.Silver:
                        {
                            int firstValue = random.Next(1, 11);
                            int secondValue = random.Next(1, 11);
                            int thirdValue = firstValue * random.Next(1, 11) + secondValue;
                            return Tuple.Create(string.Format(_questionTemplateSi, firstValue, secondValue, thirdValue),
                                        ((thirdValue - secondValue) / firstValue).ToString());
                        }
                    case Mastery.Gold:
                        break;
                    case Mastery.Platinum:
                        break;
                }
            }
            throw new ArithmeticException();
        }
    }
}
