using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Code.Validation;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using FluentValidation.Results;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class DigerController : AdminBaseController
    {
        //
        // GET: /Admin/Magazalar/
        public ActionResult Index()
        {
            ///var magazalar = Magazalar.GetMagazalar();
            return View();
        }

        [HttpPost]
        public ActionResult Index(String s)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            String notifKey = DateTime.Now.ToShortDateString();

            var notifreports = Global.Context.spGetIdleDevices(DateTime.Now.AddMonths(-6)).ToList();

            string dosyaAdi = notifKey;
            var table = notifreports;// Buraya veritabanınından gelen herhangi bir dataSource gelebilir.( DataTable, DataSet, kendi oluşturduğunuz, herhangi bir ICollection tipinde entitiy model)
            //GridView gridx = new GridView();
            //gridx.DataSource = table;
            //gridx.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".txt");

            Response.ContentType = "text/plain";
            ////Response.Charset = "";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            foreach (var x in table) { 
                sw.WriteLine(x);
            }
            ///gridx.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }

	}
}