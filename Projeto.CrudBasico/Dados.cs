using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Projeto.CrudBasico
{
    public class Dados
    {
        // VARIAVEL QUE RECEBERA A STRING DE CONEXAO

        public string strConexao = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // VARIAVEIS CONSTANTES PARA INSTRUÇÃO DO CRUD

        public const string strDelete = "DELETE FROM TB_CLIENTES WHERE ID_NOME = @ID_NOME";
        public const string strInsert = "INSERT INTO TB_CLIENTES values " +
            "(@DS_NOME, @NM_TELEFONE, @DS_EMAIL, @DS_SEXO, @ATIVO, @DT_CADASTRO)";
        public const string strSelect = "SELECT ID_NOME, DS_NOME, NM_TELEFONE, DS_EMAIL, DS_SEXO, ATIVO, DT_CADASTRO FROM TB_CLIENTES ";
        public const string strSelectLogin = "SELECT ID_LOGIN, DS_LOGIN, DS_SENHA FROM TB_LOGIN where DS_LOGIN = @DS_LOGIN AND DS_SENHA = @DS_SENHA" ;
        public const string strUpdate = "UPDATE TB_CLIENTES SET DS_NOME = @DS_NOME, NM_TELEFONE = @NM_TELEFONE, " +
            " DS_EMAIL = @DS_EMAIL, DS_SEXO = @DS_SEXO, ATIVO = @ATIVO, DT_CADASTRO = @DT_CADASTRO  WHERE ID_NOME = @ID_NOME";

        public class Clientes
        {
            public int ID_NOME { get; set;}
            public string DS_NOME { get; set; }
            public string NM_TELEFONE { get; set; }
            public string DS_EMAIL { get; set; }
            public string DS_SEXO { get; set; }
            public bool ATIVO { get; set; }
            public DateTime DT_CADASTRO { get; set; }
        }

        public class Login
        {
            public int ID_LOGIN { get; set; }
            public string DS_LOGIN { get; set; }
            public string DS_SENHA { get; set; }

        }
        #region Métodos

        public void Atualizar (int IdNome, string Nome, string Telefone, string Email, string Sexo, bool Ativo, DateTime DataCadastro)
        {

            using (SqlConnection objConexao = new SqlConnection(strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand(strUpdate, objConexao))
                {
                    objCommand.Parameters.AddWithValue("@ID_NOME", IdNome);
                    objCommand.Parameters.AddWithValue("@DS_NOME", Nome);
                    objCommand.Parameters.AddWithValue("@NM_TELEFONE", Telefone);
                    objCommand.Parameters.AddWithValue("@DS_EMAIL", Email);
                    objCommand.Parameters.AddWithValue("@DS_SEXO", Sexo);
                    objCommand.Parameters.AddWithValue("@ATIVO", Ativo);
                    objCommand.Parameters.AddWithValue("@DT_CADASTRO", DataCadastro);

                    objConexao.Open();

                    objCommand.ExecuteNonQuery();

                    objConexao.Close();
                }
            }
        }

        public List<Clientes> Consultar()
        {
            List<Clientes> lstClientes = new List<Clientes>();

            using (SqlConnection objConexao = new SqlConnection(strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand(strSelect, objConexao))
                {
                    {
                        objConexao.Open();

                        SqlDataReader objDataReader = objCommand.ExecuteReader();

                        if (objDataReader.HasRows)
                        {
                            while (objDataReader.Read())
                            {
                                Clientes objClientes = new Clientes();
                                objClientes.ID_NOME = Convert.ToInt32(objDataReader["ID_NOME"].ToString());
                                objClientes.DS_NOME = objDataReader["DS_NOME"].ToString();
                                objClientes.NM_TELEFONE = objDataReader["NM_TELEFONE"].ToString();
                                objClientes.DS_EMAIL = objDataReader["DS_EMAIL"].ToString();
                                objClientes.DS_SEXO = objDataReader["DS_SEXO"].ToString();

                                if (objDataReader["Ativo"].ToString().Equals("0"))
                                    objClientes.ATIVO = false;
                                else
                                    objClientes.ATIVO = true;

                                objClientes.DT_CADASTRO = Convert.ToDateTime(objDataReader["DT_CADASTRO"].ToString());
                                lstClientes.Add(objClientes);
                                
                            }
                            objDataReader.Close();
                        }

                        objConexao.Close();
                      }
                    }

                    return lstClientes;
              }
        }

        public List<Login> ConsultarLogin(string DS_Login, string DS_Senha)
        {
            List<Login> lstLogin = new List<Login>();

            using (SqlConnection objConexao = new SqlConnection(strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand(strSelectLogin, objConexao))
                {
                    objCommand.Parameters.AddWithValue("@DS_LOGIN", DS_Login);
                    objCommand.Parameters.AddWithValue("@DS_SENHA", DS_Senha);
                    objConexao.Open();

                        SqlDataReader objDataReader = objCommand.ExecuteReader();   

                        if (objDataReader.HasRows)
                        {
                            while (objDataReader.Read())
                            {
                                Login objLogin = new Login();
                                objLogin.ID_LOGIN = Convert.ToInt32(objDataReader["ID_LOGIN"].ToString());
                                objLogin.DS_LOGIN = objDataReader["DS_LOGIN"].ToString();
                                objLogin.DS_SENHA = objDataReader["DS_SENHA"].ToString();
                                lstLogin.Add(objLogin);

                            }
                            objDataReader.Close();
                        }

                        objConexao.Close();
                    }
                }

                return lstLogin;
            }
        

        public void Deletar (int IdNome)
        {

            using (SqlConnection objConexao = new SqlConnection(strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand(strDelete, objConexao))
                {
                    objCommand.Parameters.AddWithValue("@ID_NOME", IdNome);

                    objConexao.Open();

                    objCommand.ExecuteNonQuery();

                    objConexao.Close();
                }
            }
        }

        public void Gravar(string Nome, string Telefone, string Email, string Sexo, bool Ativo, DateTime DataCadastro)
        {
            using (SqlConnection objConexao = new SqlConnection(strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand(strInsert, objConexao))
                {
                    objCommand.Parameters.AddWithValue("@DS_NOME", Nome);
                    objCommand.Parameters.AddWithValue("@NM_TELEFONE", Telefone);
                    objCommand.Parameters.AddWithValue("@DS_EMAIL", Email);
                    objCommand.Parameters.AddWithValue("@DS_SEXO", Sexo);
                    objCommand.Parameters.AddWithValue("@ATIVO", Ativo);
                    objCommand.Parameters.AddWithValue("@DT_CADASTRO", DataCadastro);

                    objConexao.Open();

                    objCommand.ExecuteNonQuery();

                    objConexao.Close();
                }
            }
        }

        #endregion
    }
}
