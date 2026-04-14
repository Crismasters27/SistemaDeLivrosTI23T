using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // importando os comandos de conexão com o banco


namespace SistemaDeLivros
{
    class DAOLivro
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public int i;
        public int contar;
        public string[] nome;
        public int[] codigoLivro;
        public string[] descricao;
        public string[] editora;
        public int[] quantidade;
        public decimal[] precoUnitario;

        public DAOLivro()
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

        public void InserirLivro(string nome, string descricao, string editora, int quantidade, decimal precoUnitario)
        {
            try
            {
                this.dados = $"('','{nome}','{descricao}','{editora}','{quantidade}','{precoUnitario}')";
                this.comando = $"INSERT INTO Livro(codigoLivro,nome,descricao,editora,quantidade,precoUnitario) VALUES{this.dados}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Inserido com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        }

        public void PreencherVetor()
        {
            string query = "SELECT * FROM Livro";

            this.codigoLivro = new int[100];
            this.nome = new string[100];
            this.descricao = new string[100];
            this.editora = new string[100];
            this.quantidade = new int[100];
            this.precoUnitario = new decimal[100];

            for (int j = 0; j < 100; j++)
            {
                this.codigoLivro[j] = 0;
                this.nome[j] = "";
                this.descricao[j] = "";
                this.editora[j] = "";
                this.quantidade[j] = 0;
                this.precoUnitario[j] = 0;
            }

            MySqlCommand coletar = new MySqlCommand(query, this.conexao);
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            this.contar = 0;
            while (leitura.Read())
            {
                this.codigoLivro[i] = Convert.ToInt32(leitura["codigoLivro"]);
                this.nome[i] = leitura["nome"] + "";
                this.descricao[i] = leitura["descricao"] + "";
                this.editora[i] = leitura["editora"] + "";
                this.quantidade[i] = Convert.ToInt32(leitura["quantidade"]);
                this.precoUnitario[i] = Convert.ToDecimal(leitura["precoUnitario"]);
                i++;
                this.contar++;
            }
            leitura.Close();
        } // FIM DO PREENCHER VETOR

        public void ListarLivros()
        {
            this.PreencherVetor();
            Console.WriteLine("\n---- LISTA DE LIVROS ----");
            for (int j = 0; j < this.contar; j++)
            {
                Console.WriteLine($"Código:   {this.codigoLivro[j]}");
                Console.WriteLine($"Nome: {this.nome[j]}");
                Console.WriteLine($"Descrição:{this.descricao[j]}");
                Console.WriteLine($"Editora:  {this.editora[j]}");
                Console.WriteLine($"Estoque:  {this.quantidade[j]}");
                Console.WriteLine($"Preço:    {this.precoUnitario[j]:C}");
                Console.WriteLine("---------------------------");
            }
        } // FIM DO LISTAR

        public void AtualizarLivro(int codigoLivro, string descricao, string editora, int quantidade, decimal precoUnitario)
        {
            try
            {
                this.comando = $"UPDATE Livro SET descricao='{descricao}', editora='{editora}', quantidade='{quantidade}', precoUnitario='{precoUnitario}' WHERE codigoLivro={codigoLivro}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Atualizado com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO ATUALIZAR

        public void ExcluirLivro(int codigoLivro)
        {
            try
            {
                this.comando = $"DELETE FROM Livro WHERE codigoLivro={codigoLivro}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Excluído com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO EXCLUIR

    } // FIM DA CLASSE DAO LIVRO
} // FIM DO PROJETO