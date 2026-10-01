namespace AutoSpeed;

public class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public int DungTichXylanh
    {
        get => _dungTichXylanh;
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tích xi-lanh phải lớn hơn 0!");

            _dungTichXylanh = value;
        }
    }

    public XeMay(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (DungTichXylanh < 175)
        {
            return GiaGoc + GiaGoc * 0.02m;
        }

        return GiaGoc + GiaGoc * 0.05m;
    }

    public override string GetInfo()
    {
        return base.GetInfo()
            + $", Dung tích xi-lanh: {DungTichXylanh} cc";
    }
}