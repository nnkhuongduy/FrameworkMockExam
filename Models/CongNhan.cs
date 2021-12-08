namespace Covid.Models
{
  public class CongNhanModel
  {
    public string MaCongNhan { get; set; }

    public string TenCongNhan { get; set; }

    public bool GioiTinh { get; set; }

    public int NamSinh { get; set; }

    public string NuocVe { get; set; }

    public string MaDiemCachLy { get; set; }
  }

  public class CongNhanSoTrieuChungModel : CongNhanModel
  {
    public int SoTrieuChung { get; set; }
  }
}