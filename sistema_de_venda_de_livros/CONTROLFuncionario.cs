using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeLivros
{
    class CONTROLFuncionario
    {
        public int opcao;
        public DAOFuncionario dao = new DAOFuncionario();

        public void MostrarMenu()
        {
            Console.WriteLine("\n----MENU LIVRARIA (FUNCIONÁRIO)----\n" +
                "0. Sair\n" +
                "1. Adicionar Funcionário\n" +
                "2. Listar Funcionários\n" +
                "3. Atualizar Funcionário\n" +
                "4. Excluir Funcionário\n");

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
                        Console.WriteLine("\n--- ADICIONAR FUNCIONÁRIO ---");
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Endereço: ");
                        string endereco = Console.ReadLine();

                        Console.Write("Telefone: ");
                        string telefone = Console.ReadLine();

                        Console.Write("Cargo: ");
                        string cargo = Console.ReadLine();

                        Console.Write("Salário: ");
                        decimal salario = Convert.ToDecimal(Console.ReadLine());

                        Console.Write("Login: ");
                        string login = Console.ReadLine();

                        Console.Write("Senha: ");
                        string senha = Console.ReadLine();

                        dao.InserirFuncionario(nome, endereco, telefone, cargo, salario, login, senha);
                        break;

                    case 2: // LISTAR
                        dao.ListarFuncionarios();
                        break;

                    case 3: // ATUALIZAR
                        Console.WriteLine("\n--- ATUALIZAR FUNCIONÁRIO ---");
                        dao.ListarFuncionarios();

                        Console.Write("\nDigite o código do funcionário a atualizar: ");
                        int codAtualizar = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Novo Nome: ");
                        string novoNome = Console.ReadLine();

                        Console.Write("Novo Endereço: ");
                        string novoEndereco = Console.ReadLine();

                        Console.Write("Novo Telefone: ");
                        string novoTelefone = Console.ReadLine();

                        Console.Write("Novo Cargo: ");
                        string novoCargo = Console.ReadLine();

                        Console.Write("Novo Salário: ");
                        decimal novoSalario = Convert.ToDecimal(Console.ReadLine());

                        Console.Write("Novo Login: ");
                        string novoLogin = Console.ReadLine();

                        Console.Write("Nova Senha: ");
                        string novaSenha = Console.ReadLine();

                        dao.AtualizarFuncionario(codAtualizar, novoNome, novoEndereco, novoTelefone, novoCargo, novoSalario, novoLogin, novaSenha);
                        break;

                    case 4: // EXCLUIR
                        Console.WriteLine("\n--- EXCLUIR FUNCIONÁRIO ---");
                        dao.ListarFuncionarios();

                        Console.Write("\nDigite o código do funcionário a excluir: ");
                        int codExcluir = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Tem certeza? (S/N): ");
                        string confirmacao = Console.ReadLine().ToUpper();

                        if (confirmacao == "S")
                            dao.ExcluirFuncionario(codExcluir);
                        else
                            Console.WriteLine("Exclusão cancelada.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO EXECUTAR OPERACOES

    } // FIM DA CLASSE CONTROL FUNCIONARIO
} // FIM DO PROJETO

