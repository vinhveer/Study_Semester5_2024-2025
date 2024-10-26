CREATE DATABASE KT0720_64132989;
GO

USE KT0720_64132989;
GO

CREATE TABLE Lop (
    MaLop CHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50)
);
GO

CREATE TABLE SinhVien (
    MaSV CHAR(10) PRIMARY KEY,
    HoSV NVARCHAR(50),
    TenSV NVARCHAR(50),
    NgaySinh DATE,
    GioiTinh BIT,
    AnhSV VARBINARY(MAX),
    DiaChi NVARCHAR(100),
    MaLop CHAR(10),
    FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);
GO

-- Thêm dữ liệu vào bảng LOP
INSERT INTO LOP (MaLop, TenLop) VALUES
('L01', N'Tin Học 1'),
('L02', N'Tin Học 2'),
('L03', N'Tin Học 3');
GO

-- Thêm dữ liệu vào bảng SINHVIEN với tên 3 chữ
INSERT INTO SINHVIEN (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, DiaChi, MaLop) VALUES
('SV001', N'Nguyễn Văn', N'Anh', '2002-05-12', 1, N'Hà Nội', 'L01'),
('SV002', N'Trần Thị', N'Hoa', '2003-03-09', 0, N'Hồ Chí Minh', 'L02'),
('SV003', N'Lê Minh', N'Tú', '2004-01-20', 1, N'Đà Nẵng', 'L03'),
('SV004', N'Phạm Ngọc', N'Huy', '2002-09-10', 1, N'Bình Dương', 'L01'),
('SV005', N'Ngô Thị', N'Lan', '2003-11-25', 0, N'Vũng Tàu', 'L02'),
('SV006', N'Hoàng Văn', N'Hải', '2004-02-14', 1, N'Quảng Ninh', 'L03'),
('SV007', N'Vũ Thanh', N'Trung', '2002-04-21', 1, N'Hải Phòng', 'L01'),
('SV008', N'Phan Thị', N'Thảo', '2003-06-07', 0, N'Nam Định', 'L02'),
('SV009', N'Đỗ Đức', N'Hoàng', '2004-08-15', 1, N'Ninh Bình', 'L03'),
('SV010', N'Bùi Thị', N'Thu', '2002-12-30', 0, N'Lào Cai', 'L01'),
('SV011', N'Nguyễn Thị', N'Phương', '2003-10-17', 0, N'Bắc Giang', 'L02'),
('SV012', N'Hoàng Anh', N'Quân', '2004-07-23', 1, N'Phú Yên', 'L03'),
('SV013', N'Lý Hoàng', N'Anh', '2002-03-03', 1, N'Khánh Hòa', 'L01'),
('SV014', N'Nguyễn Ngọc', N'Tuyết', '2003-01-11', 0, N'Bình Phước', 'L02'),
('SV015', N'Trần Minh', N'Hùng', '2004-09-25', 1, N'Bắc Ninh', 'L03'),
('SV016', N'Lê Thị', N'Hòa', '2002-11-14', 0, N'Hà Tĩnh', 'L01'),
('SV017', N'Phạm Quốc', N'Duy', '2003-05-04', 1, N'Bình Thuận', 'L02'),
('SV018', N'Ngô Thị', N'Vy', '2004-06-18', 0, N'Đồng Nai', 'L03'),
('SV019', N'Vũ Hoàng', N'Phúc', '2002-10-01', 1, N'Gia Lai', 'L01'),
('SV020', N'Trần Ngọc', N'Hiền', '2003-02-26', 0, N'Long An', 'L02');
GO

CREATE FUNCTION fn_TimKiemSinhVien 
(
    @MaSV CHAR(10) = NULL,
    @HoTen NVARCHAR(100) = NULL,
    @MaLop CHAR(10) = NULL
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SINHVIEN
    WHERE 
        (@MaSV IS NULL OR MaSV = @MaSV)
        AND (@HoTen IS NULL OR (HoSV + ' ' + TenSV) LIKE '%' + @HoTen + '%')
        AND (@MaLop IS NULL OR MaLop = @MaLop)
);
GO