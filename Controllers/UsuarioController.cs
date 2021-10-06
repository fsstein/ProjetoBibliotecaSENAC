using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult InserirUsuario()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult InserirUsuario(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();
            usuarioService.Inserir(u);       

            return RedirectToAction("Listagem");
        }

        
        public IActionResult Listagem()
        {              
            UsuarioService user = new UsuarioService();
            List<Usuario> Listagem = user.Listar();
            return View(Listagem);
        }
      
        public IActionResult EditarUsuario(int Id)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService user = new UsuarioService();
            Usuario u = user.Listar(Id);
            return View(u);
        }

        [HttpPost]
         public IActionResult EditarUsuario(Usuario u)
        {
            UsuarioService user = new UsuarioService();
            user.Atualizar(u);
            ViewBag.Mensagem="Usuário atualizado com sucesso!";
            return RedirectToAction("Listagem");
        }

        public IActionResult ExcluirUsuario(int Id)
        {
            UsuarioService user = new UsuarioService();
            user.Excluir(Id);
            return RedirectToAction("Listagem"); 
        }
    }
}