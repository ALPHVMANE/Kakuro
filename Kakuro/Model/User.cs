using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class User
    {
        private string email;
        private string username;
        private string password;
        private int score;
        private int levelsCompleted;

        public User(string email, string username, string password)
        {
            this.email = email;
            this.username = username;
            this.password = password;
            this.score = 0; 
            this.levelsCompleted = 0;
        }

        public User(string username, string email, string password, int score, int levelsCompleted)
        {
            this.username = username;
            this.email = email;
            this.password = password;
            this.score = score;
            this.levelsCompleted = levelsCompleted;
        }

        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Score { get => score; set => score = value; }
        public int LevelsCompleted { get => levelsCompleted; set => levelsCompleted = value; }

        //private int CalculateRank()
        //{
        //    //Coming Soon....
        //}
    }
}