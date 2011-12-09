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
            var id = "Account/1";
            self.Events = A.Fake<IBankAccountEvents>();
            self.Account = new BankAccount(id, self.Events);
            return self;
        }
    }

    public class Context
    {
        public Context WithSomeMoneyOnIt(decimal money)
        {
            (Account as IBankAccountEvents).AmountDeposited(20);
            return this;
        }

        public IBankAccountEvents Events { get; set; }

        public BankAccount Account { get; set; }
    }
}

