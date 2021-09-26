using databaseManager;
using dtOperations;
using FaunaDB.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtTerminalDashboard
{
    class Program
    {
        
        static FaunaUser currentUser;
        static bool programEnded = false;

        private static void Introduction()
        {
            TerminalHelperFunctions.ShowUserInfo("Seja bem vindo(a) ao dt.money!");
        }

        private static async Task<FaunaUser> SignUp()
        {
            string nome;
            string id;
            float walletAmount;
            
            TerminalHelperFunctions.WarnUser("Legal! Então vamos começar!");
            
            while (true)
            {
                nome = TerminalHelperFunctions.AskUser("\nQual seu nome? ");
                if (nome == "")
                {
                    TerminalHelperFunctions.WarnUser("Por favor, digite algo.");
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                id = TerminalHelperFunctions.AskUser("\nDigite um ID? ");
                // Verificar se já existe
                if (id == "" || false)
                {
                    TerminalHelperFunctions.WarnUser("Por favor, digite algo.");
                }
                else
                {
                    break;
                }
            }
            while (!float.TryParse(TerminalHelperFunctions.AskUser("\nPara finalizar, qual seu saldo inicial na carteira?\nObs: para R$ 4.563,21 digite 4563.21", false), out walletAmount))
            {
                TerminalHelperFunctions.WarnUser("Oops. Formatação errada, tente novamente.");
            }
            
            FaunaUser user = new FaunaUser(nome, id, walletAmount, new ());
            
            await Database.CreateUser(user);

            return user;
        }

        private static async Task<FaunaUser> Login(string id = "")
        {
            //Depois tem q verificar pra ver se o usuário existe ou não
            if (id == "")
            {
                id = TerminalHelperFunctions.AskUser("Qual sua identificação? ");
            }
            FaunaUser user = await Database.GetUser(id);
            currentUser = user;
            return user;
        }

        private static async Task Operation(int operation)
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
                                await currentUser.Transact(transaction);
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
                    TerminalHelperFunctions.ShowUserInfo($"Você tem R$ {currentUser.GetWallet()} reais.");
                    break;
                case 4:
                    List<Transaction> transactions = currentUser.GetTransactions();
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

        private static async Task HandleSelectOperation(int operation)
        {
            int[] availableOperations = new int[]{ 1, 2, 3, 4, 5, 6 };
            if (availableOperations.Contains(operation))
            {
                await Operation(operation);
            }
            else
            {
                TerminalHelperFunctions.WarnUser("Operação inválida. Tente novamente.");
            }
        }
        
        static async Task Main(string[] args)
        {
            Introduction();

            string userId = TerminalHelperFunctions.AskUser("Se você já tem um ID, digite-o, senão, pressione enter para iniciarmos o cadastro.", false);
            if (userId == "")
            {
                FaunaUser user = await SignUp();
                userId = user.GetId();
            }
            Console.WriteLine("Logando...");
            try
            {
                currentUser = await Login(userId);
            }
            catch (Exception ex)
            {
                TerminalHelperFunctions.WarnUser($"Erro inesperado no login. Finalizando o programa.\n{ex}");
                return;
            }

            TerminalHelperFunctions.ShowUserInfo($"Olá {currentUser.GetName()}");

            while (!programEnded)
            {
                string userChoice = TerminalHelperFunctions.AskUser("\nQual operação você gostaria de fazer?\n1 - Crédito\n2 - Débito\n3 - Checar fundos\n4 - Ver histórico de transações\n5 - Ver última transação\n6 - Minhas informações\nEnter para sair do programa...\nOperação escolhida: ");
                if(userChoice == "")
                {
                    break;
                }
                if (int.TryParse(userChoice, out int operation))
                {
                    await HandleSelectOperation (operation);
                }
                else
                {
                    TerminalHelperFunctions.WarnUser("Erro de leitura. Por favor, digite somente o número relativo à operação.");
                }
            }
            
        }
    }
}
