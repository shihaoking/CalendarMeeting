-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 2014-06-30 16:50:16
-- 服务器版本： 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `mc_db`
--
DROP DATABASE `mc_db`;
CREATE DATABASE IF NOT EXISTS `mc_db` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `mc_db`;

-- --------------------------------------------------------

--
-- 表的结构 `meeting_info`
--

DROP TABLE IF EXISTS `meeting_info`;
CREATE TABLE IF NOT EXISTS `meeting_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `mi_title` varchar(50) NOT NULL,
  `mi_position_id` smallint(3) NOT NULL,
  `mi_people` varchar(200) DEFAULT NULL,
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
  KEY `mi_level_id_3` (`mi_level_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=38 ;

--
-- 转存表中的数据 `meeting_info`
--

INSERT INTO `meeting_info` (`id`, `mi_title`, `mi_position_id`, `mi_people`, `mi_start_time`, `mi_end_time`, `mi_level_id`, `mi_status`, `mi_creator`, `mi_create_time`, `mi_memo`) VALUES
(8, '去维尔维尔', 1, '', '2014-06-09 21:06:00', '2014-06-09 22:06:00', 0, 1, 1, '2014-06-09 21:06:50', ''),
(9, '会议主题阿斯蒂芬', 1, '', '2014-06-11 12:00:00', '2014-06-11 15:00:00', 0, 1, 1, '2014-06-09 21:07:24', ''),
(10, '会议主题s', 3, 'fdgfg', '2014-06-12 11:00:00', '2014-06-12 12:00:00', 0, 1, 1, '2014-06-09 22:02:59', 'd'),
(12, '为二位个方法给', 5, '', '2014-06-09 11:30:00', '2014-06-09 13:30:00', 0, 1, 1, '2014-06-10 22:52:10', ''),
(13, '项目完成进度报告', 1, '金世豪', '2014-06-10 08:00:00', '2014-06-10 09:30:00', 0, 1, 1, '2014-06-10 22:52:20', ''),
(14, '测试完毕', 4, '金世豪', '2014-06-13 10:45:00', '2014-06-13 13:00:00', 0, 1, 1, '2014-06-10 23:00:07', '哈哈'),
(15, '而且我而', 3, '', '2014-06-14 10:45:00', '2014-06-14 12:15:00', 0, 1, 1, '2014-06-10 23:00:12', ''),
(16, '沃尔vfnbxcbxzc', 1, '', '2014-06-09 07:00:00', '2014-06-09 08:00:00', 0, 1, 1, '2014-06-11 21:22:42', ''),
(17, '会议主题', 1, '', '2014-06-09 09:00:00', '2014-06-09 10:00:00', 0, 1, 1, '2014-06-11 21:22:52', ''),
(18, '我二哥的费V型', 2, '', '2014-06-09 14:00:00', '2014-06-09 15:00:00', 0, 1, 1, '2014-06-11 21:25:07', ''),
(20, '会议主题', 1, '', '2014-06-17 07:00:00', '2014-06-17 08:30:00', 0, 1, 1, '2014-06-11 21:59:48', ''),
(21, '会议主题', 1, '', '2014-06-19 05:30:00', '2014-06-19 07:30:00', 0, 1, 1, '2014-06-11 21:59:49', ''),
(22, 'AADS撒地方', 1, '', '2014-06-20 08:00:00', '2014-06-20 10:45:00', 0, 1, 1, '2014-06-11 21:59:51', ''),
(23, '农民工丰厚的', 1, '', '2014-06-21 06:30:00', '2014-06-21 08:00:00', 0, 1, 1, '2014-06-11 21:59:53', ''),
(24, '阿瑟日委屈时的想法是的法师的', 1, '', '2014-06-22 12:30:00', '2014-06-22 14:15:00', 0, 1, 1, '2014-06-11 21:59:54', ''),
(25, '会议主题', 1, '', '2014-06-15 07:00:00', '2014-06-15 08:00:00', 0, 1, 1, '2014-06-11 21:59:55', ''),
(26, '会议主题', 1, '', '2014-06-18 12:00:00', '2014-06-18 14:00:00', 0, 1, 1, '2014-06-11 22:00:00', ''),
(27, '撒的发生带我去而', 4, '', '2014-06-25 07:00:00', '2014-06-25 08:00:00', 0, 1, 1, '2014-06-11 22:00:02', ''),
(28, '范德萨丰盛的', 5, '金世豪、周小诗、郭静文、钟毅、蒋志峰、李剑舞、龚小平', '2014-06-26 07:00:00', '2014-06-26 08:00:00', 0, 1, 1, '2014-06-11 22:00:03', ''),
(29, '秦莞尔', 1, '金世豪、周小诗、郭静文、钟毅、蒋志峰、李剑舞、龚小平', '2014-06-02 07:00:00', '2014-06-02 08:00:00', 0, 1, 1, '2014-06-21 13:47:57', ''),
(30, '周小诗的会议', 1, '', '2014-06-21 11:30:00', '2014-06-21 15:45:00', 0, 1, 2, '2014-06-21 14:22:57', ''),
(31, '周小诗的会议A', 4, '金世豪、周小诗、郭静文、钟毅、蒋志峰、李剑舞、龚小平、魏启赞、王汉夫', '2014-06-11 07:00:00', '2014-06-11 08:00:00', 0, 1, 2, '2014-06-21 14:24:07', ''),
(35, '周小诗的会议B', 1, '', '2014-06-18 09:00:00', '2014-06-18 10:15:00', 0, 1, 2, '2014-06-21 14:29:45', ''),
(36, '周小诗的会议F', 1, '', '2014-06-11 09:00:00', '2014-06-11 22:00:00', 0, 1, 2, '2014-06-21 18:50:41', ''),
(37, '周小诗会议主题E周小诗会议主题E', 1, '', '2014-06-18 14:00:00', '2014-06-18 15:00:00', 0, 1, 2, '2014-06-21 18:53:42', '');

