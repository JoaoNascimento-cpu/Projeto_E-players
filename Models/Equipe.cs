using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Equipe : EplayersBase , IEquipe
    {
        //ID= Identificador

        public int IDEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        private const string PATH = "Database/Equipe.CSV";
        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }
        public string Prepare(Equipe e)
        {
            return $"{e.IDEquipe}; {e.Nome}; {e.Imagem}";
        }

        public void Create(Equipe e)
        {
            string[] linhas = {Prepare(e)};
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            
            //removemos a linha que tem o codigo a ser removido
            linhas.RemoveAll(x => x.Split(";")[0] ==  id.ToString());

            //reescreve o CSV com as alterações
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            //ler todas as linhas CSV
            string[] linhas = File.ReadAllLines(PATH);

            //Percorrer as linhas e adicionar na lista de equipes cada objeito "equipe"
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                //criamos objeto equipe
                Equipe equipe = new Equipe();


                //alimentamos objeto equipe
                equipe.IDEquipe = int.Parse(linha[0]);
                equipe.Nome     = linha[1];
                equipe.Imagem   = linha[2].Trim();

                equipes.Add(equipe);
            }

            return equipes; 
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            
            //removemos a linha que tem o codigo a ser removido
            linhas.RemoveAll(x => x.Split(";")[0] ==  e.IDEquipe.ToString());

            //adiciona a linha alterada no final do arquivo com o mesmo código
            linhas.Add( Prepare(e) );
            //reescreve o CSV com as alterações
            RewriteCSV(PATH, linhas);
        }
    }
}