using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class UserCompletion
    {
        private Dictionary<Topic, LessonCompletion> _lessons;
        public Dictionary<Topic, LessonCompletion> Lessons { get => _lessons; set => _lessons = value; }

        public UserCompletion()
        {
            Lessons = new Dictionary<Topic, LessonCompletion>
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
            Lessons[Topic.Arithmetic].Enabled = true;
        }

        public void Update(Topic topic, int percent)
        {
            if (Lessons[topic].Enabled && !Lessons[topic].IsMastered())
            {
                Lessons[topic].Update(percent);

                if (Lessons[topic].IsCompleted())
                {
                    switch(topic)
                    {
                        case Topic.Arithmetic:
                            Lessons[Topic.Algebra].Enabled = true;
                            break;
                        case Topic.Algebra:
                            Lessons[Topic.Geometry].Enabled = true;
                            break;
                        case Topic.Geometry:
                            Lessons[Topic.Series].Enabled = true;
                            Lessons[Topic.Sets].Enabled = true;
                            break;
                        case Topic.Series:
                            Lessons[Topic.Differential].Enabled = true;
                            break;
                        case Topic.Differential:
                            Lessons[Topic.Integral].Enabled = true;
                            break;
                        case Topic.Sets:
                            Lessons[Topic.Probability].Enabled = true;
                            break;
                        case Topic.Probability:
                            Lessons[Topic.Statistics].Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public string GenerateForDatabase()
        {
            string completion = "";

            foreach (KeyValuePair<Topic, LessonCompletion> entry in Lessons)
            {
                completion += (int)entry.Value.Mastery + "," + entry.Value.PercentCompleted + ",";
            }

            return completion;
        }

        public void UpdateFromDatabase(string completion)
        {
            string[] inputs = completion.Split(',');

            for (int i = 0; i < Lessons.Count; ++i)
            {
                Lessons[(Topic)i].Mastery = (Mastery)int.Parse(inputs[2 * i]);
                Lessons[(Topic)i].PercentCompleted = int.Parse(inputs[2 * i + 1]);
            }
        }
    }
}
