drop database if exists shopping;
create database shopping;
use shopping;

create table Users(
UserID int auto_increment   primary key,
UserName varchar(55) not null,
UserPass varchar(55) not null,
UserPhone varchar(11) not null,
UserAddress varchar(55) 

);

insert into Users(Username,UserPass,UserPhone, UserAddress) values
('Tom', 'nde17065', 01665851446, 'Ha Noi');
create table Category(
CategoryID int auto_increment primary key,
CategoryName varchar(255) not null
);

insert into Category(CategoryName) value 
 ('thời trang'),
 ('sức khỏe - mỹ phẩm'),
 ('văn phòng phẩm'),
 ('phụ kiện - đồ chơi'),	
 ('máy tính - điện toại - phụ kiện số'),
 ('đồ nội thất - điện gia dụng'),
 ('phương tiện - thể thao');

select * from Category;



create table Items(
ItemID int not null primary key auto_increment ,
ItemName varchar(55) not null,
ItemAmount int not null,	
ItemPrice decimal(20,0) not null,
CategoryId int(11) not null,
constraint fk_Items_category foreign key(CategoryID) references Category(CategoryID)
);
 -- insert into OrderDetail(OrderID,ItemId,Unit_Price,Amount) value (1,12,260,1) on duplicate key update Amount = Amount +1;
