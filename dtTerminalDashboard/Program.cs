using dtOperations;
using dtUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dtTerminalDashboard
{
    class Program
    {
        static User currentUser;

        static bool programEnded = false;

        static void Introduction()
        {
            TerminalHelperFunctions.ShowUserInfo("Seja bem vindo(a) ao dt.money!");
        }
        static User Login()
        {
            //Depois tem q verificar pra ver se o usuário existe ou não
            string nome = TerminalHelperFunctions.AskUser("Qual seu nome? ");
            string id = TerminalHelperFunctions.AskUser("Qual sua identificação? ");
            return new User(nome, id);
        }
        static void Operation(int operation)
        {
            int tries = 0;
            switch (operation)
            {
                case 1:
                case 2:
                    while (true)
                    {
                        if (float.TryParse(TerminalHelperFunctions.AskUser("Qual a quantia da operação? \nPara R$ 4.563,21 digite 4563.21.\nDigite 0 para cancelar a operação.\nValor: "), out float amount))
                        {
                            if (amount == 0)
                            {
                                break;
                            }
                            else
                            {
                                string title = TerminalHelperFunctions.AskUser("Título da transação: ");
                                string category = TerminalHelperFunctions.AskUser("Categoria da transação: ");
                                TransactionType transactionType;
                                if (operation == 1)
                                {
                                    transactionType = TransactionType.Credit;
                                }
                                else
                                {
                                    transactionType = TransactionType.Debit;
                                }
                                Transaction transaction = new Transaction(amount, category, title, transactionType);
                                currentUser.Transact(transaction);
                                break;
                            }

                        }
                        else
                        {
                            tries += 1;
                            if (tries == 3)
                            {
                                TerminalHelperFunctions.WarnUser("Máximo de tentativas. Encerrando operação.");
                                break;
                            }
                            TerminalHelperFunctions.WarnUser("Informação mal-formatada. Tente novamente.");
                        }
                    }
                    break;
                case 3:
                    TerminalHelperFunctions.ShowUserInfo($"Você tem R$ {currentUser.RetreiveWalletAmount()} reais.");
                    break;
                case 4:
                    List<Transaction> transactions = currentUser.RetreiveTransactionHistory();
                    transactions.ForEach(transaction =>
                    {
                        TerminalHelperFunctions.ShowUserInfo(transaction.ToString());
                    }
                    );
                    break;
                case 5:
                    TerminalHelperFunctions.ShowUserInfo(currentUser.RetreiveLastTransaction().ToString());
                    break;
                case 6:
                    TerminalHelperFunctions.ShowUserInfo(currentUser.ToString());
                    break;
                
            }
        }
        static void HandleSelectOperation(int operation)
        {
            int[] availableOperations = new int[]{ 1, 2, 3, 4, 5, 6 };
            if (availableOperations.Contains(operation))
            {
                Operation(operation);
            }
            else
            {
                TerminalHelperFunctions.WarnUser("Operação inválida. Tente novamente.");
            }
        }
        static void Main(string[] args)
        {
            Introduction();
            
            currentUser = Login();

            while (!programEnded)
            {
                string userChoice = TerminalHelperFunctions.AskUser("\nQual operação você gostaria de fazer?\n1 - Crédito\n2 - Débito\n3 - Checar fundos\n4 - Ver histórico de transações\n5 - Ver última transação\n6 - Minhas informações\nEnter para sair do programa...\nOperação escolhida: ");
                if(userChoice == "")
                {
                    break;
                }
                if (int.TryParse(userChoice, out int operation))
                {
                    HandleSelectOperation(operation);
                }
                else
                {
                    TerminalHelperFunctions.WarnUser("Erro de leitura. Por favor, digite somente o número relativo à operação.");
                }
            }
            
        }
    }
}
