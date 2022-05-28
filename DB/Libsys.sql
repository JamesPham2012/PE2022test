--May 26 2022 v0.3
CREATE TABLE `author` (
  `Author_ID` int NOT NULL,
  `Author_fname` varchar(45) NOT NULL,
  `Author_lname` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Author_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `book` (
  `Book_ID` int NOT NULL DEFAULT '0',
  `Title` varchar(45) NOT NULL,
  `Category_ID` int NOT NULL,
  `Publication_year` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`Book_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `book_author` (
  `BookID` int NOT NULL,
  `AuthorID` int NOT NULL,
  KEY `Author_ID_idx` (`AuthorID`),
  KEY `BookID_idx` (`BookID`),
  CONSTRAINT `AuthorID` FOREIGN KEY (`AuthorID`) REFERENCES `author` (`Author_ID`),
  CONSTRAINT `BookID` FOREIGN KEY (`BookID`) REFERENCES `book` (`Book_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `category` (
  `Category_ID` int NOT NULL,
  `Category_name` varchar(45) NOT NULL,
  KEY `Category_ID` (`Category_ID`),
  CONSTRAINT `Category_ID` FOREIGN KEY (`Category_ID`) REFERENCES `book` (`Book_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `fine` (
  `SessionID` int NOT NULL,
  `Fine_days` int NOT NULL DEFAULT '0',
  `Fine_amount` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`SessionID`),
  UNIQUE KEY `SessionID_UNIQUE` (`SessionID`),
  CONSTRAINT `SessionID` FOREIGN KEY (`SessionID`) REFERENCES `lease` (`SessionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `lease` (
  `SessionID` int NOT NULL,
  `Book_ID` int NOT NULL,
  `ISSN` int NOT NULL,
  `Lease_date` datetime NOT NULL,
  `Expiry_date` datetime NOT NULL,
  `Status` varchar(45) NOT NULL DEFAULT 'active',
  PRIMARY KEY (`SessionID`),
  UNIQUE KEY `SessionID_UNIQUE` (`SessionID`),
  KEY `Book_ID_idx` (`Book_ID`),
  KEY `ISSN_idx` (`ISSN`),
  CONSTRAINT `Book_ID` FOREIGN KEY (`Book_ID`) REFERENCES `book` (`Book_ID`),
  CONSTRAINT `ISSN` FOREIGN KEY (`ISSN`) REFERENCES `user` (`ISSN`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `payment` (
  `Payment_ID` int NOT NULL,
  `Customer_ID` int NOT NULL,
  `Lease_date` datetime NOT NULL,
  `Payment_date` datetime NOT NULL,
  `Payment_amount` int NOT NULL DEFAULT '7000',
  PRIMARY KEY (`Payment_ID`),
  UNIQUE KEY `Payment_ID_UNIQUE` (`Payment_ID`),
  KEY `Customer_ID_idx` (`Customer_ID`),
  CONSTRAINT `Customer_ID` FOREIGN KEY (`Customer_ID`) REFERENCES `user` (`ISSN`),
  CONSTRAINT `Payment_ID` FOREIGN KEY (`Payment_ID`) REFERENCES `lease` (`SessionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `review` (
  `ISSNum` int NOT NULL,
  `IDBook` int NOT NULL,
  `Review_date` datetime DEFAULT NULL,
  `Review_context` longtext,
  `Review_star` int DEFAULT NULL,
  KEY `ISSNum_idx` (`ISSNum`),
  KEY `IDBook_idx` (`IDBook`),
  CONSTRAINT `IDBook` FOREIGN KEY (`IDBook`) REFERENCES `book` (`Book_ID`),
  CONSTRAINT `ISSNum` FOREIGN KEY (`ISSNum`) REFERENCES `user` (`ISSN`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `user` (
  `ISSN` int NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Address` longtext NOT NULL,
  `Phone` varchar(45) NOT NULL,
  `Pass` varchar(45) NOT NULL,
  `ACCESS_CONTROL` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`ISSN`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddBook`(
	ID		INTEGER,
	Title    VARCHAR(45),
	Category_ID        INTEGER,
	Publication_year    INTEGER,
	Quantity			INTEGER(10)
)
BEGIN
	INSERT INTO book
				(Book_ID,
				 Title,
				 Category_ID,
				 Publication_year,
				 Quantity
				 )
	VALUES     ( ID,
				 Title,
				 Category_ID,
				 Publication_year,
				 Quantity
				 );
END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllBooks`()
BEGIN
	SELECT *
	FROM   book order by Title, Publication_year;
END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllUsers`()
BEGIN
	SELECT *
	FROM   user order by Name, ISSN;
END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDAuthor`(AuthorID     INTEGER,
                                          Authorfname	VARCHAR(45),
                                          Authorlname	VARCHAR(45),
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO author
                        (Author_ID,
                        Author_fname,
                        Author_lname
                         )
            VALUES     (AuthorID,
						Authorfname,
                        Authorlname
                         );
		END IF;
        
		IF StatementType = 'SELECT' 
		THEN
            SELECT *
            FROM   author order by Author_fname;
		END IF;
        
        IF StatementType = 'SELECTAuthorID' 
     THEN
            SELECT distinct Author_ID
            FROM   author
            WHERE Author_fname = Authorfname AND Author_lname = Authorlname;
		END IF;
        
      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE author
            SET    Author_fname = Authorfname,
				   Author_lname = Authorlname
            WHERE  Author_ID = AuthorID;
        END IF;
        
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM author
		WHERE  Author_ID = AuthorID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDBook`(BookID            INTEGER,
                                          Title    VARCHAR(45),
                                          Category_ID        INTEGER,
                                          Publication_year          INTEGER,
                                          Quantity					INTEGER,
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO book
                        (Book_ID,
                         Title,
                         Category_ID,
                         Publication_year,
                         Quantity
                         )
            VALUES     (BookID,
                         Title,
                         Category_ID,
                         Publication_year,
                         Quantity
                         );
		END IF;
     
     IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM   book order by Title;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE book
            SET    Title = Title,
                   Category_ID = Category_ID,
                   Publication_year = Publication_year,
                   Quantity = Quantity
            WHERE  Book_ID = BookID;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM book
		WHERE  Book_ID = BookID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDBook_Author`(Book_ID            INTEGER,
                                          Author_ID     INTEGER,
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO book_author
                        (BookID,
                         AuthorID
                         )
            VALUES     (Book_ID,
                         Author_ID
                         );
		END IF;
		IF StatementType = 'SELECT' 
		THEN
            SELECT *
            FROM   book_author order by BookID;
		END IF;
        
        IF StatementType = 'SELECTBA' 
		THEN
            SELECT *
            FROM   book_author 
            where BookID = Book_ID AND AuthorID = Author_ID;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE book_author
            SET    AuthorID = Author_ID
            WHERE  BookID = Book_ID;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM book_author
		WHERE  BookID = Book_ID AND AuthorID = Author_ID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDCategory`(CategoryID int ,
															Categoryname varchar(45),
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO category
                        (Category_ID,
						Category_name
                         )
            VALUES     (CategoryID,
						Categoryname
                         );
		END IF;
        
		IF StatementType = 'SELECT' 
		THEN
            SELECT *
            FROM   category order by Category_name;
		END IF;
        
      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE category
            SET    Category_name = Categoryname
            WHERE Category_ID = CategoryID;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM category
		WHERE  Category_ID = CategoryID OR Category_name = Categoryname ;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDFine`(Session_ID     INTEGER,
                                          Finedays		INTEGER,
                                          Fineamount		INTEGER,
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO fine
                        (SessionID,
						Fine_days,
						Fine_amount
                         )
            VALUES     (Session_ID,
						Finedays,
						Fineamount
                         );
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE fine
            SET    Fine_days = Finedays,
					Fine_amount = Fineamount
            WHERE  SessionID = Session_ID;
        END IF;
        
		IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM   fine;
		END IF;
        
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM fine
		WHERE  SessionID = Session_ID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDLease`(Session_ID            INTEGER,
                                          BookID     INTEGER,
                                          ISSNum        INTEGER,
                                          Leasedate          datetime,
                                          Expirydate		datetime,
                                          report	varchar(45),
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO lease
                        (SessionID,
						Book_ID ,
						ISSN,
						Lease_date,
						Expiry_date
						)
            VALUES     (Session_ID,
						BookID ,
						ISSNum,
						Leasedate,
						Expirydate
						);
		END IF;
     
     IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM  lease order by Lease_date;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE lease
            SET    Book_ID = BookID,
					ISSN = ISSNum,
					Lease_date = Leasedate,
					Expiry_date = Expirydate,
                    Status = report
            WHERE  SessionID = Session_ID;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM lease
		WHERE  SessionID = Session_ID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDPayment`(PaymentID int,
															CustomerID int,
															Leasedate datetime ,
															Paymentdate datetime ,
															Paymentamount int,
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO payment
                        (Payment_ID,
						Customer_ID,
						Lease_date  ,
						Payment_date  ,
						Payment_amount
                         )
            VALUES     (PaymentID,
						CustomerID,
						Leasedate  ,
						Paymentdate  ,
						Paymentamount
						);
		END IF;
     
     IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM   payment;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE payment
            SET    Customer_ID = CustomerID,
					Lease_date = Leasedate ,
					Payment_date = Paymentdate ,
					Payment_amount = Paymentamount
            WHERE  Payment_ID = PaymentID;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM payment
		WHERE  Payment_ID = PaymentID;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDReview`(ISSNumb int ,
														ID_Book int ,
														Reviewdate datetime ,
														Reviewcontext longtext ,
														Reviewstar int,
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO review
                        (ISSNum ,
						IDBook  ,
						Review_date ,
						Review_context ,
						Review_star
                         )
            VALUES     (ISSNumb ,
						ID_Book  ,
						Reviewdate ,
						Reviewcontext ,
						Reviewstar
						);
		END IF;
     
     IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM   review;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE review
            SET     Review_date = Reviewdate ,
					Review_context = Reviewcontext ,
					Review_star = Reviewstar
            WHERE  ISSNum = ISSNumb AND IDBook = ID_Book;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM review
		WHERE  ISSNum = ISSNumb 
		AND 	IDBook = ID_Book;
        END IF;
  END$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ISUDUser`(ISSN_cus int ,
														Name_cus varchar(45) ,
														Email_cus varchar(45) ,
														Address_cus longtext ,
														Phone_cus varchar(45),
                                                        PASS varchar(45),
                                          StatementType NVARCHAR(20) )
BEGIN
      IF StatementType = 'INSERT' 
      THEN
            INSERT INTO user
                        (ISSN,
						Name ,
						Email ,
						Address ,
						Phone,
                        Pass
                         )
            VALUES     (ISSN_cus,
						Name_cus ,
						Email_cus ,
						Address_cus ,
						Phone_cus,
                        PASS
                         );
		END IF;
     
     IF StatementType = 'SELECT' 
     THEN
            SELECT *
            FROM   user order by Name;
		END IF;

      IF StatementType = 'UPDATE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
            UPDATE user
            SET    Name = Name_cus,
					Email = Email_cus,
					Address = Address_cus,
					Phone = Phone_cus,
                    Pass = PASS
            WHERE  ISSN = ISSN_cus;
        END IF;
      IF StatementType = 'DELETE' 
      THEN
      SET SQL_SAFE_UPDATES = 0;
		DELETE FROM user
		WHERE  ISSN = ISSN_cus;
        END IF;
  END$$
DELIMITER ;

INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('8', 'Code8', '2', '2021', '2');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('7', 'Code7', '3', '2022', '1');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('6', 'Code6', '2', '2020', '3');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('5', 'Code5', '1', '2015', '7');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('4', 'Code4', '1', '2008', '4');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('3', 'Code3', '2', '2009', '4');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('2', 'Code2', '3', '2006', '3');
INSERT INTO `pe2022`.`book` (`Book_ID`, `Title`, `Category_ID`, `Publication_year`, `Quantity`) VALUES ('1', 'CleanCode', '1', '2022', '2');

call pe2022.ISUDCategory(1, 'Code', 'INSERT');
call pe2022.ISUDCategory(2, 'Mythic', 'INSERT');
call pe2022.ISUDCategory(3, 'Code3', 'INSERT');

call pe2022.ISUDReview(15001,1,'2022-05-08 16:37:00','hảo hán',5,'INSERT');
call pe2022.ISUDReview(15001,3,'2022-05-09 23:37:00','chán như con gián',2,'INSERT');

call pe2022.ISUDBook_Author(1,1,'INSERT');
call pe2022.ISUDBook_Author(1,3,'INSERT');
call pe2022.ISUDBook_Author(2,3,'INSERT');

call pe2022.ISUDAuthor(1,'Long','Do','INSERT');
call pe2022.ISUDAuthor(2,'Quang','Deo','INSERT');
call pe2022.ISUDAuthor(3,'Hao','Ly','INSERT');
call pe2022.ISUDAuthor(4,'Minh','Tran','INSERT');
call pe2022.ISUDAuthor(5,'Thien','Nguyen','INSERT');
call pe2022.ISUDAuthor(6,'Hoang','Nguyen','INSERT');
call pe2022.ISUDAuthor(7,'Unknown','Unknown','INSERT');

call pe2022.ISUDUser(14559,'Quang','quangdeo@gmail.com','69 Hầm Cầu','096969696','33333',1,'INSERT');
call pe2022.ISUDUser(15001,'Long','15001@student.vgu.edu.vn','A31412B','0979836970','12345',2,'INSERT');
call pe2022.ISUDUser(15382,'Hao','15382@student.vgu.edu.vn','A31412B','0979836970','11111',1,'INSERT');

call pe2022.ISUDLease(1,1,15001,'2022-05-08 00:00:00','2022-05-30 00:00:00','active','INSERT');
call pe2022.ISUDLease(2,2,15001,'2022-05-08 16:37:00','2022-05-15 16:37:00','active','INSERT');
call pe2022.ISUDLease(3,3,15001,'2022-05-08 16:48:00','2022-05-15 16:48:00','active','INSERT');

call pe2022.ISUDPayment(1,15001,'2022-05-08 00:00:00','2022-05-16 23:10:13',7000);

call pe2022.ISUDFine(1,-3,0);
call pe2022.ISUDFine(2,10,10000);
call pe2022.ISUDFine(3,10,10000);

