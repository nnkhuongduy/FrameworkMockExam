using System.Data.SqlClient;

namespace Covid.Models
{
  public class DataContext
  {
    public string ConnectionString { get; set; }

    public DataContext(string connectionString)
    {
      ConnectionString = connectionString;
    }

    private SqlConnection GetConnection()
    {
      return new SqlConnection(ConnectionString);
    }

    public bool InsertDiemCachLy(DiemCachLyModel diemCachLy)
    {
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = "INSERT INTO DiemCachLy VALUES (@MaDiemCachLy, @TenDiemCachLy, @DiaChi)";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("MaDiemCachLy", diemCachLy.MaDiemCachLy);
        cmd.Parameters.AddWithValue("TenDiemCachLy", diemCachLy.TenDiemCachLy);
        cmd.Parameters.AddWithValue("DiaChi", diemCachLy.DiaChi);

        var result = cmd.ExecuteNonQuery() != 0;

        conn.Close();

        return result;
      }
    }

    public List<CongNhanSoTrieuChungModel> ListCongNhanTrieuChung(int soTrieuChung)
    {
      List<CongNhanSoTrieuChungModel> list = new List<CongNhanSoTrieuChungModel>();

      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = @"SELECT TenCongNhan, NamSinh, NuocVe, COUNT(*) AS SoTrieuChung 
        FROM CONGNHAN cn INNER JOIN CN_TC cntc ON cn.MaCongNhan = cntc.MaCongNhan 
        GROUP BY TenCongNhan, NamSinh, NuocVe
        HAVING COUNT(*) = @SoTrieuChung;";

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("SoTrieuChung", soTrieuChung);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            list.Add(new CongNhanSoTrieuChungModel()
            {
              TenCongNhan = reader.GetString(0),
              NamSinh = reader.GetInt32(1),
              NuocVe = reader.GetString(2),
              SoTrieuChung = reader.GetInt32(3)
            });
          }
          reader.Close();
        }

        conn.Close();
      }

      return list;
    }

    public List<DiemCachLyModel> ListDiemCachLy()
    {
      List<DiemCachLyModel> list = new List<DiemCachLyModel>();

      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = @"SELECT * FROM DiemCachLy;";

        SqlCommand cmd = new SqlCommand(query, conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            list.Add(new DiemCachLyModel()
            {
              MaDiemCachLy = reader.GetString(0),
              TenDiemCachLy = reader.GetString(1),
              DiaChi = reader.GetString(2)
            });
          }
          reader.Close();
        }

        conn.Close();
      }

      return list;
    }

    public List<CongNhanModel> ListCongNhanDiemCachLy(string maDiemCachLy)
    {
      List<CongNhanModel> list = new List<CongNhanModel>();

      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = @"SELECT * FROM CongNhan WHERE MaDiemCachLy = @MaDiemCachLy;";

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("MaDiemCachLy", maDiemCachLy);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            list.Add(new CongNhanModel()
            {
              MaCongNhan = reader.GetString(0),
              TenCongNhan = reader.GetString(1),
              GioiTinh = reader.GetBoolean(2),
              NamSinh = reader.GetInt32(3),
              NuocVe = reader.GetString(4),
              MaDiemCachLy = reader.GetString(5)
            });
          }
          reader.Close();
        }

        conn.Close();
      }

      return list;
    }

    public bool DeleteCongNhan(string maCongNhan)
    {
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = @"DELETE FROM CN_TC WHERE MaCongNhan = @MaCongNhan;
        DELETE FROM CongNhan WHERE MaCongNhan = @MaCongNhan";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("MaCongNhan", maCongNhan);

        var result = cmd.ExecuteNonQuery() != 0;

        conn.Close();

        return result;
      }
    }

    public CongNhanModel GetCongNhan(string maCongNhan)
    {
      CongNhanModel ketqua = new CongNhanModel();

      using (SqlConnection conn = GetConnection())
      {
        conn.Open();

        var query = @"SELECT * FROM CongNhan WHERE MaCongNhan = @MaCongNhan;";

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("MaCongNhan", maCongNhan);

        using (var reader = cmd.ExecuteReader())
        {
          reader.Read();

          ketqua.MaCongNhan = reader.GetString(0);
          ketqua.TenCongNhan = reader.GetString(1);
          ketqua.GioiTinh = reader.GetBoolean(2);
          ketqua.NamSinh = reader.GetInt32(3);
          ketqua.NuocVe = reader.GetString(4);
          ketqua.MaDiemCachLy = reader.GetString(5);

          reader.Close();
        }

        conn.Close();
      }

      return ketqua;
    }
  }
}