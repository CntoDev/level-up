using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core
{
    public class ApplicationFormFactory
    {
        public ApplicationForm create(ICollection<string> existingNicknames, string nickname, DateTime dateOfBirth, string email)
        {
            if(existingNicknames.Contains(nickname)) {
                throw new ArgumentException("Nickname already exists");
            }

            return new ApplicationForm(nickname, dateOfBirth, email);
        }
    }
}