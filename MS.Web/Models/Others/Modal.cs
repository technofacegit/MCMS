using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MS.Core;
using MS.Web.Code.LIBS;


namespace MS.Web.Models.Others
{
    public class Modal
    {
        public Modal()
        {
            Size = ModalSize.Medium;
        }

        public string ID { get; set; }
        public string AreaLabeledID { get; set; }
        public string Message { get; set; }
        public string ModalCSS { get; set; }
        public ModalSize Size { get; set; }
        public string ModalSizeCSS
        {
            get
            {
                if (Size == ModalSize.Large)
                    return "modal-lg";
                else if (Size == ModalSize.Medium)
                    return "modal-md";
                else if (Size == ModalSize.Small)
                    return "modal-sm";
                else
                    return "modal-xsm";
            }
        }

        public ModalHeader Header { get; set; }
        public ModalFooter Footer { get; set; }
    }

    public class ModalHeader
    {
        public string ID { get; set; }
        public string Title { get; set; }
    }

    public class ModalFooter
    {
        public ModalFooter()
        {
            SubmitButtonText = "Submit";
            CancelButtonText = "Cancel";
            SubmitButtonID = "btnSubmit";
            CancelButtonID = "btnCancel";
            SubmitButtonCSS = "btn btn-primary";
        }

        public string SubmitButtonID { get; set; }
        public string SubmitButtonText { get; set; }
        public string CancelButtonID { get; set; }
        public string CancelButtonText { get; set; }
        public string SubmitButtonCSS { get; set; }
        public string FooterText { get; set; }
        public bool IsCancelButtonOnly { get; set; }
        public bool IsSubmitButtonOnly { get; set; }
    }
}