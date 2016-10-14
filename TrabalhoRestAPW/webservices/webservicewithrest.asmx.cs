using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using RepositoryBLL;
using TrabalhoRestBLL;

namespace TrabalhoRestAPW.webservices
{
    [WebService(Namespace = "work-webservice-with-rest-json")]
    [ScriptService]
    public class webservicewithrest : System.Web.Services.WebService
    {
        private static readonly webservicewithrestDataContext dc = new webservicewithrestDataContext();
        private static readonly JavaScriptSerializer jss = new JavaScriptSerializer();
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
         "<br/><p><b>Descrição:</b> Método para verificar se usuário do facebook já existe na base de dados.</p>" +
         "<p><b>Parâmetros: </b><b>  Email</b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>True ou False</b>: tipo Boolean" + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool check_user_facebook(string email)
        {
            var verificador = (from u in dc.Usuarios
                               where u.Email.Trim().Equals(email) &&
                                     u.Facebook.Value
                               select u).Count();

            return verificador > 0;
        }


        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método para validar usuário na base de dados.</p>" +
         "<p><b>Parâmetros: </b><b>  Email</b>: tipo String, <b>Senha</b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>True ou False</b>: tipo Boolean" + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool check_user_authenticate(string email, string senha)
        {
            var verificador = (from u in dc.Usuarios
                               where u.Email.Trim().Equals(email)
                               && u.Senha.Trim().Equals(senha)
                               select u).Count();

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
        public string return_user_by_id(int id)
        {
            var usuario = dc.Usuarios.
                Where(u => u.Id == id).
                   Select(u => new
                   {
                       u.Id,
                       u.Name,
                       u.Email,
                       u.Senha
                   });

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
            var usuario = dc.Usuarios.
                   Where(u => u.Email == email).
                   Select(u => new
                   {
                       u.Id,
                       u.Name,
                       u.Email,
                       u.Senha,
                       u.Facebook
                   });

            var json = jss.Serialize(usuario);

            return json;
        }

        public Usuario ObterUsuario(string email)
        {
            var query = dc.Usuarios.Where(u => u.Email == email);
            return query.FirstOrDefault();
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
            var lista = dc.Usuarios.
                   Select(u => new
                   {
                       u.Id,
                       u.Name,
                       u.Email,
                       u.Senha
                   });

            var json = jss.Serialize(lista);

            return json;
        }


        [WebMethod(Description =
          "<br/><p><b>Descrição:</b> Método retorna lista das listas de tarefas por usuario</p>" +
          "<p><b>Parâmetros: </b><b>  Id do usuário</b>: Tipo: int</p>" +
            "<ul>" +
               "<p><b>Retorno: </b></p>" +
                  "<ul>" +
                     "<li><b>Lista</b>: tipo List<>" + "</li>" +
                  "</ul>" +
            "</ul><br/>"
        )]
        public string task_of_task_lists(int id)
        {
            var lista = dc.ListaTarefas.
                Where(lt => lt.UsuarioId == id).
                Select(lt => new
                {
                    lt.Nome,
                    lt.Cor
                });

            var json = jss.Serialize(lista);

            return json;
        }


        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método retorna lista de tarefas por usuario</p>" +
         "<p><b>Parâmetros: </b><b>  Id da lista das listas de tarefas</b>: Tipo: int</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Lista</b>: tipo List<>" + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        public string task_lists(int id)
        {
            var lista = dc.Tarefas.
                Where(l => l.ListaTarefaId == id).
                Select(l => new
                {
                    l.Descricao,
                    l.Status
                });

            var json = jss.Serialize(lista);

            return json;
        }


        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método retorna lista de usuários facebook</p>" +
         "<p><b>Parâmetros: </b><b>  No parameters</b>: No type</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>List<Usuario></b>: tipo List<>" + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        public string return_list_users_facebook()
        {
            var lista = dc.Usuarios.
                Where(u => u.Facebook.Value).
                Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    u.Senha
                });

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
                    Name = nome,
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
         "<br/><p><b>Descrição:</b> Método de inserção de um novo usuário através de login do facebook.</p>" +
         "<p><b>Parâmetros: </b><b>  Nome </b>: tipo String, <b>Email </b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Usuário cadastrado com sucesso." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
       )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string create_user_facebook(string _nome, string _email)
        {
            if (check_user_facebook(_email))
            {
                mensagem = "Já existe um usuário cadastrado com este email.";
            }
            else
            {
                var usuario = new Usuario();
                var pass = Auxiliar.gerarSenha(7);

                usuario.Name = _nome;
                usuario.Email = _email;
                usuario.Senha = pass;
                usuario.Facebook = true;

                dc.Usuarios.InsertOnSubmit(usuario);
                dc.SubmitChanges();

                Auxiliar.SendEmail("robertocoliveira@gmail.com", "robertocoliveira@gmail.com", "Segue sua nova senha: " + pass);

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

            usuario.Name = nome;
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
        public string delete_user(int id)
        {
            var usuario = dc.Usuarios.SingleOrDefault(u => u.Id == id);

            if (usuario != null) dc.Usuarios.DeleteOnSubmit(usuario);
            dc.SubmitChanges();

            mensagem = "Usuário excluído com sucesso.";
            return mensagem;
        }


        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método de inserção de uma nova lista.</p>" +
         "<p><b>Parâmetros: </b><b>  UsuarioId</b>: tipo String, <b>Nome </b>: tipo String, <b>Cor </b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Lista cadastrada com sucesso." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
        )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string create_task_list(int UsuarioId, string nome, string cor)
        {
            var lTarefa = new ListaTarefa
            {
                UsuarioId = UsuarioId,
                Nome = nome,
                Cor = cor
            };

            dc.ListaTarefas.InsertOnSubmit(lTarefa);
            dc.SubmitChanges();

            mensagem = "Lista cadastrada com sucesso.";

            return mensagem;
        }

        [WebMethod(Description =
        "<br/><p><b>Descrição:</b> Método de inserção de uma nova tarefa.</p>" +
        "<p><b>Parâmetros: </b><b>  ListaDeTarefaId</b>: tipo String, <b>Descrição </b>: tipo String</p>, <b>Status </b>: tipo String</p>" +
          "<ul>" +
             "<p><b>Retorno: </b></p>" +
                "<ul>" +
                   "<li><b>Mensagem</b>: Tarefa cadastrada com sucesso." + "</li>" +
                "</ul>" +
          "</ul><br/>"
        )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string create_task(string ListaDeTarefaId, string descricao, string status)
        {
            var tarefa = new Tarefa
            {
                ListaTarefaId = int.Parse(ListaDeTarefaId),
                Descricao = descricao,
                Status = status
            };

            dc.Tarefas.InsertOnSubmit(tarefa);
            dc.SubmitChanges();

            mensagem = "Tarefa cadastrada com sucesso.";

            return mensagem;
        }



        [WebMethod(Description =
        "<br/><p><b>Descrição:</b> Método de atualização de uma tarefa.</p>" +
        "<p><b>Parâmetros: </b><b>   TarefaId</b>: tipo String, <b>Status</b>: tipo String,</p>" +
          "<ul>" +
             "<p><b>Retorno: </b></p>" +
                "<ul>" +
                   "<li><b>Mensagem</b>: Tarefa atualizada com sucesso." + "</li>" +
                "</ul>" +
          "</ul><br/>"
        )]
        public string update_task(int TarefaId, string status)
        {
            var tarefa = dc.Tarefas.SingleOrDefault(t => t.Id == TarefaId);

            tarefa.Status = status;

            dc.SubmitChanges();

            mensagem = "Tarefa atualizada com sucesso.";
            return mensagem;
        }

        [WebMethod(Description =
        "<br/><p><b>Descrição:</b> Método de atualização de uma lista de tarefa.</p>" +
        "<p><b>Parâmetros: </b><b>   ListaDeTarefaId</b>: tipo String, <b>Nome</b>: tipo String, <b>Cor</b>: tipo String</p>" +
          "<ul>" +
             "<p><b>Retorno: </b></p>" +
                "<ul>" +
                   "<li><b>Mensagem</b>: Tarefa atualizada com sucesso." + "</li>" +
                "</ul>" +
          "</ul><br/>"
        )]
        public string update_task_list(int ListaDeTarefaId, string nome, string cor)
        {
            var lista = dc.ListaTarefas.SingleOrDefault(l => l.Id == ListaDeTarefaId);

            lista.Nome = nome;
            lista.Cor = cor;

            dc.SubmitChanges();

            mensagem = "Lista de tarefa atualizada com sucesso.";
            return mensagem;
        }

        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método de exclusão de uma lista.</p>" +
         "<p><b>Parâmetros: </b><b>   ListaDeTarefaId</b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Tarefa excluída com sucesso." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
        )]
        public string remove_task_list(int ListaDeTarefaId)
        {
            var list = dc.ListaTarefas.SingleOrDefault(u => u.Id == ListaDeTarefaId);

            if (list != null) dc.ListaTarefas.DeleteOnSubmit(list);
            dc.SubmitChanges();

            mensagem = "Tarefa excluída com sucesso.";
            return mensagem;
        }

