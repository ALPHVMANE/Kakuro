using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class User
    {
        // Required properties for a User
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Note: In production, store Hashes only!

        public int Score { get; set; }
        public int LevelsCompleted { get; set; }

        // Multi-purpose constructor
        public User(string email, string username, string password,
                    int score, int levelsCompleted)
        {
            Email = email;
            Username = username;
            Password = password;
            Score = score;
            LevelsCompleted = levelsCompleted;
        }

        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
            Score = 0;
            LevelsCompleted = 0;
        }
    }
}