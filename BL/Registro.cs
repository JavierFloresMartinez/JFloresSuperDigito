using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BL
{
    public class Registro
    {
        public static string SuperDigito(string digito)
        {
            string result;
            int op = 0;
            int suma;
            int resultado = 0;
            var s = digito.ToCharArray().Select(c => c.ToString()).ToArray();
            int[] num = new int[s.Length];
            for (int i = 0; i < num.Length; i++)
            {
                num[i] = Convert.ToInt32(s[i]);
            }

            while (num.Length != 1)
            {

                while (op < num.Length)
                {
                    suma = resultado;
                    suma = suma + num[op];
                    resultado = suma;
                    op++;
                }
                op = 0;
                result = resultado.ToString();
                resultado = 0;
                var b = result.ToCharArray().Select(c => c.ToString()).ToArray();

                if (b.Length != 1)
                {
                    num = new int[b.Length];
                    for (int i = 0; i < num.Length; i++)
                    {
                        num[i] = Convert.ToInt32(b[i]);
                    }
                }
                else
                {
                    num = new int[b.Length];
                    digito = result;
                }

            }

            return digito;
        }


        public static ML.Result Add(ML.Registro registro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"RegistroAdd {registro.Numero}, {registro.Resultado},{registro.Usuario.IdUsuario}");

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

        public static ML.Result Update(int idRegistro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"RegistroUpdate {idRegistro}");
                    if (RowsAfected > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al actualizar el registro");
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

        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"RegistroDelete {idUsuario}");
                    if (RowsAfected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al eliminar los registros");
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

        public static ML.Result RegistroGetByUsuario(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    var RowsAfected = contex.Registros.FromSqlRaw($"RegistroGetByUsuario {idUsuario}").ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            ML.Registro registro = new ML.Registro();
                            registro.IdRegistro = obj.IdDigito;
                            registro.Numero = (int)obj.Numero;
                            registro.Resultado = (int)obj.Resultado;
                            registro.Fecha = obj.FechaYhora.ToString();
                            registro.Usuario = new ML.Usuario();
                            registro.Usuario.IdUsuario = (int)obj.IdUsuario;
                           

                            result.Objects.Add(registro);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
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

        public static ML.Result RegistroGetByNumero( int numero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresSuperDigitoContext contex = new DL.JfloresSuperDigitoContext())
                {
                    var RowsAfected = contex.Registros.FromSqlRaw($"RegistroGetByNumero {numero}").AsEnumerable().FirstOrDefault();
                    result.Object = new object();
                    if (RowsAfected != null)
                    {
                        ML.Registro registro = new ML.Registro();
                        registro.IdRegistro = (int)RowsAfected.IdDigito;
                        registro.Numero = (int)RowsAfected.Numero;
                        registro.Resultado = (int)RowsAfected.Resultado;
                        registro.Fecha = RowsAfected.FechaYhora.ToString();
                        registro.Usuario = new ML.Usuario();
                        registro.Usuario.IdUsuario = (int)RowsAfected.IdUsuario;
                       
                        result.Object = registro;

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