        [WebMethod(Description =
         "<br/><p><b>Descrição:</b> Método de localização e autenticação de usuário.</p>" +
         "<p><b>Parâmetros: </b><b>   Email</b>: tipo String, <b>Senha</b>: tipo String</p>" +
           "<ul>" +
              "<p><b>Retorno: </b></p>" +
                 "<ul>" +
                    "<li><b>Mensagem</b>: Retorna o usuário." + "</li>" +
                 "</ul>" +
           "</ul><br/>"
        )]
        public string user_authenticate(string email, string senha)
        {
            if (check_user_authenticate(email, senha))
            {
                var usuario = dc.Usuarios.
                    Where(u => u.Email == email).
                    Select(u => new
                    {
                        u.Id,
                        u.Name,
                        u.Email,
                        u.Senha
                    });

                var json = jss.Serialize(usuario);

                return json;
            }
            mensagem = "Email não existe na base; Senha não confere com o email cadastrado.";
            return mensagem;
        }

        [WebMethod(Description =
        "<br/><p><b>Descrição:</b> Método de localização do usuário, resetar sua senha e enviar um email com a nova senha.</p>" +
        "<p><b>Parâmetros: </b><b>   Email</b>: tipo String, <b>Senha</b>: tipo String</p>" +
          "<ul>" +
             "<p><b>Retorno: </b></p>" +
                "<ul>" +
                   "<li><b>Mensagem</b>: Retorna mensagem de confirmação ao usuário." + "</li>" +
                "</ul>" +
          "</ul><br/>"
       )]
        public string forgot_password(string email)
        {
            try
            {
                if (check_user(email))
                {
                    var usuario = ObterUsuario(email);
                    var senha = Auxiliar.gerarSenha(7);
                    var body = "Webservice lhe enviou uma nova senha: " + senha;

                    usuario.Senha = senha;
                    dc.SubmitChanges();

                    Auxiliar.SendEmail("teste@teste.com.br", email, body);

                    mensagem = "Senha atualizada com sucesso. Verifique seu email.";
                }
                else
                {
                    mensagem = "O email " + "'" + email + "'" + " não consta em nossa base de dados.";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }
    }
}
