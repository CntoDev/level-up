using System;

namespace Roster.Core.Domain
{
    public struct MemberDischarge
    {
        public MemberDischarge(DateTime dateOfDischarge, string reason)
        {
            DateOfDischarge = dateOfDischarge;
            Reason = reason;
        }

        public DateTime DateOfDischarge { get; }
        
        public string Reason { get; }
    }
}