using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Jogador : EplayersBase , IJogador
    {
        public int IDJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }

        //login
        public string Email { get; set; }
        public string Senha { get; set; }

        private const string PATH = "Database/Jogador.CSV";

        public Jogador()
        {
            CreateFolderAndFile(PATH);
        }

        private string PrepararLinha(Jogador j)
        {
            return $"{j.IDJogador};{j.IdEquipe};{j.Nome};{j.Email};{j.Senha}";
        }

        public void Create(Jogador j)
        {
            string[] linha = {PrepararLinha(j)};
            File.AppendAllLines(PATH, linha);
        }

        public void Delete(int IDJogador)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll( x => x.Split(";")[0] == IDJogador.ToString());
            RewriteCSV(PATH, linhas);
        }

        public List<Jogador> ReadAll()
        {
            List<Jogador> jogadores = new List<Jogador>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Jogador jogador = new Jogador();
                jogador.IDJogador = int.Parse(linha[0]);
                jogador.IdEquipe = int.Parse(linha[1]);
                jogador.Nome    = linha[2];
                jogador.Email     = linha[3];
                jogador.Senha     = linha[4];

                jogadores.Add(jogador);
            }
            
            return jogadores;
        }

        public void Update(Jogador j)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == j.IDJogador.ToString());
            linhas.Add(PrepararLinha(j));
            RewriteCSV(PATH, linhas);
        }
    }
}