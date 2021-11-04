## Animals API - A API de API dos animais


Esta aplicação é um grande WIP para aprendermos um pouco mais sobre APIs. 

Muito está sendo construindo, ainda. Portanto se você achar um bug, fique a vontade para abrir um MR.

Este projeto **ainda** usa componentes do .NET Core 3.1.

### Como executar

Muito simples: 

1 - Clone o repo;

2 - `dotnet run`; 

3 - Na pasta `postman`, temos as collections para testes.


### Comandos que você talvez precise rodar

```
 dotnet new webapi -o AnimalsApi
 dotnet add package Microsoft.AspNetCore.Authentication -v 3.1.20
 dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer -v 3.1.20
```

### Notas importantes

A `x-api-key` para a The Dog API é de exemplo. Você precisará de uma key válida para prosseguir.
