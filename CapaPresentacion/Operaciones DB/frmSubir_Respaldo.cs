using ExcelDataReader;
using FuncionalidadExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CapaNegocio;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuncionalidadExcel
{
    private DataSet dtsTablas = new DataSet();
    public partial class frmSubir_Respaldo : Form
    {
        public frmSubir_Respaldo()
        {
            InitializeComponent();
        }

        private void DiseñoInicial()
        {
            btnBuscar.Cursor = Cursors.Hand;
            btnMostrar.Cursor = Cursors.Hand;
            btnRegistrarData.Cursor = Cursors.Hand;
            txtRuta.Enabled = true;

            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.ReadOnly = true;
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmSubir_Respaldo_Load(object sender, EventArgs e)
        {
            DiseñoInicial();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //configuracion de ventana para seleccionar un archivo
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.Filter = "Excel Worbook|*.xlsx";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                cboHojas.Items.Clear();
                dgvDatos.DataSource = null;

                txtRuta.Text = oOpenFileDialog.FileName;

                //FileStream nos permite leer, escribir, abrir y cerrar archivos en un sistema de archivos, como matrices de bytes
                FileStream fsSource = new FileStream(oOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);


                //ExcelReaderFactory.CreateBinaryReader = formato XLS
                //ExcelReaderFactory.CreateOpenXmlReader = formato XLSX
                //ExcelReaderFactory.CreateReader = XLS o XLSX
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fsSource);

                //convierte todas las hojas a un DataSet
                dtsTablas = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                //obtenemos las tablas y añadimos sus nombres en el desplegable de hojas
                foreach (DataTable tabla in dtsTablas.Tables)
                {
                    cboHojas.Items.Add(tabla.TableName);
                }
                cboHojas.SelectedIndex = 0;

                reader.Close();

            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            dgvDatos.DataSource = dtsTablas.Tables[cboHojas.SelectedIndex];
        }

        private void btnRegistrar_Data_Click(object sender, EventArgs e)
        {
            DataTable data = (DataTable)(dgvDatos.DataSource);

            bool resultado = new CapaNegocio.Operaciones().CargarData(data);

            if (resultado)
            {
                MessageBox.Show("Se registro la data");
            }
            else
            {
                MessageBox.Show("Hubo un problema al registrar");
            }
        }
    }
}
