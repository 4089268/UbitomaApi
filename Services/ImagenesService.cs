using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RutasApi.Data;
using RutasApi.Models.Entities.ArquosMedia;
using RutasApi.Models.Entities.Sicem;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RutasApi.Models.Responses;

namespace RutasApi.Services {
    public class ImagenesService {

        private readonly ILogger<ImagenesService> logger;

        public ImagenesService(ILogger<ImagenesService> logger){
            this.logger = logger;
        }
        

        /// <summary>
        /// attempt ot insert the image to the ArquosMedia database
        /// </summary>
        /// <param name="oficina"></param>
        /// <param name="idPadron"></param>
        /// <param name="imagene"></param>
        /// <param name="descripcion"></param>
        /// <returns> 0 if fail at some point or 1 if the image was inserted </returns>
        public int AlmacenarImagenes(Ruta oficina, int idPadron, IFormFile imagene, string descripcion = "" ){
            if(imagene == null){
                return 0;
            }

            // * attempt to get the user of the ArquosMedia
            var idUsuario = ObtenerIdUsuarioPorUsuario(oficina, "CONSULTA");

            // * attempt to upload the image file
            try {
                using(var _dbContext = new ArquosMediaContext(oficina.ConnectionStringMedia)) {
                
                    var _extencion = imagene.FileName.Split(".").Last();
                    byte[] _imageData;
                    using(var ms = new MemoryStream()){
                        imagene.CopyTo(ms);
                        _imageData = ms.ToArray();
                    }

                    var _newImage = new OprImagene() {
                        IdPadron  = idPadron,
                        IdTabla = idPadron.ToString(),
                        Tabla = "CAT_PADRON",
                        Descripcion =  descripcion,
                        IdMediatype = 1,  // 1 => Imagen, 2 => Video'
                        Imagen = _imageData,
                        FechaInsert = DateTime.Now,
                        Filesize = imagene.Length,
                        FileExtension = $".{_extencion}",
                        Archivo = string.Format("Ubitoma-{0}-{1}.{2}", idPadron.ToString() ,Guid.NewGuid().ToString().Replace("-","").Substring(0,6), _extencion),
                        IdInsert = idUsuario
                    };
                    _dbContext.OprImagenes.Add(_newImage);
                    _dbContext.SaveChanges();
                }
                return 1;
            }
            catch(Exception err) {
                this.logger.LogError(err, "Fail to upload the image to the media database: {message}", err.Message);
                return 0;
            }

        }

        public IEnumerable<ImageInfoResponse>? ObtenerImagenes(Ruta ruta, DateTime desde, DateTime hasta){
            IEnumerable<ImageInfoResponse> response = Array.Empty<ImageInfoResponse>();
            try {
                using (var mediaDBContext = new ArquosMediaContext(ruta.ConnectionStringMedia)){
                    response = mediaDBContext.OprImagenes
                        .Where(item => item.FechaInsert!.Value.Date >= desde && item.FechaInsert!.Value.Date <= hasta)
                        .Where(item => item.Tabla == "CAT_PADRON" && item.Archivo.Contains("ubitoma"))
                        .Select( item => new ImageInfoResponse {
                            Id_Imagen = (int) item.IdImagen,
                            Tabla = item.Tabla,
                            Descripcion = item.Descripcion,
                            Archivo = item.Archivo,
                            FechaInsert = item.FechaInsert!.Value.ToString("yyy-MM-dd"),
                            Id_Padron = (int) item.IdPadron!,
                            Filesize = (int) (item.Filesize??0),
                            FileExtension = item.FileExtension
                        })
                        .ToList();
                }
                return response;
            }
            catch (System.Exception err){
                this.logger.LogError(err, "Fail to get the images of the media database: {message}", err.Message);
                return null;
            }
        }

        private string ObtenerIdUsuarioPorUsuario(Ruta enlace, string usuario)
        {
            var _result = "NERUS";

            using(var connection = new SqlConnection(enlace.ConnectionString))
            {
                connection.Open();
                var _query = $"SELECT id_usuario, nombre FROM [Global].[Sys_Usuarios] Where usuario = '{usuario}'";
                var _command = new SqlCommand(_query, connection);
                using(var reader = _command.ExecuteReader())
                {
                    if (reader.Read()) {
                        _result = reader["id_usuario"].ToString() ?? "";
                    }
                }
                connection.Close();
            }
            
            return _result;
        }

    }
}
