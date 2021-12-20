using System;

namespace Roster.Core.Domain
{
    public record MemberDischarge
    {
        private MemberDischarge() { }
        
        internal MemberDischarge(DateTime dateOfDischarge, DischargePath dischargePath, bool isAlumni, string comment)
        {
            DateOfDischarge = dateOfDischarge;
            DischargePath = dischargePath;
            IsAlumni = isAlumni;
            Comment = comment;
        }

        public DateTime DateOfDischarge { get; }

        public DischargePath DischargePath { get; }

        public bool IsAlumni { get; }

        public string Comment { get; }
    }
}