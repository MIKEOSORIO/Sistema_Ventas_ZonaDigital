using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NVenta
    {
        public static string Insertar(int idcliente, int idtrabajador,DateTime fecha,
            string tipo_comprobante, string serie, string correlativo, decimal iva,
            DataTable dtDetalles, string tipo_venta, string num_agente, string nombre_trabajador, string status, string agente)
        {
            DVenta Obj = new DVenta();
            Obj.Idcliente = idcliente;
            Obj.Idtrabajador = idtrabajador;
            Obj.Fecha = fecha;
            Obj.Tipo_Comprobante = tipo_comprobante;
            Obj.Serie = serie;
            Obj.Correlativo = correlativo;
            Obj.Iva = iva;
            List<DDetalle_Venta> detalles = new List<DDetalle_Venta>();
            Obj.Tipo_Venta = tipo_venta;
            Obj.Num_Agente = num_agente;
            Obj.Nombre_Trabajador = nombre_trabajador;
            Obj.Status = status;
            Obj.Agente = agente;

            foreach (DataRow row in dtDetalles.Rows)
            {
                DDetalle_Venta detalle = new DDetalle_Venta();
                detalle.Iddetalle_ingreso = Convert.ToInt32(row["iddetalle_ingreso"].ToString());
                detalle.Cantidad = Convert.ToInt32(row["cantidad"].ToString());
                detalle.Precio_Venta = Convert.ToDecimal(row["precio_venta"].ToString());
                detalle.Descuento = Convert.ToDecimal(row["descuento"].ToString());
                detalles.Add(detalle);
            }
            return Obj.Insertar(Obj, detalles);
        }
        public static string Eliminar(int idventa, string agente)
        {
            DVenta Obj = new DVenta();
            Obj.Idventa = idventa;
            Obj.Agente = agente;
            return Obj.Eliminar(Obj);
        }

        //Método Mostrar que llama al método Mostrar de la clase DVenta
        //de la CapaDatos
        public static DataTable Mostrar()
        {
            return new DVenta().Mostrar();
        }

        //Método BuscarFecha que llama al método BuscarFecha
        //de la clase DVenta de la CapaDatos

        public static DataTable BuscarFechas(string status, string textobuscar, string textobuscar2)
        {
            DVenta Obj = new DVenta();
            return Obj.BuscarFechas(status,textobuscar, textobuscar2);
        }

        public static DataTable Bucar_Venta_Trabajador(string textobuscar, string textobuscar1, string textobuscar2)
        {
            DVenta Obj = new DVenta();
            return Obj.Buscar_Ventas_Trabajador(textobuscar, textobuscar1 , textobuscar2);
        }

        public static DataTable MostrarDetalle(string textobuscar)
        {
            DVenta Obj = new DVenta();
            return Obj.MostrarDetalle(textobuscar);
        }
        public static DataTable MostrarArticulo_Venta_Nombre(string textobuscar)
        {
            DVenta Obj = new DVenta();
            return Obj.MostrarArticulo_Venta_Nombre(textobuscar);
        }
        public static DataTable MostrarArticulo_Venta_Codigo(string textobuscar)
        {
            DVenta Obj = new DVenta();
            return Obj.MostrarArticulo_Venta_codigo(textobuscar);
        }

    }
}
