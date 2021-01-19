using System.Collections.Generic;
using Eplayers.Models;

namespace Eplayers.Interfaces
{
    public interface IJogador
    {
        //criar
        void Create(Jogador j);
        //ler
        List<Jogador> ReadAll();
        //alterar
        void Update(Jogador j);
        //deletar
        void Delete(int id);
    }
}