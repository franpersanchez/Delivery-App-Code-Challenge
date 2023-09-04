# Delivery-App-Code-Challenge

# Escenario

Una empresa de reparto de comida ha creado una aplicación para sus
clientes y necesita poder rastrear en tiempo real donde se encuentra cada
pedido y el vehículo/conductor que lo lleva.
Como ya disponen de varias aplicaciones, tanto de los conductores como
de los clientes, nos piden crear una API con la que puedan:

• Añadir nuevos pedidos

• Actualizar la posición del pedido (cualquier sistema de
coordenadas valdrá)

• Obtener la posición del pedido y el vehículo mediante el número de
pedido

- ## Nivel 1 – Modelado

Crear una API, asi como el modelo de datos, para insertar, actualizar y
obtener la ubicación de los vehículos.
Además habrá que añadir a la API la posibilidad de añadir o borrar nuevos
pedidos a los vehículos existentes.
Hay que tener en cuenta que el modelo de datos debe permitir actualizar
la ubicación de cada vehículo y guardar su historial de ubicación.

- ## Nivel 2 - Comunicación

En vez de obligar a las aplicaciones a realizar consultas o polling sobre la
posición de cada vehículo/pedido, se debe añadir algún metodo para
notificar a las partes interesadas en tiempo real.

- ## Nivel 3 – Escalabilidad

Llegado a este nivel preveemos que vamos a tener muchos vehículos y
pedidos, hay que implementar elementos para asegurar la escalabilidad
de la aplicación y optimizar las llamadas.

# Comentarios sobre la Solución NIVEL 1:

La solución se presenta en un entorno dockerizado. Simplemente bastará con clonar el código en un repositorio local y tener instalado Docker en el ordenador.
Una vez en la carpeta principal del proyecto, bastará con correr en la linea de comandos:

```
docker-compose up
```

Accedidiendo a la url: http://localhost:8080/swagger/index.html tendrá acceso a la GUI de documentación de Swagger con la implementación de los requisitos del Nivel 1.

Se ha creado una solución que consta de 3 partes integradas:

	- **proyecto principal** (API)
	- Una base de datos(**BD**) en PostgresQL, que es una libreria de clases donde se encuentran los modelos, Interfaces y Repositorios(Servicios).
	- Proyecto de **Tests** que testean los controladores de la API. Se ha adjuntado un test completo del controlador ClientesController a modo de ejemplo de testing.
- Se ha optado por reducir la complejidad de los modelos.
- La complejidad total del modelo de datos se puede incrementar aumentando la cantidad de abstracciones y según consideraciones del propio modelo (hacer un objeto solo para parámetros de ubicación,...etc.).

Se han creado diferentes secciones (controllers) relacionados con cada lógica:

- ### Clientes
	- /clientes/crea -> Crea un nuevo cliente en la BD.
	- /clientes/muestra-todos -> Muestra todos los clientes de la BD.
- ### Pedidos
	Los Pedidos están sujetos a la existencia de un Cliente. Un Pedido contiene información referente al cliente, comentarios y la fecha-hora de su creación.
	- /pedidos/crea -> Crea un nuevo Pedido en la BD.
	- /pedidos/crea-rango -> Crea una serie de Pedidos a la vez.
	- /pedidos/muestra-todos -> Muestra todos los Envios de la BD.
	- /pedidos/{id}/actualiza-estado -> Actualiza el estado de un Pedido: pendiente(default), aceptado, pagado, enviado, entregado.
- ### Envios
	Los Envios están sujetos a la existencia de un Pedido y de un Vehiculo que los transporte. No pueden contener Pedidos duplicados. Cuando un Pedido se entrega, desaparece del objeto Envio. Pueden contener muchos Pedidos.
	- /envio/crea -> Crea un nuevo Envio en la BD.
	- /envio/muestra-todos -> Muestra todos los Envios de la BD.
	- /pedido/{pedido_id}/a-envio/{envio_id} ->asigna un Pedido a un Envio (puede albergar muchos Pedidos diferentes).
- ### SeguimientoPedidos
	- /seguimiento-pedido/{id} -> Permite conocer el historial de Ubicaciones (por fecha) de un Pedido en concreto.
- ### UbicacionVehiculos
	- /localizacion/vehiculo/actualiza-posicion -> Actualiza la posición de un vehiculo determinado en una fecha-hora.
	- /localizacion-historico/vehiculo/{id} -> permite conocer el historial de posiciones de un Vehiculo determinado, ordenado de mas reciente a más antigua.
	- /localizacion-actual/vehiculo/{id} -> permite conocer la posición mas reciente de un Vehiculo determinado.
- ### Vehiculos
	- /vehiculo/crea -> Crea un nuevo Vehiculo en la BD.
	- /vehiculo/muestra-todos -> Muestra todos los Vehiculos de la BD.

# Comentarios sobre la Solución NIVEL 2:
Para implementar la comunicación en tiempo real en la API, podemos utilizar tecnología como WebSockets junto con un sistema de mensajería o colas para notificar a las partes interesadas sobre la ubicación de los vehículos y los cambios en los pedidos. La aplicación, al estar en ASP.NET, sería buena idea aprovechar las ventajas de fácil integración que ofrece por otro lado SignalR, una biblioteca que simplifica la implementación de comunicación en tiempo real y proporciona abstracciones para WebSocket. Mediante SignalR, podremos establecer canales de comunicación bidireccional entre el servidor(nuestra API) y los clientes cuando estos se conectan a la API mediante WebSockets o SignalR.

En el momento que se produzca un cambio (una actualización de ubicación de un vehiculo o el estado de un pedido, podremos notificar este cambio a través del canal abierto hasta el cliente. En el lado del cliente, deberá estar implementado el manejo de eventos WebSocket o SignalR para procesar los mensajes entrantes y actualizar la información en tiempo real en la interfaz de usuario.

# Comentarios sobre la Solución NIVEL 3:
Para asegurar la escalabilidad y asegurar las llamdas a nuestra aplicación tendremos que pensar en formas que por un lado recorten las mediciones de latencia (mejora de consultas en la base de datos como indexado, etc) en las respuestas y también otras que aseguren la disponibilidad del servidor sin que ocurran cuellos de botella.
Para permitir la escalabilidad lo mejor sería afrontar una solución basada en arquitectura distribuida (varios servidores repartiendo la carga total).
También se puede hacer uso de event bus o distribuidores de cargas para distribuir las solicitudes de manera equitativa entre los servidores disponibles.
También podemos hacer uso de la Memoria Caché, para optimizar las llamadas y reducir la carga de la base de datos. Esto permitirá almacenar temporalmente datos frecuentemente accedidos, como la ubicación de los vehículos, para recuperarlos de manera más rápida.
