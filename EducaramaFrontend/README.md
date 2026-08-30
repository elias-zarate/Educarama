# Educarama ⚜️

> **Educación + Rama**: Una herramienta digital para el seguimiento del crecimiento y la progresión personal en el Movimiento Scout.

![Logo de Educarama](views/assets/images/Logo1.png)

## 📖 Sobre el Proyecto
Educarama nace como un proyecto universitario para la carrera de **Desarrollo de Software** en el Instituto Leopoldo Marechal. Su objetivo es solucionar la fragmentación de la información en los grupos scouts, donde actualmente se depende de métodos manuales o planillas de Excel genéricas que dificultan el seguimiento pedagógico.

A diferencia de los sistemas oficiales centrados en lo administrativo, Educarama es una herramienta diseñada para el **trabajo de campo del educador**, enfocándose en la visualización clara y rápida del progreso de cada joven.

---

## 🚀 Funcionalidades Core
* **Gestión de Educadores:** Registro de perfiles, autenticación segura con manejo de sesiones y control de acceso mediante middlewares.
* **Gestión de Protagonistas:** Alta, edición y visualización de beneficiarios con datos personales, rama, patrulla y gestión de vínculos familiares.
* **Progresión Personal:** Módulo para la asignación de competencias, seguimiento de acciones y control manual de entrega de insignias.
* **Panel Principal y Avisos:** Home interactivo con estadísticas, próximos eventos, cumpleaños y gestión de avisos importantes.

---

## 🛠️ Stack Tecnológico
* **Backend:** PHP (Programación Orientada a Objetos, Patrón MVC y Clean Architecture).
* **Base de Datos:** MySQL (Gestión mediante PDO y consultas preparadas).
* **Frontend:** HTML5, CSS3, JavaScript.
* **Framework CSS:** Bootstrap 5 y Bootstrap Icons.
* **Entorno de Desarrollo:** XAMPP (Localhost).

---

## 👥 Equipo de Trabajo
* **Elías Zarate**
* **Camila Cardozo**
* **Cristian López**
* **Valentina Loza**

---

## 📁 Estructura de Directorios
```text
Educarama/
├── public/                  # Punto de entrada y recursos estáticos públicos
│   ├── css/                 # Estilos personalizados (styles.css)
│   ├── js/                  # Scripts de JavaScript (app.js)
│   └── index.php            # Home principal protegido
├── src/                     # Lógica del Backend (MVC / Clean Architecture)
│   ├── Controladores/       # Controladores HTTP y Middlewares de seguridad
│   ├── Datos/               # Repositorios y conexión a Base de Datos (PDO)
│   ├── Model/               # Entidades y modelos de datos
│   ├── Repositorio/         # Interfaces genéricas de persistencia
│   └── Servicios/           # Servicios de lógica de negocio
└── views/                   # Vistas y componentes visuales
    ├── assets/              # Imágenes, logos e insignias oficiales
    └── pages/               # Vistas principales (protagonistas, ramas, patrullas, login)
