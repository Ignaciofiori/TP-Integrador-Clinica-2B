using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath.ToLower();

            if (url.Contains("listadopacientes"))
                navPacientes.Attributes["class"] = "nav-link active";

            else if (url.Contains("listadoprofesionales"))
                navProfesionales.Attributes["class"] = "nav-link active";

            else if (url.Contains("listadoobrassociales"))
                navObras.Attributes["class"] = "nav-link active";

            else if (url.Contains("listadoespecialidades"))
                navEspecialidades.Attributes["class"] = "nav-link active";

            else if (url.Contains("listadoturnos"))
                navTurnos.Attributes["class"] = "nav-link active";

            else if (url.Contains("facturacion"))
                navFacturacion.Attributes["class"] = "nav-link active";
        }

    }
}