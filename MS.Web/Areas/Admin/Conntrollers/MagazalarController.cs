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

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class MagazalarController : AdminBaseController
    {
        //
        // GET: /Admin/Magazalar/
        public ActionResult Index()
        {
            var magazalar = Magazalar.GetMagazalar();
            return View(magazalar);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase uploadFile)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            try
            {
                if (uploadFile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(uploadFile.FileName);
                    string filename = "magazalistesi" + extension;

                    string filePath = Path.Combine(Server.MapPath("~/uploads/Magazalar"), filename);
                    uploadFile.SaveAs(filePath);


                    ///string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;";
                    //string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                    string ConnectionString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+filePath+";Extended Properties=\"Excel 8.0;HDR=YES\"";

                    DataSet ds = new DataSet();

                    //A 32-bit provider which enables the use of

                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            string query = "SELECT [Sira],[MagazaNo],[MagazaAdi],[Sube],[MarkaAdi],[FormatAdi],[Durum],[AcilisTarihi],[JetKasa],[Ykoordinat],[Xkoordinat],[Tel],[Tel2],[Tel3],[Tel4],[Adres],[Il],[Ilce],[Acilis],[AcilisPazar],[Kapanis],[KapanisPazar],[UnluMamuller],[Kuruyemis],[MezeveHazirYemek],[Balikci],[BiletSatisi],[Cafe],[Iletisim],[Eczane],[FastFood],[Kitabevi],[Kurutemizleme],[MuzikMarket],[ATM],[CevreciKiosk],[EglenceDunyam],[MigrosTASATM],[FaturaTahsilat],[HediyemKart] FROM [" + sheetName + "]";
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            //DataSet ds = new DataSet();
                            adapter.Fill(ds, "Items");
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ModelState.IsValid)
                                    {
                                        Global.Context.ExecuteStoreCommand("TRUNCATE TABLE Magazalar");
                                        //var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)new ProjectDbContext()).ObjectContext;
                                        //objCtx.ExecuteStoreCommand("TRUNCATE TABLE Magazalar");
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            Magazalar magaza = new Magazalar();
                                            magaza.Sira = ds.Tables[0].Rows[i]["Sira"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["Sira"]) : (double?)null;
                                            magaza.MagazaNo = ds.Tables[0].Rows[i]["MagazaNo"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["MagazaNo"]) : (double?)null;
                                            magaza.MagazaAdi = ds.Tables[0].Rows[i]["MagazaAdi"].ToString();
                                            magaza.Sube = ds.Tables[0].Rows[i]["Sube"].ToString();
                                            magaza.MarkaAdi = ds.Tables[0].Rows[i]["MarkaAdi"].ToString();
                                            magaza.FormatAdi = ds.Tables[0].Rows[i]["FormatAdi"].ToString();
                                            magaza.Durum = ds.Tables[0].Rows[i]["Durum"].ToString();
                                            magaza.AcilisTarihi = Convert.IsDBNull(ds.Tables[0].Rows[i]["AcilisTarihi"]) == false ? Convert.ToDateTime(ds.Tables[0].Rows[i]["AcilisTarihi"]) : (DateTime?)null;
                                            magaza.JetKasa = ds.Tables[0].Rows[i]["JetKasa"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["JetKasa"]) : (double?)null;
                                            magaza.Ykoordinat = ds.Tables[0].Rows[i]["Ykoordinat"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["Ykoordinat"]) : (double?)null;
                                            magaza.Xkoordinat = ds.Tables[0].Rows[i]["Xkoordinat"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["XKoordinat"]) : (double?)null;
                                            magaza.Tel = ds.Tables[0].Rows[i]["Tel"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["Tel"]) : (double?)null;
                                            magaza.Tel2 = ds.Tables[0].Rows[i]["Tel2"].ToString().Trim() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["Tel2"]) : (double?)null;
                                            magaza.Tel3 = ds.Tables[0].Rows[i]["Tel3"].ToString();
                                            magaza.Tel4 = ds.Tables[0].Rows[i]["Tel4"].ToString();
                                            magaza.Adres = ds.Tables[0].Rows[i]["Adres"].ToString();
                                            magaza.Il = ds.Tables[0].Rows[i]["Il"].ToString();
                                            magaza.Ilce = ds.Tables[0].Rows[i]["Ilce"].ToString();
                                            magaza.Acilis = Convert.IsDBNull(ds.Tables[0].Rows[i]["Acilis"]) == false ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Acilis"]) : (DateTime?)null;
                                            magaza.AcilisPazar = Convert.IsDBNull(ds.Tables[0].Rows[i]["AcilisPazar"]) == false ? Convert.ToDateTime(ds.Tables[0].Rows[i]["AcilisPazar"]) : (DateTime?)null;
                                            magaza.Kapanis = Convert.IsDBNull(ds.Tables[0].Rows[i]["Kapanis"]) == false ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Kapanis"]) : (DateTime?)null;
                                            magaza.KapanisPazar = Convert.IsDBNull(ds.Tables[0].Rows[i]["KapanisPazar"]) == false ? Convert.ToDateTime(ds.Tables[0].Rows[i]["KapanisPazar"]) : (DateTime?)null;
                                            magaza.UnluMamuller = ds.Tables[0].Rows[i]["UnluMamuller"].ToString();
                                            magaza.Kuruyemis = ds.Tables[0].Rows[i]["Kuruyemis"].ToString();
                                            magaza.MezeveHazirYemek = ds.Tables[0].Rows[i]["MezeveHazirYemek"].ToString();
                                            magaza.Balikci = ds.Tables[0].Rows[i]["Balikci"].ToString();
                                            magaza.BiletSatisi = ds.Tables[0].Rows[i]["BiletSatisi"].ToString();
                                            magaza.Cafe = ds.Tables[0].Rows[i]["Cafe"].ToString();
                                            magaza.Iletisim = ds.Tables[0].Rows[i]["Iletisim"].ToString();
                                            magaza.Eczane = ds.Tables[0].Rows[i]["Eczane"].ToString();
                                            magaza.FastFood = ds.Tables[0].Rows[i]["FastFood"].ToString();
                                            magaza.Kitabevi = ds.Tables[0].Rows[i]["Kitabevi"].ToString();
                                            magaza.Kurutemizleme = ds.Tables[0].Rows[i]["Kurutemizleme"].ToString();
                                            magaza.MuzikMarket = ds.Tables[0].Rows[i]["MuzikMarket"].ToString();
                                            magaza.ATM = ds.Tables[0].Rows[i]["ATM"].ToString();
                                            magaza.CevreciKiosk = ds.Tables[0].Rows[i]["CevreciKiosk"].ToString();
                                            magaza.EglenceDunyam = ds.Tables[0].Rows[i]["EglenceDunyam"].ToString();
                                            magaza.MigrosTASATM = ds.Tables[0].Rows[i]["MigrosTASATM"].ToString();
                                            magaza.FaturaTahsilat= ds.Tables[0].Rows[i]["FaturaTahsilat"].ToString();
                                            magaza.HediyemKart= ds.Tables[0].Rows[i]["HediyemKart"].ToString();
                                            Global.Context.Magazalar.AddObject(magaza);
                                        }
                                        Global.Context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    
                    ShowMessageBox(MessageType.Success, "Kayıt başarıyla gerçekleşti.", false);
                    var magazalar = Magazalar.GetMagazalar();
                    return View(magazalar);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(MessageType.Danger, "Kayıt sırasında hata oluştu. Hata Mesajı: " + ex.Message, false);
            }

            return RedirectToAction("Index");
        }

	}
}