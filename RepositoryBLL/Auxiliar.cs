using System;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
            }
        }

        public static void TextoAlternativo(object sender, EventArgs e)
        {
            ((DropDownList)sender).Items.Insert(0, new ListItem("Selecione...", "-1"));
        }
    }
}
