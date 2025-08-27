-- SQL INSERT statements for Bouquet table
-- Make sure BouquetType table has records with ID 1 and 2 before running these inserts

INSERT INTO Bouquet (Name, Description, BouquetTypeID, ImageURL, isAvailable, CreatedDate, UpdatedDate)
VALUES 
('Bó Hoa Baby Hồng', 'Bó hoa baby hồng tươi mát, thích hợp cho các dịp lễ tình nhân và kỷ niệm', 1, 'Images/Flower/BoHoaBabyHong.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Cam Chuông Violetta', 'Bó hoa Cam Chuông Violetta tươi mát, tím liệm', 1, 'Images/Flower/BoHoaCamChuongVioletta.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Cánh Cúc', 'Bó hoa cánh cúc trắng tinh khôi, mang lại cảm giác bình yên và trang nhã', 1, 'Images/Flower/BoHoaCanhCuc.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng', 'Bó hoa Hồng đặc sắc, mang lại cảm giác bình yên và trang nhã', 1, 'Images/Flower/BoHoaHong.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Cam Chuông', 'Bó hoa hồng cam chuông rực rỡ, thể hiện sự ấm áp và năng động', 1, 'Images/Flower/BoHoaHongCamChuong.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Cam Spirit', 'Bó hoa hồng cam spirit tươi sáng, tượng trưng cho tinh thần lạc quan', 1, 'Images/Flower/BoHoaHongCamSpirit.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Đỏ', 'Bó hoa hồng đỏ thắm, biểu tượng của tình yêu chân thành và nồng nàn', 1, 'Images/Flower/BoHoaHongDo.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Hót Gà', 'Bó hoa hồng hót gà độc đáo, mang phong cách hiện đại và sang trọng', 1, 'Images/Flower/BoHoaHongHotGa.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng LaVieEn Rose', 'Bó hoa hồng LaVieEn độc đáo, mang phong cách hiện đại và sang trọng', 1, 'Images/Flower/BoHoaHongLaVieEnRose.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Sáp', 'Bó hoa hồng sáp vàng óng, thể hiện sự quý phái và thịnh vượng', 1, 'Images/Flower/BoHoaHongSap.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hồng Song Hỷ', 'Bó hoa hồng song hỷ may mắn, thích hợp cho các dịp cưới hỏi', 1, 'Images/Flower/BoHoaHongSongHy.jpg', 1, GETDATE(), GETDATE()),
('Bó Hoa Hướng Dương', 'Bó hoa hướng dương tươi sáng, mang đến năng lượng tích cực và niềm vui', 1, 'Images/Flower/BoHoaHuongDuong.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hồng Dreamlike', 'Giở hoa hồng dreamlike, đẹp như mơ', 2, 'Images/Flower/BoHoaHongDreamlike.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hồng Gorgeous', 'Giỏ hoa hồng gorgeous lộng lẫy, thể hiện vẻ đẹp quyến rũ và sang trọng', 2, 'Images/Flower/GioHoaHongGorgeous.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hồng Hướng Dương', 'Giỏ hoa kết hợp hồng và hướng dương, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaHongHuongDuong.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hồng Kem', 'Giỏ hoa kết hợp hồng và kem, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaHongKem.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hồng Wonderland', 'Giỏ hoa hồng Wonderland, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaHongWonderland.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Hướng Dương', 'Giỏ hoa hướng dương, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaHuongDuong.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Đầm Ấm Amber', 'Giỏ hoa kết hợp hồng và Amber, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaIntimateAmber.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Lãng Mạng', 'Giỏ hoa kết hợp hồng và Amber, tạo nên sự hài hòa đầy màu sắc', 2, 'Images/Flower/GioHoaRomance.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Sắc Cam', 'Giỏ hoa sắc cam rực rỡ, mang lại cảm giác ấm áp và năng động', 2, 'Images/Flower/GioHoaSacCam.jpg', 1, GETDATE(), GETDATE()),
('Giỏ Hoa Virgorousness', 'Giỏ hoa sắc cam rực rỡ, mang lại cảm giác ấm áp và năng động', 2, 'Images/Flower/GioHoaVirgorousness.jpg', 1, GETDATE(), GETDATE());

-- Optional: Insert BouquetType records if they don't exist
-- INSERT INTO BouquetType (Id, Name, Description, CreatedDate, UpdatedDate, isActive)
-- VALUES 
-- (1, 'Bó Hoa', 'Loại hoa được bó thành từng bó nhỏ, thích hợp làm quà tặng', GETDATE(), GETDATE(), 1),
-- (2, 'Giỏ Hoa', 'Loại hoa được sắp xếp trong giỏ, thích hợp cho trang trí và quà tặng', GETDATE(), GETDATE(), 1);
