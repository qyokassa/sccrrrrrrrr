using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace помогите_
{
    internal class TestRepository
    {



        static TestRepository instance;
        public static TestRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TestRepository();
                return instance;
            }
        }

        internal void AddQuestion(Questions questions, int idTest)
        {
            int idQuestion = MySqlDB.Instance.GetAutoID("questions");
            questions.ID = idQuestion;
            string sql = "INSERT INTO questions VALUES (0, @qtid, @qtext)";
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            {
                mc.Parameters.AddWithValue("qtid", idTest);
                mc.Parameters.AddWithValue("qtext", questions.QuestionText);
                mc.ExecuteNonQuery();
            }
            sql = "INSERT INTO answers VALUES (0, @atext, @correct, @qid)";
            foreach (var answer in questions.Answers)
            {
                using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
                {
                    mc.Parameters.AddWithValue("atext", answer.Title);
                    mc.Parameters.AddWithValue("correct", answer.Correct);
                    mc.Parameters.AddWithValue("qid", idQuestion);
                    mc.ExecuteNonQuery();
                }
            }

        }


        internal List<TestInfo> GetTests()
        {
            List<TestInfo> result = new();
            string sql = "SELECT test.id, test.test_name, test.id_creator, users.lastname FROM test, users WHERE id_creator = users.ID";
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    TestInfo q = new TestInfo
                    {
                        ID = dr.GetInt32("id"),
                        Author = dr.GetString("lastname"),
                        IDAuthor = dr.GetInt32("id_creator"),
                        Name = dr.GetString("test_name"),
                    };
                    result.Add(q);
                }
            }
            return result;
        }

        internal List<Questions> GetQuestionsByTest(int id)
        {
            List<Questions> result = new();
            string sql = "SELECT * FROM questions Where id_test = " + id;
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    Questions q = new Questions
                    {
                        ID = dr.GetInt32("id"),
                        Answers = new System.Collections.ObjectModel.ObservableCollection<Answer>(),
                        QuestionText = dr.GetString("question")
                    };
                    result.Add(q);
                }
            }
            return result;
        }

        internal TestInfo AddTest(string testTitile, Users user)
        {
            int id = MySqlDB.Instance.GetAutoID("test");
            string sql = "INSERT INTO test VALUES (0, @testname, @userid, null)";
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            {
                mc.Parameters.AddWithValue("testname", testTitile);
                mc.Parameters.AddWithValue("userid", user.ID);
                mc.ExecuteNonQuery();
            }
            return new TestInfo { ID = id, Author = user.Surname, IDAuthor = user.ID, Name = testTitile, NumberOfQuestions = 0 };
        }

        internal List<Answer> GetAnswerByQuestion(int id)
        {
            List<Answer> result = new List<Answer>();
            string sql = "SELECT * FROM answers Where id_question = " + id;
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    Answer answer = new Answer
                    {
                        ID = dr.GetInt32("id_answer"),
                        Correct = dr.GetBoolean("right_answer"),
                        Title = dr.GetString("text_answer")
                    };

                }
            }
            return result;
        }

        internal void UpdateQuestion(Questions questions)
        {
            // обновление текста вопроса
            string sql = "UPDATE questions set question = @qtext where id = " + questions.ID;
            using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
            {
                mc.Parameters.AddWithValue("qtext", questions.QuestionText);
                mc.ExecuteNonQuery();
            }
            var oldAnswers = GetAnswerByQuestion(questions.ID);

            // удаление удаленных вариантов ответов
            var toRemove = oldAnswers.Select(s => s.ID).Except(questions.Answers.Where(s => s.ID != 0).Select(s => s.ID));
            if (toRemove.Count() > 0)
            {
                foreach (var rId in toRemove)
                {
                    sql = "delete from answers where id_answer = " + rId;
                    using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
                        mc.ExecuteNonQuery();
                }
            }

            foreach (var answer in questions.Answers)
            {
                // если ответ есть в бд, обновляем его через update
                if (answer.ID != 0)
                    sql = "UPDATE answers SET text_answer = @atext, right_answer = @correct, id_question = @qid WHERE id_answer = " + answer.ID;
                else // иначе создаем новый ответ
                    sql = "INSERT INTO answers VALUES (0, @atext, @correct, @qid)";
                using (var mc = new MySqlCommand(sql, MySqlDB.Instance.GetConnection()))
                {
                    mc.Parameters.AddWithValue("atext", answer.Title);
                    mc.Parameters.AddWithValue("correct", answer.Correct);
                    mc.Parameters.AddWithValue("qid", questions.ID);
                    mc.ExecuteNonQuery();
                }
            }
        }

        internal void AddQuestion(object questions, int iD)
        {
            throw new NotImplementedException();
        }
    }
}
