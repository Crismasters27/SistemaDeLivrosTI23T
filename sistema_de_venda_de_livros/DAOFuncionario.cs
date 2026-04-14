using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // importando os comandos de conexão com o banco


namespace SistemaDeLivros
{
    class DAOFuncionario
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public int i;
        public int contar;
        public int[] codigoFuncionario;
        public string[] nome;
        public string[] endereco;
        public string[] telefone;
        public string[] cargo;
        public decimal[] salario;
        public string[] login;
        public string[] senha;

        public DAOFuncionario()
        {
            conexao = new MySqlConnection("server=localhost;Port=3307;DataBase=SistemaLivro;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                this.conexao.Open();
                Console.WriteLine("Conectado com Sucesso!");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Algo deu errado!\n\n {erro}");
                this.conexao.Close();
            }
        } // FIM DO CONSTRUTOR

        public bool Autenticar(string login, string senha)
        {
            try
            {
                this.comando = $"SELECT * FROM Funcionario WHERE login='{login}' AND senha='{senha}'";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                MySqlDataReader leitura = sql.ExecuteReader();

                if (leitura.Read())
                {
                    leitura.Close();
                    return true;
                }
                leitura.Close();
                return false;
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
                return false;
            }
        } // FIM DO AUTENTICAR

        public void InserirFuncionario(string nome, string endereco, string telefone, string cargo, decimal salario, string login, string senha)
        {
            try
            {
                this.dados = $"('','{nome}','{endereco}','{telefone}','{cargo}','{salario}','{login}','{senha}')";
                this.comando = $"INSERT INTO Funcionario(codigoFuncionario,nome,endereco,telefone,cargo,salario,login,senha) VALUES{this.dados}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Inserido com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO INSERIR

        public void PreencherVetor()
        {
            string query = "SELECT * FROM Funcionario";

            this.codigoFuncionario = new int[100];
            this.nome = new string[100];
            this.endereco = new string[100];
            this.telefone = new string[100];
            this.cargo = new string[100];
            this.salario = new decimal[100];
            this.login = new string[100];
            this.senha = new string[100];

            for (int j = 0; j < 100; j++)
            {
                this.codigoFuncionario[j] = 0;
                this.nome[j] = "";
                this.endereco[j] = "";
                this.telefone[j] = "";
                this.cargo[j] = "";
                this.salario[j] = 0;
                this.login[j] = "";
                this.senha[j] = "";
            }

            MySqlCommand coletar = new MySqlCommand(query, this.conexao);
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            this.contar = 0;
            while (leitura.Read())
            {
                this.codigoFuncionario[i] = Convert.ToInt32(leitura["codigoFuncionario"]);
                this.nome[i] = leitura["nome"] + "";
                this.endereco[i] = leitura["endereco"] + "";
                this.telefone[i] = leitura["telefone"] + "";
                this.cargo[i] = leitura["cargo"] + "";
                this.salario[i] = Convert.ToDecimal(leitura["salario"]);
                this.login[i] = leitura["login"] + "";
                this.senha[i] = leitura["senha"] + "";
                i++;
                this.contar++;
            }
            leitura.Close();
        } // FIM DO PREENCHER VETOR

        public void ListarFuncionarios()
        {
            this.PreencherVetor();
            Console.WriteLine("\n----- LISTA DE FUNCIONÁRIOS -----");
            for (int j = 0; j < this.contar; j++)
            {
                Console.WriteLine($"Código:    {this.codigoFuncionario[j]}");
                Console.WriteLine($"Nome:      {this.nome[j]}");
                Console.WriteLine($"Endereço:  {this.endereco[j]}");
                Console.WriteLine($"Telefone:  {this.telefone[j]}");
                Console.WriteLine($"Cargo:     {this.cargo[j]}");
                Console.WriteLine($"Salário:   {this.salario[j]:C}");
                Console.WriteLine($"Login:     {this.login[j]}");
                Console.WriteLine("----------------------------------");
            }
        } // FIM DO LISTAR

        public void AtualizarFuncionario(int codigoFuncionario, string nome, string endereco, string telefone, string cargo, decimal salario, string login, string senha)
        {
            try
            {
                this.comando = $"UPDATE Funcionario SET nome='{nome}', endereco='{endereco}', telefone='{telefone}', cargo='{cargo}', salario='{salario}', login='{login}', senha='{senha}' WHERE codigoFuncionario={codigoFuncionario}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Atualizado com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO ATUALIZAR

        public void ExcluirFuncionario(int codigoFuncionario)
        {
            try
            {
                this.comando = $"DELETE FROM Funcionario WHERE codigoFuncionario={codigoFuncionario}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Excluído com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO EXCLUIR

    } // FIM DA CLASSE DAO FUNCIONARIO
} // FIM DO PROJETO