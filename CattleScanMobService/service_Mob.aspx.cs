using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class service_Mob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static int GetData(DateTime dt,string user_id,int days,string eventpar)
    {
        int resultVal = 0;
        ;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                ObjectParameter result = new ObjectParameter("result", typeof(int));
                context.SP_GET_Farm_TotalAlertsPeriod(dt, user_id, days, eventpar, result);
                resultVal = Convert.ToInt16(result.Value.ToString());
            }
        }
        catch (Exception ex)
        {
            string x = ex.Message;
            resultVal = -1;
        }
        return resultVal;
    }
}