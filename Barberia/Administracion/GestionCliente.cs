﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barberia
{
    public partial class GestionCliente : Form
    {
        Consultas consultas = new Consultas();
        string consulta = "";
        int id = 0;
        int fila = 0;
        string tbl = "tbl_clientes";

        public GestionCliente() //termindado
        {
            InitializeComponent();
            dgvGestionarClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGestionarClientes.RowHeadersVisible = false;
            txtIDCliente.Enabled = false;
            dgvGestionarClientes.DataSource = consultas.ActualizarTabla(tbl);

        }
        // volver al home
        private void btnHome_Click(object sender, EventArgs e) //terminado
        {
            Home AbrirHome = new Home(1);
            AbrirHome.Show();
            Hide();
        }
        // agragar cliente a la base de datos
        private void btnAgregar_Click(object sender, EventArgs e) //
        {
            if (validartxt(groupBox1))
            {

                consulta = $"INSERT INTO `tbl_clientes`(`Nombre_Cliente`, `Apellido_Cliente`, `Direccion_Cliente`, `Telefono_Cliente`, `DUI_Cliente`, `Correo_Electronico`, `Fecha_Registro`) VALUES ({txtNombre_Cliente.Text}','{txtApellido_Cliente.Text}','{txtDireccion_Cliente}','{mtxtTelefono_Cliente.Text}','{txtDUI_Cliente.Text}','{txtCorreo_Electronico.Text}',CURDATE())";
                MessageBox.Show(consulta);
                consultas.Query(consulta);
                dgvGestionarClientes.DataSource = consultas.ActualizarTabla("tbl_clientes");
                limpiar(groupBox1);
            }
            else
            {
                MessageBox.Show("Llene todos los campos");
            }

        }
        // boton para eliminar
        private void btnEliminar_Click(object sender, EventArgs e) // terminado
        {

            consulta = $"DELETE FROM `tbl_clientes` WHERE idCliente = {id} ";
            consultas.Query(consulta);
            dgvGestionarClientes.DataSource = consultas.ActualizarTabla("tbl_clientes");

        }
        // boton para aceptar los cambios y enviarlos a la base de datos
        private void btnAceptar_Click(object sender, EventArgs e) 
        {
            if (validartxt(groupBox1))
            {

                consulta = $"UPDATE `tbl_clientes` SET `Nombre_Cliente`='{txtNombre_Cliente.Text}',`Apellido_Cliente`='{txtApellido_Cliente.Text}',`Direccion_Cliente`='{txtDireccion_Cliente}',`Telefono_Cliente`='{mtxtTelefono_Cliente.Text}',`DUI_Cliente`='{txtDUI_Cliente.Text}',`Correo_Electronico`='{txtCorreo_Electronico.Text}',`Fecha_Registro`=CURDATE()  WHERE idCliente = {id} ";
                consultas.Query(consulta);
                dgvGestionarClientes.DataSource = consultas.ActualizarTabla("tbl_clientes");


                btnAceptar.Visible = false;
                btnEliminar.Enabled = true;
                btnHome.Enabled = true;
                btnAgregar.Enabled = true;
                limpiar(groupBox1);
            }
            else
            {
                MessageBox.Show("Llene todos los campos");

            }
        }
        // para ibtener la fila seleccionada y el id 
        private void dgvGestionarClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) //termindado
        {
            if (dgvGestionarClientes.CurrentRow != null)
            {
                fila = dgvGestionarClientes.CurrentRow.Index;
                var cellValue = dgvGestionarClientes.Rows[fila].Cells[0].Value;
                if (cellValue != null)
                {
                    id = int.Parse(cellValue.ToString());
                }
            }
        }

        // obtenemos los datos de la fila seleccionada y los mostramos en los txt
        private void btnEditar_Click(object sender, EventArgs e) //termindado
        {
            txtIDCliente.Text = dgvGestionarClientes.Rows[fila].Cells[0].Value.ToString();
            txtNombre_Cliente.Text = dgvGestionarClientes.Rows[fila].Cells[1].Value.ToString();
            txtApellido_Cliente.Text = dgvGestionarClientes.Rows[fila].Cells[2].Value.ToString();
            txtDireccion_Cliente.Text = dgvGestionarClientes.Rows[fila].Cells[3].Value.ToString();
            mtxtTelefono_Cliente.Text = dgvGestionarClientes.Rows[fila].Cells[4].Value.ToString();
            txtDUI_Cliente.Text = dgvGestionarClientes.Rows[fila].Cells[5].Value.ToString();
            txtCorreo_Electronico.Text = dgvGestionarClientes.Rows[fila].Cells[6].Value.ToString();
            btnAceptar.Visible = true;
            btnEliminar.Enabled = false;
            btnHome.Enabled = false;
            btnAgregar.Enabled = false;
        }
        // limiar txt
        private void limpiar(GroupBox gb) //terminado
        {
            foreach (Control item in gb.Controls)
            {
                if (item is TextBox)
                {
                    if (item != null)
                    {
                        item.Text = "";
                    }
                }
            }
            mtxtTelefono_Cliente.Text = null;
        }
        //--------------------------------------------------------------------------------
        private bool validartxt(GroupBox gb)
        {
            foreach (Control item in gb.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GestionCliente_Load(object sender, EventArgs e)
        {

        }
    }

}
