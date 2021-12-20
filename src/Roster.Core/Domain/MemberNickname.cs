using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Roster.Core.Domain
{
    public readonly struct MemberNickname
    {
        public string Nickname { get; }

        public MemberNickname(string nickname)
        {
            Validate(nickname);
            Nickname = nickname;
        }

        // Validate nickname based on community spec. For reference, check the Interview Handbook.
        private static void Validate(string nickname)
        {
            // Maximum length is 10 characters.
            if(nickname.Length is < 1 or > 10) {
                throw new ArgumentException("Nickname must be between 1 and 10 characters long");
            }
            // Maximum 1 whitespace.
            if(nickname.Count(c => (c == ' ')) > 1) {
                throw new ArgumentException("Nickname cannot contain more than 1 whitespaces");
            }
            // Only letters are allowed.
            Regex lettersOnly = new("^[a-zA-Z]*$");
            if(!lettersOnly.IsMatch(nickname)) {
                throw new ArgumentException("Nickname cannot contain numbers or symbols");
            }
            // If longer than 3 characters, cannot be all caps.
            Regex capsLetters = new("^[A-Z]*$");
            if(nickname.Length > 3 && capsLetters.IsMatch(nickname)) {
                throw new ArgumentException("Nickname cannot be all caps if longer than three characters");
            }
            // First letter of every word must be capitalised.
            foreach(var word in nickname.Split(' ')) {
                if(!Char.IsUpper(word[0])) {
                    throw new ArgumentException("First letter must be capitalised");
                }
            }
        }

        public override string ToString() => Nickname;
    }
}