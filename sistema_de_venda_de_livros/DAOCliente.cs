using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Importando os comandos de conexão com o banco

namespace Sistema_de_Livros

{

    class DAOCliente

    {

        public MySqlConnection conexao;

        public string dados;

        public string comando;

        public string[] nome;

        public string[] endereco;

        public string[] telefone;

        public DateTime[] data_Nascimento;

        public string[] email;

        public string[] senha;
 
        public DAOCliente()

        {

            conexao = new MySqlConnection("server=localhost;DataBase=SistemaDeVendaDeLivros;Uid=root;Password=;Convert Zero DateTime=True");

            try

            {

                this.conexao.Open();

                Console.WriteLine("Conectado com Sucesso!");

            }

            catch (Exception erro)

            {

                Console.WriteLine($"Algo deu errado!\n\n {erro}");

                this.conexao.Close();

            } // Fim do try e catch

        } // Fim do construtor
 
        public void Cadastrar(string nome, string telefone, string endereco, DateTime dtNascimento, string login, string senha)

        {

            try

            {

                string sql = "INSERT INTO usuario (nome, endereco, telefone, data_nascimento, login, senha) " +

                             "VALUES (@nome, @endereco, @telefone, @dtNascimento, @login, @senha)";
 
                MySqlCommand cmd = new MySqlCommand(sql, this.conexao);

                cmd.Parameters.AddWithValue("@nome", nome);

                cmd.Parameters.AddWithValue("@endereco", endereco);

                cmd.Parameters.AddWithValue("@telefone", telefone);

                cmd.Parameters.AddWithValue("@dtNascimento", dtNascimento);

                cmd.Parameters.AddWithValue("@login", login);

                cmd.Parameters.AddWithValue("@senha", senha);
 
                cmd.ExecuteNonQuery();

                Console.WriteLine("Usuario cadastrado com sucesso!");

            }

            catch (Exception erro)

            {

                Console.WriteLine($"Erro ao cadastrar: {erro.Message}");

            }

        }
 
    } // Fim da classe DAOCliente

} // Fim do namespace
 
 
namespace Sistema_de_Livros

{

    class CONTROLCliente

    {
 
        public void Cadastrar()

        {

            Console.WriteLine("--- CADASTRO DE USUARIO ---");
 
            Console.Write("Nome: ");

            string nome = Console.ReadLine();
 
            Console.Write("Telefone: ");

            string telefone = Console.ReadLine();
 
            Console.Write("Endereço: ");

            string endereco = Console.ReadLine();
 
            Console.Write("Data de Nascimento (dd/mm/aaaa): ");

            DateTime dtNascimento = Convert.ToDateTime(Console.ReadLine());
 
            Console.Write("Login: ");

            string login = Console.ReadLine();
 
            Console.Write("Senha: ");

            string senha = Console.ReadLine();
 
            this.cliente.Cadastrar(nome, telefone, endereco, dtNascimento, login, senha);

        }
 
        DAOCliente cliente;

        public int opcao;
 
        public CONTROLCliente()

        {

            this.cliente = new DAOCliente();

        }
 
        public void MostrarMenu()

        {

            Console.WriteLine("-------MENU--------\n\n " +

                "\n 0 Sair" +

                "\n 1 Login" +

                "\n 2 Cadastrar Usuario" +

                "\n 3 Atualizar Usuario" +

                "\n 4 Excluir Usuarios" +

                "\n 5 Visualizar Usuarios" +

                "\n Escolha uma das opções acima");

            this.opcao = Convert.ToInt32(Console.ReadLine());

        }
 
        public void ExecutarOperacao()

        {

            do

            {

                this.MostrarMenu();

                switch (this.opcao)

                {

                    case 0:

                        Console.WriteLine("Obrigado! Fechando Sistema");

                    break;

                    case 1:

                        Console.WriteLine("Ainda em desenvolvimento!");

                    break;

                    case 2:

                        this.Cadastrar();

                    break;

                    default:

                        Console.WriteLine("Opção inválida! digite algumas entre os numeros");

                    break;
 
 
                } // fecha o switch

            } while (this.opcao != 0); // fecha o do-while

        } // fecha ExecutarOperacao
 
    } // fecha CONTROLCliente

} // fecha namespace

 
