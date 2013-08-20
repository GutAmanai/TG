# Host: localhost  (Version: 6.0.3-alpha-community)
# Date: 2013-07-29 23:11:40
# Generator: MySQL-Front 5.3  (Build 4.4)

/*!40101 SET NAMES utf8 */;

#
# Source for table "cliente"
#

USE tg;

DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Documento` varchar(100) NOT NULL,
  `Responsavel` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Contato` varchar(100) NOT NULL,
  `FotoUrl` varchar(100) DEFAULT NULL,
  `Senha` varchar(100) NOT NULL,
  `Longitude` double NOT NULL,
  `Latitude` double NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "cliente"
#




#
# Source for table "promocao"
#

DROP TABLE IF EXISTS `promocao`;
CREATE TABLE `promocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `DataLiberacao` datetime NOT NULL,
  `DataExpiracao` datetime NOT NULL,
  `Descricao` varchar(100) NOT NULL,
  `ImagemUrl` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "promocao"
#


#
# Source for table "clientepromocao"
#

DROP TABLE IF EXISTS `clientepromocao`;
CREATE TABLE `clientepromocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataExpiracao` datetime NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `FK_ClientePromocao_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `promocao` (`Id`),
  CONSTRAINT `FK_ClientePromocao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "clientepromocao"
#

