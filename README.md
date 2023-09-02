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

- Clientes: permite crear o mostrar el total de clientes en la base de datos.
	- /clientes/crea -> Crea un nuevo cliente en la BD
- Envios
- Pedidos
- SeguimientoPedidos
- UbicacionPedidos
- Vehiculos
