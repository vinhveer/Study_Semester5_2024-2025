CREATE DATABASE ThiGK_64132989;
GO

USE ThiGK_64132989;
GO

-- Tạo bảng LoaiTinTuc
CREATE TABLE LoaiTinTuc (
    MaLTT INT PRIMARY KEY,
    TenLTT NVARCHAR(100) NOT NULL
);
GO

-- Tạo bảng TinTuc
CREATE TABLE TinTuc (
    MaTT VARCHAR(5) PRIMARY KEY,
    TieuDe NVARCHAR(200) NOT NULL,
    NgonNgu BIT NOT NULL,  -- 0 = Tiếng Việt, 1 = Tiếng Anh
    NguoiDang NVARCHAR(100) NOT NULL,
    NgayDang DATE NOT NULL,
    AnhDaiDien NVARCHAR(100) DEFAULT 'anh.jpg',
    MaLTT INT,
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaLTT) REFERENCES LoaiTinTuc(MaLTT)
);
GO

-- Chèn 5 dòng dữ liệu vào bảng LoaiTinTuc
INSERT INTO LoaiTinTuc (MaLTT, TenLTT)
VALUES
(1, N'Công Nghệ'),
(2, N'Giải Trí'),
(3, N'Thể Thao'),
(4, N'Kinh Tế'),
(5, N'Văn Hóa');
GO

-- Chèn 30 dòng dữ liệu vào bảng TinTuc
INSERT INTO TinTuc (MaTT, TieuDe, NgonNgu, NguoiDang, NgayDang, MaLTT, GhiChu)
VALUES
('TT001', N'Ra mắt sản phẩm điện thoại mới', 1, N'Nguyễn Minh Tuấn', '2024-01-01', 1, N'Thông tin chi tiết về sản phẩm mới'),
('TT002', N'Giải vô địch bóng đá châu Á', 0, N'Lê Thị Thanh', '2024-02-05', 3, NULL),
('TT003', N'Phim hành động nổi bật mùa hè', 1, N'Trần Quốc Khánh', '2024-03-01', 2, N'Đánh giá phim chi tiết'),
('TT004', N'Khám phá nghệ thuật đương đại', 0, N'Phạm Hữu Tài', '2024-03-15', 5, N'Triển lãm lớn tại Hà Nội'),
('TT005', N'Tình hình thị trường chứng khoán', 1, N'Hoàng Ngọc Lan', '2024-03-20', 4, N'Phân tích xu hướng thị trường'),
('TT006', N'Ứng dụng AI vào công việc hằng ngày', 1, N'Nguyễn Văn Hưng', '2024-03-25', 1, NULL),
('TT007', N'Sự kiện âm nhạc quốc tế', 0, N'Lê Hoàng Minh', '2024-04-10', 2, N'Truyền hình trực tiếp'),
('TT008', N'Tổng hợp tin tức thể thao tuần này', 1, N'Trần Văn Hoàng', '2024-04-15', 3, NULL),
('TT009', N'Công nghệ robot trong đời sống', 1, N'Phan Văn Bình', '2024-05-01', 1, N'Nghiên cứu mới về AI'),
('TT010', N'Bộ phim mới được yêu thích', 0, N'Tô Thị Thảo', '2024-05-10', 2, N'Phim hài nổi bật'),
('TT011', N'Thị trường tài chính trong tháng qua', 1, N'Vũ Thanh Bình', '2024-05-15', 4, NULL),
('TT012', N'Cuộc thi âm nhạc truyền thống', 0, N'Nguyễn Đăng Quang', '2024-06-01', 5, N'Sự kiện âm nhạc lớn'),
('TT013', N'Sự phát triển của công nghệ sinh học', 1, N'Ngô Minh Phúc', '2024-06-10', 1, NULL),
('TT014', N'Đánh giá vòng loại World Cup', 0, N'Đỗ Quốc Cường', '2024-06-15', 3, NULL),
('TT015', N'Triển lãm phim quốc tế', 1, N'Trịnh Văn Cường', '2024-07-01', 2, NULL),
('TT016', N'Thị trường bất động sản biến động', 1, N'Trần Hải Long', '2024-07-15', 4, N'Nhận định về tình hình thị trường'),
('TT017', N'Sự kiện văn hóa vùng cao', 0, N'Hoàng Thu Thủy', '2024-08-01', 5, N'Tôn vinh văn hóa dân tộc'),
('TT018', N'Ứng dụng Blockchain trong tài chính', 1, N'Lê Quang Huy', '2024-08-05', 1, NULL),
('TT019', N'Thể thao điện tử ngày càng phổ biến', 1, N'Nguyễn Văn Đức', '2024-08-10', 3, N'Thông tin giải đấu lớn'),
('TT020', N'Phim tâm lý xã hội được mong đợi', 0, N'Phạm Văn Tài', '2024-08-15', 2, N'Dự kiến ra mắt mùa hè'),
('TT021', N'Phân tích nền kinh tế thế giới', 1, N'Nguyễn Thị Hương', '2024-09-01', 4, NULL),
('TT022', N'Chương trình biểu diễn thời trang', 0, N'Hoàng Anh Tuấn', '2024-09-10', 5, N'Sự kiện thời trang lớn'),
('TT023', N'Smartphone mới ra mắt', 1, N'Phạm Ngọc Hải', '2024-09-20', 1, N'Điện thoại thông minh công nghệ cao'),
('TT024', N'Tin tức giải trí cuối tuần', 0, N'Lê Hồng Phát', '2024-10-01', 2, NULL),
('TT025', N'Thế vận hội Olympic mùa hè', 1, N'Trần Nhật Minh', '2024-10-05', 3, N'Tin tức tổng hợp về Olympic'),
('TT026', N'Diễn đàn kinh tế châu Á', 0, N'Vũ Văn Hùng', '2024-10-10', 4, N'Thảo luận các vấn đề kinh tế'),
('TT027', N'Kỷ lục mới trong thể thao', 1, N'Lê Hoàng Long', '2024-10-20', 3, NULL),
('TT028', N'Triển lãm công nghệ toàn cầu', 1, N'Nguyễn Bảo Nam', '2024-11-01', 1, NULL),
('TT029', N'Ngành du lịch phục hồi sau dịch', 0, N'Phạm Quang Linh', '2024-11-05', 5, N'Các địa điểm du lịch nổi bật'),
('TT030', N'Phim khoa học viễn tưởng đáng xem', 1, N'Trần Thị Hoa', '2024-11-10', 2, NULL);
GO