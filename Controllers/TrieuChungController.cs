using Covid.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Controllers
{
  public class TrieuChungController : Controller
  {
    public IActionResult ChonTrieuChung()
    {
      return View();
    }

    public IActionResult LietKeCongNhan(int soTrieuChung)
    {
      DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;

      return View(context.ListCongNhanTrieuChung(soTrieuChung));
    }
  }
}