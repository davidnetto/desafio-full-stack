Desafio

Considere um sistema que tenha CRUD completo:
Locadoras, com os seguintes atributos: nome fantasia, razão social, CNPJ, e-mail, telefone, e uma locadora possui um endereço com os seguintes atributos: cep, rua, número, bairro, estado e cidade. O sistema não deve permitir a duplicidade de locadoras (considere o CNPJ como referência).
Montadoras, com: nome. Não permitir duplicidade no cadastro de montadora.
Modelo, com é composto por: nome e uma montadora. Não permitir o a duplicidade de modelo (considere o nome e montadora como referência).
Veículos possui: número de portas, modelo, cor, fabricante, ano modelo, ano fabricação, placa, chassi, data de criação. O sistema não deve permitir a duplicidade de veículos (considere a placa e o chassi como referência).
A locadora possui veículos em seu registro.
Um veículo pode pertencer a somente uma locadora, mas ao decorrer de sua vida, ele pode sair da locadora A e ir para locadora B por exemplo. 
Ter um log do período que um veículo pertenceu a determinada locadora.
Criar uma base de relatórios:
Locadoras X Veículos - Trazer nome da locadora (atual dona do veículo), modelo*, placa, data de cadastro. Fazer um filtro de pesquisa por: locadora, data de criação do veículo e por modelos (trazer um dropdown com todos os modelos registrados no sistema), se não preenchido o filtro, trazer todos os registros.
Locadoras X Modelos - Trazer o nome da locadora, modelos*, e a quantidade de veículos por modelo.
Log - Trazer o log completo de cada veículo com nome da locadora, modelo*, data início e data fim (se houver) de cada período.
*modelo considerar no nome do modelo e o nome da montadora.
Tecnologias permitidas:
Banco da dados: SQL Server.
Backend: C#. 
Frontend: Bootstrap, Angular.
*permitido o uso de framework da sua escolha.

Disponibilizar o código fonte e a criação de todas as tabelas no seu GitHub.
Enviar por e-mail, após conclusão do teste contendo o link do GitHub.
