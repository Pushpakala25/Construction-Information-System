using ProjectAK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace ProjectAK.Controllers
{
    
    public class AdminController : Controller
    {
        Contact_dbEntities db = new Contact_dbEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(admin adm)
        {
            admin ad = db.admins.Where(model => model.ad_name == adm.ad_name && model.ad_password == adm.ad_password).SingleOrDefault();
            if(ad != null)
            {
                Session["ad_id"] = ad.ad_id.ToString();
                Session["ad_name"] = ad.ad_name.ToString();
                return RedirectToAction("Category");
            }
            else
            {
                ViewBag.error = "Invalid User Name or Password";
            }

            ModelState.Clear();
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult Category()
        {
            if(Session["ad_id"] == null)
            {
                return RedirectToAction("Login");
            }
                       

            return View();
        }

        [HttpPost]
        public ActionResult Category(category cat, HttpPostedFileBase imgfile)
        {
            admin ad = new admin();
            string path = uploadimage(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "image  could not be uploaded";
            }
            else
            {
                category ca = new category();
                ca.cat_name = cat.cat_name;
                ca.cat_img = path;
                ca.cat_statuss = 1;
                ca.ad_id_fk = Convert.ToInt32(Session["ad_id"].ToString());
                db.categories.Add(ca);
                db.SaveChanges();

                return RedirectToAction("ViewCategory");

            }

            return View();
        }

        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.categories.Where(model => model.cat_statuss == 1).OrderByDescending(model => model.cat_id).ToList();
            IPagedList<category> cate = list.ToPagedList(pageindex, pagesize);
            return View(cate);
           
        }      

       
        public ActionResult Delete(int?id, FormCollection collection)
        {
            try
            {
                using (Contact_dbEntities db = new Contact_dbEntities())
                {
                    category c = db.categories.Where(model => model.cat_id == id).SingleOrDefault();
                    db.categories.Remove(c);
                    db.SaveChanges();
                    return RedirectToAction("ViewCategory");
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteC(int? id, FormCollection collection)
        {
            try
            {
                using (Contact_dbEntities db = new Contact_dbEntities())
                {
                    person pr = db.people.Where(model => model.pro_id == id).SingleOrDefault();
                    db.people.Remove(pr);
                    db.SaveChanges();
                    return RedirectToAction("ViewCategory");
                }

            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult CreateAdd()
        {
            List<category> li = db.categories.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdd(person p, HttpPostedFileBase imgfile)
        {
            List<category> li = db.categories.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");

            admin ad = new admin();
            string path = uploadimage(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "image could not be uploaded";
            }
            else
            {
                person pr = new person();
                pr.pro_name = p.pro_name;
                pr.pro_img = path;
                pr.cat_id_fk = p.pro_adm_id_fk;
                pr.pro_desc = p.pro_desc;
                pr.pro_adm_id_fk = Convert.ToInt32(Session["ad_id"].ToString());
                db.people.Add(pr);
                db.SaveChanges();

                Response.Redirect("ViewCategory");
            }
            return View();
        }

        public ActionResult DisplayAdd(int ? id, int ? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.people.Where(model => model.cat_id_fk == id).OrderByDescending(model => model.pro_adm_id_fk).ToList();
            IPagedList<person> cate = list.ToPagedList(pageindex, pagesize);
            return View(cate);
        }

        public ActionResult AdminViewAdds(int? id)
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

        public ActionResult Ad_Delete(int? id)
        {
            person p = db.people.Where(model => model.pro_id == id).SingleOrDefault();
            db.people.Remove(p);
            db.SaveChanges();
            return View("Category");
        }

        public ActionResult AdminBdisplay()
        {
            return View(db.bookings.ToList());
        }

        public ActionResult Userdisplay()
        {
            return View(db.tbl_usr.ToList());
        }

        public ActionResult Catdisplay()
        {
            return View(db.categories.ToList());
        }

        public ActionResult Peopledisplay()
        {
            return View(db.people.ToList());
        }









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