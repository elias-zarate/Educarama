# Documentación del MVP 1 - Educarama Backend

Este documento detalla el estado funcional, la arquitectura y los componentes desarrollados para el **MVP 1** del backend del sistema **Educarama**.

---

## 1. Arquitectura del Proyecto
El proyecto está construido bajo los principios de **Clean Architecture (Arquitectura Limpia)**, dividiendo la solución en capas bien definidas para garantizar el desacoplamiento y la escalabilidad:

*   **`Dominio`**: Contiene las entidades de negocio puras con sus reglas de validación encapsuladas y excepciones descriptivas.
*   **`Aplicacion`**: Contiene la lógica de negocio pura mediante **Casos de Uso**, **DTOs** (Data Transfer Objects), **Mappers** desacoplados y las interfaces de abstracción.
*   **`Datos`**: Contiene el contexto de Entity Framework Core (`EducaramaContext`) y los modelos generados por scaffolding para la persistencia en SQL Server.
*   **`Repositorio`**: Encargado de implementar la persistencia y la conexión entre la base de datos y la capa de aplicación.
*   **`ApiEducaramaBackend`**: Capa de presentación y exposición de los servicios (Endpoints / Controladores).

---

## 2. Funcionalidades Desarrolladas (Módulos del MVP 1)

### 📌 Gestión de Usuarios y Roles
*   Registro de nuevos usuarios en el sistema.
*   Asignación de roles, grupos y ramas institucionales a los usuarios.
*   Consulta de perfiles de usuario (por ID y listado general).
*   Modificación de perfiles y eliminación de usuarios.

### 📌 Gestión de Protagonistas (Scouts)
*   Alta (registro) de un protagonista vinculado a los datos de una persona.
*   Actualización de los datos del perfil del protagonista.
*   Eliminación de protagonistas del sistema.
*   Consulta de perfiles individuales y listados completos.
*   Listado de protagonistas filtrados por rama específica.
*   **Caso de uso de Cambio de Rama:** Permite actualizar la rama de un protagonista (por crecimiento de edad) y desasigna automáticamente su patrulla anterior (`IdPatrulla = null`) cumpliendo con la regla de negocio scout.

### 📌 Gestión de Ramas y Categorías
*   Creación y alta de nuevas ramas dentro de los grupos.
*   Consulta del catálogo de categorías de ramas.

### 📌 Gestión de Patrullas
*   Creación de patrullas vinculadas estrictamente a una Rama.
*   Asignación y modificación de la patrulla de un protagonista.
*   Listado de patrullas disponibles filtradas por rama (utilizado principalmente en las ramas que admiten patrullas).

### 📌 Módulo de Insignias y Progreso
*   Registro del otorgamiento u obtención de insignias a los protagonistas (`Obtiene`).
*   Registro del cumplimiento de las acciones del sendero de progreso (`Cumple`).

---

## 3. Manejo de Errores y Validaciones
*   **Dominio:** Las propiedades críticas de las entidades validan restricciones de negocio (como campos obligatorios, límites de longitud o rangos numéricos válidos) arrojando `ArgumentException` con mensajes descriptivos.
*   **Aplicación:** Se implementó validación defensiva en los mappers y casos de uso para los objetos entrantes (DTOs), utilizando `ArgumentNullException` con el uso correcto de `nameof` y mensajes descriptivos en español.
*   **Búsquedas:** Control de existencia mediante `KeyNotFoundException` en caso de intentar operar sobre registros inexistentes.

---

## 4. Próximos Pasos (Próxima Fase)
*   Implementación de las **Clases Repository** concretas utilizando el patrón de mapeo de modelos de base de datos a entidades de dominio.
*   Configuración definitiva de los Endpoints HTTP en la API para la exposición pública de los casos de uso.