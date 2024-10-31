CREATE DATABASE KT0720_64132989;  -- Thay 'MaSV' bằng mã sinh viên của bạn
GO

USE KT0720_64132989;
GO

CREATE TABLE Lop (
    MaLop CHAR(5) PRIMARY KEY,
    TenLop NVARCHAR(50) NOT NULL
);

CREATE TABLE SinhVien (
    MaSV CHAR(5) PRIMARY KEY,
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(50) NOT NULL,          -- Tên sinh viên trên 2 từ
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL,                -- 1 cho Nam, 0 cho Nữ
    AnhSV NVARCHAR(255) DEFAULT 'anh.jpg',-- Ảnh mặc định là anh.jpg
    DiaChi NVARCHAR(255),
    MaLop CHAR(5),
    FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);

INSERT INTO Lop (MaLop, TenLop) VALUES 
('L001', N'Lớp Kế Toán'),
('L002', N'Lớp Nhân Sự'),
('L003', N'Lớp Công Nghệ Thông Tin'),
('L004', N'Lớp Marketing');

INSERT INTO SinhVien (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, DiaChi, MaLop) VALUES 
('SV001', N'Nguyễn', N'Văn An', '2000-01-15', 1, N'Hà Nội', 'L001'),
('SV002', N'Trần', N'Thị Minh Hoa', '2001-05-20', 0, N'Hồ Chí Minh', 'L002'),
('SV003', N'Phạm', N'Quang Hùng', '1999-03-10', 1, N'Đà Nẵng', 'L003'),
('SV004', N'Lê', N'Thị Bích Ngọc', '2002-08-25', 0, N'Cần Thơ', 'L004'),
('SV005', N'Vũ', N'Thành Công', '2000-04-18', 1, N'Hà Nội', 'L001'),
('SV006', N'Bùi', N'Kim Yến Nhi', '2001-12-12', 0, N'Hải Phòng', 'L002'),
('SV007', N'Hoàng', N'Phúc Hậu', '1998-06-07', 1, N'Huế', 'L003'),
('SV008', N'Đỗ', N'Thị Hồng Phúc', '2000-09-23', 0, N'Nha Trang', 'L004'),
('SV009', N'Ngô', N'Văn Sơn', '1999-11-15', 1, N'Quảng Ninh', 'L001'),
('SV010', N'Thạch', N'Thị Kim Hoa', '2002-02-19', 0, N'Đà Lạt', 'L002');


CREATE PROCEDURE TimKiemSinhVien
    @MaSV NVARCHAR(50) = NULL,
    @HoTen NVARCHAR(50) = NULL
AS
BEGIN
    SELECT * 
    FROM SinhVien
    WHERE (@MaSV IS NULL OR MaSV LIKE '%' + @MaSV + '%')
      AND (@HoTen IS NULL OR (HoSV + ' ' + TenSV LIKE '%' + @HoTen + '%'));
END;

EXEC TimKiemSinhVien @MaSV = 'SV005'