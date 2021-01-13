using System.Collections.Generic;
using System.IO;

namespace Eplayers
{
    public class EplayersBase
    {
        public void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();

            //using -> abrir e fechar determinado tipo de arquivo ou conexão
            //StreamReader -> Ler as informações do CSV
            using (StreamReader file = new StreamReader(path))
            {
                string linha;
                while((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void RewriteCSV(string path, List<string> linhas)
        {
            //streamwrigter -> escrita de arquivos
            using(StreamWriter output = new StreamWriter(path))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + '\n');
                }
            }
        }
    }
}