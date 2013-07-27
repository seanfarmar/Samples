using System;

namespace SimpleMatrix.InternalMessages.AccountManagement
{
    public class CreateAccount
    {
        public Guid AccountId { get; set; }

        public string Email { get; set; }   
    }
}
