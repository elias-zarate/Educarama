# 📑 Guía de Consumo de APIs y Documentación - Educarama (MVP 1)

Este documento detalla el estado funcional, la arquitectura y los endpoints disponibles del backend del sistema **Educarama** para el equipo de frontend.

---

## 1. Arquitectura del Proyecto
El proyecto está construido bajo los principios de **Clean Architecture (Arquitectura Limpia)**, dividiendo la solución en capas bien definidas para garantizar el desacoplamiento y la escalabilidad:
* **`Dominio`**: Contiene las entidades de negocio puras con sus reglas de validación encapsuladas y excepciones descriptivas.
* **`Aplicacion`**: Contiene la lógica de negocio pura mediante **Casos de Uso**, **DTOs** (Data Transfer Objects), **Mappers** desacoplados y las interfaces de abstracción.
* **`Datos`**: Contiene el contexto de Entity Framework Core (`EducaramaContext`) para la persistencia en SQL Server.
* **`Repositorio`**: Encargado de implementar la persistencia y la conexión entre la base de datos y la capa de aplicación.
* **`ApiEducaramaBackend`**: Capa de presentación y exposición de los servicios (Endpoints / Controladores).

---

## 2. Configuración General y Estándares
* **URL Base de la API:** `https://localhost:7232/api`
* **Formato de Comunicación:** JSON (`Content-Type: application/json`)
* **Manejo de Errores Estándar:** 
  * `400 Bad Request`: Errores de validación de dominio o reglas de negocio (`ArgumentException`).
  * `404 Not Found`: Recurso no encontrado en la base de datos (`KeyNotFoundException`).
  * `500 Internal Server Error`: Errores internos o inesperados del servidor.

---

## 3. Módulos y Funcionalidades del MVP 1

### 👥 Gestión de Usuarios y Roles
* Registro de nuevos usuarios en el sistema.
* Asignación de roles, grupos y ramas institucionales a los usuarios.
* Consulta de perfiles de usuario (por ID y listado general), modificación y eliminación.

### ⚜️ Gestión de Protagonistas (Scouts)
* Alta (registro) de un protagonista vinculado a los datos de una persona.
* Actualización de los datos del perfil del protagonista y eliminación.
* Consulta de perfiles individuales, listados completos y filtrados por rama específica.
* **Caso de uso de Cambio de Rama:** Permite actualizar la rama de un protagonista por crecimiento de edad y desasigna automáticamente su patrulla anterior (`IdPatrulla = null`) cumpliendo con la regla de negocio scout.

### 🏕️ Gestión de Ramas y Categorías
* Creación y alta de nuevas ramas dentro de los grupos.
* Consulta del catálogo de categorías de ramas.

### 🛡️ Gestión de Patrullas
* Creación de patrullas vinculadas estrictamente a una Rama.
* Asignación y modificación de la patrulla de un protagonista.
* Listado de patrullas disponibles filtradas por rama.

### 📈 Módulo de Insignias y Progreso
* Registro del otorgamiento u obtención de insignias a los protagonistas (`Obtiene`).
* Registro del cumplimiento de las acciones del sendero de progreso (`Cumple`).

---

## 4. Endpoints Disponibles

### 👥 Módulo de Usuarios (`/api/usuarios`)

#### Registrar Usuario
* **Ruta:** `POST /api/usuarios`
* **Body (JSON):**
  ```json
  {
    "email": "usuario@scout.com",
    "contrasenia": "Password123",
    "idRol": 1,
    "idGrupo": 1
  }
  ```
* **Respuestas Exitosas:** 
  * `201 Created`
* **Respuestas de Error:** 
  * `400 Bad Request`
  * `500 Internal Server Error`

