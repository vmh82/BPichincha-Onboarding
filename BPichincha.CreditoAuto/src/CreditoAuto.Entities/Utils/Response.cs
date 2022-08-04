using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Utils
{
    public class Response<TResponse>
    {
        public HttpStatusCode Status { get; }
        public bool EsError { get; set; }
        public object Mensaje { get; set; }
        public TResponse Data { get; set; }

        public Response(TResponse response, object mensaje)
        {
            Data = response;
            EsError = false;
            Mensaje = mensaje;
            Status = HttpStatusCode.OK;
        }
        protected Response(object mensajeError)
        {
            Mensaje = mensajeError;
            EsError = true;
            Status = HttpStatusCode.InternalServerError;
        }

        public static Response<TResponse> Error(string error)
        {
            return new Response<TResponse>(error);
        }
        public static Response<TResponse> Ok(TResponse response, object descripcion)
        {
            return new Response<TResponse>(response, descripcion);
        }
    }
}
