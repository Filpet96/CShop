# ************************************************************
# Sequel Pro SQL dump
# Version 4541
#
# http://www.sequelpro.com/
# https://github.com/sequelpro/sequelpro
#
# Host: localhost (MySQL 5.6.38)
# Database: EShop
# Generation Time: 2018-04-18 15:39:15 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump of table animals
# ------------------------------------------------------------

DROP TABLE IF EXISTS `animals`;

CREATE TABLE `animals` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(200) DEFAULT NULL,
  `price` int(200) DEFAULT NULL,
  `img` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

LOCK TABLES `animals` WRITE;
/*!40000 ALTER TABLE `animals` DISABLE KEYS */;

INSERT INTO `animals` (`id`, `name`, `price`, `img`)
VALUES
	(1,'Parrot',1000,'http://pngimg.com/uploads/parrot/parrot_PNG713.png'),
	(2,'Lion',500,'http://pngimg.com/uploads/lion/lion_PNG23278.png'),
	(3,'Panda',950000,'http://pngimg.com/uploads/panda/panda_PNG27.png');

/*!40000 ALTER TABLE `animals` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table cart
# ------------------------------------------------------------

DROP TABLE IF EXISTS `cart`;

CREATE TABLE `cart` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `productId` int(11) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `guid` blob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;



# Dump of table orderRows
# ------------------------------------------------------------

DROP TABLE IF EXISTS `orderRows`;

CREATE TABLE `orderRows` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `guid` blob,
  `productName` text,
  `productPrice` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

LOCK TABLES `orderRows` WRITE;
/*!40000 ALTER TABLE `orderRows` DISABLE KEYS */;

INSERT INTO `orderRows` (`id`, `guid`, `productName`, `productPrice`)
VALUES
	(5,X'39386638616532382D666366612D343939312D626635652D633633363933656237636439','Lion',500),
	(6,X'39386638616532382D666366612D343939312D626635652D633633363933656237636439','Panda',950000),
	(7,X'36616532353132362D353531652D343135612D613662372D373363653032353436356231','Parrot',1000),
	(8,X'36616532353132362D353531652D343135612D613662372D373363653032353436356231','Lion',500),
	(9,X'36616532353132362D353531652D343135612D613662372D373363653032353436356231','Panda',950000);

/*!40000 ALTER TABLE `orderRows` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table orders
# ------------------------------------------------------------

DROP TABLE IF EXISTS `orders`;

CREATE TABLE `orders` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `guid` blob,
  `email` text,
  `address` text,
  `total` decimal(11,0) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;

INSERT INTO `orders` (`id`, `guid`, `email`, `address`, `total`)
VALUES
	(13,X'39386638616532382D666366612D343939312D626635652D633633363933656237636439','Fpetersson96@gmail.com','Linbastavägen 36',1901000),
	(14,X'36616532353132362D353531652D343135612D613662372D373363653032353436356231','Fpetersson96@gmail.com','Linbastavägen 36',952500);

/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
