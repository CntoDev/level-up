using System;

namespace Roster.Core.Domain
{
    struct MemberDischarge
    {
        internal MemberDischarge(DateTime dateOfDischarge, DischargePath dischargePath, string comment)
        {
            DateOfDischarge = dateOfDischarge;
            DischargePath = dischargePath;
            Comment = comment;
        }

        internal DateTime DateOfDischarge { get; }
        
        internal DischargePath DischargePath { get; }

        internal string Comment { get; }
    }
}