using dtOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dtUser
{
    public class User
    {
        private float _walletAmmount;
        private List<Transaction>_transactionsHistory;
        private string _name;
        private string _id;

        public User(string name, string id)
        {
            _name = name;
            _id = id;
            _walletAmmount = 0;
            // Só sera vazio se não existir o usuário, caso contrário, achar no banco e preencher esses dados
            _transactionsHistory = new List<Transaction> { };
        }
        override public string ToString()
        {
            return $"# Nome\n#  {_name}\n# Id\n#  {_id}\n# Fundos\n#  {_walletAmmount}\n# Última transação  \n{RetreiveLastTransaction()}";
        }
        public Transaction Transact(Transaction transaction)
        {
            TransactionType transactionType = transaction.GetTransactionType();
            float amount = transaction.GetTransactionAmount();
            
            if (transactionType == TransactionType.Credit)
            {
                _walletAmmount += amount;
            }
            else if (transactionType == TransactionType.Debit)
            {
                _walletAmmount -= amount;
            }
            AddTransactionToHistory(transaction);
            return transaction;
        }
        private void AddTransactionToHistory(Transaction transaction)
        {
            _transactionsHistory.Add(transaction);
        }
        /// <summary>
        /// Retreive user's last transaction
        /// </summary>
        /// <returns>The last Transaction if it exists, otherwise, return a Transaction of amount 0</returns>
        public Transaction RetreiveLastTransaction()
        {
            // Esse tratamento fica aqui ou temq ser capturado pela UI?
            try
            {
                return _transactionsHistory.Last();
            }
            catch (Exception)
            {
                return new Transaction(0, "", "", TransactionType.Credit);
                throw;
            }
        }
        public List<Transaction> RetreiveTransactionHistory()
        {
            return _transactionsHistory;
        }
        public float RetreiveWalletAmount()
        {
            return _walletAmmount;
        }
    }
}
