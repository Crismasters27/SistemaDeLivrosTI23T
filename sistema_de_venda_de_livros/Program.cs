using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeLivros
{
    class Program
    {
        static void Main(string[] args)
        {
            DAOFuncionario daoFunc = new DAOFuncionario();
            bool logado = false;

            // ---- MENU PRÉ-LOGIN ----
            int opcaoLogin = -1;
            while (!logado)
            {
                Console.WriteLine("\n------ SISTEMA DE LIVROS ------");
                Console.WriteLine("1. Logar");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha: ");
                opcaoLogin = Convert.ToInt32(Console.ReadLine());

                if (opcaoLogin == 0)
                {
                    Console.WriteLine("Encerrando sistema...");
                    return;
                }

                if (opcaoLogin == 1)
                {
                    Console.Write("\nLogin: ");
                    string login = Console.ReadLine();

                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    if (daoFunc.Autenticar(login, senha))
                    {
                        logado = true;
                        Console.WriteLine("\nLogin realizado com sucesso! Bem-vindo!");
                    }
                    else
                    {
                        Console.WriteLine("\nLogin ou senha incorretos! Tente novamente.");
                    }
                }
            } // FIM DO WHILE LOGIN

            //MENU PRINCIPAL
            int opcao = -1;
            do
            {
                Console.WriteLine("\n---- MENU PRINCIPAL -----");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Clientes");
                Console.WriteLine("2. Funcionários");
                Console.WriteLine("3. Livros");
                Console.WriteLine("4. Compras");
                Console.WriteLine("5. Reservas");
                Console.Write("Escolha: ");
                opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando sistema...");
                        break;

                    case 1: // CLIENTES
                        CONTROLCliente controlCliente = new CONTROLCliente();
                        controlCliente.ExecutarOperacoes();
                        break;

                    case 2: // FUNCIONÁRIOS
                        CONTROLFuncionario controlFuncionario = new CONTROLFuncionario();
                        controlFuncionario.ExecutarOperacoes();
                        break;

                    case 3: // LIVROS
                        CONTROLLivro controlLivro = new CONTROLLivro();
                        controlLivro.ExecutarOperacoes();
                        break;

                    case 4: // COMPRAS
                        CONTROLCompra controlCompra = new CONTROLCompra();
                        controlCompra.ExecutarOperacoes();
                        break;

                    case 5: // RESERVAS
                        CONTROLReserva controlReserva = new CONTROLReserva();
                        controlReserva.ExecutarOperacoes();
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO MAIN
    } // FIM DA CLASSE PROGRAM
} // FIM DO PROJETO

