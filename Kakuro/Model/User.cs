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
        private string rank;

        public User(string username, string email, string password, int score, string rank)
        {
            this.username = username;
            this.email = email;
            this.Password = password;
            this.score = score;
            this.rank = rank;
        }

        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Score { get => score; set => score = value; }
        public string Rank { get => rank; set => rank = value; }


    }
}