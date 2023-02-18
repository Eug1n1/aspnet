using System;
using System.Web;
using static System.Net.WebRequestMethods;
using System.Web.UI.WebControls.WebParts;

namespace lab_1
{
    public class SESGETHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var res = context.Response;
            var req = context.Request;
                
            res.Write($"GET - Http - SES:ParmA = {req.Params.Get("ParmA")}, ParmB = {req.Params.Get("ParmB")}");
        }

        #endregion
    }
}
