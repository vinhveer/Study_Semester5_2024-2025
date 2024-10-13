-- Tạo cơ sở dữ liệu Quiz
CREATE DATABASE Quiz;
GO

USE Quiz;
GO

-- 1. Bảng Users: Quản lý thông tin người dùng
CREATE TABLE Users (
    UserId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,   -- GUID tự động tạo
    Username NVARCHAR(100) NOT NULL UNIQUE,               -- Tên đăng nhập
    PasswordHash NVARCHAR(255) NOT NULL,                  -- Mật khẩu băm
    Email NVARCHAR(255) NOT NULL,                         -- Email
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()         -- Thời gian tạo tài khoản
);
GO

-- 2. Bảng Quizzes: Quản lý thông tin các bài kiểm tra
CREATE TABLE Quizzes (
    QuizId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- GUID tự động tạo
    UserId UNIQUEIDENTIFIER NOT NULL,                     -- Người tạo bài kiểm tra (UserId)
    Title NVARCHAR(255) NOT NULL,                         -- Tiêu đề bài kiểm tra
    Description NVARCHAR(MAX),                            -- Mô tả bài kiểm tra
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),        -- Thời gian tạo bài kiểm tra
    FOREIGN KEY (UserId) REFERENCES Users(UserId)         -- Khóa ngoại tham chiếu đến bảng Users
);
GO

-- 3. Bảng Questions: Quản lý thông tin các câu hỏi trong bài kiểm tra
CREATE TABLE Questions (
    QuestionId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- GUID tự động tạo
    QuizId UNIQUEIDENTIFIER NOT NULL,                         -- Thuộc về bài kiểm tra nào (QuizId)
    Text NVARCHAR(MAX) NOT NULL,                             -- Nội dung câu hỏi
    ImageUrl NVARCHAR(255),                                  -- URL hình ảnh đính kèm (nếu có)
    Type INT NOT NULL,                                       -- 0: ShortAnswer, 1: MultipleChoice (Enum)
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),           -- Thời gian tạo câu hỏi
    FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId)          -- Khóa ngoại tham chiếu tới bảng Quizzes
);
GO

-- 4. Bảng ShortAnswerAnswers: Câu trả lời dạng ngắn cho các câu hỏi
CREATE TABLE ShortAnswerAnswers (
    AnswerId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,   -- GUID tự động tạo
    QuestionId UNIQUEIDENTIFIER NOT NULL,                   -- Thuộc về câu hỏi nào (QuestionId)
    Text NVARCHAR(MAX) NOT NULL,                            -- Nội dung câu trả lời dạng ShortAnswer
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),          -- Thời gian tạo
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId) -- Khóa ngoại tham chiếu tới bảng Questions
);
GO

-- 5. Bảng MultipleChoiceAnswers: Câu trả lời dạng trắc nghiệm cho các câu hỏi
CREATE TABLE MultipleChoiceAnswers (
    AnswerId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,   -- GUID tự động tạo
    QuestionId UNIQUEIDENTIFIER NOT NULL,                   -- Thuộc về câu hỏi nào (QuestionId)
Text NVARCHAR(MAX) NOT NULL,                            -- Nội dung câu trả lời dạng MultipleChoice
    IsCorrect BIT NOT NULL DEFAULT 0,                       -- Đánh dấu câu trả lời đúng hay sai
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),          -- Thời gian tạo
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId) -- Khóa ngoại tham chiếu tới bảng Questions
);
GO

-- 6. Bảng QuizResults: Kết quả của người dùng khi thực hiện bài kiểm tra
CREATE TABLE QuizResults (
    ResultId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- GUID tự động tạo
    UserId UNIQUEIDENTIFIER NOT NULL,                       -- Người dùng thực hiện bài kiểm tra
    QuizId UNIQUEIDENTIFIER NOT NULL,                       -- Bài kiểm tra nào (QuizId)
    Score FLOAT NOT NULL,                                   -- Điểm đạt được
    CompletedAt DATETIME NOT NULL DEFAULT GETDATE(),        -- Thời gian hoàn thành bài kiểm tra
    FOREIGN KEY (UserId) REFERENCES Users(UserId),          -- Khóa ngoại tham chiếu đến bảng Users
    FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId)         -- Khóa ngoại tham chiếu đến bảng Quizzes
);
GO

-- 7. Bảng UserAnswers: Lưu câu trả lời của người dùng cho các câu hỏi
CREATE TABLE UserAnswers (
    UserAnswerId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- GUID tự động tạo
    ResultId UNIQUEIDENTIFIER NOT NULL,                        -- Kết quả bài kiểm tra (QuizResultId)
    QuestionId UNIQUEIDENTIFIER NOT NULL,                      -- Câu hỏi được trả lời (QuestionId)
    ShortAnswerId UNIQUEIDENTIFIER NULL,                       -- Câu trả lời dạng ShortAnswer (nếu có)
    MultipleChoiceAnswerId UNIQUEIDENTIFIER NULL,              -- Câu trả lời dạng MultipleChoice (nếu có)
    FOREIGN KEY (ResultId) REFERENCES QuizResults(ResultId),   -- Khóa ngoại tham chiếu đến bảng QuizResults
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId), -- Khóa ngoại tham chiếu đến bảng Questions
    FOREIGN KEY (ShortAnswerId) REFERENCES ShortAnswerAnswers(AnswerId), -- Khóa ngoại tới ShortAnswerAnswers
    FOREIGN KEY (MultipleChoiceAnswerId) REFERENCES MultipleChoiceAnswers(AnswerId) -- Khóa ngoại tới MultipleChoiceAnswers
);
GO

CREATE TABLE ReviewAnswers (
    ReviewAnswerId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- GUID tự động tạo
    UserAnswerId UNIQUEIDENTIFIER NOT NULL,                       -- Tham chiếu đến câu trả lời của người dùng
    QuestionId UNIQUEIDENTIFIER NOT NULL,                         -- Câu hỏi được trả lời
    SelectedAnswer NVARCHAR(MAX) NULL,                            -- Câu trả lời mà người dùng đã chọn
    IsCorrect BIT NOT NULL DEFAULT 0,                            -- Đánh dấu câu trả lời đúng hay sai
    ReviewTime DATETIME NOT NULL DEFAULT GETDATE(),              -- Thời gian xem lại câu trả lời
    FOREIGN KEY (UserAnswerId) REFERENCES UserAnswers(UserAnswerId), -- Khóa ngoại tham chiếu đến UserAnswers
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)    -- Khóa ngoại tham chiếu đến Questions
);
GO