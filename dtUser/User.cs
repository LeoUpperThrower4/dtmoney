using databaseManager;
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
        private List<Transaction> _transactionsHistory;
        private string _name;
        private string _id;

        public User(string name, string id)
        {
            _name = name;
            _id = id;
            _walletAmmount = 0;
            // Só será vazio se não existir o usuário, caso contrário, achar no banco e preencher esses dados
            _transactionsHistory = new List<Transaction> { };
        }
        
        
        /// <summary>
        /// Retreive user's last transaction
        /// </summary>
        /// <returns>The last Transaction if it exists, otherwise, return a Transaction of amount 0</returns>
        
        
    }
}
