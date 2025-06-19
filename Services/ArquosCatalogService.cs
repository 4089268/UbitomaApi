using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RutasApi.Data;
using RutasApi.Interfaces;
using RutasApi.Models;
using RutasApi.Models.Entities.Arquos;

namespace RutasApi.Services
{
    public class ArquosCatalogService : ICatalogService
    {
        public ICollection<Dictionary<string, object>>? AnomaliasPredio(IEnlace ruta)
        {
            try{
                var result = new List<Dictionary<string, object>>();
                var catAnomalias = new List<CatAnomaliaPredio>();

                // Realizar consulta sql
                var query = "Select  id_anomalia, " +
                            "descripcion = LTRIM(a.descripcion), " +
                            "observaciones = REPLACE(REPLACE(REPLACE(a.observaciones,char(10),'-'),char(13),''),char(9),'') " +
                            "From Facturacion.Cat_Anomalias as a with(nolock) " +
                            "Inner join Facturacion.Cat_TiposAnomalia as c with(nolock) on c.id_tipoanomalia=a.id_tipoanomalia " +
                            "Where id_anomalia >0 and en_campo = 1 and a.inactivo=0 " +
                            "Order By descripcion";
                using(var sqlConenction = new SqlConnection(ruta.ConnectionString)){
                    sqlConenction.Open();
                    var _command = new SqlCommand(query, sqlConenction);
                    using(SqlDataReader reader = _command.ExecuteReader() ){
                        while(reader.Read()){
                            catAnomalias.Add(CatAnomaliaPredio.FromSqlDataReader(reader));
                        }
                    }
                    sqlConenction.Close();
                }

                result = catAnomalias.Select( item =>
                    new Dictionary<string, object>(){
                        {"id", item.Id},
                        {"descripcion", item.Descripcion.ToUpper()},
                        {"observaciones", item.Observaciones},
                    }
                ).ToList();

                return result;

            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de anomalias predio Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }   
        }

        public ICollection<Dictionary<string, object>> DiametrosTomas(IEnlace ruta)
        {
            var collection = new List<Dictionary<string, object>>
            {
                new() { { "id", 1 }, { "descripcion", "1/2\"" } },
                new() { { "id", 2 }, { "descripcion", "3/4\"" } },
                new() { { "id", 3 }, { "descripcion", "1\"" } },
                new() { { "id", 4 }, { "descripcion", "1-1/2\"" } },
                new() { { "id", 5 }, { "descripcion", "2\"" } },
                new() { { "id", 6 }, { "descripcion", "3\"" } },
                new() { { "id", 7 }, { "descripcion", "4\"" } },
                new() { { "id", 8 }, { "descripcion", "5\"" } },
                new() { { "id", 9 }, { "descripcion", "6\"" } }
            };
            return collection;
        }

