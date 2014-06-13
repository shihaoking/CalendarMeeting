-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 2014-06-13 09:10:38
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

-- --------------------------------------------------------

--
-- 表的结构 `meeting_info`
--

CREATE TABLE IF NOT EXISTS `meeting_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `m_title` varchar(50) NOT NULL,
  `m_position` smallint(6) NOT NULL,
  `m_people` varchar(200) NOT NULL,
  `m_start_time` datetime NOT NULL,
  `m_end_time` datetime NOT NULL,
  `m_level` int(11) NOT NULL,
  `m_creator` smallint(6) NOT NULL,
  `m_create_time` datetime NOT NULL,
  `m_memo` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_meeting_info_ibfk_1` (`m_position`),
  KEY `IX_FK_meeting_info_ibfk_2` (`m_creator`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=21 ;

--
-- 转存表中的数据 `meeting_info`
--

INSERT INTO `meeting_info` (`id`, `m_title`, `m_position`, `m_people`, `m_start_time`, `m_end_time`, `m_level`, `m_creator`, `m_create_time`, `m_memo`) VALUES
(1, 'CRM开发进度汇报', 2, '何继恒、甘琼、龚小平、魏启赞、吴英博', '2014-06-10 09:30:00', '2014-06-10 10:45:00', 0, 1, '2014-06-09 10:44:00', '如无其他事情请准时'),
(2, '放假通知', 3, '阿斯蒂芬', '2014-06-11 09:30:00', '2014-06-11 10:45:00', 0, 1, '2014-06-09 14:42:04', '阿斯蒂芬阿斯蒂芬'),
(10, '会议主题', 1, '', '2014-06-09 04:20:00', '2014-06-09 05:20:00', 0, 1, '2014-06-09 16:20:04', ''),
(13, '会议主题', 1, '', '2014-06-13 06:45:00', '2014-06-13 08:00:00', 0, 1, '2014-06-11 09:18:19', ''),
(14, '会议主题', 1, '', '2014-06-13 09:00:00', '2014-06-13 11:00:00', 0, 1, '2014-06-11 09:18:31', ''),
(16, '会议主题', 1, '', '2014-06-14 07:00:00', '2014-06-14 08:00:00', 0, 1, '2014-06-11 09:19:43', ''),
(17, '会议主题', 1, '', '2014-06-12 08:15:00', '2014-06-12 10:45:00', 0, 1, '2014-06-11 09:21:47', ''),
(18, '会议主题', 1, '', '2014-06-12 13:00:00', '2014-06-12 15:30:00', 0, 1, '2014-06-11 09:22:45', ''),
(19, '会议主题', 1, 'sadf', '2014-06-10 07:15:00', '2014-06-10 08:30:00', 0, 1, '2014-06-12 11:58:08', 'asdf'),
(20, '会议主题', 1, '', '2014-06-11 15:00:00', '2014-06-11 16:00:00', 0, 1, '2014-06-12 11:58:36', '');

-- --------------------------------------------------------

--
-- 表的结构 `meeting_position`
--

CREATE TABLE IF NOT EXISTS `meeting_position` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `p_name` varchar(30) NOT NULL,
  `p_size` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- 转存表中的数据 `meeting_position`
--

INSERT INTO `meeting_position` (`id`, `p_name`, `p_size`) VALUES
(1, '会议室A', 1),
(2, '会议室B', 2),
(3, '会议室C', 1),
(4, '会议室D', 1),
(5, '会议室E', 1);

-- --------------------------------------------------------

--
-- 表的结构 `user_info`
--

CREATE TABLE IF NOT EXISTS `user_info` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `u_name` varchar(50) NOT NULL,
  `u_password` varchar(15) NOT NULL,
  `u_level` tinyint(4) NOT NULL,
  `u_email` varchar(50) NOT NULL,
  `u_status` varchar(1) NOT NULL,
  `u_create_time` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `user_info`
--

INSERT INTO `user_info` (`id`, `u_name`, `u_password`, `u_level`, `u_email`, `u_status`, `u_create_time`) VALUES
(1, '金世豪', 'jinshihao', 0, 'shihaoking@email.com', 'A', '2014-06-09 10:44:00');

--
-- 限制导出的表
--

--
-- 限制表 `meeting_info`
--
ALTER TABLE `meeting_info`
  ADD CONSTRAINT `FK_meeting_info_ibfk_1` FOREIGN KEY (`m_position`) REFERENCES `meeting_position` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_meeting_info_ibfk_2` FOREIGN KEY (`m_creator`) REFERENCES `user_info` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
