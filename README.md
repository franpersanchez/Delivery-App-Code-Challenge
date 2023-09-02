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

# Comentarios sobre la Solución:

La solución se presenta en un entorno dockerizado. Simplemente bastará con clonar el código en un repositorio local y tener instalado Docker en el ordenador.
Una vez en la carpeta principal del proyecto, bastará con correr en la linea de comandos:

```
docker-compose up
```

Accedidiendo a la url: http://localhost:8080/swagger/index.html tendrá acceso a la GUI de documentación de Swagger con la implementación de los requisitos del Nivel 1.

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
	Los Envios están sujetos a la existencia de un Pedido y de un Vehiculo que los transporte. No pueden contener Pedidos duplicados. Cuando un Pedido se entrega, desaparece del objeto Envio.
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
