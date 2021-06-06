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
    public partial class frmConsulta_Stock_Articulos : Form
    {
        public frmConsulta_Stock_Articulos()
        {
            InitializeComponent();
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            //this.dataListado.Columns[5].Visible = false;
            //this.dataListado.Columns[6].Visible = false;

        }

        //Método Mostrar
       
       private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar_Inventario();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }


        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmConsulta_Stock_Articulos_Load_1(object sender, EventArgs e)
        {
            this.Mostrar();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmInventario frm = new frmInventario();
            frm.ShowDialog();
        }
    }
}
