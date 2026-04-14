using SistemaDeLivros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeLivros
{
    class CONTROLLivro
    {
        public int opcao;
        public DAOLivro dao = new DAOLivro();

        public void MostrarMenu()
        {
            Console.WriteLine("\n----MENU LIVRARIA (LIVROS)----\n" +
                "0. Voltar\n" +
                "1. Adicionar Livro\n" +
                "2. Listar Livros\n" +
                "3. Atualizar Livro\n" +
                "4. Excluir Livro\n" +
                "5. Comprar/Reservar Livro\n");

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
                        Console.WriteLine("\n--- ADICIONAR LIVRO ---");

                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Descrição: ");
                        string descricao = Console.ReadLine();

                        Console.Write("Editora: ");
                        string editora = Console.ReadLine();

                        Console.Write("Quantidade em estoque: ");
                        int quantidade = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Preço Unitário: ");
                        decimal preco = Convert.ToDecimal(Console.ReadLine());

                        dao.InserirLivro(nome, descricao, editora, quantidade, preco);
                        break;

                    case 2: // LISTAR
                        dao.ListarLivros();
                        break;

                    case 3: // ATUALIZAR
                        Console.WriteLine("\n--- ATUALIZAR LIVRO ---");
                        dao.ListarLivros();

                        Console.Write("\nDigite o código do livro a atualizar: ");
                        int codAtualizar = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Nova Descrição: ");
                        string novaDescricao = Console.ReadLine();

                        Console.Write("Nova Editora: ");
                        string novaEditora = Console.ReadLine();

                        Console.Write("Nova Quantidade: ");
                        int novaQuantidade = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Novo Preço: ");
                        decimal novoPreco = Convert.ToDecimal(Console.ReadLine());

                        dao.AtualizarLivro(codAtualizar, novaDescricao, novaEditora, novaQuantidade, novoPreco);
                        break;

                    case 4: // EXCLUIR
                        Console.WriteLine("\n--- EXCLUIR LIVRO ---");
                        dao.ListarLivros();

                        Console.Write("\nDigite o código do livro a excluir: ");
                        int codExcluir = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Tem certeza? (S/N): ");
                        string confirmacao = Console.ReadLine().ToUpper();

                        if (confirmacao == "S")
                            dao.ExcluirLivro(codExcluir);
                        else
                            Console.WriteLine("Exclusão cancelada.");
                        break;

                    case 5: // COMPRAR / RESERVAR
                        Console.WriteLine("\n--- COMPRAR / RESERVAR LIVRO ---");

                        // vetores temporários do carrinho
                        int[] carrinhoLivro = new int[100];
                        int[] carrinhoQtd = new int[100];
                        decimal[] carrinhoPreco = new decimal[100];
                        int totalItens = 0;
                        decimal valorFinal = 0;
                        string adicionarMais = "S";

                        while (adicionarMais == "S")
                        {
                            dao.ListarLivros();

                            Console.Write("\nDigite o código do livro desejado: ");
                            int codLivro = Convert.ToInt32(Console.ReadLine());

                            // encontra o índice do livro
                            int indice = -1;
                            for (int j = 0; j < dao.contar; j++)
                            {
                                if (dao.codigoLivro[j] == codLivro)
                                {
                                    indice = j;
                                    break;
                                }
                            }

                            if (indice == -1)
                            {
                                Console.WriteLine("Livro não encontrado!");
                            }
                            else if (dao.quantidade[indice] > 0)
                            {
                                Console.WriteLine($"Estoque disponível: {dao.quantidade[indice]}");
                                Console.Write("Quantos exemplares deseja? ");
                                int qtdDesejada = Convert.ToInt32(Console.ReadLine());

                                if (qtdDesejada > dao.quantidade[indice])
                                {
                                    Console.WriteLine($"Quantidade insuficiente! Temos apenas {dao.quantidade[indice]} em estoque.");
                                    Console.Write("Deseja reservar? (S/N): ");
                                    string reservarResto = Console.ReadLine().ToUpper();
                                    if (reservarResto == "S")
                                    {
                                        CONTROLReserva controlReserva = new CONTROLReserva();
                                        controlReserva.EfetuarReserva(codLivro);
                                    }
                                }
                                else
                                {
                                    // adiciona ao carrinho
                                    carrinhoLivro[totalItens] = codLivro;
                                    carrinhoQtd[totalItens] = qtdDesejada;
                                    carrinhoPreco[totalItens] = dao.precoUnitario[indice] * qtdDesejada;
                                    valorFinal += carrinhoPreco[totalItens];
                                    totalItens++;
                                    Console.WriteLine($"Livro adicionado! Subtotal: {carrinhoPreco[totalItens - 1]:C}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Livro indisponível no estoque!");
                                Console.Write("Deseja reservar? (S/N): ");
                                string reservar = Console.ReadLine().ToUpper();
                                if (reservar == "S")
                                {
                                    CONTROLReserva controlReserva = new CONTROLReserva();
                                    controlReserva.EfetuarReserva(codLivro);
                                }
                            }

                            Console.Write("\nDeseja adicionar mais livros? (S/N): ");
                            adicionarMais = Console.ReadLine().ToUpper();

                        } // FIM DO WHILE CARRINHO

                        if (totalItens > 0)
                        {
                            Console.WriteLine("\n------ RESUMO DO CARRINHO ------");
                            for (int j = 0; j < totalItens; j++)
                            {
                                Console.WriteLine($"Livro código: {carrinhoLivro[j]} | Qtd: {carrinhoQtd[j]} | Subtotal: {carrinhoPreco[j]:C}");
                            }
                            Console.WriteLine($"VALOR TOTAL: {valorFinal:C}");
                            Console.Write("\nDeseja finalizar a compra? (S/N): ");
                            string finalizar = Console.ReadLine().ToUpper();

                            if (finalizar == "S")
                            {
                                CONTROLCompra controlCompra = new CONTROLCompra();
                                controlCompra.EfetuarCompra(carrinhoLivro, carrinhoQtd, carrinhoPreco, totalItens, valorFinal);
                            }
                            else
                            {
                                Console.WriteLine("Compra cancelada.");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;

                } // FIM DO SWITCH

            } while (opcao != 0);

        } // FIM DO EXECUTAR OPERACOES

    } // FIM DA CLASSE CONTROL LIVRO
} // FIM DO PROJETO