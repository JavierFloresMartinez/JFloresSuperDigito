using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public object Viewbag { get; private set; }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            ML.Usuario usuario = new ML.Usuario();
            var bcrip = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrip.GetBytes(20);
            ML.Result result = BL.Usuario.GetByUsername(username);
            if (result.Correct)
            {
                usuario = (ML.Usuario)result.Object;
                if (usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("Index", "Home", usuario);
                }
                else
                {
                    ViewBag.Message = "Error contraseña Incorrecta";
                    return View("Modal");
                }

            }
            else
            {
                ViewBag.Message = "Error credenciales Incorrectas";
                return View("Modal");
            }

           
        }

        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario, string password, string paswrd)
        {
            if(password == paswrd)
            {
                var bcrip = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
                var passwordHash = bcrip.GetBytes(20);
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ingreso Correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "Contraseñas No Iguales";
            }

            return View("Modal");
        }

      

    }
}
