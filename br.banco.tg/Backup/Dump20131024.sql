CREATE DATABASE  IF NOT EXISTS `gustavoama1` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `gustavoama1`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: gustavoama1
-- ------------------------------------------------------
-- Server version	5.5.27

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
-- Table structure for table `Promocao`
--

DROP TABLE IF EXISTS `Promocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Promocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Descricao` text NOT NULL,
  `ImagemUrl` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `Usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Contato` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ClienteLocalizacao`
--

DROP TABLE IF EXISTS `ClienteLocalizacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ClienteLocalizacao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `FK_ClienteLocalizacao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `Cliente` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ClientePromocao`
--

DROP TABLE IF EXISTS `ClientePromocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ClientePromocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataLiberacao` datetime NOT NULL,
  `DataExpiracao` datetime NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `FK_ClientePromocao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `Cliente` (`Id`),
  CONSTRAINT `FK_ClientePromocao_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `Promocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `QualificacaoPromocao`
--

DROP TABLE IF EXISTS `QualificacaoPromocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `QualificacaoPromocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Gostou` tinyint(1) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `IdClientePromocao` int(11) DEFAULT NULL,
  `IdUsuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdClientePromocao` (`IdClientePromocao`),
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `FK88673F6347ECA03D` FOREIGN KEY (`IdClientePromocao`) REFERENCES `ClientePromocao` (`Id`),
  CONSTRAINT `FK_QualificacaoPromocao_Usuario` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PromocaoAcesso`
--

DROP TABLE IF EXISTS `PromocaoAcesso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PromocaoAcesso` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `FK_PromocaoAcesso_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `Promocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Cliente`
--

DROP TABLE IF EXISTS `Cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Cliente` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Documento` varchar(100) NOT NULL,
  `Responsavel` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Contato` varchar(100) NOT NULL,
  `FotoUrl` varchar(100) DEFAULT NULL,
  `Senha` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-10-24 12:03:38
