using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectAK.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ProjectAK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Projects()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Completed()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Newly()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Upcoming()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Awards()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Carrers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Carrers(CrClass cr, HttpPostedFileBase file)
        {
            string Mainconn = ConfigurationManager.ConnectionStrings["DefaultConection"].ConnectionString;
            SqlConnection Sqlconn = new SqlConnection(Mainconn);
            string Sqlq = "insert into [dbo].[Car_Table](CrName,CrEmail,CrFile,CrMsg) values (@CrName,@CrEmail,@CrFile,@CrMsg)";
            SqlCommand Sqlcmd = new SqlCommand(Sqlq, Sqlconn);
            Sqlconn.Open();
            Sqlcmd.Parameters.AddWithValue("@CrName", cr.CrName);
            Sqlcmd.Parameters.AddWithValue("@CrEmail", cr.CrEmail);
            
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/img/Career_doc"), filename);
                file.SaveAs(imgpath);
            }
            Sqlcmd.Parameters.AddWithValue("@CrFile", "~/img//Career_doc" + file.FileName);

            Sqlcmd.Parameters.AddWithValue("@CrMsg", cr.CrMsg);
            Sqlcmd.ExecuteNonQuery();
            Sqlconn.Close();
            ViewData["Message"] = "user Record" + cr.CrName + "saved";

            ModelState.Clear();
            return View();

        }



        public ActionResult Contact()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Contact(UserClass uc)
        {

            string Mainconn = ConfigurationManager.ConnectionStrings["DefaultConection"].ConnectionString;
            SqlConnection Sqlconn = new SqlConnection(Mainconn);
            string Sqlq = "insert into [dbo].[Ccon_Table](Cname,Cemail,Cmobile,Cmsg) values (@Cname,@Cemail,@Cmobile,@Cmsg)";
            SqlCommand Sqlcmd = new SqlCommand(Sqlq, Sqlconn);
            Sqlconn.Open();
            Sqlcmd.Parameters.AddWithValue("@Cname", uc.Cname);
            Sqlcmd.Parameters.AddWithValue("@Cemail", uc.Cemail);
            Sqlcmd.Parameters.AddWithValue("@Cmobile", uc.Cmobile);
            Sqlcmd.Parameters.AddWithValue("@Cmsg", uc.Cmsg);
            Sqlcmd.ExecuteNonQuery();
            Sqlconn.Close();
            ViewData["Message"] = "user Record" + uc.Cname + "saved";

            ModelState.Clear();
            return View();
        }



    }
}