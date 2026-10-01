namespace AutoSpeed;

public class QuanLyPhuongTien
{
    private readonly List<PhuongTien> _danhSachPhuongTien = new();

    public void AddPhuongTien(PhuongTien pt)
    {
        if (pt == null)
            throw new ArgumentNullException(nameof(pt));

        _danhSachPhuongTien.Add(pt);
    }

    public void DisplayAll()
    {
        if (_danhSachPhuongTien.Count == 0)
        {
            Console.WriteLine("Danh sách phương tiện đang trống.");
            return;
        }

        Console.WriteLine("===== DANH SÁCH PHƯƠNG TIỆN =====");

        foreach (PhuongTien pt in _danhSachPhuongTien)
        {
            Console.WriteLine(pt.GetInfo());
            Console.WriteLine(
                $"Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            Console.WriteLine("----------------------------------");
        }
    }

    public PhuongTien? FindMaxGiaLanBanh()
    {
        if (_danhSachPhuongTien.Count == 0)
            return null;

        return _danhSachPhuongTien
            .OrderByDescending(pt => pt.TinhGiaLanBanh())
            .First();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<PhuongTien>();

        return _danhSachPhuongTien
            .Where(pt => pt.TenHang.Contains(
                keyword.Trim(),
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}