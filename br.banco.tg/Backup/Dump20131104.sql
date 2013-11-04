CREATE DATABASE  IF NOT EXISTS `tg` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `tg`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: tg
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
-- Table structure for table `promocao`
--

DROP TABLE IF EXISTS `promocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `promocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Descricao` text NOT NULL,
  `ImagemUrl` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promocao`
--

LOCK TABLES `promocao` WRITE;
/*!40000 ALTER TABLE `promocao` DISABLE KEYS */;
INSERT INTO `promocao` VALUES (3,'ERP Customizado','2013-11-04 09:14:22','Que tal organizar sua empresa da melhor forma possível? Venha conhecer esta tecnologia!',NULL),(4,'Seu site Mobile','2013-11-04 09:16:18','Convertemos seu site para os diversos mobiles! Veja uma prévia em nosso site!',NULL),(5,'Alocação de Recursos','2013-11-04 09:17:22','Precisa de alguém para realizar aquela atividade trabalhosa e em tempo recorde? Nós faremos para você!',NULL),(6,'Testes de Software','2013-11-04 09:18:15','Software sem qualidade? Não sabe de onde vem os bugs? Nossa equipe ira apontar todos os defeitos a corrigir!',NULL),(7,'Aplicações Mobile','2013-11-04 09:18:51','Temos soluções para os principais mobiles do mercado! Venha já pra Lotus!',NULL),(8,'Oportunidade de Ouro','2013-11-04 09:19:18','Venha fazer parte do time Lotus! Mas não é pra qualquer um! Aqui os desafios são nossa meta!',NULL),(9,'Novos Horizontes','2013-11-04 09:21:08','Livro \'Novos Horizontes\', para você que quer além do alcance!',NULL),(10,'Coisa Velha','2013-11-04 09:22:24','Livro \'Coisa Velha\', por que nem tudo é lixo!',NULL),(11,'TI Comportamentos','2013-11-04 09:23:34','Livro de estudo de comportamento de profissionais de TI.',NULL),(12,'TI Melhoramentos','2013-11-04 09:23:55','Livro de estudo de melhoramentos para o profissional de TI.',NULL),(13,'Costela da Casa','2013-11-04 09:26:41','Venha conhecer a deliciosa costela da casa em preço promocional!',NULL),(14,'Na Manteiga','2013-11-04 09:28:34','Todos os pratos quentes, hoje, feitos na manteiga, velha deliciar-se!',NULL),(15,'Bem assado','2013-11-04 09:29:32','Aqui gostamos é bem passado! Venha provar os melhores cortes!',NULL),(16,'Carne Explosiva','2013-11-04 09:31:14','A melhor carne louca no seu pastel, especialidade da casa! Venha!',NULL),(17,'Três Queijos Boom','2013-11-04 09:32:45','Uma explosão de sabor: Mussarela, Catupiry e Parmesão!',NULL),(18,'Frango com Tudo','2013-11-04 09:33:11','Não é de vento! Tem com milho, ervilha, salsicha, ovo, presunto.. TUDO!',NULL),(19,'Vegê','2013-11-04 09:34:52','Legumes diversos, o melhor do vegetariano. Brinde sobremesa + suco!',NULL),(20,'Lacto','2013-11-04 09:35:49','Exploramos o melhor do leite nestes pratos. Brinde sobremesa + suco!',NULL),(21,'Prato Saúde','2013-11-04 09:38:34','Especial da casa em promoção especial. Brinde sobremesa + suco!',NULL);
/*!40000 ALTER TABLE `promocao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
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
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientelocalizacao`
--

DROP TABLE IF EXISTS `clientelocalizacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clientelocalizacao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `FK_ClienteLocalizacao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientelocalizacao`
--

LOCK TABLES `clientelocalizacao` WRITE;
/*!40000 ALTER TABLE `clientelocalizacao` DISABLE KEYS */;
INSERT INTO `clientelocalizacao` VALUES (4,'2013-11-04 09:07:24',-23.5708074,-46.7133609,4),(5,'2013-11-04 09:07:53',-23.5695036,-46.715528800000016,5),(7,'2013-11-04 10:30:07',-23.57356529325444,-46.710444688797,6),(8,'2013-11-04 10:30:58',-23.567940418699802,-46.70994579792023,3),(9,'2013-11-04 10:31:15',-23.570772553484,-46.7193067073822,2);
/*!40000 ALTER TABLE `clientelocalizacao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientepromocao`
--

DROP TABLE IF EXISTS `clientepromocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clientepromocao` (
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
  CONSTRAINT `FK_ClientePromocao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`Id`),
  CONSTRAINT `FK_ClientePromocao_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `promocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientepromocao`
--

LOCK TABLES `clientepromocao` WRITE;
/*!40000 ALTER TABLE `clientepromocao` DISABLE KEYS */;
INSERT INTO `clientepromocao` VALUES (3,'2013-11-04 00:00:00','2013-12-01 19:00:00','2013-11-04 09:14:22',1,2,3),(4,'2013-11-04 00:00:00','2013-12-26 00:00:00','2013-11-04 09:16:18',0,2,4),(5,'2013-11-04 00:00:00','2013-12-18 00:00:00','2013-11-04 09:17:22',1,2,5),(6,'2013-11-04 00:00:00','2013-12-31 00:00:00','2013-11-04 09:18:15',1,2,6),(7,'2013-11-04 00:00:00','2013-12-29 00:00:00','2013-11-04 09:18:51',1,2,7),(8,'2013-11-04 00:00:00','2013-12-18 00:00:00','2013-11-04 09:19:18',1,2,8),(9,'2013-11-04 00:00:00','2013-12-11 00:00:00','2013-11-04 09:21:08',1,3,9),(10,'2013-11-08 00:00:00','2013-12-15 00:00:00','2013-11-04 09:22:24',0,3,10),(11,'2013-11-05 00:00:00','2013-12-10 00:00:00','2013-11-04 09:23:34',1,3,11),(12,'2013-11-04 00:00:00','2013-12-28 00:00:00','2013-11-04 09:23:55',1,3,12),(13,'2013-11-04 00:00:00','2013-12-20 00:00:00','2013-11-04 09:26:41',1,4,13),(14,'2013-11-04 00:00:00','2013-12-25 00:00:00','2013-11-04 09:28:34',0,4,14),(15,'2013-11-04 00:00:00','2013-12-23 00:00:00','2013-11-04 09:29:32',1,4,15),(16,'2013-11-04 00:00:00','2013-12-23 00:00:00','2013-11-04 09:31:14',0,5,16),(17,'2013-11-04 00:00:00','2013-11-12 00:00:00','2013-11-04 09:32:45',1,5,17),(18,'2013-11-04 00:00:00','2013-12-19 00:00:00','2013-11-04 09:33:11',1,5,18),(19,'2013-11-04 00:00:00','2013-12-07 00:00:00','2013-11-04 09:34:52',1,6,19),(20,'2013-11-04 00:00:00','2013-12-09 00:00:00','2013-11-04 09:35:49',1,6,20),(21,'2013-11-04 00:00:00','2013-12-30 00:00:00','2013-11-04 09:38:34',0,6,21);
/*!40000 ALTER TABLE `clientepromocao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qualificacaopromocao`
--

DROP TABLE IF EXISTS `qualificacaopromocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `qualificacaopromocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Gostou` tinyint(1) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `IdClientePromocao` int(11) DEFAULT NULL,
  `IdUsuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdClientePromocao` (`IdClientePromocao`),
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `FK88673F6347ECA03D` FOREIGN KEY (`IdClientePromocao`) REFERENCES `clientepromocao` (`Id`),
  CONSTRAINT `FK_QualificacaoPromocao_Usuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qualificacaopromocao`
--

LOCK TABLES `qualificacaopromocao` WRITE;
/*!40000 ALTER TABLE `qualificacaopromocao` DISABLE KEYS */;
/*!40000 ALTER TABLE `qualificacaopromocao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promocaoacesso`
--

DROP TABLE IF EXISTS `promocaoacesso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `promocaoacesso` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `FK_PromocaoAcesso_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `promocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promocaoacesso`
--

LOCK TABLES `promocaoacesso` WRITE;
/*!40000 ALTER TABLE `promocaoacesso` DISABLE KEYS */;
INSERT INTO `promocaoacesso` VALUES (98,'2013-11-04 10:18:28',20),(99,'2013-11-04 10:19:55',3),(100,'2013-11-04 10:20:15',6),(101,'2013-11-04 10:20:28',17),(102,'2013-11-04 10:20:53',15),(103,'2013-11-04 10:27:32',3),(104,'2013-11-04 10:28:08',3),(105,'2013-11-04 10:28:46',9),(106,'2013-11-04 10:28:58',21);
/*!40000 ALTER TABLE `promocaoacesso` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (2,'Lotus Tecnologia','2013-11-04 10:31:15','24.422.333/0001-77','Dhouglas Lombello','contato@lotustg.com.br','(11) 9819-92080',NULL,'tBZ2QO1w0yZw9AWjrQg+Kg=='),(3,'Editora Boas Novas','2013-11-04 10:30:58','05.131.044/0001-74','João Novaes','joao@boasnovas.com','(11) 9999-99999',NULL,'tBZ2QO1w0yZw9AWjrQg+Kg=='),(4,'Restaurante Satisfação','2013-11-04 09:07:24','50.232.541/0001-13','Maria da Graça','maria@satisfacao.com','(11) 3328-9230',NULL,'tBZ2QO1w0yZw9AWjrQg+Kg=='),(5,'Pastelaria FazMaisUm','2013-11-04 09:07:53','07.723.755/0001-63','Honda Nakombi','nakombi@fazmaisum.com','(11) 2523-9870',NULL,'tBZ2QO1w0yZw9AWjrQg+Kg=='),(6,'Restaurante DaIndia','2013-11-04 10:30:07','27.848.652/0001-37','Jaad Hindu','jaad@daindia.com','(11) 3561-0987',NULL,'tBZ2QO1w0yZw9AWjrQg+Kg==');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-04 10:31:38
