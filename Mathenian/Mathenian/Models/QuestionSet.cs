using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public enum Topic
    {
        Arithmetic,
        Algebra,
        Geometry,
        Series,
        Differential,
        Integral,
        Sets,
        Probability,
        Statistics
    }

    public class QuestionSet
    {
        private readonly Dictionary<Topic, QuestionSetFactory> _factories;

        public QuestionSet()
        {
            _factories = new Dictionary<Topic, QuestionSetFactory>
            {
                { Topic.Arithmetic, new ArithmeticFactory() }
            };
        }

        public AbstractQuestionSet ExecuteCreate(Topic topic, int numQuestions, Mastery mastery) => _factories[topic].Create(numQuestions, mastery);
    }
}
