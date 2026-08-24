# Educarama ⚜️

> **Educación + Rama**: Una herramienta digital para el seguimiento del crecimiento y la progresión personal en el Movimiento Scout.

![Logo de Educarama](EducaramaFrontend/assets/images/Logo1.png)

## 📖 Sobre Projecto
Educarama nace como un proyecto universitario para la carrera de **Tecnicatura Superior en Desarrollo de Software**. Su objetivo es solucionar la fragmentación de la información en los grupos scouts, donde actualmente se depende de métodos manuales o planillas de Excel genéricas que dificultan el seguimiento pedagógico.

A diferencia de los sistemas oficiales centrados en lo administrativo, Educarama es una herramienta diseñada para el **trabajo de campo del educador**, enfocándose en la visualización clara y rápida del progreso de cada joven.

---

## 🚀 Funcionalidades Core (MVP 1)
En esta primera etapa, el sistema se centra en los requerimientos operativos clave para garantizar una gestión de punta a punta:

* **Gestión de Ramas y Categorías:** Administración de las ramas del movimiento scout y sus instancias operativas.
* **Gestión de Scouts:** Alta de beneficiarios con datos personales (DNI, edad) y seguimiento de su progresión.
* **Gestión de Patrullas:** Control y organización de equipos de trabajo asociados a cada rama.
* **Progresión Personal:** Módulo para la asignación y cumplimiento de competencias y acciones educativas.

---

## 🛠️ Stack Tecnológico
* **Backend:** C# / .NET 10 (ASP.NET Core Web API).
* **ORM:** Entity Framework Core.
* **Base de Datos:** SQL Server.
* **Documentación de API:** Scalar / OpenAPI.
* **Frontend:** HTML5, CSS3, JavaScript (Vanilla).
* **Framework CSS:** Bootstrap 5.
* **Herramientas de Desarrollo:** Visual Studio / Visual Studio Code, Live Server.

---

## 👥 Equipo de Trabajo
* **Elias Zarate**
* **Camila Cardozo**
* **Cristian Lopez**
* **Valentina Loza**
* **German Lucero**
---

## 📂 Estructura de la Solución

El proyecto está estructurado bajo una arquitectura limpia en capas (`Backend`) separada del cliente (`Frontend`):

```text
/EducaramaBackend
│
├── /ApiEducaramaBackend   # Controladores de la API y configuración de middlewares (CORS, Scalar)
├── /Aplicacion            # DTOs, Mappers y Casos de Uso (Lógica de negocio)
├── /Datos                 # DbContext, entidades de base de datos y migraciones de EF Core
├── /Dominio               # Entidades de dominio puro y validaciones de negocio
└── /Repositorio           # Implementación de los repositorios y acceso a datos

/EducaramaFrontend
│
├── /config.json           # Configuración centralizada de la URL base de la API
├── /styles.css            # Estilos globales y variables institucionales scout
├── /ramas                 # Vistas (HTML) y lógica (JS) para la gestión de ramas
├── /protagonistas         # Vistas (HTML) y lógica (JS) para la gestión de beneficiarios
└── /patrullas             # Vistas (HTML) y lógica (JS) para la gestión de patrullas
