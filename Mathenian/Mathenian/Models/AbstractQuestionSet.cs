using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public enum Mastery
    {
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    public abstract class AbstractQuestionSet
    {
        protected static readonly Random random = new Random();
        protected static readonly object syncLock = new object();

        protected int _numQuestions;
        protected Mastery _mastery;
        protected string[] _questionSet;
        protected string[] _answerSet;

        protected AbstractQuestionSet(int numQuestions, Mastery mastery)
        {
            _numQuestions = numQuestions;
            _mastery = mastery;
            _questionSet = new string[numQuestions];
            _answerSet = new string[numQuestions];
        }

        public Tuple<string[], string[]> GenerateQuestionSet()
        {
            for (int i = 0; i < _numQuestions; ++i)
            {
                Tuple<string, string> result = GenerateQuestion();
                _questionSet[i] = result.Item1;
                _answerSet[i] = result.Item2;
            }
            return Tuple.Create(_questionSet, _answerSet);
        }

        protected abstract Tuple<string, string> GenerateQuestion();
    }
}
