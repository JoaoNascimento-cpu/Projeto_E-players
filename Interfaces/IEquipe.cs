using System.Collections.Generic;
using Eplayers.Models;

namespace Eplayers.Interfaces
{
    public interface IEquipe
    {
        //CRUD
        void Create(Equipe e);
        List<Equipe> ReadAll();
        void Update(Equipe e);
        void Delete(int id);
    }
}