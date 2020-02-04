using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class UserCompletion
    {
        private Dictionary<Topic, LessonCompletion> _lessons;

        public UserCompletion()
        {
            _lessons = new Dictionary<Topic, LessonCompletion>
            {
                { Topic.Arithmetic, new LessonCompletion() },
                { Topic.Algebra, new LessonCompletion() },
                { Topic.Geometry, new LessonCompletion() },
                { Topic.Series, new LessonCompletion() },
                { Topic.Differential, new LessonCompletion() },
                { Topic.Integral, new LessonCompletion() },
                { Topic.Sets, new LessonCompletion() },
                { Topic.Probability, new LessonCompletion() },
                { Topic.Statistics, new LessonCompletion() }
            };
            _lessons[Topic.Arithmetic].Enabled = true;
        }

        public void Update(Topic topic, int percent)
        {
            if (_lessons[topic].Enabled && !_lessons[topic].IsMastered())
            {
                _lessons[topic].Update(percent);

                if (_lessons[topic].IsCompleted())
                {
                    switch(topic)
                    {
                        case Topic.Arithmetic:
                            _lessons[Topic.Algebra].Enabled = true;
                            break;
                        case Topic.Algebra:
                            _lessons[Topic.Geometry].Enabled = true;
                            break;
                        case Topic.Geometry:
                            _lessons[Topic.Series].Enabled = true;
                            _lessons[Topic.Sets].Enabled = true;
                            break;
                        case Topic.Series:
                            _lessons[Topic.Differential].Enabled = true;
                            break;
                        case Topic.Differential:
                            _lessons[Topic.Integral].Enabled = true;
                            break;
                        case Topic.Sets:
                            _lessons[Topic.Probability].Enabled = true;
                            break;
                        case Topic.Probability:
                            _lessons[Topic.Statistics].Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
