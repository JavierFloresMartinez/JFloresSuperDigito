using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class RegistroController : Controller
    {
        [HttpGet]
        public IActionResult GetAll(ML.Usuario usuario)
        {
            ML.Registro registro = new ML.Registro();
            registro.Usuario = new ML.Usuario();
            registro.Registros = new List<object>();
            ML.Result result = new ML.Result();
            result = BL.Registro.RegistroGetByUsuario(usuario.IdUsuario);
            if (result.Correct)
            {
                registro.Registros = result.Objects;
                registro.Usuario = usuario;
            }
            return View(registro);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Registro registro)
        {
            
            ML.Result resultNumero = BL.Registro.RegistroGetByNumero(registro.Numero);

            if (resultNumero.Correct)
            {
                ML.Registro registroUpdate = new ML.Registro();
                registroUpdate = (ML.Registro)resultNumero.Object;
                ML.Result resultUpdate = BL.Registro.Update(registroUpdate.IdRegistro);
                if (resultUpdate.Correct)
                {
                    ViewBag.Message = "Se agrego con exito";
                }
            }
            else
            {
                string numero = registro.Numero.ToString();
                registro.Resultado = int.Parse(BL.Registro.SuperDigito(numero));

                 ML.Result result = BL.Registro.Add(registro);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego con exito";
                }
            }
            ML.Result resultRegistros = BL.Registro.RegistroGetByUsuario(registro.Usuario.IdUsuario);
            if (resultRegistros.Correct)
            {
                registro.Registros = new List<object>();
                registro.Registros = resultRegistros.Objects;
            }

            return View(registro);
        }

        public IActionResult Delete(int idUsuario)
        {   
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Registro.Delete(idUsuario);
            if (result.Correct)
            {
                ML.Result resultUsuario = new ML.Result();
                resultUsuario = BL.Usuario.GetById(idUsuario);
                if (resultUsuario.Correct)
                {
                    usuario = (ML.Usuario)resultUsuario.Object;
                }
               
            }
            return RedirectToAction("GetAll", "Registro", usuario);
        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Login", "Usuario");
        }
    }
}
