using System;
using Instaview.Services;

namespace Instaview.Utils
{
    internal static class Dialog
    {
        private static readonly Lazy<DialogService> LazyDialogService = new();

        internal static DialogService Service => LazyDialogService.Value;
    }
}
