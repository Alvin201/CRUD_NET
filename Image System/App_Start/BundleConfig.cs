﻿using System.Web;
using System.Web.Optimization;

namespace Image_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/menu.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                       "~/Scripts/jquery-3.4.1.js",
                       "~/Content/DataTables/css/jquery.dataTables.css",
                       "~/Scripts/DataTables/dataTables.bootstrap.min.js",
                       "~/Scripts/DataTables/responsive.bootstrap.min.js",
                       "~/Scripts/DataTables/jquery.dataTables.min.js",
                       "~/Scripts/DataTables/dataTables.bootstrap4.js",
                       "~/Scripts/DataTables/dataTables.bootstrap4.min.js",
                       "~/Content/DataTables/css/dataTables.bootstrap4.css",
                       "~/Content/DataTables/css/dataTables.bootstrap4.min.css"));
        }
    }
}