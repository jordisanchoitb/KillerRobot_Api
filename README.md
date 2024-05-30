# Hunting Simulacrum Api
Esta API es responsable de actuar de intermediario entre las aplicaciones que consumen la API y la base de datos permitiendo revisar puntuaciones, subir puntuaciones, revisar si un login es correcto,
alterar la contraseña de un usuario y eliminar un usuario si el consumidor puede autentificar que la cuenta que va a eliminar le pertenece a el.
## Instrucciones de uso
Como se definio antes la API tiene metodos para operar con los datos de la base de datos y son los que las aplicaciones necesitan, y estan separados en 2 controladores, una para las puntuaciones y otra para los jugadores.
Todas las peticiones siempre devolveran un JSON conteniendo los siguientes datos:
```JSON
{
  "data": {

  },
  "isSuccess": true,
  "message": ""
}
```
- data: Si una peticion tiene que devolver algo lo devolvera en data.
- isSuccess: Si la peticion ha ido correctamente devolvera true
- message: Si ha ocurrido un problema normalmente se enviara un mensaje.
### Player Controller
Las peticiones que se pueden hacer con el jugador son para revisar sus puntuaciones relacionadas, revisar login y operar los datos de su cuenta con los siguientes metodos:
#### POST /CheckLogin:
 Este metodo sirve para autentificar los datos de autentificacion de las aplicaciones para eso hay que enviar en el POST un JSON con los siguientes datos: 
```JSON
{
  "name": "user",
  "password": "Pretend there is a password here"
}
```
Si la respuesta devuelve true en isSuccess significa que la informacion de validacion es correcto y nunca devolvera datos.
```JSON
{
  "data": null,
  "isSuccess": true,
  "message": ""
}
```
#### POST /ShowPlayerScores
Esta peticion devolvera un array de scores pertenecientes al usuario que se especifique en el post y para autentificar que el usuario es quien dice ser se tiene que proporcionar contraseña ademas de nombre  y hay que enviar la misma informacion que en CheckLogin:
```JSON
{
  "name": "user",
  "password": "Best password in the world"
}
```
Devolvera una respuesta en el que en data contendra un array de puntuaciones como en este ejemplo.
```JSON
{
  "data": [
    {
      "id": 15,
      "playerName": "user",
      "score": 20000,
      "completionTime": 1,
      "level": "string"
    },
    {
      "id": 16,
      "playerName": "user",
      "score": 20000,
      "completionTime": 100,
      "level": "string"
    },
    {
      "id": 17,
      "playerName": "user",
      "score": 20000,
      "completionTime": 50,
      "level": "string"
    }
  ],
  "isSuccess": true,
  "message": ""
}
```
#### POST /RegisterPlayer
Se proporciona un nombre y una contraseña y si el nombre no exite aun se creara correctamente o sino saltara un error.
En el cuerpo hay que proporcionar un JSON con el nombre y la contraseña del usuario:
```JSON
{
  "name": "string",
  "password": "string"
}
```
Devolvera este mensaje si todo ha ido bien:
```JSON
{
  "data": null,
  "isSuccess": true,
  "message": ""
}
```
#### PUT /ChangePassword
Se proporciona la informacion de autentificacion del usuario que se desea cambiar la contraseña y la nueva contraseña si la autentificacion es correcta se efectua el cambio de contraseña.
El cuerpo a proporcionar tiene que tener la siguiente estructura: 
```JSON
{
  "playerCheck": {
    "name": "user",
    "password": "WorstPassword"
  },
  "newPassword": "1234 PERFECT"
}
```
Si todo se ha efectuado bien devolvera la siguiete respuesta:
```JSON
{
  "data": null,
  "isSuccess": true,
  "message": ""
}
```
#### DELETE /DeleteUser
Se proporciona la informacion de autentificacion del usuario a eliminar y si la autentificacion es correcta se elimina y la estructura de la peticion tiene que ser esta:
```JSON
{
  "name": "user",
  "password": "Another Password"
}
```
Si la respuesta es correcta devuelve lo siguiente y el usuario no existira en la base de datos:
```JSON
{
  "data": null,
  "isSuccess": true,
  "message": ""
}
```
### Scores Controller
La API permite la lectura e insertacion de puntuaciones a traves de los siguientes metodos:
#### GET /GetScore/{id}
Obtendra la puntuacion con la id especificada en {id} y si la peticion es correcta devolvera algo similar lo siguiente en el cuerpo de la respuesta:
```JSON
{
  "data": {
    "id": 15,
    "playerName": "user",
    "score": 20000,
    "completionTime": 1,
    "level": "string"
  },
  "isSuccess": true,
  "message": ""
}
```
#### GET /GetScore
Obtendra todas las puntuaciones de la base de datos y si la peticion es correcta devolvera algo similar lo siguiente en el cuerpo de la respuesta:
```JSON
{
  "data": [
    {
      "id": 11,
      "playerName": "user",
      "score": 0,
      "completionTime": 0,
      "level": "string"
    },
    {
      "id": 12,
      "playerName": "Joan",
      "score": 0,
      "completionTime": 0,
      "level": "string"
    }
  ],
  "isSuccess": true,
  "message": ""
}
```
#### POST /PostScore
En el cuerpo de la peticion hay que proporcionar un JSON con la siguiente escructura:
```JSON
{
  "playerName": "user",
  "score": 0,
  "completionTime": 0,
  "level": "string"
}
```
Si todo es correcto la respuesta devolvera:
```JSON
{
  "data": null,
  "isSuccess": true,
  "message": "string"
}
```
## Tecnologias utilizadas
### Algoritmo de encriptacion Hash SHA-256
Para el hasheo de contraseñas se implementa una encriptacion de un solo sentido en el que deberia ser imposible desencriptarlo y esto es porque nadie excepto quien creo la contraseña
deberia conocer la contraseña utilizada y de esta menera la unica manera de averiguar la contraseña es proporcionar una contraseña que su hash sea similar al que esta en la base de datos
### Entity Framework Core
Un framework de microsoft que es para el mapeado de modelos a base de datos y en este caso con el gestor de base de datos de microsft (SQL server) en el que gracias a visual studio y Nuget se puede automatizar la creacion de las tablas de la base de datos proporcionando atributos a los datos de la clases de los modelos para especificar que caracteristicas tendran en la base de datos como, que dato es la clave foranea, que es la clave principal, etc...\
Una vez creado los modelos con sus atributos hay que ejecutar Add-migration (nameMigration) en la consola de Nugget, nameMigration puede ser cualquiera y creara las clases responsables de crear las tablas y mapeo de clases con la base de datos y para utilizarlos solo hay que volver a la consola de Nugget para ejecutar Update-Database.
### SQL server
El gestor de base de datos relacionales de microsoft donde la API almacena la informacion.
### REST API
Se utiliza una API de tipo REST en el que todas las peticiones que se pueden efectuar es a traves de GET,PUT,POST y DELETE
### CORS
Las aplicaciones webs no permiten aceptar peticiones de sitios que no posean CORS el cual es una abreviacion de "Intercambio de recursos de origen cruzado" una tecnologia que se encuentra en las cabezeras de las peticiones HTTP que indican directivas sobre que metodos y hosts estan permitidos en la peticion.\
Por eso la API tiene que añadir CORS en la cabezera de sus respuestas.
## Otros repositorios relacionados
[KillerRobot_Web](https://github.com/jordisanchoitb/KillerRobot_Web)  
[KillerRobot_Game](https://github.com/jordisanchoitb/KillerRobot_Game)

