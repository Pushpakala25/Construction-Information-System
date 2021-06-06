using PagedList;
using ProjectAK.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProjectAK.Controllers
{
    public class UserController : Controller
    {
        Contact_dbEntities db = new Contact_dbEntities();
        // GET: User
        public ActionResult Index(int ? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.categories.Where(model => model.cat_statuss == 1).OrderByDescending(model => model.cat_id).ToList();
            IPagedList<category> cate = list.ToPagedList(pageindex, pagesize);
            return View(cate);
            
        }

        public ActionResult IDisplayAdd(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.people.Where(model => model.cat_id_fk == id).OrderByDescending(model => model.pro_adm_id_fk).ToList();
            IPagedList<person> cate = list.ToPagedList(pageindex, pagesize);
            return View(cate);
        }

        public ActionResult UserViewAdds(int? id)
        {
            ad_view_model adm = new ad_view_model();

            person p = db.people.Where(model => model.pro_id == id).SingleOrDefault();
            adm.pro_id = p.pro_id;
            adm.pro_name = p.pro_name;
            adm.pro_img = p.pro_img;
            adm.pro_desc = p.pro_desc;

            category cat = db.categories.Where(model => model.cat_id == p.cat_id_fk).SingleOrDefault();
            adm.cat_name = cat.cat_name;
            admin a = db.admins.Where(model => model.ad_id == p.pro_adm_id_fk).SingleOrDefault();
            adm.ad_name = a.ad_name;
            adm.pro_adm_id_fk = a.ad_id;

            return View(adm);
        }

        
        public ActionResult BookAdd()
        {
            List<category> li = db.categories.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");

            List<person> lis = db.people.ToList();
            ViewBag.peoplelist = new SelectList(lis, "pro_id", "pro_name");

            return View();      
        }

        [HttpPost]
        public ActionResult BookAdd(booking bk)
        {

            List<category> li = db.categories.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");

            //List<person> lis = db.people.Where(model => model.cat_id_fk == bk.bk_cat_id_fk).ToList();
            //ViewBag.peoplelist = new SelectList(lis, "pro_id", "pro_name");

            List<person> lis = db.people.ToList();
            ViewBag.peoplelist = new SelectList(lis, "pro_id", "pro_name");

            booking br = new booking();
           
            br.bk_mobile = bk.bk_mobile;
            br.bk_mail = bk.bk_mail;
            
            br.bk_cal = bk.bk_cal;
            br.bk_msg = bk.bk_msg;

            br.bk_usr_id_fk = Convert.ToInt32(Session["u_id"].ToString());

            br.bk_cat_id_fk = bk.bk_cat_id_fk;
            br.bk_pro_id_fk = bk.bk_pro_id_fk;

            //br.bk_cat_id_fk = Convert.ToInt32(Session["cat_id"].ToString());
            //br.bk_pro_id_fk = Convert.ToInt32(Session["pro_id"].ToString());
            db.bookings.Add(br);
            db.SaveChanges();
            ViewBag.result = " your meeting is booked!";
           
            //Response.Write("<script>alert('Your meeting submitted successfully....');</script>");

            //Response.Redirect("BookAdd");
            return View();            
        }

        public ActionResult Bdisplay()
        {

            return View(db.bookings.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_usr us,HttpPostedFileBase imgfile)
        {
            string path = uploadimage(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "image  could not be uploaded";
            }
            else
            {
                tbl_usr u = new tbl_usr();
                u.u_name = us.u_name;
                u.u_password = us.u_password;
                u.u_contact = us.u_contact;
                u.u_email = us.u_email;
                u.u_img = path;
                db.tbl_usr.Add(u);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
                return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_usr svm)
        {
            tbl_usr ad = db.tbl_usr.Where(model => model.u_email == svm.u_email && model.u_password == svm.u_password).SingleOrDefault();
           
            if (ad != null)
            {
                Session["u_id"] = ad.u_id.ToString();
                Session["u_name"] = ad.u_name.ToString();
                return RedirectToAction("Index");
            }            
            else
            {
                ViewBag.error = "Invalid User Name or Password";
            }            

            ModelState.Clear();
            return View();
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }








        //img
        public string uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/img/Admin_Img/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/img/Admin_Img/" + random + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg, jpeg or png formats are acceptable....');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file');</script>");
                path = "-1";
            }
            return path;
        }
    }
}