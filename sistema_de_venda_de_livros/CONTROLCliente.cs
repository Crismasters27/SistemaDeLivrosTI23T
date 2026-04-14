using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeLivros
{
    class CONTROLCliente
    {
        public int opcao;
        public DAOCliente dao = new DAOCliente();

        public void MostrarMenu()
        {
            Console.WriteLine("\n----MENU LIVRARIA (CLIENTE)----\n" +
                "0. Sair\n" +
                "1. Adicionar Cliente\n" +
                "2. Listar Clientes\n" +
                "3. Atualizar Cliente\n" +
                "4. Excluir Cliente\n");

            this.opcao = Convert.ToInt32(Console.ReadLine());
        } // FIM DO MOSTRAR MENU

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

                    case 1: // ADICIONAR
                        Console.WriteLine("\n--- ADICIONAR CLIENTE ---");
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Telefone: ");
                        string telefone = Console.ReadLine();

                        Console.Write("Endereço: ");
                        string endereco = Console.ReadLine();

                        Console.Write("Data de Nascimento (dd/MM/yyyy): ");
                        DateTime dtNascimento = Convert.ToDateTime(Console.ReadLine());

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Senha: ");
                        string senha = Console.ReadLine();

                        dao.InserirCliente(nome, telefone, endereco, dtNascimento, email, senha);
                        break;

                    case 2: // LISTAR
                        dao.ListarClientes();
                        break;

                    case 3: // ATUALIZAR
                        Console.WriteLine("\n--- ATUALIZAR CLIENTE ---");

                        dao.ListarClientes();

                        Console.Write("\nDigite o código do cliente a atualizar: ");
                        int codAtualizar = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Novo Nome: ");
                        string novoNome = Console.ReadLine();

                        Console.Write("Novo Telefone: ");
                        string novoTelefone = Console.ReadLine();

                        Console.Write("Novo Endereço: ");
                        string novoEndereco = Console.ReadLine();

                        Console.Write("Nova Data de Nascimento (dd/MM/yyyy): ");
                        DateTime novaDt = Convert.ToDateTime(Console.ReadLine());

                        Console.Write("Novo Email: ");
                        string novoEmail = Console.ReadLine();

                        Console.Write("Nova Senha: ");
                        string novaSenha = Console.ReadLine();

                        dao.AtualizarCliente(codAtualizar, novoNome, novoTelefone, novoEndereco, novaDt, novoEmail, novaSenha);
                        break;

                    case 4: // EXCLUIR
                        Console.WriteLine("\n--- EXCLUIR CLIENTE ---");

                        // mostra a lista primeiro
                        dao.ListarClientes();

                        Console.Write("\nDigite o código do cliente a excluir: ");
                        int codExcluir = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Tem certeza? (S/N): ");
                        string confirmacao = Console.ReadLine().ToUpper();

                        if (confirmacao == "S")
                            dao.ExcluirCliente(codExcluir);
                        else
                            Console.WriteLine("Exclusão cancelada.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO EXECUTAR OPERACOES

    } // FIM DA CLASSE CONTROL CLIENTE
} // FIM DO PROJETO