using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TrabalhoRestBLL;

namespace RepositoryBLL
{
    public static class Auxiliar
    {
        public static PropertyInfo GetPrimaryKey(this Type entityType)
        {
            foreach (var property in entityType.GetProperties().Where(property => property.IsPrimaryKey()))
            {
                if (property.PropertyType != typeof(int))
                {
                    throw new Exception($"Primary key, '{property.Name}', do tipo '{entityType}' não é int");
                }
                return property;
            }
            throw new ApplicationException($"Sem primary key definida para o tipo '{entityType.Name}'");
        }

        public static bool IsPrimaryKey(this PropertyInfo propertyInfo)
        {
            var columnAttribute = propertyInfo.GetAttributeOf<ColumnAttribute>();

            return columnAttribute != null && columnAttribute.IsPrimaryKey;
        }

        public static T GetAttributeOf<T>(this PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(T), true);
            if (attributes.Length == 0)
            {
                return default(T);
            }
            return (T)attributes[0];
        }

        [Serializable]
        public class PrimaryKeyNotFoundException : Exception
        {
            public PrimaryKeyNotFoundException() { }

            public PrimaryKeyNotFoundException(string message) : base(message) { }

            public PrimaryKeyNotFoundException(string message, Exception innerException) : base(message, innerException) { }

            protected PrimaryKeyNotFoundException(SerializationInfo info, StreamingContext streamingContext) : base(info, streamingContext) { }
        }

        public static void ClearControls(Control controle)
        {
            foreach (Control ctr in controle.Controls)
            {
                if (ctr.HasControls())
                {
                    ClearControls(ctr);
                }

                if (ctr is TextBox)
                {
                    var tb = (TextBox)ctr;
                    tb.Text = "";
                }

                if (ctr is DropDownList)
                {
                    var ddl = (DropDownList)ctr;
                    ddl.DataBind();
                }

                if (ctr is HtmlInputText)
                {
                    var hit = (HtmlInputText)ctr;
                    hit.Value = "";
                }

                if (ctr is GridView)
                {
                    var gv = (GridView)ctr;
                    gv.DataBind();
                }
            }
        }

        public static void TextoAlternativo(object sender, EventArgs e)
        {
            ((DropDownList)sender).Items.Insert(0, new ListItem("Selecione...", "-1"));
        }


        public static void SendEmail(string quemEnviou, string quemRecebera)
        {
            #region // Corpo do email
            const string email = "roberto.oliveira.engineer@gmail.com";
            const string password = "rm121319";
            #endregion
            var body = "" + quemEnviou;

            try
            {
                var mail = new MailMessage();
                mail.To.Add(quemRecebera);
                mail.From = new MailAddress(email);
                mail.Subject = "Teste de envio de email via Webservice com rest.";
                mail.Body = body;
                mail.IsBodyHtml = true;

                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Credentials = new NetworkCredential(email, password),
                    Port = 587,
                    EnableSsl = true
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar o email: " + ex.Message);
            }
        }

        /// <summary>
        /// Gera uma senha alfanumérica aleatória, contendo letras maiúsculas, minúsculas e números.
        /// </summary>
        /// <param name="pTamanho">Tamanho da senha a ser gerada.</param>
        /// <returns>string -> senha gerada.</returns>
        public static string gerarSenha(int pTamanho)
        {
            var senha = new char[pTamanho];

            var randomizer = new Random();

            for (var i = 0; i < pTamanho; i++)
            {
                // Índices (range de códigos da tabela ASCII).
                // 0: números -> 48 ~ 57; 
                // 1: maiúsculas -> 65 ~ 90; 
                // 2: minúsculas -> 97 ~ 122;

                // O método Next() da classe Random obtém um valor aleatório no range numérico que lhe é passado.
                // Esse "+1" é apenas para ilustrar que, num range (x, y),
                // o método Next() obtém valores MAIORES OU IGUAIS a x e MENORES que y.
                // Dessa forma é necessário adicionar 1 ao segundo parametro.
                int[] caracteresAleatorios = { randomizer.Next(48, 57 + 1), randomizer.Next(65, 90 + 1), randomizer.Next(97, 122 + 1) };

                // Exemplo do funcionamento do método next:
                // Para se obter um índice de 0 a 2, precisamos de um range 0 ~ 3.

                // O valor inteiro recebido é convertido em char, 
                // obtendo o caractere referente ao código decimal ASCII.
                senha[i] = (char)caracteresAleatorios[randomizer.Next(0, 3)];

            }

            // O array de char agora é transformado em uma string e então retornado.
            return new string(senha);
        }
    }
}