        public ICollection<Dictionary<string, object>>? Estatus(IEnlace ruta)
        {
            try{
                var result = new List<Dictionary<string, object>>();
                
                using(var context = new ArquosContext(ruta.ConnectionString)){
                    List<CatEstatus> datos = context.CatEstatuses.Where(item => item.Tabla == "CAT_PADRON").ToList();
                    foreach(var item in datos){
                        result.Add(
                            new Dictionary<string, object>(){
                                { "id", (int)item.IdEstatus},
                                {"descripcion", item.Descripcion.ToUpper()}
                            }
                        );
                    }
                }
                return result;

            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de oficinas Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public ICollection<Dictionary<string, object>>? Giros(IEnlace ruta)
        {
            try{
                var result = new List<Dictionary<string, object>>();
                
                using(var context = new ArquosContext(ruta.ConnectionString)){
                    List<CatGiro> datos = context.CatGiros.ToList();
                    foreach(var item in datos){
                        result.Add( new Dictionary<string, object>(){
                            {"id", (int)item.IdGiro},
                            {"descripcion", item.Descripcion!.ToUpper()}
                        });
                    }
                }
                return result;

            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de oficinas Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public ICollection<Dictionary<string, object>> MarcasMedidores(IEnlace ruta)
        {
            var collection = new List<Dictionary<string, object>>
            {
                new() { { "id", 1 }, { "descripcion", "BADGER METER" } },
                new() { { "id", 2 }, { "descripcion", "ITRON" } },
                new() { { "id", 3 }, { "descripcion", "CICASA" } },
                new() { { "id", 4 }, { "descripcion", "VEAGN" } },
                new() { { "id", 5 }, { "descripcion", "DOROT" } },
                new() { { "id", 6 }, { "descripcion", "WATER METER CORP" } },
                new() { { "id", 7 }, { "descripcion", "ACQUAMET" } },
                new() { { "id", 8 }, { "descripcion", "NAANDANJAIN" } }
            };
            return collection;
        }

        public ICollection<Dictionary<string, object>> SectoresHidraulicos(IEnlace ruta)
        {
            var collection = new List<Dictionary<string, object>>
            {
                new() { { "id", 1 }, { "descripcion", "AEROPUERTO" } },
                new() { { "id", 2 }, { "descripcion", "BACHILLERES" } },
                new() { { "id", 3 }, { "descripcion", "INSURGENTES" } }
            };
            return collection;
        }

        public ICollection<Dictionary<string, object>>? SituacionesPredio(IEnlace ruta)
        {
            try{
                var result = new List<Dictionary<string, object>>();
                
                using(var context = new ArquosContext(ruta.ConnectionString)){
                    List<CatSituacionesToma> datos = context.CatSituacionesTomas.ToList();
                    foreach(var item in datos){
                        result.Add( new Dictionary<string, object>(){
                            { "id", (int)item.IdSituacion},
                            {"descripcion", item.Descripcion.ToUpper()}
                        });
                    }
                }
                return result;

            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de oficinas Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public ICollection<Dictionary<string, object>>? Tarifas(IEnlace ruta)
        {
            try{
                var result = new List<Dictionary<string, object>>();
                
                using(var context = new ArquosContext(ruta.ConnectionString)){
                    List<CatTiposUsuario> datos = context.CatTiposUsuarios.ToList();
                    foreach(var item in datos){
                        result.Add( new Dictionary<string, object>(){
                            { "id", (int)item.IdTipousuario},
                            {"descripcion", item.Descripcion.ToUpper()}
                        });
                    }
                }
                return result;

            }catch(Exception err){
                Console.WriteLine($">> Error al obtener el catalogo de oficinas Error:{err.Message}\n\tStacktrace:{err.StackTrace}");
                return null;
            }
        }

        public ICollection<Dictionary<string, object>> TiposIntalacion(IEnlace ruta)
        {
            var collection = new List<Dictionary<string, object>>
            {
                new() { { "id", 1 }, { "descripcion", "DENTRO DEL DOMICILIO/SIN ACCESO" } },
                new() { { "id", 2 }, { "descripcion", "DE PARED" } },
                new() { { "id", 3 }, { "descripcion", "DE BANQUETA" } },
                new() { { "id", 4 }, { "descripcion", "EMPOTRADO EN PARED" } }
            };
            return collection;
        }

        public ICollection<Dictionary<string, object>> TiposToma(IEnlace ruta)
        {
            var collection = new List<Dictionary<string, object>>
            {
                new() { { "id", 1 }, { "descripcion", "SIN CUADRO" } },
                new() { { "id", 2 }, { "descripcion", "CUADRO CON MEDIDOR" } },
                new() { { "id", 3 }, { "descripcion", "CUADRO SIN MEDIDOR" } },
                new() { { "id", 4 }, { "descripcion", "CUADRO CON VÁLVULA" } },
                new() { { "id", 5 }, { "descripcion", "CUADRO CON MEDIDOR Y VÁLVULA" } },
                new() { { "id", 6 }, { "descripcion", "BOTA CON MEDIDOR" } },
                new() { { "id", 7 }, { "descripcion", "BOTA CON MEDIDOR Y VÁLVULA" } },
                new() { { "id", 8 }, { "descripcion", "BOTA SIN MEDIDOR Y VÁLVULA" } },
                new() { { "id", 9 }, { "descripcion", "EMPOTRADO" } }
            };
            return collection;
        }
    }
}