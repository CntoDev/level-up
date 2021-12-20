using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Core.Domain;

namespace Roster.Core
{
    public class MemberNicknameFactory
    {
        public static MemberNickname CreateForApplicant(ICollection<string> existingNicknames, string nickname)
        {
            if(existingNicknames.Any(x => x.Equals(nickname, StringComparison.OrdinalIgnoreCase))) {
                throw new ArgumentException("Nickname already exists");
            }

            return new MemberNickname(nickname);
        }
    }
}