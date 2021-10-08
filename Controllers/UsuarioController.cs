using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            Autenticacao.verificaSeUsuarioAdminExiste(this);

            //var listUser = new UsuarioService().Listar();
            //return View(listUser);
            return View();
        }

        [HttpPost]
        public IActionResult InserirUsuario(Usuario u)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdminExiste(this);

            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            UsuarioService usuarioService = new UsuarioService();
            usuarioService.Inserir(u);
            return RedirectToAction("Listagem");
        }


        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdminExiste(this);
            UsuarioService user = new UsuarioService();
            List<Usuario> Listagem = user.Listar();
            return View(Listagem);
        }

        public IActionResult EditarUsuario(int Id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdminExiste(this);
            UsuarioService user = new UsuarioService();
            Usuario u = user.Listar(Id);
            return View(u);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario u)
        {
            UsuarioService user = new UsuarioService();
            user.Atualizar(u);
            return RedirectToAction("Listagem");
        }

        public IActionResult ExcluirUsuario(int Id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdminExiste(this);
            UsuarioService user = new UsuarioService();
            user.Excluir(Id);
            return RedirectToAction("Listagem");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            Autenticacao.CheckLogin(this);
            HttpContext.Session.Clear();
            return RedirectToAction("/Home/Login");
        }
    }
}