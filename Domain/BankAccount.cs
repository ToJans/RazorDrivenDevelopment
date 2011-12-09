using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public interface IBankAccountEvents
    {
        void AmountDeposited(decimal Amount);
        void AmountWithDrawn(decimal Amount);
    }

    public class BankAccount:IBankAccountEvents
    {
        private string id;
        private IBankAccountEvents events;
        private decimal balance;

        public BankAccount(string id, IBankAccountEvents events)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.events = events;
        }

        public void Deposit(decimal Amount)
        {
            Guard.Against(Amount <= 0, "You can not deposit an amount smaller or equal to zero");
            Apply(x=>x.AmountDeposited(Amount));
        }

        public void Withdraw(decimal Amount)
        {
            Guard.Against(Amount <= 0, "You can not withdraw an amount smaller or equal to zero");
            Guard.Against(Amount > balance, "You can not withdraw more then the current balance");
            Apply(x => x.AmountWithDrawn(Amount));
        }

        void Apply(Action<IBankAccountEvents> act)
        {
            act(this);
            act(events);
        }

        static class Guard {
            public static void Against(bool assertion, string message)
            {
                if (assertion)
                    throw new InvalidOperationException(message);
            }
        }

        void IBankAccountEvents.AmountDeposited(decimal Amount)
        {
            balance += Amount;
        }

        void IBankAccountEvents.AmountWithDrawn(decimal Amount)
        {
            balance -= Amount;
        }

    }
}