using System;
using System.Collections.Generic;

namespace Roster.Core.Models
{
    public class ApplicationFormDto
    {
        public string nickname { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public string biNickname { get; set; }
        public string steamId { get; set; }
        public string gmail { get; set; }
        public string githubNickname { get; set; }
        public string discordId { get; set; }
        public string teamspeakId { get; set; }
        public ICollection<Arma3Dlc> ownedDlcs { get; set; }
    }
}