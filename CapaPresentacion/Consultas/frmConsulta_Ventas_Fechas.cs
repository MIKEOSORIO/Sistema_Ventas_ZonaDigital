using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion.Consultas
{
    public partial class frmConsulta_Ventas_Fechas : Form
    {


        public frmConsulta_Ventas_Fechas()
        {
            InitializeComponent();
        
        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }


        private void Buscar_Ventras_Fechas()
        {
            this.dataListado.DataSource = NVenta.Bucar_Venta_Trabajador(this.txtNum_Agente1.Text, this.dtFecha1.Value.ToString("dd/MM/yyyy"),
            this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }



        private void frmConsulta_Ventas_Fechas_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataListado_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {

            this.Buscar_Ventras_Fechas();

            if (dataListado.Rows.Count > 0)
            {
                double total = 0;
                foreach (DataGridViewRow row in dataListado.Rows)
                {
                    total += Convert.ToDouble(row.Cells["Total"].Value);
                }

                lblTotal_Vendido.Text = Convert.ToString(total);
            }
        }
        private void MostrarDetalle()
        {
            this.dataListadoDetalle.DataSource = NVenta.MostrarDetalle(this.txtIdventa.Text);

        }

        private void dataListadoDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdventa.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Idventa"].Value);
            this.txtCliente.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Cliente"].Value);
            // this.txtIdtrabajador.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idtrabajador"].Value);
            this.dtFecha.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha"].Value);
            this.cbTipo_Comprobante.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Total"].Value);
            this.txtIVA.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Impuesto"].Value);
            //   this.cbTipo_Venta.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Tipo_Venta"].Value);
            this.txtNum_Agente.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["num_agente"].Value);
            this.txtNombre_Trabajador.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Trabajador"].Value);
            this.MostrarDetalle();
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

   
    }
}
