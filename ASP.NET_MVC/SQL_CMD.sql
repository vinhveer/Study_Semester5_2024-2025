-- Tạo cơ sở dữ liệu QLNV_64132989
CREATE DATABASE QLNV_64132989;
GO

-- Sử dụng cơ sở dữ liệu QLNV_64132989
USE QLNV_64132989;
GO

-- Tạo bảng PhongBan
CREATE TABLE PhongBan (
    MaPhongBan INT PRIMARY KEY IDENTITY(1, 1),
    TenPhongBan NVARCHAR(100) NOT NULL
);

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1, 1),
    HoNV NVARCHAR(50) NOT NULL,
    TenNV NVARCHAR(50) NOT NULL,
    GioiTinh BIT NOT NULL, -- 1 = Nam, 0 = Nữ
    NgaySinh DATE,
    Luong DECIMAL(18, 2),
    AnhNV NVARCHAR(255),
    DiaChi NVARCHAR(255),
    MaPhongBan INT,
    FOREIGN KEY (MaPhongBan) REFERENCES PhongBan(MaPhongBan)
);

-- Thêm dữ liệu vào bảng PhongBan
INSERT INTO PhongBan (TenPhongBan)
VALUES 
(N'Phòng Nhân Sự'),
(N'Phòng Kỹ Thuật'),
(N'Phòng Kinh Doanh'),
(N'Phòng Tài Chính'),
(N'Phòng IT');

-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (HoNV, TenNV, GioiTinh, NgaySinh, Luong, AnhNV, DiaChi, MaPhongBan)
VALUES 
(N'Nguyễn', N'Văn Anh', 1, '1990-05-15', 10000000, N'anhA.jpg', N'Hà Nội', 1),
(N'Lê', N'Thị Hà Linh', 0, '1992-07-20', 12000000, N'anhB.jpg', N'Hồ Chí Minh', 2),
(N'Trần', N'Văn Chung', 1, '1988-11-10', 9000000, N'anhC.jpg', N'Đà Nẵng', 3),
(N'Phạm', N'Ánh Dương', 0, '1995-01-22', 15000000, N'anhD.jpg', N'Bình Dương', 4),
(N'Hoàng', N'Văn Thụ', 1, '1993-03-15', 11000000, N'anhE.jpg', N'Cần Thơ', 5),
(N'Vũ', N'Thị Cà Thơi', 0, '1990-08-01', 9500000, N'anhF.jpg', N'Hải Phòng', 1),
(N'Ngô', N'Văn Giang', 1, '1991-06-10', 10500000, N'anhG.jpg', N'Quảng Ninh', 2),
(N'Lý', N'Thị Hoàng', 0, '1989-09-05', 9800000, N'anhH.jpg', N'Hà Nam', 3),
(N'Doãn', N'Văn Hà', 1, '1994-02-18', 10000000, N'anhI.jpg', N'Nam Định', 4),
(N'Bùi', N'Thị Trâm Anh', 0, '1996-10-25', 13000000, N'anhJ.jpg', N'Hà Nội', 5),
(N'Tô', N'Văn Khang', 1, '1997-11-11', 14500000, N'anhK.jpg', N'Đà Nẵng', 1),
(N'Dinh', N'Thị Liên', 0, '1990-07-07', 11500000, N'anhL.jpg', N'Thái Bình', 2),
(N'Tạ', N'Văn Minh', 1, '1992-12-12', 14000000, N'anhM.jpg', N'Hà Tĩnh', 3),
(N'Tôn', N'Thị Ngân', 0, '1993-05-30', 9900000, N'anhN.jpg', N'Quảng Nam', 4),
(N'Tôn', N'Văn Toàn', 1, '1991-03-25', 13500000, N'anhO.jpg', N'Nghệ An', 5),
(N'Nguyễn', N'Thị Phương', 0, '1994-12-10', 12500000, N'anhP.jpg', N'Đà Nẵng', 1),
(N'Phan', N'Văn Quang', 1, '1987-02-12', 10200000, N'anhQ.jpg', N'Hà Nội', 2),
(N'Đinh', N'Thị Cẩm Tú', 0, '1993-06-18', 9400000, N'anhR.jpg', N'Hải Phòng', 3),
(N'Trịnh', N'Văn Sáng', 1, '1990-01-01', 10800000, N'anhS.jpg', N'Hà Nam', 4),
(N'Dương', N'Thị Trang', 0, '1995-09-09', 16000000, N'anhT.jpg', N'Hà Nội', 5);
