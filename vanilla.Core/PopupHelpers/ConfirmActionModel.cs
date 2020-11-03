using System;
namespace vanilla.Core.PopupHelpers
{
    public class ConfirmActionModel
    {
            public Action<bool> ConfirmActionCallback { get; set; }

            public string Title { get; set; } = "Are you sure?";
            public string Question { get; set; } = "Click OK to confirm.";
            public string OKButtonText { get; set; } = "OK";
            public string CancelButtonText { get; set; } = "Cancel";
    }
}
