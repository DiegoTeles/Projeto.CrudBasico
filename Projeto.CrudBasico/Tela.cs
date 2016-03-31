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
    public partial class Tela : Form
    {
        public Tela()
        {
            InitializeComponent();
        }

        #region Variavveis Publicas

        public int Codigo = 0;
        public string Nome;
        public string Endereco;
        public string Telefone;
        public string Sexo;
        public bool Ativo;
        public DateTime DataCadastro;

        #endregion 

        private void Atualizar(int IdCliente, string Nome, string Email, string Telefone, string Sexo, bool Ativo)
        {
            try
            {
                Dados objDados = new Dados();
                objDados.Atualizar(IdCliente, Nome, Email, Telefone, Sexo, Ativo, DateTime.Now);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ops! Erro: " + ex.Message);
            }
        }


        public void Gravar ( string Nome, string Email, string Telefone, string Sexo, bool Ativo)
        {
            try
            {
                Dados objDados = new Dados();
                objDados.Gravar(Nome, Email, Telefone, Sexo, Ativo, DateTime.Now);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);   
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNome.Text) &&
                !String.IsNullOrEmpty(txtEndereco.Text))
            {
                string strSexo = string.Empty;
                bool blnAtivo = false;

                if (rbtMasculino.Checked)
                    strSexo = "M";
                else
                    strSexo = "F";

                if (rbtAtivo.Checked)
                    blnAtivo = true;
                else
                    blnAtivo = false;

                if (Codigo == 0)
                    Gravar(txtNome.Text, txtEndereco.Text, mskFone.Text, strSexo, blnAtivo);
                else
                    Atualizar(Codigo, txtNome.Text, txtEndereco.Text, mskFone.Text, strSexo, blnAtivo);
            }
            else
            {
                if (String.IsNullOrEmpty(txtNome.Text))
                {
                    epError.SetError(txtNome, "Informe o Nome");
                }
                if (String.IsNullOrEmpty(txtEndereco.Text))
                {
                    epError.SetError(txtEndereco, "Informe o Endereço");
                }
            }
        }

        //private void btnExcluir_Click(object sender, EventArgs e)
        //{
        //    Dados objDados = new Dados();
        //    objDados.Deletar(Convert.ToInt32(txtId.Text));
        //}

        private void Tela_Load(object sender, EventArgs e)
        {
            if(Codigo > 0)
            {
                btnGravar.Text = "Atualizar";
                txtNome.Text = Nome;
                txtEndereco.Text = Endereco;
                mskFone.Text = Telefone;

                if (Sexo.Equals("M"))
                    rbtMasculino.Checked = true;
                else
                    rbtFeminino.Checked = true;

                if (Ativo)
                    rbtAtivo.Checked = true;
                else
                    rbtInativo.Checked = true;
            }
            btnGravar.Text = "Gravar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.ShowDialog();
        }
    }
}
