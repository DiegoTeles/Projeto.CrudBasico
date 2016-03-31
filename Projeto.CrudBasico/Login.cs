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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            if( !string.IsNullOrEmpty(txtLogin.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            AcessarSistema(this.txtLogin.Text, this.txtSenha.Text);
        }

        private void AcessarSistema(string Login, String Senha)
        {
            Dados objDados = new Dados();
            List<Dados.Login> lstRetorno = objDados.ConsultarLogin(Login, Senha);

            if (lstRetorno != null && lstRetorno.Count > 0)
            {
                Tela objTela = new Tela();
                objTela.ShowDialog();
            }
        }
    }
}
