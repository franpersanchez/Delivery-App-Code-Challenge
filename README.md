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

- ### Clientes
	- /clientes/crea -> Crea un nuevo cliente en la BD.
	- /clientes/muestra-todos -> Muestra todos los clientes de la BD.
- ### Pedidos
	Los Pedidos est�n sujetos a la existencia de un Cliente. Un Pedido contiene informaci�n referente al cliente, comentarios y la fecha-hora de su creaci�n.
	- /pedidos/crea -> Crea un nuevo Pedido en la BD.
	- /pedidos/crea-rango -> Crea una serie de Pedidos a la vez.
	- /pedidos/muestra-todos -> Muestra todos los Envios de la BD.
	- /pedidos/{id}/actualiza-estado -> Actualiza el estado de un Pedido: pendiente(default), aceptado, pagado, enviado, entregado.
- ### Envios
	Los Envios est�n sujetos a la existencia de un Pedido y de un Vehiculo que los transporte. No pueden contener Pedidos duplicados. Cuando un Pedido se entrega, desaparece del objeto Envio.
	- /envio/crea -> Crea un nuevo Envio en la BD.
	- /envio/muestra-todos -> Muestra todos los Envios de la BD.
	- /pedido/{pedido_id}/a-envio/{envio_id} ->asigna un Pedido a un Envio (puede albergar muchos Pedidos diferentes).
- ### SeguimientoPedidos
	- /seguimiento-pedido/{id} -> Permite conocer el historial de Ubicaciones (por fecha) de un Pedido en concreto.
- ### UbicacionVehiculos
	- /localizacion/vehiculo/actualiza-posicion -> Actualiza la posici�n de un vehiculo determinado en una fecha-hora.
	- /localizacion-historico/vehiculo/{id} -> permite conocer el historial de posiciones de un Vehiculo determinado, ordenado de mas reciente a m�s antigua.
	- /localizacion-actual/vehiculo/{id} -> permite conocer la posici�n mas reciente de un Vehiculo determinado.
- ### Vehiculos
	- /vehiculo/crea -> Crea un nuevo Vehiculo en la BD.
	- /vehiculo/muestra-todos -> Muestra todos los Vehiculos de la BD.
