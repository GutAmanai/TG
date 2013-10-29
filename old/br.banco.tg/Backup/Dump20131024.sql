--
-- Table structure for table `Promocao`
--

DROP TABLE IF EXISTS `Promocao`;
CREATE TABLE `Promocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Descricao` text NOT NULL,
  `ImagemUrl` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `Usuario`;
CREATE TABLE `Usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Contato` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;

--
-- Table structure for table `Cliente`
--

DROP TABLE IF EXISTS `Cliente`;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;
--
-- Table structure for table `ClienteLocalizacao`
--

DROP TABLE IF EXISTS `ClienteLocalizacao`;
CREATE TABLE `ClienteLocalizacao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `FK_ClienteLocalizacao_Cliente` FOREIGN KEY (`IdCliente`) REFERENCES `Cliente` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;

--
-- Table structure for table `ClientePromocao`
--

DROP TABLE IF EXISTS `ClientePromocao`;
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;

--
-- Table structure for table `QualificacaoPromocao`
--

DROP TABLE IF EXISTS `QualificacaoPromocao`;
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
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;

--
-- Table structure for table `PromocaoAcesso`
--

DROP TABLE IF EXISTS `PromocaoAcesso`;
CREATE TABLE `PromocaoAcesso` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `FK_PromocaoAcesso_Promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `Promocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=latin1 COLLATE = latin1_swedish_ci;
