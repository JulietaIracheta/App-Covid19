using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace WebCovid19.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /************************************* STYLE BUNDLE ***************************************/
                                            //Esto es solo un nombre para identificar
            bundles.Add(new StyleBundle("~/bundles/css/boostrap")
                .IncludeDirectory(
                "~/Content/Css", "*.css", true));

            bundles.Add(new StyleBundle("~/bundles/css/main")
                .Include(
                "~/Content/Css/main.css"));

            bundles.Add(new StyleBundle("~/bundles/css/Linearicons")
                .Include(
                "~/Content/fonts/Linearicons/icon-font.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css/font-awesome")
                .Include(
                "~/Content/fonts/Linearicons/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/fonts/Linearicons")
         .Include(
               "~/Content/fonts/Linearicons/font-awesome.min.css",
                      "~/Content/fonts/Linearicons/icon-font.min.css"));

            bundles.Add(new StyleBundle("~/bundles/fonts/Font-Awesome-min")
                .Include(
                "~/Content/fonts/font-awesome.min.css"));


            /************************************* Scripts BUNDLE ***************************************/
            bundles.Add(new ScriptBundle("~/bundles/Scripts/umdJS")
               .IncludeDirectory(
               "~/Scripts/umd", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/js")
               .IncludeDirectory(
               "~/Scripts", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/carrucel")
               .Include(
               "~/Scripts/jquery-{version}.slim.min.js"));

            /*Validaciones del lado del cliente*/
            bundles.Add(new ScriptBundle("~/bundles/Scripts/ValidacionesCliente")
                .Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));




            BundleTable.EnableOptimizations = true; //optimiza aun mas en un solo archivo
        }
    }
}