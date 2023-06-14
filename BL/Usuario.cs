using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Username}', @Password", new SqlParameter("@Password", usuario.Password));

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; ;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al ingresar el registro");
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result GetByUsername(string username)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    var RowsAfected = contex.Usuarios.FromSqlRaw($"UsuarioGetByUsername '{username}'").AsEnumerable().FirstOrDefault();
                    result.Object = new object();
                    if (RowsAfected != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = RowsAfected.IdUsuario;
                        usuario.Username = RowsAfected.Username;
                        usuario.Password = RowsAfected.Password;
                     

                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    var RowsAfected = contex.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();
                    result.Object = new object();
                    if (RowsAfected != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = RowsAfected.IdUsuario;
                        usuario.Username = RowsAfected.Username;
                        usuario.Password = RowsAfected.Password;


                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }



    }
}