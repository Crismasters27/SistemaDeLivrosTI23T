using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaDeLivros
{
    class CONTROLReserva
    {
        public int opcao;
        public DAOReserva dao = new DAOReserva();

        public void MostrarMenu()
        {
            Console.WriteLine("\n----MENU LIVRARIA (RESERVAS)----\n" +
                "0. Voltar\n" +
                "1. Listar Reservas\n" +
                "2. Excluir Reserva\n");

            this.opcao = Convert.ToInt32(Console.ReadLine());
        } // FIM DO MOSTRAR MENU

        public void EfetuarReserva(int idLivro)
        {
            Console.WriteLine("\n--- EFETUAR RESERVA ---");

            // lista clientes para escolher
            DAOCliente daoCliente = new DAOCliente();
            daoCliente.ListarClientes();
            Console.Write("\nDigite o código do cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            // lista funcionários para escolher
            DAOFuncionario daoFunc = new DAOFuncionario();
            daoFunc.ListarFuncionarios();
            Console.Write("\nDigite o código do funcionário responsável: ");
            int idFuncionario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nResumo da reserva:");
            Console.WriteLine($"Livro código:      {idLivro}");
            Console.WriteLine($"Cód. Cliente:      {idCliente}");
            Console.WriteLine($"Cód. Funcionário:  {idFuncionario}");
            Console.WriteLine($"Data:              {DateTime.Now:dd/MM/yyyy}");
            Console.Write("\nConfirmar reserva? (S/N): ");
            string confirmar = Console.ReadLine().ToUpper();

            if (confirmar == "S")
            {
                dao.InserirReserva(idCliente, idFuncionario);

                // pega o id da reserva recém inserida
                this.dao.comando = "SELECT LAST_INSERT_ID()";
                MySqlCommand sql = new MySqlCommand(this.dao.comando, this.dao.conexao);
                int idNovaReserva = Convert.ToInt32(sql.ExecuteScalar());

                dao.InserirTer(idNovaReserva, idCliente, idFuncionario, idLivro);

                Console.WriteLine("Reserva finalizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Reserva cancelada.");
            }
        } // FIM DO EFETUAR RESERVA

        public void ExecutarOperacoes()
        {
            do
            {
                this.MostrarMenu();

                switch (this.opcao)
                {
                    case 0:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;

                    case 1: // LISTAR
                        dao.ListarReservas();
                        break;

                    case 2: // EXCLUIR
                        Console.WriteLine("\n--- EXCLUIR RESERVA ---");
                        dao.ListarReservas();

                        Console.Write("\nDigite o código da reserva a excluir: ");
                        int codExcluir = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Tem certeza? (S/N): ");
                        string confirmacao = Console.ReadLine().ToUpper();

                        if (confirmacao == "S")
                            dao.ExcluirReserva(codExcluir);
                        else
                            Console.WriteLine("Exclusão cancelada.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO EXECUTAR OPERACOES

    } // FIM DA CLASSE CONTROL RESERVA
} // FIM DO PROJETO
