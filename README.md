# Delivery-App-Code-Challenge

# Escenario

Una empresa de reparto de comida ha creado una aplicaci�n para sus
clientes y necesita poder rastrear en tiempo real donde se encuentra cada
pedido y el veh�culo/conductor que lo lleva.
Como ya disponen de varias aplicaciones, tanto de los conductores como
de los clientes, nos piden crear una API con la que puedan:

� A�adir nuevos pedidos

� Actualizar la posici�n del pedido (cualquier sistema de
coordenadas valdr�)

� Obtener la posici�n del pedido y el veh�culo mediante el n�mero de
pedido

- ## Nivel 1 � Modelado

Crear una API, asi como el modelo de datos, para insertar, actualizar y
obtener la ubicaci�n de los veh�culos.
Adem�s habr� que a�adir a la API la posibilidad de a�adir o borrar nuevos
pedidos a los veh�culos existentes.
Hay que tener en cuenta que el modelo de datos debe permitir actualizar
la ubicaci�n de cada veh�culo y guardar su historial de ubicaci�n.

- ## Nivel 2 - Comunicaci�n

En vez de obligar a las aplicaciones a realizar consultas o polling sobre la
posici�n de cada veh�culo/pedido, se debe a�adir alg�n metodo para
notificar a las partes interesadas en tiempo real.

- ## Nivel 3 � Escalabilidad

Llegado a este nivel preveemos que vamos a tener muchos veh�culos y
pedidos, hay que implementar elementos para asegurar la escalabilidad
de la aplicaci�n y optimizar las llamadas.

# Comentarios sobre la Soluci�n:

La soluci�n se presenta en un entorno dockerizado. Simplemente bastar� con clonar el c�digo en un repositorio local y tener instalado Docker en el ordenador.
Una vez en la carpeta principal del proyecto, bastar� con correr en la linea de comandos:

```
docker-compose up
```

Accedidiendo a la url: http://localhost:8080/swagger/index.html tendr� acceso a la GUI de documentaci�n de Swagger con la implementaci�n de los requisitos del Nivel 1.

Se han creado diferentes secciones (controllers) relacionados con cada l�gica:

- Clientes: permite crear o mostrar el total de clientes en la base de datos.
	- /clientes/crea -> Crea un nuevo cliente en la BD
- Envios
- Pedidos
- SeguimientoPedidos
- UbicacionPedidos
- Vehiculos
