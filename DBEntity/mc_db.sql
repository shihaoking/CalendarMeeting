CREATE DATABASE  IF NOT EXISTS `mc_db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mc_db`;
-- MySQL dump 10.13  Distrib 5.6.24, for Win64 (x86_64)
--
-- Host: localhost    Database: mc_db
-- ------------------------------------------------------
-- Server version	5.6.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `meeting_info`
--

DROP TABLE IF EXISTS `meeting_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `mi_title` varchar(50) NOT NULL,
  `mi_position_id` smallint(3) NOT NULL,
  `mi_people` varchar(256) DEFAULT NULL,
  `mi_people_name` varchar(1024) DEFAULT NULL,
  `mi_start_time` datetime NOT NULL,
  `mi_end_time` datetime NOT NULL,
  `mi_level_id` tinyint(2) NOT NULL,
  `mi_status` tinyint(1) NOT NULL,
  `mi_creator` smallint(6) NOT NULL,
  `mi_create_time` datetime NOT NULL,
  `mi_memo` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `m_title` (`mi_title`),
  KEY `m_memo` (`mi_memo`(255)),
  KEY `m_creator` (`mi_creator`),
  KEY `m_position` (`mi_position_id`),
  KEY `mi_level_id` (`mi_level_id`),
  KEY `mi_level_id_2` (`mi_level_id`),
  KEY `mi_level_id_3` (`mi_level_id`),
  CONSTRAINT `meeting_info_ibfk_2` FOREIGN KEY (`mi_creator`) REFERENCES `user_info` (`id`) ON UPDATE NO ACTION,
  CONSTRAINT `meeting_info_ibfk_3` FOREIGN KEY (`mi_level_id`) REFERENCES `meeting_level_catg` (`id`) ON UPDATE NO ACTION,
  CONSTRAINT `meeting_info_ibfk_1` FOREIGN KEY (`mi_position_id`) REFERENCES `meeting_position` (`id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meeting_info`
--

LOCK TABLES `meeting_info` WRITE;
/*!40000 ALTER TABLE `meeting_info` DISABLE KEYS */;
INSERT INTO `meeting_info` VALUES (9,'会议主题阿斯蒂芬',1,'',NULL,'2014-06-11 12:00:00','2014-06-11 15:00:00',0,1,1,'2014-06-09 21:07:24',''),(10,'会议主题s',3,'fdgfg',NULL,'2014-06-12 11:00:00','2014-06-12 12:00:00',0,1,1,'2014-06-09 22:02:59','d'),(12,'为二位个方法给',5,'',NULL,'2014-06-09 11:30:00','2014-06-09 13:30:00',0,1,1,'2014-06-10 22:52:10',''),(13,'项目完成进度报告',1,'金世豪',NULL,'2014-06-10 08:00:00','2014-06-10 09:30:00',0,1,1,'2014-06-10 22:52:20',''),(14,'测试完毕',4,'金世豪',NULL,'2014-06-13 10:45:00','2014-06-13 13:00:00',0,1,1,'2014-06-10 23:00:07','哈哈'),(15,'而且我而',3,'',NULL,'2014-06-14 10:45:00','2014-06-14 12:15:00',0,1,1,'2014-06-10 23:00:12',''),(16,'沃尔vfnbxcbxzc',1,'',NULL,'2014-06-09 07:00:00','2014-06-09 08:00:00',0,1,1,'2014-06-11 21:22:42',''),(17,'会议主题',1,'',NULL,'2014-06-09 09:00:00','2014-06-09 10:00:00',0,1,1,'2014-06-11 21:22:52',''),(18,'我二哥的费V型',2,'',NULL,'2014-06-09 14:00:00','2014-06-09 15:00:00',0,1,1,'2014-06-11 21:25:07',''),(20,'会议主题',1,'',NULL,'2014-06-17 07:00:00','2014-06-17 08:30:00',0,1,1,'2014-06-11 21:59:48',''),(21,'会议主题',1,'',NULL,'2014-06-19 05:30:00','2014-06-19 07:30:00',0,1,1,'2014-06-11 21:59:49',''),(22,'AADS撒地方',1,'',NULL,'2014-06-20 08:00:00','2014-06-20 10:45:00',0,1,1,'2014-06-11 21:59:51',''),(23,'农民工丰厚的',1,'',NULL,'2014-06-21 06:30:00','2014-06-21 08:00:00',0,1,1,'2014-06-11 21:59:53',''),(24,'阿瑟日委屈时的想法是的法师的',1,'',NULL,'2014-06-22 12:30:00','2014-06-22 14:15:00',0,1,1,'2014-06-11 21:59:54',''),(25,'会议主题',1,'',NULL,'2014-06-15 07:00:00','2014-06-15 08:00:00',0,1,1,'2014-06-11 21:59:55',''),(26,'会议主题',1,'',NULL,'2014-06-18 12:00:00','2014-06-18 14:00:00',0,1,1,'2014-06-11 22:00:00',''),(27,'撒的发生带我去而',4,'',NULL,'2014-06-25 07:00:00','2014-06-25 08:00:00',0,1,1,'2014-06-11 22:00:02',''),(28,'范德萨丰盛的',5,'金世豪、郭静文、钟毅、蒋志峰、李剑舞、龚小平',NULL,'2014-06-26 07:00:00','2014-06-26 08:00:00',0,1,1,'2014-06-11 22:00:03',''),(29,'秦莞尔',1,'金世豪、郭静文、钟毅、蒋志峰、李剑舞、龚小平',NULL,'2014-06-02 07:00:00','2014-06-02 08:00:00',0,1,1,'2014-06-21 13:47:57',''),(39,'啦啦啦ABC',1,'AASDF',NULL,'2014-07-30 07:15:00','2014-07-30 08:15:00',0,1,1,'2014-07-01 21:17:47','ASDF'),(41,'会议主题',1,'',NULL,'2014-06-09 20:10:00','2014-06-09 21:00:00',0,0,1,'2014-07-03 21:28:38',''),(42,'年度总结大会',3,'金世豪、胡锦涛、温家宝、郭静文',NULL,'2014-07-09 07:00:00','2014-07-09 23:00:00',0,0,8,'2014-07-07 21:09:08',''),(43,'新产品研讨会议',1,'金世豪、郭静文、乔布斯、马化腾',NULL,'2014-07-10 13:00:00','2014-07-10 14:00:00',0,0,8,'2014-07-07 21:09:59',''),(44,'新公司人事调动',1,'郭静文、金世豪、李彦宏',NULL,'2014-07-10 08:00:00','2014-07-10 09:00:00',0,0,8,'2014-07-07 21:10:50',''),(45,'SAD ',1,'啊啊是dASD',NULL,'2014-07-18 07:00:00','2014-07-18 08:00:00',0,1,1,'2014-07-07 21:48:30',''),(46,'蚂蚁聚宝股票购买功能开发',1,'1,4','金世豪, 龚小平','2015-09-15 07:00:00','2015-09-15 08:00:00',0,1,1,'2015-09-04 11:10:36','啦啦啦');
/*!40000 ALTER TABLE `meeting_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `meeting_info_detail`
--

