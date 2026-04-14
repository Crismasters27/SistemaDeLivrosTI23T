using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // importando os comandos de conexão com o banco

namespace SistemaDeLivros
{
    class DAOCompra
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public int i;
        public int contar;
        public int[] codigoCompra;
        public DateTime[] data;
        public decimal[] valorTotal;
        public string[] formaDePagamento;
        public int[] ClienteCodigo;
        public int[] FuncionarioCodigo;

        public DAOCompra()
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

        public void InserirCompra(decimal valorTotal, string formaDePagamento, int ClienteCodigo, int FuncionarioCodigo)
        {
            try
            {
                this.dados = $"('','{DateTime.Now:yyyy-MM-dd}','{valorTotal}','{formaDePagamento}','{ClienteCodigo}','{FuncionarioCodigo}')";
                this.comando = $"INSERT INTO Compra(codigoCompra,dataCompra,valorTotal,formaDePagamento,ClienteCodigo,FuncionarioCodigo) VALUES{this.dados}";

                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                sql.ExecuteNonQuery();

                this.comando = "SELECT LAST_INSERT_ID()";
                sql = new MySqlCommand(this.comando, this.conexao);
                int idNovaCompra = Convert.ToInt32(sql.ExecuteScalar());

                Console.WriteLine($"Compra registrada com sucesso! Código da compra: {idNovaCompra}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO INSERIR COMPRA

        public void InserirTer(int CompraCodigoo, int ClienteCodigo, int FuncionarioCodigo, int LivroCodigo)
        {
            try
            {
                this.dados = $"('','{CompraCodigoo}','{LivroCodigo}')";
                this.comando = $"INSERT INTO tercompra(codigoTerCompra,CompraCodigo,LivroCodigo) VALUES{this.dados}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                sql.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO INSERIR TER


        public void PreencherVetor()
        {
            string query = "SELECT * FROM Compra";

            this.codigoCompra = new int[100];
            this.data = new DateTime[100];
            this.valorTotal = new decimal[100];
            this.formaDePagamento = new string[100];
            this.ClienteCodigo = new int[100];
            this.FuncionarioCodigo = new int[100];

            for (int j = 0; j < 100; j++)
            {
                this.codigoCompra[j] = 0;
                this.data[j] = DateTime.MinValue;
                this.valorTotal[j] = 0;
                this.formaDePagamento[j] = "";
                this.ClienteCodigo[j] = 0;
                this.FuncionarioCodigo[j] = 0;
            }

            MySqlCommand coletar = new MySqlCommand(query, this.conexao);
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            this.contar = 0;
            while (leitura.Read())
            {
                this.codigoCompra[i] = Convert.ToInt32(leitura["codigoCompra"]);
                this.data[i] = Convert.ToDateTime(leitura["dataCompra"]);
                this.valorTotal[i] = Convert.ToDecimal(leitura["valorTotal"]);
                this.formaDePagamento[i] = leitura["formaDePagamento"] + "";
                this.ClienteCodigo[i] = Convert.ToInt32(leitura["ClienteCodigo"]);
                this.FuncionarioCodigo[i] = Convert.ToInt32(leitura["FuncionarioCodigo"]);
                i++;
                this.contar++;
            }
            leitura.Close();
        } // FIM DO PREENCHER VETOR

        public void ListarCompras()
        {
            this.PreencherVetor();
            Console.WriteLine("\n----- LISTA DE COMPRAS -----");
            for (int j = 0; j < this.contar; j++)
            {
                Console.WriteLine($"Código:            {this.codigoCompra[j]}");
                Console.WriteLine($"Data:              {this.data[j]:dd/MM/yyyy}");
                Console.WriteLine($"Valor Total:       {this.valorTotal[j]:C}");
                Console.WriteLine($"Pagamento:         {this.formaDePagamento[j]}");
                Console.WriteLine($"Cód. Cliente:      {this.ClienteCodigo[j]}");
                Console.WriteLine($"Cód. Funcionário:  {this.FuncionarioCodigo[j]}");
                Console.WriteLine("-----------------------------");
            }
        } // FIM DO LISTAR

        public void ExcluirCompra(int codigoCompra)
        {
            try
            {
                this.comando = $"DELETE FROM Compra WHERE codigoCompra={codigoCompra}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Excluído com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO EXCLUIR

    } // FIM DA CLASSE DAO COMPRA
} // FIM DO PROJETO