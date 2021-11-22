--Cadastro de funcionários (0,3):
--o O cadastro deverá contar com os seguintes campos: Nome,
--Data de Nascimento, CPF, Endereço;
--o Não é permitido mais de um cadastro para o mesmo CPF;

CREATE TABLE Funcionarios (
	CPF VARCHAR(11) PRIMARY KEY,
	NOME VARCHAR(100) NOT NULL,
	DATA_NASCIMENTO DATE NOT NULL,
	ENDERECO VARCHAR(250) NOT NULL,
);

SELECT * FROM Funcionarios;

--• Cadastro de veículo (0,3):
--o O cadastro deverá contar com os seguintes campos: placa,
--marca, modelo, ano;
--o O sistema deverá validar se já existe um cadastro para a
--mesma placa;

CREATE TABLE Veiculos (
	PLACA VARCHAR(7) PRIMARY KEY,
	MARCA VARCHAR(50) NOT NULL,
	MODELO VARCHAR(100) NOT NULL,
	ANO INT NOT NULL
);

SELECT * FROM Veiculos;

--• Registro da Lavagem (0,3):
--o O registro será o controle das lavagens realizadas diariamente
--pelos funcionarios e deverá contar com as seguintes
--informações: placa do veículo, CPF do funcionário, data da
--lavagem, hora, tipo da lavagem (simples, completa), valor
--cobrado;
--o O sistema deverá validar se já existe alguma lavagem
--programada para o funcionário na data e hora informada;
--o O sistema deve listar os funcionarios disponiveis e veiculos
--disponiveis no momento do registro da lavagem;

CREATE TABLE TipoLavagem (
	CODIGO INT PRIMARY KEY,
	NOME_LAVAGEM VARCHAR(50) NOT NULL
);

CREATE TABLE Lavagens (
	CPF VARCHAR(11) NOT NULL,
	PLACA VARCHAR(7) NOT NULL,
	DATA_LAVAGEM DATETIME NOT NULL,
	COD_TIPO_LAVAGEM INT NOT NULL,
	VALOR DECIMAL(4, 2) NOT NULL
);

ALTER TABLE Lavagens
ADD CONSTRAINT PK_teste_mult 
PRIMARY KEY (CPF, PLACA, DATA_LAVAGEM)
GO

ALTER TABLE Lavagens
ADD CONSTRAINT CPF
FOREIGN KEY (CPF) REFERENCES Funcionarios(CPF);

ALTER TABLE Lavagens
ADD CONSTRAINT PLACA
FOREIGN KEY (PLACA) REFERENCES Veiculos(PLACA);

ALTER TABLE Lavagens
ADD CONSTRAINT COD_TIPO_LAVAGEM
FOREIGN KEY (COD_TIPO_LAVAGEM) REFERENCES TipoLavagem(CODIGO);

SELECT * FROM Lavagens;
