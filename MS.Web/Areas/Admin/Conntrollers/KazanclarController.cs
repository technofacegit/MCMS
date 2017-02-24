using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class KazanclarController : AdminBaseController
    {
        //
        // GET: /Admin/Kazanclar/
        public ActionResult Index()
        {
            var kazanclar = Kazanclar.GetKazanclars();
            return View(kazanclar);
        }

        [HttpPost]
        public ActionResult KazanclarActive(int ID)
        {
            var kazanclarEntity = Kazanclar.GetKazanclar(ID);
            kazanclarEntity.Status = !kazanclarEntity.Status;
            kazanclarEntity.Save();
            return NewtonsoftJsonResult(new { IsSuccess = true });
        }

        [HttpGet]
        public ActionResult addeditkazanclar(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                Kazanclar kazanclarDataById = Kazanclar.GetKazanclar(id.Value);
                KazanclarViewModel kazanclarviewmodel = new KazanclarViewModel();
                kazanclarviewmodel.KazanciID =Convert.ToInt32(id);
                kazanclarviewmodel.KazancTitle = kazanclarDataById.KazancTitle;
                kazanclarviewmodel.KazancTipi = kazanclarDataById.KazancTipi;
                kazanclarviewmodel.KazancTag = kazanclarDataById.KazancTag;
                kazanclarviewmodel.KazancBackground = kazanclarDataById.KazancBackground;
                kazanclarviewmodel.KazancOptinGerekliMi =Convert.ToBoolean(kazanclarDataById.KazancOptinGerekliMi);
                kazanclarviewmodel.DisplayOrder = Convert.ToInt16(kazanclarDataById.DisplayOrder);
                kazanclarviewmodel.Status = Convert.ToBoolean(kazanclarDataById.Status);
                kazanclarviewmodel.ShowTL = Convert.ToInt16(kazanclarDataById.ShowTL);
                kazanclarviewmodel.ItemTitle =kazanclarDataById.ItemTitle;
                kazanclarviewmodel.ShowInfo =Convert.ToBoolean(kazanclarDataById.ShowInfo);
                kazanclarviewmodel.KazancTag = kazanclarDataById.KazancTag;

                return PartialView("_addeditKazanclar", kazanclarviewmodel);
            }

            return PartialView("_addeditKazanclar", new KazanclarViewModel());

        }

        [HttpPost]
        public ActionResult addeditkazanclar(KazanclarViewModel kazanclarViewModel, FormCollection FC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (kazanclarViewModel.KazanciID > 0)
                    {
                        Kazanclar kazanclarDataById = Kazanclar.GetKazanclar(kazanclarViewModel.KazanciID);
                        kazanclarDataById.KazancTipi = kazanclarViewModel.KazancTipi;
                        kazanclarDataById.KazancTitle = kazanclarViewModel.KazancTitle;
                        kazanclarDataById.KazancOptinGerekliMi = kazanclarViewModel.KazancOptinGerekliMi;
                        kazanclarDataById.KazancBackground = kazanclarViewModel.KazancBackground;
                        kazanclarDataById.DisplayOrder = kazanclarViewModel.DisplayOrder;
                        kazanclarDataById.Status = kazanclarViewModel.Status;
                        kazanclarDataById.ShowTL =Convert.ToBoolean(kazanclarViewModel.ShowTL);
                        kazanclarDataById.ItemTitle = kazanclarViewModel.ItemTitle;
                        kazanclarDataById.ShowInfo = kazanclarViewModel.ShowInfo;
                        kazanclarDataById.KazancTag = kazanclarViewModel.KazancTag;
                        
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                if (objjson.MovePhotos("temp", "KazancBackgroundImage", item))
                                {
                                    kazanclarDataById.KazancBackground = item;
                                }
                            }
                        }

                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Kazanclar list has been updated successfully!!", false);
                        //return Redirect("~/admin/CampaignCategory/Index");
                    }
                    else
                    {
                        Kazanclar kazanclarDataById = new Kazanclar();
                        kazanclarDataById.KazancTipi =Convert.ToString(kazanclarViewModel.KazancTipi);
                        kazanclarDataById.KazancTitle = Convert.ToString(kazanclarViewModel.KazancTitle);
                        kazanclarDataById.KazancOptinGerekliMi = kazanclarViewModel.KazancOptinGerekliMi;
                        kazanclarDataById.KazancBackground = kazanclarViewModel.KazancBackground;
                        kazanclarDataById.DisplayOrder = kazanclarViewModel.DisplayOrder;
                        kazanclarDataById.Status = kazanclarViewModel.Status;
                        kazanclarDataById.ShowTL = Convert.ToBoolean(kazanclarViewModel.ShowTL);
                        kazanclarDataById.ItemTitle = kazanclarViewModel.ItemTitle;
                        kazanclarDataById.ShowInfo = kazanclarViewModel.ShowInfo;
                        kazanclarDataById.KazancTag = kazanclarViewModel.KazancTag;
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                if (objjson.MovePhotos("temp", "KazancBackgroundImage", item))
                                {
                                    kazanclarDataById.KazancBackground = item;
                                }
                            }
                        }
                        Global.Context.Kazanclars.AddObject(kazanclarDataById);
                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Kazanclar list has been updated successfully!!", false);
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
        public ActionResult AddPhotos()
        {
            return PartialView("_AddPhotos");
        }

        [HttpGet]
        public ActionResult delete(int ID)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure want to delete this kazanclar ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete kazanclar" },
                Footer = new ModalFooter
                {
                    SubmitButtonText = "Yes",
                    CancelButtonText = "No"
                }
            });
        }

        [HttpPost]
        public ActionResult delete(int ID, FormCollection FC)
        {
            try
            {
                var kazanclarEntity = Kazanclar.GetKazanclar(ID);
                List<string> imageList = new List<string>();
                imageList.Add(kazanclarEntity.KazancBackground);

                kazanclarEntity.Delete();
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/KazancBackgroundImage/");

                ShowMessageBox(MessageType.Success, "Kazanclar has been deleted successfully!!", false);
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

            // return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });

            return RedirectToAction("Index");
        }
	}
}