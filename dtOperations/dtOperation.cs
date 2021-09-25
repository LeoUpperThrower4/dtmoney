using System;

namespace dtOperations
{
    public class Transaction
    {
        private float _amount;
        private string _catergory;
        private string _title;
        private TransactionType _type;
        public Transaction(float amount, string category, string title, TransactionType type)
        {
            _amount = amount;
            _catergory = category;
            _title = title;
            _type = type;
        }

        public override string ToString()
        {
            return $"Título: {_title}\nValor: {_amount}\nTipo: {_type}";
        }
        public TransactionType GetTransactionType()
        {
            return _type;
        }
        public float GetTransactionAmount()
        {
            return _amount;
        }
    }
    public enum TransactionType
    {
        Credit,
        Debit
    }
}
