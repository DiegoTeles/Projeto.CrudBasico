using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.CrudBasico
{
    public partial class Consulta : Form
    {
        public Consulta()
        {
            InitializeComponent();
        }

        

        private void EditarRegistro()
        {

        int Codigo;
        string Nome;
        string Endereco;
        string Telefone;
        string Sexo;
        bool Ativo;
        DateTime DataCadastro;

            try
            {
                if( lstClientes.SelectedItems.Count > 0)
                {
                    Codigo = Convert.ToInt32(lstClientes.SelectedItems[0].Text);
                    Nome = lstClientes.SelectedItems[0].SubItems[1].Text;
                    Telefone = lstClientes.SelectedItems[0].SubItems[2].Text;
                    Endereco = lstClientes.SelectedItems[0].SubItems[3].Text;
                    Sexo = lstClientes.SelectedItems[0].SubItems[4].Text;

                    if (lstClientes.SelectedItems[0].SubItems[5].Text.Equals("Sim"))
                        Ativo = true;
                    else
                        Ativo = false;
                    
                    DataCadastro = Convert.ToDateTime(lstClientes.SelectedItems[0].SubItems[6].Text);

                    Tela objTela = new Tela();
                    objTela.Codigo = Codigo;
                    objTela.Nome = Nome;
                    objTela.Endereco = Endereco;
                    objTela.Telefone = Telefone;
                    objTela.Sexo = Sexo;
                    objTela.Ativo = Ativo;
                    objTela.DataCadastro = DataCadastro;
                    objTela.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ops! Erro: " + ex.Message);
            }
    }

        private void ExcluirRegistro()
        {
            int Codigo = 0;

            try
            {
                if (lstClientes.SelectedItems.Count > 0)
                    Codigo = Convert.ToInt32(lstClientes.SelectedItems[0].Text);

                Dados objDados = new Dados();

                if (Codigo > 0)
                    objDados.Deletar(Codigo);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ops! Erro: " + ex.Message);
            }

        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            CarregarListView();
        }

        //private void CarregarGridView()
        //{
        //    Dados objDados = new Dados();
        //    List<CrudBasico.Dados.Clientes> lstClientes = new List<Dados.Clientes>();

        //    lstClientes = objDados.Consultar();
        //    dgvClientes.DataSource = lstClientes;

        //}

        private void CarregarListView()
        {
            Dados objDados = new Dados();
            List<CrudBasico.Dados.Clientes> listaClientes = new List<Dados.Clientes>();

            listaClientes = objDados.Consultar();
            

            foreach (var itemLista in listaClientes)
            {
                ListViewItem objListViewItem = new ListViewItem();
                objListViewItem.Text = itemLista.ID_NOME.ToString();
                objListViewItem.SubItems.Add(itemLista.DS_NOME);
                objListViewItem.SubItems.Add(itemLista.DS_EMAIL);
                objListViewItem.SubItems.Add(itemLista.NM_TELEFONE);
                objListViewItem.SubItems.Add(itemLista.DS_SEXO);

                if (itemLista.ATIVO)
                    objListViewItem.SubItems.Add("Sim");
                else
                    objListViewItem.SubItems.Add("Não");

                objListViewItem.SubItems.Add(itemLista.DT_CADASTRO.ToShortDateString());
                lstClientes.Items.Add(objListViewItem);
            }
        }

        #region botoes

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ExcluirRegistro();
         }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        #endregion

    
    }
}
