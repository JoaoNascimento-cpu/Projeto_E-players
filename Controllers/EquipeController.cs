using System;
using System.IO;
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
            NovaEquipe.IdEquipe =   Int32.Parse(form["IdEquipe"]);
            NovaEquipe.Nome     =   form["Nome"];
            NovaEquipe.Imagem   =   form["Imagem"];

            //upload início
            //verificação se o usuário anexou um arquivo
            if ( form.Files.Count > 0)
            {
                //se ele selecionar um arquivo, será armazenado na variável "file"
                var file    = form.Files[0];
                var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //se a pasta não existir, ela será criada
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                } 
                                                    //localhost:5001+                   +Equipes +  equipe.jpg.  
                var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/",folder,file.FileName.Trim());

                using (var stream = new FileStream(path, FileMode.Create))
                {   
                    //arquivo salvo no caminho especificado
                    file.CopyTo(stream);
                }

                NovaEquipe.Imagem = file.FileName;
            }else
            {
                NovaEquipe.Imagem = "padrão.png";
            }
            //upload término

            //método create salva a NovaEquipe no CSV
            equipeModel.Create(NovaEquipe);
            ViewBag.Equipe = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
            //http://localhost:5001/Equipe/1
            [Route("{id}")]
            public IActionResult Excluir(int id)
            {
                equipeModel.Delete(id);

                ViewBag.Equipes = equipeModel.ReadAll();
                
                return LocalRedirect("~/Equipe/Listar");
            }
    }
}