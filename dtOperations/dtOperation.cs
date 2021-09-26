using System;
using FaunaDB.Types;

namespace dtOperations
{
    public class FaunaTransaction
    {
        [FaunaField("amount")]
        private float _amount;
        [FaunaField("category")]
        private string _catergory;
        [FaunaField("title")]
        private string _title;
        [FaunaField("type")]
        private TransactionType _type;
        
        [FaunaConstructor]
        public FaunaTransaction(float amount, string category, string title, TransactionType type)
        {
            _amount = amount;
            _catergory = category;
            _title = title;
            _type = type;
        }

        public string GetCategory()
        {
            return _catergory;
        }
        
        public TransactionType GetTransactionType()
        {
            return _type;
        }
        
        public float GetTransactionAmount()
        {
            return _amount;
        }

        public override string ToString()
        {
            return $"Título: {_title}\nValor: {_amount}\nTipo: {_type}";
        }
    }
    public enum TransactionType
    {
        Credit,
        Debit
    }
}
