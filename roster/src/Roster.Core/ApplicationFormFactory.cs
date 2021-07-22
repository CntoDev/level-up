using System;
using System.Collections.Generic;
using Roster.Core.Domain;
using System.Linq;

namespace Roster.Core
{
    public class ApplicationFormFactory
    {
        public ApplicationForm Create(ICollection<string> existingNicknames, string nickname, DateTime dateOfBirth, string email)
        {
            if(existingNicknames.Any(x => x.Equals(nickname, StringComparison.OrdinalIgnoreCase))) {
                throw new ArgumentException("Nickname already exists");
            }

            return new ApplicationForm(nickname, dateOfBirth, email);
        }
    }
}