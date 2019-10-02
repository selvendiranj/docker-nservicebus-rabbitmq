using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using NServiceBus;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.GUI
{
    public static class GetNServiceBusInfo
    {
        public static IHtmlContent OutputNServiceBusInfo(this IHtmlHelper _htmlHelper)
        {
            var nsbAssembly = typeof(IEndpointInstance).Assembly;
            var att = nsbAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute))
                                 .OfType<AssemblyFileVersionAttribute>()
                                 .First();

            var v = new Version(att.Version);
            var script = $"<script>window.NSB_VERSION = '{v.Major}.{v.Minor}.{v.Build}';</script>";
            return new HtmlString(script);
        }
    }
}
