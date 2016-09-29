using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using TrabalhoRestBLL;

namespace TrabalhoRestAPW.webservices
{
    [WebService(Namespace = "roberto-oliveira")]
    [ScriptService]
    public class webservicewithrest : System.Web.Services.WebService
    {
        private static readonly webservicewithrestDataContext dc = new webservicewithrestDataContext();
        private static string mensagem { get; set; }


        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método para verificar se usuário já existe na base de dados.</p>" +
          "<p><b>Parâmetros: </b><b>  Email</b>: tipo String</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>True ou False</b>: tipo Boolean" + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool check_user(string email)
        {
            var verificador = (from result in dc.Usuarios
                               where result.Email.Trim().Equals(email)
                               select result).Count();

            return verificador > 0;
        }

        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método retorna o objeto usuário através de seu Id.</p>" +
          "<p><b>Parâmetros: </b><b>  Id</b>: tipo String</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>Id</b>: tipo String" + "</li>" +
                     "<li><b>Nome</b>: tipo String" + "</li>" +
                     "<li><b>Email</b>: tipo String" + "</li>" +
                     "<li><b>Senha</b>: tipo String" + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        public string return_user_by_id(string id)
        {
            var usuario = from result in dc.Usuarios
                          where result.Id == int.Parse(id)
                          select result;

            var jss = new JavaScriptSerializer();

            var json = jss.Serialize(usuario);

            return json;
        }

        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método retorna o objeto usuário através de seu Email.</p>" +
          "<p><b>Parâmetros: </b><b>  Email</b>: tipo String</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>Id</b>: tipo String" + "</li>" +
                     "<li><b>Nome</b>: tipo String" + "</li>" +
                     "<li><b>Email</b>: tipo String" + "</li>" +
                     "<li><b>Senha</b>: tipo String" + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        public string return_user_by_email(string email)
        {
            var usuario = from result in dc.Usuarios
                          where result.Email == email
                          select result;

            var jss = new JavaScriptSerializer();

            var json = jss.Serialize(usuario);

            return json;
        }

        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método retorna lista de usuários</p>" +
          "<p><b>Parâmetros: </b><b>  No parameters</b>: No type</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>List<Usuario></b>: tipo List<>" + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        public string return_list_users()
        {
            var lista = from result in dc.Usuarios
                        select result;

            var jss = new JavaScriptSerializer();
            var json = jss.Serialize(lista);

            return json;
        }


        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método de inserção de um novo usuário.</p>" +
          "<p><b>Parâmetros: </b><b>  Nome</b>: tipo String, <b>Email </b>: tipo String, <b>Senha </b>: tipo String</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>Mensagem</b>: Usuário cadastrado com sucesso." + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string create_user(string nome, string email, string senha)
        {
            if (check_user(email))
            {
                mensagem = "Já existe um usuário cadastrado com este email.";
            }
            else
            {
                var usuario = new Usuario
                {
                    Nome = nome,
                    Email = email,
                    Senha = senha
                };

                dc.Usuarios.InsertOnSubmit(usuario);
                dc.SubmitChanges();

                mensagem = "Usuário cadastrado com sucesso.";
            }
            return mensagem;
        }


        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método de atualização de um usuário.</p>" +
         "<p><b>Parâmetros: </b><b>   Id</b>: tipo String, <b>Nome</b>: tipo String, <b>Email </b>: tipo String, <b>Senha </b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Os dados do usuário foram atualizados com sucesso." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        public string update_user(int id, string nome, string email, string senha)
        {
            var usuario = dc.Usuarios.SingleOrDefault(u => u.Id == id);

            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Senha = senha;

            dc.SubmitChanges();

            mensagem = "Os dados do usuário foram atualizados com sucesso.";
            return mensagem;
        }

        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método de exclusão de um usuário.</p>" +
         "<p><b>Parâmetros: </b><b>   Id</b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Usuário excluído com sucesso." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        public string delete_user(int Id)
        {
            var usuario = dc.Usuarios.SingleOrDefault(u => u.Id == Id);

            if (usuario != null) dc.Usuarios.DeleteOnSubmit(usuario);
            dc.SubmitChanges();

            mensagem = "Usuário excluído com sucesso.";
            return mensagem;
        }
    }
}