update Items set ItemAmount=ItemAmount-2 where ItemID=1; 
Select * from Items where CategoryID = 1;
insert into Items( ItemName,ItemAmount,ItemPrice, CategoryID)
values ( 'áo ống ren',                              7, 40.000, 1),
 ( 'áo ba lỗ kèm lót - B70',                       14, 52.000, 1),
 ( 'áo hai dây nữ đủ màu',                         286, 19.000, 1),
 ( 'áo sơ mi nam trơn',                            32, 170.000, 1),
 ( 'đầm maxi chấm bi',                             380, 265.000, 1),
 ( 'áo khoác dù loca people',                      9, 88.000, 1),
 ( 'áo khoác jean nữ',                             249, 289.000, 1),
 ( 'Quần short kaki',                              97, 209.000, 1),
 ( 'Quần kaki trung niên',                         19, 150.000, 1),
 ( 'Túi xách du lịch loại lớn',                    137, 89.000, 1),
 ( 'Ba lô KiTy Bags - 1008',                       60, 89.000, 1),
 ( 'chân váy xòe dài phối túi',                    32, 260.000, 1),
 ( 'Vòng Đeo tay Theo Dõi Sức Khỏe QS8',           14, 650.000, 2),
 ( 'ĐAI NỊT BỤNG GIẢM MỠ LÀM THON EO',             36, 79.000, 2),
 ( 'Găng tay y tế',                                76, 80.000, 2),
 ( 'Cân sức khỏe',                                 95, 270.000, 2),
 ( 'detoxic trị kí sinh trùng',                    68, 86.000, 2),
 ( 'Bộ giác hơi không lửa Hàn quốc ',              32, 360.000, 2),
 ( 'kem trị giãn tĩnh mạch chân varikosette',      73, 82.000, 2),
 ( 'kem nâng ngực upsize',                         80, 84.000, 2),
 ( 'Nhiệt kế kiển thị màn LCD đầu dò 1m',          85, 35.000, 2),
 ( 'Bộ Giác Hơi Không Dùng Lửa',                   25, 54.000, 2),
 ( 'Viên Thực Phẩm Chức Năng',                     60, 150.000, 2),
 ( 'Tế bào gốc Vi tảo lục đặc trị mụn Mwhite',     50, 225.000, 2),
 ( 'Giấy Double A4 ĐL 70/90',                      32, 62.000, 3),
 ( 'Bút bi Thiên Long 023',                        14, 2.800, 3),
 ( 'Bút bi nước TL Gel 08 Sunbeam ',               286, 4.500, 3),
 ( 'Bút dấu dòng HaloBig HL02',                    32, 7.000, 3),
 ( 'File còng nhẫn 2.5F TL3302A',                  80, 13.000, 3),
 ( 'Cặp hộp vuông Trà My 7FL1',                    9, 18.000, 3),
 ( 'Sổ A5 đầu bằng 200T ',                         249, 12.500, 3),
 ( 'Sổ lò xo A5 200T Hải Tiến ',                   97, 13.500, 3),
 ( 'Dập ghim 10 Plus PS10E',                       19, 27.000, 3),
 ( 'Ghim KW - TriO 23/13',                         137, 16.000, 3),
 ( 'Phiếu thu A5 2 liên 80T',                      60, 10.000, 3),
 ( 'Giấy giới thiệu',                              32, 4.000, 3),
 ( 'Máy bay chiến đấu điều khiển từ xa Model King',7, 339.000, 4),
 ( 'Rubik Gương tặng kèm rubik mini',              14, 90.000, 4),
 ( 'lắc tay bạc nữ đơn giản',                      86, 240.000, 4),
 ( 'Đồng hồ Led unisex Royal Crown màu xanh dương',32, 150.000, 4),
 ( 'Nón Kết Nam Nữ',                               80, 170.000, 4),
 ( 'NÓN DÙ TIỆN LỢI',                              9, 43.000, 4), 
 ( 'Ma sói Werewolf',                              97, 99.000, 4),
 ( 'VÒNG CỔ CHOCKER 12 DÂY',                       19, 49.000, 4),
 ( 'Madam Dzi - Cài Áo Bướm Hạt Màu',              37, 96.000, 4),
 ( 'Quần vớ - quần tất sexy',                      96, 69.000, 4),
 ( 'găng tay chống nắng',                          32, 25.000, 4),
 ( 'LG G4 CHÍNH HÃNG_MÁY MỚI FULL PK',             50, 1745.000, 5),
 ( 'ĐIỆN THOẠI SONY XPERIA XZ',                    20, 4350.000, 5),
 ( 'Asus X541UV-XX244D',                           86, 10290.000, 5),
 ( 'Lenovo Thinkpad',                              45, 5520.000, 5),
 ( 'Dell Ins N7566A ',                             30, 25490.000, 5),
 ( 'Laptop Dell Vostro 3568 XF6C62',               9, 16500.000, 5),
 ( 'Kính cường lực Galaxy',                        49, 110.000, 5),
 ( 'Jack chia audio và mic - Jack chia tai nghe',  97, 25.000, 5),
 ( 'Tai nghe Bluetooth Earpods i7S',               19, 229.000, 5),
 ( 'Loa Bluetooth Keling F4 Sang Trọng',           37, 274.000, 5),
 ( 'Loa di động',                                  60, 54.000, 5),
 ( 'Laptop E6430 i5 3320M Ram 4GB vỏ nhôm',        32, 4400.000, 5),
 ( 'Bút thử điện Xuyên vỏ dây',                    7, 85.000, 6),
 ( 'BỘ CỜ LÊ - MỎ LẾT ĐA NĂNG ',                   14, 79.000, 6),
 ( 'Bộ 3 Mũi Khoan Tháp - A040025',                86, 235.000, 6),
 ( 'BỘ LỤC GIÁC BOSI - LG05',                      32, 76.000, 6),
 ( 'Nồi kho cá nấu cháo điện đa năng 1.5L',        97, 152.000, 6),
 ( 'NỒI CƠM ĐIỆN KIM CƯƠNG 0.6L',                  9, 188.000, 6),
 ( 'Quạt sạc điện kiêm đèn pin Mini Fan',          49, 155.000, 6),
 ( 'Máy Hút Bụi Mini 2 chiều Cầm Tay',             97, 350.000, 6),
 ( 'Bếp điện đôi mâm nhiệt',                       19, 440.000, 6),
 ( 'Ấm siêu tốc Panafresh 1,8L Malaysia',          37, 245.000, 6),
 ( 'BÌNH THUỶ ĐIỆN KATOMO - KA978',                14, 750.000, 6),
 ( 'Đèn ngủ bàn 2 chế độ sáng - SKU86',            67, 320.000, 6),
 ( 'Xe đạp Giant 2017 ESCAPE 2 CITY',              7, 9000.000, 7),
 ( 'Nón bảo hiểm thể thao',                        14, 117.000, 7),
 ( 'Máy tập cơ bụng,cơ bắp nam nữ ',               42, 125.000, 7),
 ( 'Đai Massage Giảm Mỡ ',                         32, 243.000, 7),
 ( 'Xe Số Honda Blade 110cc Căm Đùm',              80, 18090.000, 7),
 ( 'Xe máy tay ga Yamaha Grande',                  9, 41770.000, 7),
 ( 'Xe đạp trẻ em Beifujia',                       8, 1549.000, 7),
 ( 'Dụng cụ tập thể thao Tummy Trimmer',           97, 65.000, 7),
 ( 'Con lăn tập cơ bụng 4 bánh',                   36, 88.000, 7),
 ( 'Vòng Lắc Eo Giảm Mỡ Massage Hoop',             37, 122.000, 7),
 ( 'BÓNG ĐÁ MAY SỐ 4 CHÍNH HÃNG ',                 99, 99.000, 7),
 ( 'TẤT BÓNG ĐÁ CHỐNG TRƠN ',                      96, 40.000, 7);

select * from Items;

create table Orders(
OrderID int primary key not null auto_increment,
UserID int,
OD_Status int,
Constraint fk_Orders_Customer foreign key(UserID) references Users(UserID)
);
insert into orders(userid, od_status) value (1,2);

create table OrderDetail(
ItemID int not null,
OrderID int not null,
Unit_price decimal ,
Amount int not null default 1,
OD_Status int,

 CONSTRAINT fk_OrderDetail_OrderID FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
  CONSTRAINT fk_OrderDetail_Item_ID FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
  Constraint pk_OrderDetail primary key (OrderID, ItemID)
);
delimiter $$
create trigger tg_CheckAmount
	before update on Items
	for each row
	begin
		if new.ItemAmount < 0 then
            signal sqlstate '45001' set message_text = 'Sản phẩm đã hết hàng ';
        end if;
    end $$
delimiter ;
 -- insert into OrderDetail(OrderID, ItemID, Unit_Price,Amount ,OD_Status) value
                        --   (1,1, 20,2,1);
insert into OrderDetail(OrderID,ItemId,Unit_price,Amount) value (1,12,260,1) on duplicate key update Amount = Amount +1;
Select * From Users  where Username = 'Tom' and Userpass = 'nde17065';
