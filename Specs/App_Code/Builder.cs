using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeItEasy;
using Domain;

public class Builder
{
    public static Context ABankAccount
    {
        get
        {
            var self = new Context();
            self.AccountId = "Account/1";
            self.Changes = A.Fake<IHandleBankAccountChanges>();
            self.Account = new BankAccount(self.AccountId, self.Changes);
            return self;
        }
    }

    public class Context
    {
        public Context WithSomeMoneyOnIt(decimal Amount)
        {
            (Account as IHandleBankAccountChanges).OnAmountDeposited(AccountId,Amount);
            return this;
        }

        public IHandleBankAccountChanges Changes { get; set; }

        public BankAccount Account { get; set; }

        public string AccountId { get; set; }
    }
}

