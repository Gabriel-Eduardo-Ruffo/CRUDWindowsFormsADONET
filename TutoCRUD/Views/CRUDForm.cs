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

namespace TutoCRUD.Views
{
    public partial class CRUDForm : Form
    {
        private int? id;//es un int nulleable
        public CRUDForm(int? _id=null)//si NO recibe un parametro el id es null
        {
            InitializeComponent();

            //Si el CRUDForm recibe un id. lo asignamos a una variable y probamos de cargar los
            //datos de los campos a editar por medio del metodo LoadData
            this.id = _id;
            if(this.id != null)
            {
                titleFormCrud.Text = "EDITAR REGISTRO";//cambiamos el titulo para que diga editar en lugar de crear como esta en el disenio
                LoadData();
            }
        }

        private void LoadData()
        {
            MyConnectionDB myConnectionDB = new MyConnectionDB();
            //del model dataDB (donde estan las properties del objeto que hace referencia a las columnas de la tabla en la DB)
            //creamos el objeto para llenarlo con lo que nos devuelve el metodo GetIdDataDB(id)
            DataDB dataDB = myConnectionDB.GetIdDataDB((int)id);

            //cargamos los datos en los campos correspondientes.
            txt_FirstName.Text = dataDB.FirstName;
            txt_LastName.Text = dataDB.LastName;
            txt_Age.Text = dataDB.Age.ToString();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            MyConnectionDB myConnectionDB = new MyConnectionDB();
            try
            {
                if(id != null)
                {
                    //Si no recibe id crea los datos
                    myConnectionDB.Update((int)id, txt_FirstName.Text, txt_LastName.Text, int.Parse(txt_Age.Text));
                }
                else
                {
                    //Si no recibe id crea los datos
                    myConnectionDB.Create(txt_FirstName.Text, txt_LastName.Text, int.Parse(txt_Age.Text));
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al crear un registro: " + ex.Message);
            }
        }
    }
}
