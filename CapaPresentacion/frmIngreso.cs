using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion;
using CapaNegocio;
using System.Data.SqlClient;

namespace CapaPresentacion
{
    public partial class frmIngreso : Form
    {

        private bool IsNuevo = false;
        private bool IsEditar = false;
        public int Idtrabajador;

        private DataTable dtDetalle;

        private decimal totalPagado = 0;

        private static frmIngreso _instancia;


        public static frmIngreso GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new frmIngreso();
            }
            return _instancia;
        }

        

        public void setProveedor(string idproveedor, string nombre)
        {
            this.txtIdproveedor.Text = idproveedor;
            this.txtProveedor.Text = nombre;
        }
        public void setArticulo(string idarticulo, string nombre, string upc)
        {
            this.txtUPC.Text = upc;
            this.txtIdarticulo.Text = idarticulo;
            this.txtArticulo.Text = nombre;
        }

        public frmIngreso()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtProveedor, "Seleccione el Proveedor");
            this.ttMensaje.SetToolTip(this.txtSerie, "Ingrese la serie del comprobante");
            this.ttMensaje.SetToolTip(this.txtCorrelativo, "Ingrese el número del comprobante");
            this.ttMensaje.SetToolTip(this.txtStock, "Ingrese la cantidad de compra");
            this.ttMensaje.SetToolTip(this.txtArticulo, "Seleccione el Artículo de compra");
            this.txtUPC.ReadOnly = true;
            this.txtIdproveedor.Visible = false;
            //this.lblProveedor.Visible = false;
            //this.txtnombreProveedor.Visible = false;
            this.txtIdarticulo.Visible = false;
            this.txtProveedor.ReadOnly = true;
            this.txtArticulo.ReadOnly = true;
            this.txtnombreProveedor.Visible = false;
            this.lblProveedor.Visible = false;
            this.dtFecha_Produccion.Visible = false;
            this.dtFecha_Vencimiento.Visible = false;
            

        }
        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtIdingreso.Text = string.Empty;
            this.txtUPC.Text = string.Empty;
            this.txtIdproveedor.Text = string.Empty;
            this.txtProveedor.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCorrelativo.Text = string.Empty;
            this.txtIva.Text = "0";
            this.lblTotalPagado.Text = "0,0";
            this.crearTabla();

        }

        private void limpiarDetalle()
        {
            this.txtIdarticulo.Text = string.Empty;
            this.txtArticulo.Text = string.Empty;
            this.txtStock.Text = string.Empty;
            this.txtPrecio_Compra.Text = string.Empty;
            this.txtPrecio_Venta.Text = string.Empty;
            //this.txtIva.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtIdingreso.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCorrelativo.ReadOnly = !valor;
            this.txtIva.ReadOnly = !valor;
            
            this.dtFecha.Enabled = valor;
            this.cbTipo_Comprobante.Enabled = valor;
            this.txtStock.ReadOnly = !valor;
            this.txtPrecio_Compra.ReadOnly = !valor;
            this.txtPrecio_Venta.ReadOnly = !valor;
            this.btnAgregar.Enabled = valor;
            this.btnQuitar.Enabled = valor;
            this.btnBuscarProveedor.Enabled = valor;
            this.btnBuscarArticulo.Enabled = valor;
            this.txtIdingreso.Visible = false;
        }

        //Habilitar los botones
        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }

        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        //Método Mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NIngreso.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarFecha
        private void BuscarFechas() 
        {
            this.dataListado.DataSource = NIngreso.BuscarFechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
            this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarProveedorFechas()
        {

            this.dataListado.DataSource = NIngreso.BuscarProveedorFechas(this.txtnombreProveedor.Text.ToUpper(), this.dtFecha1.Value.ToString("dd/MM/yyyy"),
            this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarProveedor()
        {
            this.dataListado.DataSource = NIngreso.BuscarProveedor(this.txtnombreProveedor.Text.ToUpper());
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }


        //Método BuscarDetalles
        private void MostrarDetalles()
        {
            
            this.dataListadoDetalle.DataSource = NIngreso.MostrarDetalle(this.txtIdingreso.Text);
        }

        //Crea la tabla de Detalle 
        private void crearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("codigo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("precio_compra", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("precio_venta", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("stock_inicial", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
           // this.dtDetalle.Columns.Add("Impuesto", System.Type.GetType("System.Decimal"));
            //Relacionar nuestro DataGRidView con nuestro DataTable
            this.dataListadoDetalle.DataSource = this.dtDetalle;

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtSerie_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            //Para ubicar al formulario en la parte superior del contenedor
            this.Top = 0;
            this.Left = 0;
            //Mostrar
            this.Mostrar();
            //Deshabilita los controles
            this.Habilitar(false);
            //Establece los botones
            this.Botones();
            this.crearTabla();
        }

        private void frmIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            frmVistaProveedor_Ingreso vista = new frmVistaProveedor_Ingreso();
            vista.ShowDialog();
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            frmVistaArticulo_Ingreso vista = new frmVistaArticulo_Ingreso();
            vista.ShowDialog();

        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdproveedor_TextChanged(object sender, EventArgs e)
        {

        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (cbOpcion.Text.Equals("Fechas"))
            {
                this.BuscarFechas();
            }
            else if (cbOpcion.Text.Equals("Proveedor y Fecha"))
            {
                this.BuscarProveedorFechas();

            } else if (cbOpcion.Text.Equals("Proveedor"))
            {
                this.BuscarProveedor();
            }

            if (dataListado.Rows.Count > 0)
            {
                double total = 0;
                foreach (DataGridViewRow row in dataListado.Rows)
                {
                    total += Convert.ToDouble(row.Cells["Total"].Value);
                }

                lblTotal_Comprado.Text = Convert.ToString(total);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Anular los Ingresos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NIngreso.Anular(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Anuló Correctamente el registro");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdingreso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idingreso"].Value);
            this.txtProveedor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["proveedor"].Value);
            this.dtFecha.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fecha"].Value);
            this.cbTipo_Comprobante.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Total"].Value);
            this.txtIva.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Impuesto"].Value);
            this.MostrarDetalles();
            this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.limpiarDetalle();
            this.Habilitar(true);
            this.txtSerie.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtIdproveedor.Text == string.Empty || this.txtSerie.Text == string.Empty
                    || this.txtCorrelativo.Text == string.Empty || this.txtIva.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtIdproveedor, "Ingrese un Valor");
                    errorIcono.SetError(txtSerie, "Ingrese un Valor");
                    errorIcono.SetError(txtCorrelativo, "Ingrese un Valor");
                    errorIcono.SetError(txtIva, "Ingrese un Valor");
                }
                else
                {

                    if (this.IsNuevo)
                    {
                        rpta = NIngreso.Insertar(Idtrabajador, Convert.ToInt32(this.txtIdproveedor.Text),
                            dtFecha.Value, cbTipo_Comprobante.Text, txtSerie.Text, txtCorrelativo.Text,
                            Convert.ToDecimal(txtIva.Text), "EMITIDO", dtDetalle);

                    }


                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta el registro");
                        }


                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                    this.IsNuevo = false;
                    this.Botones();
                    this.Limpiar();
                    this.limpiarDetalle();
                    this.Mostrar();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.txtIdarticulo.Text == string.Empty || this.txtStock.Text == string.Empty
                    || this.txtPrecio_Compra.Text == string.Empty || this.txtPrecio_Venta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtIdarticulo, "Ingrese un Valor");
                    errorIcono.SetError(txtStock, "Ingrese un Valor");
                    errorIcono.SetError(txtPrecio_Compra, "Ingrese un Valor");
                    errorIcono.SetError(txtPrecio_Venta, "Ingrese un Valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idarticulo"]) == Convert.ToInt32(this.txtIdarticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el artículo en el detalle");
                        }
                    }
                    if (registrar)
                    {
                        decimal subTotal = Convert.ToDecimal(this.txtStock.Text) * Convert.ToDecimal(this.txtPrecio_Compra.Text);
                        totalPagado = totalPagado + subTotal;
                        this.lblTotalPagado.Text = totalPagado.ToString("");
                        //Agregar ese detalle al datalistadoDetalle
                        DataRow row = this.dtDetalle.NewRow();
                        row["idarticulo"] = Convert.ToInt32(this.txtIdarticulo.Text);
                        row["articulo"] = this.txtArticulo.Text;
                        row["codigo"] = this.txtUPC.Text;
                        row["precio_compra"] = Convert.ToDouble(this.txtPrecio_Compra.Text);
                        row["precio_venta"] = Convert.ToDouble(this.txtPrecio_Venta.Text);
                        row["stock_inicial"] = Convert.ToInt32(this.txtStock.Text);
                        row["subtotal"] = subTotal;

                        this.dtDetalle.Rows.Add(row);
                        this.limpiarDetalle();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.Botones();
            this.Limpiar();
            this.Limpiar();
            this.txtIdingreso.Text = string.Empty;
        }

        private void btnNuevo_click(object sender, EventArgs e)
        {
            try
            {

                if (this.txtIdarticulo.Text == string.Empty || this.txtStock.Text == string.Empty || txtPrecio_Compra.Text == string.Empty || txtPrecio_Venta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtArticulo, "Seleccione un Artículo");
                    errorIcono.SetError(txtStock, "Ingrese el stock inicial");
                    errorIcono.SetError(txtPrecio_Compra, "Ingrese el precio de Compra");
                    errorIcono.SetError(txtPrecio_Venta, "Ingrese el precio de Venta");
                }
                else
                {
                    //Variable que va a indicar si podemos registrar el detalle
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idarticulo"]) == Convert.ToInt32(this.txtIdarticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el artículo en el detalle");
                        }
                    }
                    //Si podemos registrar el producto en el detalle
                    if (registrar)
                    {
                        //Calculamos el sub total del detalle sin descuento
                        decimal subTotal = Convert.ToDecimal(this.txtPrecio_Compra.Text) * Convert.ToDecimal(txtStock.Text);
                        totalPagado = totalPagado + subTotal;
                        this.lblTotalPagado.Text = totalPagado.ToString("");
                        //Agregamos al fila a nuestro datatable
                        DataRow row = this.dtDetalle.NewRow();
                        row["idarticulo"] = Convert.ToInt32(this.txtIdarticulo.Text);
                        row["articulo"] = this.txtArticulo.Text;
                        row["codigo"] = this.txtUPC.Text;
                        row["precio_compra"] = Convert.ToDecimal(this.txtPrecio_Compra.Text);
                        row["precio_venta"] = Convert.ToDecimal(this.txtPrecio_Venta.Text);
                        row["stock_inicial"] = Convert.ToInt32(this.txtStock.Text);
                        row["subTotal"] = subTotal;

                        this.dtDetalle.Rows.Add(row);
                        this.limpiarDetalle();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                //Indice dila actualmente seleccionado y que vamos a eliminar
                int indiceFila = this.dataListadoDetalle.CurrentCell.RowIndex;
                //Fila que vamos a eliminar
                DataRow row = this.dtDetalle.Rows[indiceFila];
                //Disminuimos el total a pagar
                this.totalPagado = this.totalPagado - Convert.ToDecimal(row["subTotal"].ToString());
                this.lblTotalPagado.Text = totalPagado.ToString("#0.0#");
                //Removemos la fila
                this.dtDetalle.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MensajeError("No hay fila para remover");
            }
        }

        private void txtIgv_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataListadoDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public void Ver(bool bol)
        {
            this.lblProveedor.Visible = bol;
            this.txtnombreProveedor.Visible = bol;
        }

        public void Ver2(bool bol)
        {
            this.label10.Visible = bol;
            this.label2.Visible = bol;
            this.dtFecha2.Visible = bol;
            this.dtFecha1.Visible = bol;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbOpcion.Text.Equals("Fechas"))
            {
                this.Ver(false);
                this.Ver2(true);
            }
            else if (cbOpcion.Text.Equals("Proveedor y Fecha"))
            {
                this.Ver(true);
                this.Ver2(true);

            }
            else if (cbOpcion.Text.Equals("Proveedor"))
            {
                this.Ver(true);
                this.Ver2(false);

            }
            
                
           
        }

        private void txtnombreProveedor_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void lblProveedor_Click(object sender, EventArgs e)
        {
           
        }
    }
}