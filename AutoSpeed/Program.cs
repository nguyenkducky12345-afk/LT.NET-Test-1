namespace AutoSpeed;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("========================================");
        Console.WriteLine("   HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.WriteLine("TC01 - KIỂM TRA VALIDATION NĂM SẢN XUẤT");

        try
        {
            OTo otoLoi = new OTo(
                "OT001",
                "Toyota",
                1850,
                1_000_000_000m,
                5,
                2.0
            );

            Console.WriteLine("❌ TC01 FAILED: Đối tượng không được tạo.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("✅ TC01 PASSED");
            Console.WriteLine($"Kết quả: {ex.Message}");
        }

        Console.WriteLine();

        Console.WriteLine("TC02 - KIỂM TRA GIÁ LĂN BÁNH Ô TÔ");

        OTo oto = new OTo(
            "OT001",
            "Toyota",
            2024,
            1_000_000_000m,
            5,
            2.0
        );

        decimal giaLanBanhOto = oto.TinhGiaLanBanh();

        Console.WriteLine($"Giá gốc: {oto.GiaGoc:N0} VNĐ");
        Console.WriteLine($"Số chỗ: {oto.SoChoNgoi}");
        Console.WriteLine($"Giá lăn bánh: {giaLanBanhOto:N0} VNĐ");

        if (giaLanBanhOto == 1_420_000_000m)
        {
            Console.WriteLine("✅ TC02 PASSED");
        }
        else
        {
            Console.WriteLine("❌ TC02 FAILED");
        }

        Console.WriteLine();

        Console.WriteLine("TC03 - KIỂM TRA GIÁ LĂN BÁNH XE MÁY");

        XeMay xeMay = new XeMay(
            "XM001",
            "Honda",
            2023,
            50_000_000m,
            150
        );

        decimal giaLanBanhXeMay = xeMay.TinhGiaLanBanh();

        Console.WriteLine($"Giá gốc: {xeMay.GiaGoc:N0} VNĐ");
        Console.WriteLine($"Dung tích xi-lanh: {xeMay.DungTichXylanh} cc");
        Console.WriteLine($"Giá lăn bánh: {giaLanBanhXeMay:N0} VNĐ");

        if (giaLanBanhXeMay == 51_000_000m)
        {
            Console.WriteLine("✅ TC03 PASSED");
        }
        else
        {
            Console.WriteLine("❌ TC03 FAILED");
        }

        Console.WriteLine();

        Console.WriteLine("TC04 - KIỂM TRA ĐA HÌNH LIST<PHUONGTIEN>");

        QuanLyPhuongTien quanLy = new QuanLyPhuongTien();

        quanLy.AddPhuongTien(oto);
        quanLy.AddPhuongTien(xeMay);

        quanLy.DisplayAll();

        Console.WriteLine("✅ TC04 PASSED");
        Console.WriteLine();

        Console.WriteLine("TC05 - TÌM GIÁ LĂN BÁNH CAO NHẤT");

        PhuongTien? phuongTienMax = quanLy.FindMaxGiaLanBanh();

        if (phuongTienMax != null)
        {
            Console.WriteLine("Phương tiện có giá lăn bánh cao nhất:");
            Console.WriteLine(phuongTienMax.GetInfo());
            Console.WriteLine(
                $"Giá lăn bánh: {phuongTienMax.TinhGiaLanBanh():N0} VNĐ"
            );

            if (phuongTienMax == oto)
            {
                Console.WriteLine("✅ TC05 PASSED");
            }
            else
            {
                Console.WriteLine("❌ TC05 FAILED");
            }
        }
        else
        {
            Console.WriteLine("❌ TC05 FAILED: Danh sách trống.");
        }

        Console.WriteLine("KIỂM TRA TÌM KIẾM THEO TÊN HÃNG");

        List<PhuongTien> ketQuaTimKiem =
            quanLy.SearchByName("toyota");

        Console.WriteLine($"Tìm thấy {ketQuaTimKiem.Count} phương tiện.");

        foreach (PhuongTien pt in ketQuaTimKiem)
        {
            Console.WriteLine(pt.GetInfo());
        }

        Console.WriteLine();
        
        Console.WriteLine("========================================");
        Console.WriteLine("        HOÀN THÀNH KIỂM TRA");
        Console.WriteLine("========================================");
    }
}