#### Obtener Perfil de Usuario
* **Ruta:** `GET /api/usuarios/{id}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve `PerfilUsuarioDTO`)
* **Respuestas de Error:** 
  * `404 Not Found`

#### Actualizar Usuario
* **Ruta:** `PUT /api/usuarios/{id}`
* **Nota:** El `id` de la ruta debe coincidir con el identificador del cuerpo.
* **Body (JSON):**
  ```json
  {
    "id": 1,
    "email": "nuevo_correo@scout.com"
  }
  ```
* **Respuestas Exitosas:** 
  * `200 OK`
* **Respuestas de Error:** 
  * `400 Bad Request`
  * `404 Not Found`
  * `500 Internal Server Error`

#### Eliminar Usuario
* **Ruta:** `DELETE /api/usuarios/{id}`
* **Respuestas Exitosas:** 
  * `200 OK`
* **Respuestas de Error:** 
  * `404 Not Found`
  * `500 Internal Server Error`

---

### 🏕️ Módulo de Ramas (`/api/ramas`)

#### Crear Rama
* **Ruta:** `POST /api/ramas`
* **Body (JSON):**
  ```json
  {
    "nombre": "Unidades Scouts",
    "idGrupo": 1,
    "idCategoriaRama": 2
  }
  ```
* **Respuestas Exitosas:** 
  * `201 Created`
* **Respuestas de Error:** 
  * `400 Bad Request`
  * `500 Internal Server Error`

#### Obtener Todas las Ramas
* **Ruta:** `GET /api/ramas`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve una lista de `RamaDTO`)

#### Obtener Rama por ID
* **Ruta:** `GET /api/ramas/{id}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve `RamaDTO`)
* **Respuestas de Error:** 
  * `404 Not Found`

---

### 🛡️ Módulo de Patrullas (`/api/patrullas`)

#### Obtener Todas las Patrullas
* **Ruta:** `GET /api/patrullas`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve una lista de `PatrullaDTO`)

#### Obtener Patrulla por ID
* **Ruta:** `GET /api/patrullas/{id}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve `PatrullaDTO`)
* **Respuestas de Error:** 
  * `404 Not Found`

#### Obtener Patrullas por Rama
* **Ruta:** `GET /api/patrullas/por-rama/{idRama}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve las patrullas filtradas por el ID de la rama)

---

### ⚜️ Módulo de Protagonistas (`/api/protagonistas`)

#### Registrar Protagonista (Scout)
* **Ruta:** `POST /api/protagonistas`
* **Body (JSON):**
  ```json
  {
    "dni": 40123456,
    "nombre": "Juan",
    "apellido": "Pérez",
    "fechaNacimiento": "2010-05-15",
    "promesa": false,
    "idRama": 2,
    "idPatrulla": 1,
    "idUsuario": 1
  }
  ```
* **Respuestas Exitosas:** 
  * `201 Created`
* **Respuestas de Error:** 
  * `400 Bad Request` (Ej: Fechas futuras o DNI inválido)

#### Obtener Todos los Protagonistas
* **Ruta:** `GET /api/protagonistas`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve lista de `PerfilProtagonistaDTO`)

#### Obtener Protagonista por ID
* **Ruta:** `GET /api/protagonistas/{id}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve `PerfilProtagonistaDTO`)
* **Respuestas de Error:** 
  * `404 Not Found`

#### Obtener Protagonistas por Rama
* **Ruta:** `GET /api/protagonistas/por-rama/{idRama}`
* **Respuestas Exitosas:** 
  * `200 OK` (Devuelve listado de protagonistas filtrados por su rama)

#### Actualizar Perfil de Protagonista
* **Ruta:** `PUT /api/protagonistas/{id}`
* **Respuestas Exitosas:** 
  * `200 OK`

#### Eliminar Protagonista
* **Ruta:** `DELETE /api/protagonistas/{id}`
* **Respuestas Exitosas:** 
  * `200 OK`

#### Asignar Patrulla a Protagonista
* **Ruta:** `PATCH /api/protagonistas/{id}/patrulla`
* **Body (JSON):**
  ```json
  {
    "idPatrulla": 2
  }
  ```
* **Respuestas Exitosas:** 
  * `200 OK`

#### Cambiar de Rama a Protagonista
* **Ruta:** `PATCH /api/protagonistas/{id}/rama`
* **Nota:** Al cambiar de rama, desasigna automáticamente la patrulla anterior.
* **Body (JSON):**
  ```json
  {
    "idRama": 3
  }
  ```
* **Respuestas Exitosas:** 
  * `200 OK`

---

### 📈 Módulo de Progreso e Insignias

#### Registrar Cumplimiento de Acción (`/api/cumplimientos`)
* **Ruta:** `POST /api/cumplimientos`
* **Body (JSON):**
  ```json
  {
    "idProtagonista": 1,
    "idAccion": 5,
    "accionCumplida": true
  }
  ```
* **Respuestas Exitosas:** 
  * `201 Created`

#### Otorgar Insignia (`/api/obtenciones`)
* **Ruta:** `POST /api/obtenciones`
* **Body (JSON):**
  ```json
  {
    "idProtagonista": 1,
    "idInsignia": 2,
    "fecha": "2026-06-01"
  }
  ```
* **Respuestas Exitosas:** 
  * `201 Created`