CREATE DATABASE ThiGK64CNTT3_64132989_De2;
GO

USE ThiGK64CNTT3_64132989_De2;
GO

-- Create table
CREATE TABLE LoaiTaiSan (
    MaLTS VARCHAR(10) PRIMARY KEY, -- Asset Category ID
    TenLTS NVARCHAR(50) NOT NULL   -- Asset Category Name
);
GO

CREATE TABLE TaiSan (
    MaTS VARCHAR(10) PRIMARY KEY,        -- Asset ID
    TenTS NVARCHAR(100) NOT NULL,        -- Product Name
    DVT NVARCHAR(10) NOT NULL,           -- Unit of Measure
    XuatXu BIT NOT NULL,                 -- Origin (1: Domestic, 0: Imported)
    DonGia DECIMAL(18, 2) NOT NULL,      -- Unit Price
    AnhMH NVARCHAR(255) DEFAULT 'anh.jpg', -- Default Image
    MaLTS VARCHAR(10),                   -- Foreign Key to Asset Category
    GhiChu NVARCHAR(255),                -- Notes
    FOREIGN KEY (MaLTS) REFERENCES LoaiTaiSan(MaLTS)
);
GO

-- Insert Data
INSERT INTO LoaiTaiSan (MaLTS, TenLTS) VALUES
('LTS001', N'Điện tử'),
('LTS002', N'Nội thất'),
('LTS003', N'Văn phòng phẩm');
GO

-- Insert additional data into TAI_SAN
INSERT INTO TaiSan (MaTS, TenTS, DVT, XuatXu, DonGia, MaLTS, GhiChu) VALUES
-- Electronics
('TS001', N'Asus Vivobook 15', N'Cái', 1, 15000000, 'LTS001', N'Laptop văn phòng hiệu năng cao'),
('TS002', N'Dell Inspiron 14', N'Cái', 1, 17000000, 'LTS001', N'Laptop dành cho công việc văn phòng'),
('TS003', N'Máy in Canon LBP2900', N'Cái', 1, 2500000, 'LTS001', N'Máy in đen trắng văn phòng'),
('TS004', N'Máy chiếu Epson EB-X05', N'Cái', 0, 8500000, 'LTS001', N'Máy chiếu hỗ trợ thuyết trình'),
('TS005', N'Máy điều hòa Daikin 1HP', N'Cái', 0, 12000000, 'LTS001', N'Dùng cho phòng làm việc nhỏ'),
('TS006', N'Tivi Samsung 55 inch', N'Cái', 0, 15000000, 'LTS001', N'Dùng cho phòng hội thảo'),
('TS007', N'Switch Cisco 24 cổng', N'Cái', 1, 5000000, 'LTS001', N'Dùng cho mạng văn phòng'),
('TS008', N'Router TP-Link Archer C6', N'Cái', 1, 1200000, 'LTS001', N'Router wifi văn phòng'),
('TS009', N'Loa Bluetooth JBL Charge 4', N'Cái', 0, 3000000, 'LTS001', N'Loa di động cho văn phòng'),
('TS010', N'Webcam Logitech C920', N'Cái', 1, 1000000, 'LTS001', N'Dùng cho các cuộc họp trực tuyến'),

-- Furniture
('TS011', N'Ghế văn phòng Ergonomic', N'Cái', 0, 2500000, 'LTS002', N'Ghế xoay hỗ trợ cột sống'),
('TS012', N'Tủ hồ sơ 4 ngăn', N'Cái', 1, 3000000, 'LTS002', N'Tủ đựng hồ sơ văn phòng'),
('TS013', N'Bàn làm việc chân sắt', N'Cái', 1, 1800000, 'LTS002', N'Bàn làm việc văn phòng'),
('TS014', N'Sofa phòng chờ', N'Cái', 0, 5500000, 'LTS002', N'Sofa dùng cho phòng chờ văn phòng'),
('TS015', N'Bàn họp gỗ tự nhiên', N'Cái', 1, 7000000, 'LTS002', N'Bàn lớn dùng cho phòng họp'),
('TS016', N'Kệ sách văn phòng', N'Cái', 1, 1500000, 'LTS002', N'Dùng để đựng sách và tài liệu'),
('TS017', N'Tủ đựng giày văn phòng', N'Cái', 1, 900000, 'LTS002', N'Tủ gọn nhẹ cho văn phòng'),
('TS018', N'Tủ lạnh mini Electrolux', N'Cái', 0, 4500000, 'LTS002', N'Tủ lạnh nhỏ cho văn phòng'),
('TS019', N'Ghế sofa đơn', N'Cái', 0, 2000000, 'LTS002', N'Dùng trong phòng chờ nhỏ'),
('TS020', N'Bàn tiếp khách gỗ công nghiệp', N'Cái', 1, 2500000, 'LTS002', N'Dùng trong khu vực tiếp khách'),

-- Office Supplies
('TS021', N'Bút bi Thiên Long TL-08', N'Cây', 1, 5000, 'LTS003', N'Dùng cho viết lách'),
('TS022', N'Giấy A4 Double A', N'Ram', 1, 60000, 'LTS003', N'Sử dụng để in ấn tài liệu'),
('TS023', N'Stapler Deli 0224', N'Cái', 1, 40000, 'LTS003', N'Dập ghim cho tài liệu'),
('TS024', N'Kẹp giấy 32mm', N'Hộp', 1, 10000, 'LTS003', N'Kẹp tài liệu văn phòng'),
('TS025', N'Thước kẻ 30cm', N'Cái', 1, 8000, 'LTS003', N'Dụng cụ đo cho văn phòng'),
('TS026', N'Bìa kẹp tài liệu A4', N'Cái', 1, 15000, 'LTS003', N'Dùng để lưu trữ tài liệu'),
('TS027', N'Bảng trắng văn phòng', N'Cái', 1, 900000, 'LTS003', N'Dùng cho thuyết trình và ghi chú'),
('TS028', N'Kẹp bướm 15mm', N'Hộp', 1, 7000, 'LTS003', N'Dụng cụ kẹp giấy'),
('TS029', N'Bút dạ quang Stabilo', N'Cây', 1, 12000, 'LTS003', N'Dùng để làm nổi bật văn bản'),
('TS030', N'Máy hủy giấy Bosser', N'Cái', 0, 2500000, 'LTS003', N'Dùng để hủy tài liệu');
GO