DROP TABLE IF EXISTS `meeting_info_detail`;
/*!50001 DROP VIEW IF EXISTS `meeting_info_detail`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `meeting_info_detail` AS SELECT 
 1 AS `id`,
 1 AS `mi_title`,
 1 AS `mi_position_id`,
 1 AS `mi_position`,
 1 AS `mi_position_size`,
 1 AS `mi_people`,
 1 AS `mi_start_time`,
 1 AS `mi_end_time`,
 1 AS `mi_level_id`,
 1 AS `mi_level`,
 1 AS `mi_level_name`,
 1 AS `mi_creator`,
 1 AS `mi_creator_name`,
 1 AS `mi_status`,
 1 AS `mi_create_time`,
 1 AS `mi_memo`,
 1 AS `mi_people_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `meeting_level_catg`
--

DROP TABLE IF EXISTS `meeting_level_catg`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting_level_catg` (
  `id` tinyint(2) NOT NULL AUTO_INCREMENT,
  `ml_level` tinyint(2) NOT NULL,
  `ml_name` varchar(8) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ml_name` (`ml_name`),
  UNIQUE KEY `ml_level` (`ml_level`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meeting_level_catg`
--

LOCK TABLES `meeting_level_catg` WRITE;
/*!40000 ALTER TABLE `meeting_level_catg` DISABLE KEYS */;
INSERT INTO `meeting_level_catg` VALUES (0,9,'紧急'),(1,8,'重要'),(2,7,'一般');
/*!40000 ALTER TABLE `meeting_level_catg` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meeting_people`
--

DROP TABLE IF EXISTS `meeting_people`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting_people` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `mp_meeting_id` int(11) NOT NULL,
  `mp_user_id` smallint(6) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_user_id_idx` (`mp_user_id`),
  KEY `fk_meeting_id_idx` (`mp_meeting_id`),
  CONSTRAINT `fk_meeting_id` FOREIGN KEY (`mp_meeting_id`) REFERENCES `meeting_info` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_id` FOREIGN KEY (`mp_user_id`) REFERENCES `user_info` (`id`) ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meeting_people`
--

LOCK TABLES `meeting_people` WRITE;
/*!40000 ALTER TABLE `meeting_people` DISABLE KEYS */;
INSERT INTO `meeting_people` VALUES (9,46,1),(10,46,4);
/*!40000 ALTER TABLE `meeting_people` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meeting_position`
--

DROP TABLE IF EXISTS `meeting_position`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting_position` (
  `id` smallint(3) NOT NULL AUTO_INCREMENT,
  `mp_name` varchar(30) NOT NULL,
  `mp_size` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `p_name` (`mp_name`),
  KEY `id_2` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meeting_position`
--

LOCK TABLES `meeting_position` WRITE;
/*!40000 ALTER TABLE `meeting_position` DISABLE KEYS */;
INSERT INTO `meeting_position` VALUES (1,'潘多拉',1),(2,'托马斯',0),(3,'艾薇拉',1),(4,'三楼会议室',2),(5,'三楼会议室B',1);
/*!40000 ALTER TABLE `meeting_position` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_grade_catg`
--

DROP TABLE IF EXISTS `user_grade_catg`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_grade_catg` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `gc_level` tinyint(2) NOT NULL,
  `gc_name` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `grade_level` (`gc_level`),
  KEY `grade_name` (`gc_name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_grade_catg`
--

LOCK TABLES `user_grade_catg` WRITE;
/*!40000 ALTER TABLE `user_grade_catg` DISABLE KEYS */;
INSERT INTO `user_grade_catg` VALUES (0,9,'管理员'),(1,1,'用户');
/*!40000 ALTER TABLE `user_grade_catg` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_info`
--

DROP TABLE IF EXISTS `user_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_info` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `ui_name` varchar(50) NOT NULL,
  `ui_password` varchar(64) NOT NULL,
  `ui_email` varchar(50) NOT NULL,
  `ui_grade_id` tinyint(4) NOT NULL,
  `ui_gender` bit(1) NOT NULL DEFAULT b'1',
  `ui_status` tinyint(1) NOT NULL DEFAULT '1',
  `ui_create_time` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `u_grade_id` (`ui_grade_id`),
  CONSTRAINT `user_info_ibfk_1` FOREIGN KEY (`ui_grade_id`) REFERENCES `user_grade_catg` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_info`
--

LOCK TABLES `user_info` WRITE;
/*!40000 ALTER TABLE `user_info` DISABLE KEYS */;
INSERT INTO `user_info` VALUES (1,'金世豪','FCB55479D7C6F71DA034F38D9409556B','shihaoking@gmail.com',0,'',1,'2014-06-08 11:19:00'),(3,'何继恒','AEAE37989F1D08A2F4C2528023A6C32E','jiheng.he@panduo.com.cm',1,'\0',1,'2014-06-25 10:54:26'),(4,'龚小平','AEAE37989F1D08A2F4C2528023A6C32E','shihaoking@hotmail.com',1,'\0',1,'2014-06-25 19:57:04'),(5,'甘琼','AEAE37989F1D08A2F4C2528023A6C32E','shihaoking@yeah.net',1,'\0',1,'2014-06-25 19:57:42'),(6,'魏启赞','AEAE37989F1D08A2F4C2528023A6C32E','shihaoking@hotmial.com',1,'\0',1,'2014-06-25 19:58:02'),(7,'王海波','AEAE37989F1D08A2F4C2528023A6C32E','shihaoking@gmail.com',1,'\0',1,'2014-06-25 19:58:25'),(8,'郭静文','FCB55479D7C6F71DA034F38D9409556B','shihaoking@outlook.com',1,'\0',1,'2014-07-07 20:00:00');
/*!40000 ALTER TABLE `user_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `user_info_detail`
--

DROP TABLE IF EXISTS `user_info_detail`;
/*!50001 DROP VIEW IF EXISTS `user_info_detail`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `user_info_detail` AS SELECT 
 1 AS `id`,
 1 AS `ui_name`,
 1 AS `ui_password`,
 1 AS `ui_email`,
 1 AS `ui_grade_id`,
 1 AS `ui_gender`,
 1 AS `ui_status`,
 1 AS `ui_create_time`,
 1 AS `ui_grade_level`,
 1 AS `ui_grade_name`,
 1 AS `ui_meeting_count`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `meeting_info_detail`
--

/*!50001 DROP VIEW IF EXISTS `meeting_info_detail`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `meeting_info_detail` AS select `meeting_info`.`id` AS `id`,`meeting_info`.`mi_title` AS `mi_title`,`meeting_info`.`mi_position_id` AS `mi_position_id`,`meeting_position`.`mp_name` AS `mi_position`,`meeting_position`.`mp_size` AS `mi_position_size`,`meeting_info`.`mi_people` AS `mi_people`,`meeting_info`.`mi_start_time` AS `mi_start_time`,`meeting_info`.`mi_end_time` AS `mi_end_time`,`meeting_info`.`mi_level_id` AS `mi_level_id`,`meeting_level_catg`.`ml_level` AS `mi_level`,`meeting_level_catg`.`ml_name` AS `mi_level_name`,`meeting_info`.`mi_creator` AS `mi_creator`,`user_info`.`ui_name` AS `mi_creator_name`,`meeting_info`.`mi_status` AS `mi_status`,`meeting_info`.`mi_create_time` AS `mi_create_time`,`meeting_info`.`mi_memo` AS `mi_memo`,`meeting_info`.`mi_people_name` AS `mi_people_name` from (((`meeting_info` join `meeting_level_catg`) join `meeting_position`) join `user_info`) where ((`meeting_info`.`mi_level_id` = `meeting_level_catg`.`id`) and (`meeting_info`.`mi_position_id` = `meeting_position`.`id`) and (`meeting_info`.`mi_creator` = `user_info`.`id`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `user_info_detail`
--

/*!50001 DROP VIEW IF EXISTS `user_info_detail`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `user_info_detail` AS select `user_info`.`id` AS `id`,`user_info`.`ui_name` AS `ui_name`,`user_info`.`ui_password` AS `ui_password`,`user_info`.`ui_email` AS `ui_email`,`user_info`.`ui_grade_id` AS `ui_grade_id`,`user_info`.`ui_gender` AS `ui_gender`,`user_info`.`ui_status` AS `ui_status`,`user_info`.`ui_create_time` AS `ui_create_time`,`user_grade_catg`.`gc_level` AS `ui_grade_level`,`user_grade_catg`.`gc_name` AS `ui_grade_name`,(select count(1) from `meeting_info` where (`meeting_info`.`mi_creator` = `user_info`.`id`)) AS `ui_meeting_count` from (`user_info` join `user_grade_catg` on((`user_grade_catg`.`id` = `user_info`.`ui_grade_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-09-04 12:13:11
