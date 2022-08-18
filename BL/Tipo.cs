using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Tipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {

                    var query = context.Tipos.FromSqlRaw($"TipoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Tipo tipo = new ML.Tipo();
                            tipo.IdTipo = obj.IdTipo;
                            tipo.Nombre = obj.Nombre;
                            
                            result.Objects.Add(tipo);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro";
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
