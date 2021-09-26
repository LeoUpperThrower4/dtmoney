using System;
using System.Collections.Generic;
using dtOperations;
using System.Linq;
using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Types;

namespace databaseManager
{
    public class FaunaUser
    {
        private string _name;
        private string _id;
        private float _wallet;
        private List<Transaction> _transactionsList;
        
        /// <summary>
        /// Creates a FaunaUser. This is the type used to represent a user in the "users" collection on FaunaDB
        /// </summary>
        /// <param name="name">The name of the user</param>
        /// <param name="id">The unique ID of the user</param>
        /// <param name="wallet">The user's wallet amount</param>
        /// <param name="transactionsList">A list containing every transaction made by the user</param>
        public FaunaUser(string name, string id, float wallet, List<Transaction> transactionsList)
        {
            _name = name;
            _id = id;
            _wallet = wallet;
            _transactionsList = transactionsList;
        }

        // Getter functions
        public string GetName()
        {
            return _name;
        }

        public string GetId()
        {
            return _id;
        }

        public float GetWallet()
        {
            return _wallet;
        }

        public List<Transaction> GetTransactions()
        {
            return _transactionsList;
        }

        /// <summary>
        /// Formalizes a transaction by updating the user's transactions history in the database.
        /// </summary>
        /// <param name="transaction">The transaction that will be added to the transactions list</param>
        /// <returns>The transaction that was added to the list</returns>
        public async Task<Transaction> Transact(Transaction transaction)
        {
            TransactionType transactionType = transaction.GetTransactionType();
            float amount = transaction.GetTransactionAmount();

            if (transactionType == TransactionType.Credit)
            {
                _wallet += amount;
            }
            else if (transactionType == TransactionType.Debit)
            {
                _wallet -= amount;
            }

            _transactionsList.Add(transaction);

            FaunaUser updatedUser = new FaunaUser(_name, _id, _wallet, _transactionsList);

            await Database.UpdateUser(_id, updatedUser);

            return transaction;
        }
        
        /// <summary>
        /// Function that returns the last transaction made by the user
        /// </summary>
        /// <returns>A credit transaction of amount 0 if no transactions were ever made by the user. The last transaction is returned, otherwise</returns>
        public Transaction RetreiveLastTransaction()
        {
            // Esse tratamento fica aqui ou tem q ser capturado pela UI?
            try
            {
                return _transactionsList.Last();
            }
            catch (Exception)
            {
                return new Transaction(0, "","Nenhuma transação feita até o momento.", TransactionType.Credit);
                throw;
            }
        }
        override public string ToString()
        {
            return $"# Nome\n#  {_name}\n# Id\n#  {_id}\n# Fundos\n#  {_wallet}\n# Última transação  \n{RetreiveLastTransaction()}";
        }
    }
}
