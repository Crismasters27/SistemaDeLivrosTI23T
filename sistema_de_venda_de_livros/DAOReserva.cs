using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // importando os comandos de conexão com o banco

namespace SistemaDeLivros
{
    class DAOReserva
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public int i;
        public int contar;
        public int[] codigoReserva;
        public DateTime[] data;
        public int[] ClienteCodigo;
        public int[] FuncionarioCodigo;
        public int[] LivroCodigo;

        public DAOReserva()
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
        public void InserirReserva(int ClienteCodigo, int FuncionarioCodigo)
        {
            try
            {
                this.dados = $"('','{DateTime.Now:yyyy-MM-dd}','{ClienteCodigo}','{FuncionarioCodigo}')";
                this.comando = $"INSERT INTO Reserva(codigoReserva,dataReserva,ClienteCodigo,FuncionarioCodigo) VALUES{this.dados}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                sql.ExecuteNonQuery();

                this.comando = "SELECT LAST_INSERT_ID()";
                sql = new MySqlCommand(this.comando, this.conexao);
                int idNovaReserva = Convert.ToInt32(sql.ExecuteScalar());

                Console.WriteLine($"Reserva registrada com sucesso! Código: {idNovaReserva}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO INSERIR RESERVA
        public void InserirTer(int ReservaCodigo, int ClienteCodigo, int FuncionarioCodigo, int LivroCodigo)
        {
            try
            {
                this.dados = $"('','{ReservaCodigo}','{LivroCodigo}')";
                this.comando = $"INSERT INTO terreserva(codigoTerReserva,ReservaCodigo,LivroCodigo) VALUES{this.dados}";
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
            string query = "SELECT r.*, t.LivroCodigo FROM Reserva r " +
                           "JOIN terreserva t ON t.ReservaCodigo = r.codigoReserva";

            this.codigoReserva = new int[100];
            this.data = new DateTime[100];
            this.ClienteCodigo = new int[100];
            this.FuncionarioCodigo = new int[100];
            this.LivroCodigo = new int[100];

            for (int j = 0; j < 100; j++)
            {
                this.codigoReserva[j] = 0;
                this.data[j] = DateTime.MinValue;
                this.ClienteCodigo[j] = 0;
                this.FuncionarioCodigo[j] = 0;
                this.LivroCodigo[j] = 0;
            }

            MySqlCommand coletar = new MySqlCommand(query, this.conexao);
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            this.contar = 0;
            while (leitura.Read())
            {
                this.codigoReserva[i] = Convert.ToInt32(leitura["codigoReserva"]);
                this.data[i] = Convert.ToDateTime(leitura["dataReserva"]);
                this.ClienteCodigo[i] = Convert.ToInt32(leitura["ClienteCodigo"]);
                this.FuncionarioCodigo[i] = Convert.ToInt32(leitura["FuncionarioCodigo"]);
                this.LivroCodigo[i] = Convert.ToInt32(leitura["LivroCodigo"]);
                i++;
                this.contar++;
            }
            leitura.Close();
        } // FIM DO PREENCHER VETOR
        public void ListarReservas()
        {
            this.PreencherVetor();
            Console.WriteLine("\n----- LISTA DE RESERVAS -----");
            for (int j = 0; j < this.contar; j++)
            {
                Console.WriteLine($"Código Reserva:    {this.codigoReserva[j]}");
                Console.WriteLine($"Data:              {this.data[j]:dd/MM/yyyy}");
                Console.WriteLine($"Cód. Cliente:      {this.ClienteCodigo[j]}");
                Console.WriteLine($"Cód. Funcionário:  {this.FuncionarioCodigo[j]}");
                Console.WriteLine($"Cód. Livro:        {this.LivroCodigo[j]}");
                Console.WriteLine("------------------------------");
            }
        } // FIM DO LISTAR
        public void ExcluirReserva(int id)
        {
            try
            {
                this.comando = $"DELETE FROM Reserva WHERE codigoReserva={id}";
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine($"Reserva excluída com sucesso!\n\n {resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"ERRO ENCONTRADO\n\n {erro}");
            }
        } // FIM DO EXCLUIR

    } // FIM DA CLASSE DAO RESERVA
} // FIM DO PROJETO