﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public interface IHandleBankAccountChanges
    {
        void OnAmountDeposited(string BankAccountId,decimal Amount);
        void OnAmountWithDrawn(string BankAccountId,decimal Amount);
    }

    public class BankAccount:IHandleBankAccountChanges
    {
        private string id;
        private IHandleBankAccountChanges[] relatedinstances;
        private decimal balance;

        public BankAccount(string id, params IHandleBankAccountChanges[] relatedinstances)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.relatedinstances = relatedinstances;
        }

        public void Deposit(decimal Amount)
        {
            Guard.Against(Amount <= 0, "You can not deposit an amount smaller or equal to zero");
            Apply(x=>x.OnAmountDeposited(id,Amount));
        }

        public void Withdraw(decimal Amount)
        {
            Guard.Against(Amount <= 0, "You can not withdraw an amount smaller or equal to zero");
            Guard.Against(Amount > balance, "You can not withdraw more then the current balance");
            Apply(x => x.OnAmountWithDrawn(id,Amount));
        }

        void Apply(Action<IHandleBankAccountChanges> change)
        {
            change(this);
            foreach(var instance in relatedinstances)
                change(instance);
        }

        static class Guard {
            public static void Against(bool assertion, string message)
            {
                if (assertion)
                    throw new InvalidOperationException(message);
            }
        }

        void IHandleBankAccountChanges.OnAmountDeposited(string BankAccountId, decimal Amount)
        {
            balance += Amount;
        }

        void IHandleBankAccountChanges.OnAmountWithDrawn(string BankAccountId, decimal Amount)
        {
            balance -= Amount;
        }

    }
}