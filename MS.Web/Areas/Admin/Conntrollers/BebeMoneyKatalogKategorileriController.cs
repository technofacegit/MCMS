using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MS.Business;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.LIBS;
using MS.Core;
using MS.Web.Models.Others;
using MS.Web.Code.Attributes;

namespace MS.Web.Areas.Admin.Conntrollers
{
     [AdminAuthorization]
    public class BebeMoneyKatalogKategorileriController : AdminBaseController
    {
       
        public ActionResult Index()
        {
            var bebeMoneyKatalogKategorileri = BebeMoneyKatalogKategorileri.GetBebeMoneyKatalogKategorileries();
            return View(bebeMoneyKatalogKategorileri);
        }

        [HttpGet]
        public ActionResult addeditBebeMoneyKatalogKategorileri(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                BebeMoneyKatalogKategorileri bebeMoneyKatalogKategorileri = BebeMoneyKatalogKategorileri.GetBebeMoneyKatalogKategorileri(id.Value);
                BebeMoneyKatalogKategorileriViewModel bebeMoneyKatalogKategorileriViewModel = new BebeMoneyKatalogKategorileriViewModel();
                bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriID = Convert.ToInt32(id);
                bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriAdi = bebeMoneyKatalogKategorileri.BebeMoneyKategoriAdi;
                bebeMoneyKatalogKategorileriViewModel.DisplayOrder = bebeMoneyKatalogKategorileri.DisplayOrder.Value;
                bebeMoneyKatalogKategorileriViewModel.KategoriTag = bebeMoneyKatalogKategorileri.KategoriTag;
                return PartialView("_addediBebeMoneyKatalogKategorileri", bebeMoneyKatalogKategorileriViewModel);
            }

            return PartialView("_addediBebeMoneyKatalogKategorileri", new BebeMoneyKatalogKategorileriViewModel());

        }

        [HttpPost]
        public ActionResult addeditBebeMoneyKatalogKategorileri(BebeMoneyKatalogKategorileriViewModel bebeMoneyKatalogKategorileriViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriID > 0)
                    {
                        BebeMoneyKatalogKategorileri bebeMoneyKatalogKategorileri = BebeMoneyKatalogKategorileri.GetBebeMoneyKatalogKategorileri(bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriID);
                        bebeMoneyKatalogKategorileri.KategoriTag = bebeMoneyKatalogKategorileriViewModel.KategoriTag;
                        bebeMoneyKatalogKategorileri.BebeMoneyKategoriAdi = bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriAdi;
                        bebeMoneyKatalogKategorileri.DisplayOrder = bebeMoneyKatalogKategorileriViewModel.DisplayOrder;
                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Bebe Money Katalog list has been updated successfully!!", false);
                     }
                    else
                    {
                        BebeMoneyKatalogKategorileri bebeMoneyKatalogKategorileri = new BebeMoneyKatalogKategorileri();
                        bebeMoneyKatalogKategorileri.BebeMoneyKategoriAdi = bebeMoneyKatalogKategorileriViewModel.BebeMoneyKategoriAdi;
                        bebeMoneyKatalogKategorileri.DisplayOrder = bebeMoneyKatalogKategorileriViewModel.DisplayOrder;
                        bebeMoneyKatalogKategorileri.KategoriTag = bebeMoneyKatalogKategorileriViewModel.KategoriTag;
                        Global.Context.BebeMoneyKatalogKategorileris.AddObject(bebeMoneyKatalogKategorileri);
                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Bebe Money Katalog list has been updated successfully!!", false);
                        // return Redirect("~/admin/CampaignCategory/Index");
                    }
                    return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Kazanclar list!!");
                return CreateModelStateErrors();
            }
            return CreateModelStateErrors();
        }

        [HttpGet]
        public ActionResult delete()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure want to delete this Bebe Money Katalog ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete Bebe Money Katalog" },
                Footer = new ModalFooter
                {
                    SubmitButtonText = "Yes",
                    CancelButtonText = "No"
                }
            });
        }

        [HttpPost]
        public ActionResult delete(int ID)
        {
            try
            {
                var BebeMoneyKatalogKategorileriEntity = BebeMoneyKatalogKategorileri.GetBebeMoneyKatalogKategorileri(ID);
                BebeMoneyKatalogKategorileriEntity.Delete();
                ShowMessageBox(MessageType.Success, "Bebe Money Katalog has been deleted successfully!!", false);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                {
                    message = "You can't delete this because it has some child records.";
                }
                ShowMessageBox(MessageType.Danger, message, false);
            }
               return RedirectToAction("Index");
        }
	}
}