-- --------------------------------------------------------

--
-- 替换视图以便查看 `meeting_info_detail`
--
DROP VIEW IF EXISTS `meeting_info_detail`;
CREATE TABLE IF NOT EXISTS `meeting_info_detail` (
`id` int(11)
,`mi_title` varchar(50)
,`mi_position_id` smallint(3)
,`mi_position` varchar(30)
,`mi_position_size` tinyint(4)
,`mi_people` varchar(200)
,`mi_start_time` datetime
,`mi_end_time` datetime
,`mi_level_id` tinyint(2)
,`mi_level` tinyint(2)
,`mi_level_name` varchar(8)
,`mi_creator` smallint(6)
,`mi_creator_name` varchar(50)
,`mi_status` tinyint(1)
,`mi_create_time` datetime
,`mi_memo` varchar(300)
);
-- --------------------------------------------------------

--
-- 表的结构 `meeting_level_catg`
--

DROP TABLE IF EXISTS `meeting_level_catg`;
CREATE TABLE IF NOT EXISTS `meeting_level_catg` (
  `id` tinyint(2) NOT NULL AUTO_INCREMENT,
  `ml_level` tinyint(2) NOT NULL,
  `ml_name` varchar(8) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ml_name` (`ml_name`),
  UNIQUE KEY `ml_level` (`ml_level`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- 转存表中的数据 `meeting_level_catg`
--

INSERT INTO `meeting_level_catg` (`id`, `ml_level`, `ml_name`) VALUES
(0, 9, '紧急'),
(1, 8, '重要'),
(2, 7, '一般');

-- --------------------------------------------------------

--
-- 表的结构 `meeting_position`
--

DROP TABLE IF EXISTS `meeting_position`;
CREATE TABLE IF NOT EXISTS `meeting_position` (
  `id` smallint(3) NOT NULL AUTO_INCREMENT,
  `mp_name` varchar(30) NOT NULL,
  `mp_size` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `p_name` (`mp_name`),
  KEY `id_2` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- 转存表中的数据 `meeting_position`
--

INSERT INTO `meeting_position` (`id`, `mp_name`, `mp_size`) VALUES
(1, '潘多拉', 1),
(2, '托马斯', 0),
(3, '艾薇拉', 1),
(4, '三楼会议室', 2),
(5, '三楼会议室B', 1);

-- --------------------------------------------------------

--
-- 表的结构 `user_grade_catg`
--

DROP TABLE IF EXISTS `user_grade_catg`;
CREATE TABLE IF NOT EXISTS `user_grade_catg` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `gc_level` tinyint(2) NOT NULL,
  `gc_name` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `grade_level` (`gc_level`),
  KEY `grade_name` (`gc_name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `user_grade_catg`
--

INSERT INTO `user_grade_catg` (`id`, `gc_level`, `gc_name`) VALUES
(0, 9, '管理员'),
(1, 1, '用户');

-- --------------------------------------------------------

--
-- 表的结构 `user_info`
--

DROP TABLE IF EXISTS `user_info`;
CREATE TABLE IF NOT EXISTS `user_info` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `ui_name` varchar(50) NOT NULL,
  `ui_password` varchar(64) NOT NULL,
  `ui_email` varchar(50) NOT NULL,
  `ui_grade_id` tinyint(4) NOT NULL,
  `ui_gender` bit(1) NOT NULL DEFAULT b'1',
  `ui_status` tinyint(1) NOT NULL DEFAULT '1',
  `ui_create_time` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `u_grade_id` (`ui_grade_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8 ;

--
-- 转存表中的数据 `user_info`
--

INSERT INTO `user_info` (`id`, `ui_name`, `ui_password`, `ui_email`, `ui_grade_id`, `ui_gender`, `ui_status`, `ui_create_time`) VALUES
(1, '金世豪', 'FCB55479D7C6F71DA034F38D9409556B', 'shihaoking@gmail.com', 0, b'1', 1, '2014-06-08 11:19:00'),
(2, '周小诗', 'AEAE37989F1D08A2F4C2528023A6C32E', 'shihaoking@gmail.com', 1, b'0', 1, '2014-06-21 11:19:00'),
(3, '何继恒', 'AEAE37989F1D08A2F4C2528023A6C32E', 'jiheng.he@panduo.com.cm', 1, b'0', 1, '2014-06-25 10:54:26'),
(4, '龚小平', 'AEAE37989F1D08A2F4C2528023A6C32E', 'shihaoking@hotmail.com', 1, b'0', 1, '2014-06-25 19:57:04'),
(5, '王珊珊', 'AEAE37989F1D08A2F4C2528023A6C32E', 'shihaoking@yeah.net', 1, b'0', 1, '2014-06-25 19:57:42'),
(6, '魏启赞', 'AEAE37989F1D08A2F4C2528023A6C32E', 'shihaoking@hotmial.com', 1, b'0', 1, '2014-06-25 19:58:02'),
(7, '王海波', 'AEAE37989F1D08A2F4C2528023A6C32E', 'shihaoking@gmail.com', 1, b'0', 1, '2014-06-25 19:58:25');

-- --------------------------------------------------------

--
-- 替换视图以便查看 `user_info_detail`
--
DROP VIEW IF EXISTS `user_info_detail`;
CREATE TABLE IF NOT EXISTS `user_info_detail` (
`id` smallint(6)
,`ui_name` varchar(50)
,`ui_password` varchar(64)
,`ui_email` varchar(50)
,`ui_grade_id` tinyint(4)
,`ui_gender` bit(1)
,`ui_status` tinyint(1)
,`ui_create_time` datetime
,`ui_grade_level` tinyint(2)
,`ui_grade_name` varchar(10)
,`ui_meeting_count` bigint(21)
);
-- --------------------------------------------------------

--
-- 视图结构 `meeting_info_detail`
--
DROP TABLE IF EXISTS `meeting_info_detail`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `meeting_info_detail` AS select `meeting_info`.`id` AS `id`,`meeting_info`.`mi_title` AS `mi_title`,`meeting_info`.`mi_position_id` AS `mi_position_id`,`meeting_position`.`mp_name` AS `mi_position`,`meeting_position`.`mp_size` AS `mi_position_size`,`meeting_info`.`mi_people` AS `mi_people`,`meeting_info`.`mi_start_time` AS `mi_start_time`,`meeting_info`.`mi_end_time` AS `mi_end_time`,`meeting_info`.`mi_level_id` AS `mi_level_id`,`meeting_level_catg`.`ml_level` AS `mi_level`,`meeting_level_catg`.`ml_name` AS `mi_level_name`,`meeting_info`.`mi_creator` AS `mi_creator`,`user_info`.`ui_name` AS `mi_creator_name`,`meeting_info`.`mi_status` AS `mi_status`,`meeting_info`.`mi_create_time` AS `mi_create_time`,`meeting_info`.`mi_memo` AS `mi_memo` from (((`meeting_info` join `meeting_level_catg`) join `meeting_position`) join `user_info`) where ((`meeting_info`.`mi_level_id` = `meeting_level_catg`.`id`) and (`meeting_info`.`mi_position_id` = `meeting_position`.`id`) and (`meeting_info`.`mi_creator` = `user_info`.`id`));

-- --------------------------------------------------------

--
-- 视图结构 `user_info_detail`
--
DROP TABLE IF EXISTS `user_info_detail`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `user_info_detail` AS select `user_info`.`id` AS `id`,`user_info`.`ui_name` AS `ui_name`,`user_info`.`ui_password` AS `ui_password`,`user_info`.`ui_email` AS `ui_email`,`user_info`.`ui_grade_id` AS `ui_grade_id`,`user_info`.`ui_gender` AS `ui_gender`,`user_info`.`ui_status` AS `ui_status`,`user_info`.`ui_create_time` AS `ui_create_time`,`user_grade_catg`.`gc_level` AS `ui_grade_level`,`user_grade_catg`.`gc_name` AS `ui_grade_name`,(select count(1) from `meeting_info` where (`meeting_info`.`mi_creator` = `user_info`.`id`)) AS `ui_meeting_count` from (`user_info` join `user_grade_catg` on((`user_grade_catg`.`id` = `user_info`.`ui_grade_id`)));

--
-- 限制导出的表
--

--
-- 限制表 `meeting_info`
--
ALTER TABLE `meeting_info`
  ADD CONSTRAINT `meeting_info_ibfk_1` FOREIGN KEY (`mi_position_id`) REFERENCES `meeting_position` (`id`) ON DELETE NO ACTION,
  ADD CONSTRAINT `meeting_info_ibfk_2` FOREIGN KEY (`mi_creator`) REFERENCES `user_info` (`id`) ON UPDATE NO ACTION,
  ADD CONSTRAINT `meeting_info_ibfk_3` FOREIGN KEY (`mi_level_id`) REFERENCES `meeting_level_catg` (`id`);

--
-- 限制表 `user_info`
--
ALTER TABLE `user_info`
  ADD CONSTRAINT `user_info_ibfk_1` FOREIGN KEY (`ui_grade_id`) REFERENCES `user_grade_catg` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
