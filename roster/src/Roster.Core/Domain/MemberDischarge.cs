using System;

namespace Roster.Core.Domain
{
    public record MemberDischarge
    {
        private MemberDischarge() { }
        
        internal MemberDischarge(DateTime dateOfDischarge, DischargePath dischargePath, string comment)
        {
            DateOfDischarge = dateOfDischarge;
            DischargePath = dischargePath;
            Comment = comment;
        }

        public DateTime DateOfDischarge { get; }

        public DischargePath DischargePath { get; }

        public string Comment { get; }
    }
}