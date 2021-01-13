using System;
using Eplayers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eplayers.Controllers
{
    //http://LocalHost:5001/Equipe
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        //criação da instancia equipemodel com a estrutura equipe
        Equipe equipeModel = new Equipe();
        [Route("Listar")]
        public IActionResult Index()
        {
            //listando todas as equipes e enviando para view, através da viewbag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //criação da nova instancia de equipe
            //e é armazenamento dos dados enviados pelo usuário através do formulário
            // e é salvo no objeto NovaEquipe
            Equipe NovaEquipe = new Equipe();
            NovaEquipe.IDEquipe =   Int32.Parse(form["IdEquipe"]);
            NovaEquipe.Nome     =   form["Nome"];
            NovaEquipe.Imagem   =   form["Imagem"];

            //método create salva a NovaEquipe no CSV
            equipeModel.Create(NovaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
    }
}