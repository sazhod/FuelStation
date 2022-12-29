CREATE DATABASE  IF NOT EXISTS `gus_station` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gus_station`;
-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: localhost    Database: gus_station
-- ------------------------------------------------------
-- Server version	8.0.29

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `id` int NOT NULL AUTO_INCREMENT,
  `lastname` varchar(45) NOT NULL,
  `firsname` varchar(45) NOT NULL,
  `patronymic` varchar(45) NOT NULL,
  `idrole` int NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user_role_idx` (`idrole`),
  CONSTRAINT `employee_role` FOREIGN KEY (`idrole`) REFERENCES `role` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,'Каюмов','Сажод','Акрамчонович',1,'admin','admin'),(2,'Иванов','Иван','Иванович',2,'ivan','ivan'),(3,'Артёмов','Артём','Артёмович',3,'artem','artem'),(6,'Олегов','Олег','Олегович',4,'oleg','oleg');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fuel`
--

DROP TABLE IF EXISTS `fuel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fuel` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `idmeasure` int NOT NULL,
  `idvendor` int NOT NULL,
  `cost_per_unit` decimal(5,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fuel_measure_idx` (`idmeasure`),
  KEY `fuel_vendor_idx` (`idvendor`),
  CONSTRAINT `fuel_measure` FOREIGN KEY (`idmeasure`) REFERENCES `measure` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `fuel_vendor` FOREIGN KEY (`idvendor`) REFERENCES `vendor` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fuel`
--

LOCK TABLES `fuel` WRITE;
/*!40000 ALTER TABLE `fuel` DISABLE KEYS */;
INSERT INTO `fuel` VALUES (1,'АИ-92',1,1,45.00),(2,'АИ-95',1,3,48.00),(3,'АИ-98',1,3,51.00);
/*!40000 ALTER TABLE `fuel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fuel_sales`
--

DROP TABLE IF EXISTS `fuel_sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fuel_sales` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idemployee` int NOT NULL,
  `idfuel` int NOT NULL,
  `datetime` datetime NOT NULL,
  `quantity` decimal(5,2) NOT NULL,
  `totalcost` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fuel_sales_employee_idx` (`idemployee`),
  KEY `fuel_sales_fuel_idx` (`idfuel`),
  CONSTRAINT `fuel_sales_employee` FOREIGN KEY (`idemployee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fuel_sales_fuel` FOREIGN KEY (`idfuel`) REFERENCES `fuel` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fuel_sales`
--

LOCK TABLES `fuel_sales` WRITE;
/*!40000 ALTER TABLE `fuel_sales` DISABLE KEYS */;
INSERT INTO `fuel_sales` VALUES (2,2,1,'2022-12-26 22:19:52',20.00,900),(3,2,2,'2022-12-26 22:25:05',123.00,5535);
/*!40000 ALTER TABLE `fuel_sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fuel_supplies`
--

DROP TABLE IF EXISTS `fuel_supplies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fuel_supplies` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_receiving_employee` int NOT NULL,
  `id_bringing_employee` int NOT NULL,
  `datetime` datetime NOT NULL,
  `idfuel` int NOT NULL,
  `quantity` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fuel_supplies_fuel_idx` (`idfuel`),
  KEY `fuel_supplies_receiving_employee_idx` (`id_receiving_employee`),
  KEY `fuel_supplies_bringing_employee_idx` (`id_bringing_employee`),
  CONSTRAINT `fuel_supplies_bringing_employee` FOREIGN KEY (`id_bringing_employee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fuel_supplies_fuel` FOREIGN KEY (`idfuel`) REFERENCES `fuel` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fuel_supplies_receiving_employee` FOREIGN KEY (`id_receiving_employee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fuel_supplies`
--

LOCK TABLES `fuel_supplies` WRITE;
/*!40000 ALTER TABLE `fuel_supplies` DISABLE KEYS */;
INSERT INTO `fuel_supplies` VALUES (12,2,6,'2022-12-30 01:41:33',1,1000);
/*!40000 ALTER TABLE `fuel_supplies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `measure`
--

DROP TABLE IF EXISTS `measure`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `measure` (
  `id` int NOT NULL AUTO_INCREMENT,
  `measure` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `measure`
--

LOCK TABLES `measure` WRITE;
/*!40000 ALTER TABLE `measure` DISABLE KEYS */;
INSERT INTO `measure` VALUES (1,'литр'),(2,'м^3');
/*!40000 ALTER TABLE `measure` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(45) NOT NULL,
  `salary` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Администратор',40000),(2,'Менеджер',35000),(3,'Продавец-консультатн',25000),(4,'Доставщик топлива',40000);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendor`
--

DROP TABLE IF EXISTS `vendor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendor` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendor`
--

LOCK TABLES `vendor` WRITE;
/*!40000 ALTER TABLE `vendor` DISABLE KEYS */;
INSERT INTO `vendor` VALUES (1,'Башнефть','нет'),(2,'ГазпромНефть','нет'),(3,'Лукойл','нет');
/*!40000 ALTER TABLE `vendor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'gus_station'
--

--
-- Dumping routines for database 'gus_station'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-30  1:50:40
