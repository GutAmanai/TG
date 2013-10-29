--
-- Table structure for table `Promocao`
--

DROP TABLE IF EXISTS `promocao`;
CREATE TABLE `promocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Descricao` text NOT NULL,
  `ImagemUrl` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Contato` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;

--
-- Table structure for table `Cliente`
--

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
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;
--
-- Table structure for table `ClienteLocalizacao`
--

DROP TABLE IF EXISTS `clienteLocalizacao`;
CREATE TABLE `clienteLocalizacao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `IdCliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `fk_clienteLocalizacao_cliente` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;

--
-- Table structure for table `ClientePromocao`
--

DROP TABLE IF EXISTS `clientePromocao`;
CREATE TABLE `clientePromocao` (
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
  CONSTRAINT `fk_clientePromocao_cliente` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`Id`),
  CONSTRAINT `fk_clientePromocao_promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `promocao` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;

--
-- Table structure for table `QualificacaoPromocao`
--

DROP TABLE IF EXISTS `qualificacaoPromocao`;
CREATE TABLE `qualificacaoPromocao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Gostou` tinyint(1) NOT NULL,
  `DataEntrada` datetime NOT NULL,
  `IdClientePromocao` int(11) DEFAULT NULL,
  `IdUsuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdClientePromocao` (`IdClientePromocao`),
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `fk_qualificacaoPromocao_clientePromocao` FOREIGN KEY (`IdClientePromocao`) REFERENCES `clientePromocao` (`Id`),
  CONSTRAINT `fk_qualificacaoPromocao_usuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;

--
-- Table structure for table `PromocaoAcesso`
--

DROP TABLE IF EXISTS `promocaoAcesso`;
CREATE TABLE `promocaoAcesso` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime NOT NULL,
  `IdPromocao` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPromocao` (`IdPromocao`),
  CONSTRAINT `fk_promocaoAcesso_promocao` FOREIGN KEY (`IdPromocao`) REFERENCES `promocao` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE = utf8_general_ci;