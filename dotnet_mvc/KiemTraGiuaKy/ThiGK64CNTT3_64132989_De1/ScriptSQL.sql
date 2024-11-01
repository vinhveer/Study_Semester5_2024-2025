-- Tạo cơ sở dữ liệu
CREATE DATABASE ThiGK64CNTT3_64132989_De1;
GO

USE ThiGK64CNTT3_64132989_De1;
GO

-- Tạo bảng TINH
CREATE TABLE Tinh (
    MaTinh INT PRIMARY KEY,
    TenTinh NVARCHAR(50) NOT NULL
);
GO

-- Tạo bảng THANHVIEN
CREATE TABLE ThanhVien (
    MaTV VARCHAR(5) PRIMARY KEY,
    HoTV NVARCHAR(50) NOT NULL,
    TenTV NVARCHAR(50) NOT NULL,
    AnhDaiDien NVARCHAR(50) DEFAULT 'anh.jpg',
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL, -- 1: Nam, 0: Nu
    Email NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100),
    MaTinh INT FOREIGN KEY REFERENCES TINH(MaTinh)
);
GO

-- Thêm dữ liệu mẫu vào bảng TINH
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (1, N'Hà Nội');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (79, N'TP. Hồ Chí Minh');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (31, N'Hải Phòng');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (48, N'Đà Nẵng');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (92, N'Cần Thơ');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (37, N'Nam Định');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (25, N'Vĩnh Phúc');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (35, N'Ninh Bình');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (22, N'Quảng Ninh');
INSERT INTO Tinh (MaTinh, TenTinh) VALUES (27, N'Bắc Giang');
GO

-- Thêm dữ liệu mẫu vào bảng THANHVIEN
INSERT INTO ThanhVien (MaTV, HoTV, TenTV, NgaySinh, GioiTinh, Email, DiaChi, MaTinh) VALUES
('NV1', N'Nguyễn Văn', N'An', '2000-01-01', 1, 'nguyenvanan@example.com', N'Hà Nội', 1),
('NV2', N'Trần Thị', N'Bích Ngọc', '2001-02-02', 0, 'tranthibichngoc@example.com', N'TP. Hồ Chí Minh', 79),
('NV3', N'Phạm Công', N'Thanh Bình', '2002-03-03', 1, 'phamcongthanhbinh@example.com', N'Hải Phòng', 31),
('NV4', N'Lê Thị', N'Kim Chi', '2003-04-04', 0, 'lethikimchi@example.com', N'Đà Nẵng', 48),
('NV5', N'Vũ Minh', N'Tâm', '2004-05-05', 1, 'vuminhtam@example.com', N'Cần Thơ', 92),
('NV6', N'Đặng Quốc', N'Anh Khoa', '1999-06-06', 1, 'dangquocthinh@example.com', N'Nam Định', 37),
('NV7', N'Nguyễn Thị', N'Thu Hà', '2000-07-07', 0, 'nguyenthithuha@example.com', N'Vĩnh Phúc', 25),
('NV8', N'Trương Thị', N'Diễm My', '2001-08-08', 0, 'truongthidiemmy@example.com', N'Ninh Bình', 35),
('NV9', N'Phạm Văn', N'Hùng', '2002-09-09', 1, 'phamvanhung@example.com', N'Quảng Ninh', 22),
('NV10', N'Lý Hải', N'Quang Vinh', '2003-10-10', 1, 'lyhaiquangvinh@example.com', N'Bắc Giang', 27),
('NV11', N'Ngô Bảo', N'Trân', '2000-11-11', 0, 'ngobaotran@example.com', N'Hà Nội', 1),
('NV12', N'Đỗ Hoàng', N'Linh Chi', '1999-12-12', 0, 'dohoanglinhchi@example.com', N'TP. Hồ Chí Minh', 79),
('NV13', N'Trần Hữu', N'Phước', '2001-01-13', 1, 'tranhuuphuoc@example.com', N'Hải Phòng', 31),
('NV14', N'Nguyễn Ngọc', N'Hà My', '2000-02-14', 0, 'nguyenngochamy@example.com', N'Đà Nẵng', 48),
('NV15', N'Phạm Thanh', N'Huyền', '1998-03-15', 0, 'phamthanhhuyen@example.com', N'Cần Thơ', 92),
('NV16', N'Lê Quốc', N'Khánh', '2002-04-16', 1, 'lequockhanh@example.com', N'Nam Định', 37),
('NV17', N'Võ Nhật', N'Minh', '1997-05-17', 1, 'vonhatminh@example.com', N'Vĩnh Phúc', 25),
('NV18', N'Bùi Thanh', N'Phương', '1996-06-18', 0, 'buithanhphuong@example.com', N'Ninh Bình', 35),
('NV19', N'Đinh Tuấn', N'Anh Tú', '1995-07-19', 1, 'dinhtuananhtu@example.com', N'Quảng Ninh', 22),
('NV20', N'Hoàng Thị', N'Lan Anh', '2003-08-20', 0, 'hoangthilananh@example.com', N'Bắc Giang', 27);
GO
