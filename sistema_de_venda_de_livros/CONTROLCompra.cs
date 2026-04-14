using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaDeLivros
{
    class CONTROLCompra
    {
        public int opcao;
        public DAOCompra dao = new DAOCompra();

        public void MostrarMenu()
        {
            Console.WriteLine("\n----MENU LIVRARIA (COMPRAS)----\n" +
                "0. Voltar\n" +
                "1. Listar Compras\n" +
                "2. Excluir Compra\n");

            this.opcao = Convert.ToInt32(Console.ReadLine());
        } // FIM DO MOSTRAR MENU
        public void EfetuarCompra(int[] carrinhoLivro, int[] carrinhoQtd, decimal[] carrinhoPreco, int totalItens, decimal valorFinal)
        {
            Console.WriteLine("\n--- EFETUAR COMPRA ---");

            DAOCliente daoCliente = new DAOCliente();
            daoCliente.ListarClientes();
            Console.Write("\nDigite o código do cliente: ");
            int ClienteCodigo = Convert.ToInt32(Console.ReadLine());

            DAOFuncionario daoFunc = new DAOFuncionario();
            daoFunc.ListarFuncionarios();
            Console.Write("\nDigite o código do funcionário responsável: ");
            int FuncionarioCodigo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nForma de pagamento:");
            Console.WriteLine("1. Cartão de Crédito");
            Console.WriteLine("2. Cartão de Débito");
            Console.WriteLine("3. Pix");
            Console.Write("Escolha: ");
            int formaPagamentoOpcao = Convert.ToInt32(Console.ReadLine());

            string formaPagamento = "";
            switch (formaPagamentoOpcao)
            {
                case 1: formaPagamento = "Cartão de Crédito"; break;
                case 2: formaPagamento = "Cartão de Débito"; break;
                case 3: formaPagamento = "Pix"; break;
                default: formaPagamento = "Outros"; break;
            }

            Console.Write("\nConfirmar compra? (S/N): ");
            string confirmar = Console.ReadLine().ToUpper();

            if (confirmar == "S")
            {
                // insere a compra
                dao.InserirCompra(valorFinal, formaPagamento, ClienteCodigo, FuncionarioCodigo);

                // pega o código da compra recém inserida
                this.dao.comando = "SELECT LAST_INSERT_ID()";
                MySqlCommand sql = new MySqlCommand(this.dao.comando, this.dao.conexao);
                int codigoNovaCompra = Convert.ToInt32(sql.ExecuteScalar());

                // para cada livro do carrinho
                DAOLivro daoLivro = new DAOLivro();
                daoLivro.PreencherVetor();

                for (int j = 0; j < totalItens; j++)
                {
                    // insere na tabela ter
                    dao.InserirTer(codigoNovaCompra, ClienteCodigo, FuncionarioCodigo, carrinhoLivro[j]);

                    // desconta do estoque
                    for (int k = 0; k < daoLivro.contar; k++)
                    {
                        if (daoLivro.codigoLivro[k] == carrinhoLivro[j])
                        {
                            daoLivro.AtualizarLivro(
                                carrinhoLivro[j],
                                daoLivro.descricao[k],
                                daoLivro.editora[k],
                                daoLivro.quantidade[k] - carrinhoQtd[j],
                                daoLivro.precoUnitario[k]
                            );
                            break;
                        }
                    }
                }
                Console.WriteLine("Compra finalizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Compra cancelada.");
            }
        } // FIM DO EFETUAR COMPRA


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
                        dao.ListarCompras();
                        break;

                    case 2: // EXCLUIR
                        Console.WriteLine("\n--- EXCLUIR COMPRA ---");
                        dao.ListarCompras();

                        Console.Write("\nDigite o código da compra a excluir: ");
                        int codExcluir = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Tem certeza? (S/N): ");
                        string confirmacao = Console.ReadLine().ToUpper();

                        if (confirmacao == "S")
                            dao.ExcluirCompra(codExcluir);
                        else
                            Console.WriteLine("Exclusão cancelada.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO EXECUTAR OPERACOES

    } // FIM DA CLASSE CONTROL COMPRA
} // FIM DO PROJETO