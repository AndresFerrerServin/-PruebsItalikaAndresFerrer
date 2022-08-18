using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Motocicleta
    {
        public static ML.Result GetAll(ML.Motocicleta motocicleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {
                    motocicleta.SKU = (motocicleta.SKU == null) ? "" : motocicleta.SKU;
                    motocicleta.Modelo = (motocicleta.Modelo == null) ? "" : motocicleta.Modelo;

                    var query = context.Motocicleta.FromSqlRaw($"MotocicletaGetAll '{motocicleta.SKU}','{motocicleta.Modelo}'").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            motocicleta = new ML.Motocicleta();
                            motocicleta.IdMotocicleta = obj.IdMotocicleta;
                            motocicleta.SKU = obj.Sku;
                            motocicleta.Fert = obj.Fert;
                            motocicleta.Modelo = obj.Modelo;

                            motocicleta.Tipo = new ML.Tipo();
                            motocicleta.Tipo.IdTipo = obj.IdTipo.Value;
                            motocicleta.Tipo.Nombre = obj.Nombre;

                            motocicleta.NúmeroDeSerie = obj.NúmeroDeSerie;
                            motocicleta.Fecha = obj.Fecha.Value.ToString("dd-MM-yyyy");
                            motocicleta.Imagen = obj.Imagen;



                            result.Objects.Add(motocicleta);

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

        public static ML.Result Add(ML.Motocicleta motocicleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MotocicletaAdd '{motocicleta.SKU}','{motocicleta.Fert}'" +
                        $"                                                      ,'{motocicleta.Modelo}',{motocicleta.Tipo.IdTipo}" +
                        $"                                                      ,'{motocicleta.NúmeroDeSerie}','{motocicleta.Fecha}','{motocicleta.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Motocicleta motocicleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MotocicletaUpdate {motocicleta.IdMotocicleta},'{motocicleta.SKU}','{motocicleta.Fert}'" +
                        $"                                                      ,'{motocicleta.Modelo}',{motocicleta.Tipo.IdTipo}" +
                        $"                                                      ,'{motocicleta.NúmeroDeSerie}','{motocicleta.Fecha}','{motocicleta.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo";
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

        public static ML.Result Delete(ML.Motocicleta motocicleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MotocicletaDelete {motocicleta.IdMotocicleta}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se elimino";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetId(int IdMotocicleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ItalikaContext context = new DL.ItalikaContext())
                {
                    var query = context.Motocicleta.FromSqlRaw($"MotocicletaGetById {IdMotocicleta} ").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        ML.Motocicleta motocicleta = new ML.Motocicleta();

                        motocicleta.IdMotocicleta = query.IdMotocicleta;
                        motocicleta.SKU = query.Sku;
                        motocicleta.Fert = query.Fert;
                        motocicleta.Modelo = query.Modelo;

                        motocicleta.Tipo = new ML.Tipo();
                        motocicleta.Tipo.IdTipo = query.IdTipo.Value;
                        motocicleta.Tipo.Nombre = query.Nombre;

                        motocicleta.NúmeroDeSerie = query.NúmeroDeSerie;
                        motocicleta.Fecha = query.Fecha.Value.ToString("dd-MM-yyyy");
                        motocicleta.Imagen = query.Imagen;

                        result.Object = motocicleta;
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