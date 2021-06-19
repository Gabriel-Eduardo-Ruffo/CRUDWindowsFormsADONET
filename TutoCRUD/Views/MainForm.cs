using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TutoCRUD.Models;
using TutoCRUD.Views;

namespace TutoCRUD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyRefresh();
        }

        public void MyRefresh()
        {
            MyConnectionDB myConnectionDB = new MyConnectionDB();
            dataGridView1.DataSource = myConnectionDB.GetDataDB();
        }

        private void btn_TestConnection_Click(object sender, EventArgs e)
        {
            MyConnectionDB myConnectionDB = new MyConnectionDB();
            if (myConnectionDB.ConnectionOK())
            {
                MessageBox.Show("Conectado");
            }
            else
            {
                MessageBox.Show("no anda");
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            MyRefresh();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            CRUDForm crudForm = new CRUDForm();
            crudForm.ShowDialog();
            MyRefresh();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //Si tengo fila ROW seleccionada el CRUDForm que voy a instanciar es el mismo pero sobrecargo el metodo con el id
            //Con el ID por parametro que paso, se va a buscar los datos necesarios a la DB y seran mostrados en el mismo formulario
            //que se uso para crear. Ahora se usa para editar.
            int? id = GetSelectedRow();
            if(id != null)
            {
                CRUDForm crudForm = new CRUDForm(id);
                crudForm.ShowDialog();
                MyRefresh();
            }
            //si no tengo ninguna row seleccionada, este boton no hace nada.
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedRow();
            try
            {
                if (id != null)
                {
                    MyConnectionDB myConnection = new MyConnectionDB();
                    myConnection.Delete((int)id);//metodo instanciado para solo borrar.
                    MyRefresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar borrar: " + ex.Message);
            }

        }

        #region HELPER
        private int? GetSelectedRow()
        {
            //seteamos en el datagridview que sea seleccionable toda la fila
            //accedemos a ese valor por medio de este helper
            //dataGridView1--> buscamos la ROW seleccionada por su index--> elejimos la celda 0 que es donde esta el id de la tabla en la DB
            //de la celda seleccionada sacamos su valor convirtiendolo en string... y por ultimo con int.Parse lo convertimos a INT
            try{
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }           
        }
        #endregion

    }
}
