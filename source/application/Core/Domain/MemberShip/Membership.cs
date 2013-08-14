using System;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain.MemberShip
{
    public class Membership : AggregateRoot
    {
        public DateTime? CreateDate { get; protected set; }
        public string ConfirmationToken { get; protected set; }
        public bool? IsConfirmed { get; protected set; }
        public DateTime? LastPasswordFailureDate { get; protected set; }
        public int PasswordFailuresSinceLastSuccess { get; protected set; }
        public string Password { get; protected set; }
        public DateTime? PasswordChangedDate { get; protected set; }
        public string PasswordSalt { get; protected set; }
        public string PasswordVerificationToken { get; protected set; }
        public DateTime? PasswordVerificationTokenExpirationDate { get; protected set; } 
    }
}
