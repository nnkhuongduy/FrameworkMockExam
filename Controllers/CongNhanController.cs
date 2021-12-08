using Covid.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Controllers
{
  public class CongNhanController : Controller
  {
    public IActionResult ChonDiemCachLy()
    {
      DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;

      return View(context.ListDiemCachLy());
    }

    public IActionResult DanhSach(string maDiemCachLy)
    {
      DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;

      return View(context.ListCongNhanDiemCachLy(maDiemCachLy));
    }

    public IActionResult XoaCongNhan(string Id)
    {
      DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;

      if (context.DeleteCongNhan(Id))
        ViewData["KetQua"] = "Xóa thành công";
      else ViewData["KetQua"] = "Xóa không thành công";

      return View();
    }

    public IActionResult XemCongNhan(string Id)
    {
      DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;

      var congNhan = context.GetCongNhan(Id);

      ViewData.Model = congNhan;

      return View();
    }
  }
}