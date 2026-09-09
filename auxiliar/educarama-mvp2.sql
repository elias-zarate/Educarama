-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 01-09-2026 a las 01:29:39
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `educarama`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `accion`
--

CREATE TABLE `accion` (
  `id` int(11) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `id_competencia` int(11) NOT NULL,
  `codigo_competencia` int(11) NOT NULL,
  `id_tipo_accion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asignacion_rol`
--

CREATE TABLE `asignacion_rol` (
  `id` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `id_rol` int(11) NOT NULL,
  `id_grupo` int(11) DEFAULT NULL,
  `id_rama` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aviso`
--

CREATE TABLE `aviso` (
  `id` int(11) NOT NULL,
  `titulo` varchar(200) NOT NULL,
  `descripcion` text NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date DEFAULT NULL,
  `prioridad` varchar(50) NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `aviso`
--

INSERT INTO `aviso` (`id`, `titulo`, `descripcion`, `fecha_inicio`, `fecha_fin`, `prioridad`, `activo`) VALUES
(1, 'Campa', 'Campamento Manada en Pencahuasi', '2026-09-12', '2026-09-13', 'media', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categoria_rama`
--

CREATE TABLE `categoria_rama` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `categoria_rama`
--

INSERT INTO `categoria_rama` (`id`, `nombre`) VALUES
(1, 'Manada'),
(2, 'Unidad'),
(3, 'Caminantes'),
(4, 'Rover');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `competencia`
--

CREATE TABLE `competencia` (
  `id` int(11) NOT NULL,
  `codigo` int(11) NOT NULL,
  `descripcion` varchar(300) DEFAULT NULL,
  `id_categoria_rama` int(11) NOT NULL,
  `id_sendero` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cumple`
--

CREATE TABLE `cumple` (
  `id_protagonista` int(11) NOT NULL,
  `id_accion` int(11) NOT NULL,
  `accion_cumplida` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desarrolla`
--

CREATE TABLE `desarrolla` (
  `id_protagonista` int(11) NOT NULL,
  `id_competencia` int(11) NOT NULL,
  `codigo_competencia` int(11) NOT NULL,
  `competencia_desarrollada` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `documento`
--

CREATE TABLE `documento` (
  `id` int(11) NOT NULL,
  `tipo_documento` varchar(25) DEFAULT NULL,
  `descripcion` text DEFAULT NULL,
  `id_protagonista` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `es_familia`
--

CREATE TABLE `es_familia` (
  `id_protagonista` int(11) NOT NULL,
  `id_familiar` int(11) NOT NULL,
  `vinculo` varchar(100) DEFAULT NULL,
  `es_contacto_de_emergencia` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `familiar`
--

CREATE TABLE `familiar` (
  `id` int(11) NOT NULL,
  `telefono_contacto` int(11) DEFAULT NULL,
  `email_contacto` varchar(200) DEFAULT NULL,
  `id_persona` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `grupo`
--

CREATE TABLE `grupo` (
  `id` int(11) NOT NULL,
  `codigo` varchar(50) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `grupo`
--

INSERT INTO `grupo` (`id`, `codigo`, `nombre`) VALUES
(1, '64', 'Posta de Pardo'),
(2, '414', 'San Calixto Martir'),
(3, '655', 'Nuestra Señora del Pilar');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `insignia`
--

CREATE TABLE `insignia` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `id_categoria_rama` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `obtiene`
--

CREATE TABLE `obtiene` (
  `id_protagonista` int(11) NOT NULL,
  `id_insignia` int(11) NOT NULL,
  `fecha` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `patrulla`
--

CREATE TABLE `patrulla` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `id_rama` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `patrulla`
--

INSERT INTO `patrulla` (`id`, `nombre`, `id_rama`) VALUES
(1, 'Orcas', 2),
(2, 'Aguilas', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `persona`
--

CREATE TABLE `persona` (
  `id` int(11) NOT NULL,
  `dni` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `apellido` varchar(100) DEFAULT NULL,
  `fecha_nacimiento` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `protagonista`
--

CREATE TABLE `protagonista` (
  `id` int(11) NOT NULL,
  `promesa` tinyint(1) DEFAULT NULL,
  `id_persona` int(11) NOT NULL,
  `id_rama` int(11) NOT NULL,
  `id_patrulla` int(11) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rama`
--

CREATE TABLE `rama` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `id_grupo` int(11) NOT NULL,
  `id_rama` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `rama`
--

INSERT INTO `rama` (`id`, `nombre`, `id_grupo`, `id_rama`) VALUES
(1, 'Pata Tierna', 1, 1),
(2, 'Malvinas Argentinas', 1, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rol`
--

CREATE TABLE `rol` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sendero`
--

CREATE TABLE `sendero` (
  `id` int(11) NOT NULL,
  `tipo` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_accion`
--

CREATE TABLE `tipo_accion` (
  `id` int(11) NOT NULL,
  `tipo` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `contrasenia` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id`, `nombre`, `email`, `contrasenia`) VALUES
(1, 'zacarias', 'noobmaster69@gmail.com', '$2y$10$TxpJsnAajc69lwxcy2jNMeGz6WdFLT9KA6dblVVJ5IWjpvQsJbsC.'),
(4, 'zacarias', 'eliaszaratebenito@gmail.com', '$2y$10$7NS5WI7g.w3Krn3yjNICc.6ZdfNCuAEVxDbsew4371r9bcHC7wZvu'),
(5, 'Elias Hernan Zarate Benito', 'hugozarate@hotmail.es', '$2y$10$jqyxX9sc/JHIPcnySfhiku6ne/Pd6jlsLas/M.Tdv9B7Do8tUhiHK'),
(6, 'valen', 'vale@gmail.com', '$2y$10$2vO27HB7DnvD.wIT7bsNzeaqNJ6gphFGcwg6bSI4peuKKLD783ahW'),
(7, 'profe', 'profe@gmail.com', '$2y$10$4ZR1Oa4npDtUU1NT4qi13.h5Iqjp79ZZUkL.GRoYn3kvM24WsrK/W'),
(8, 'ejemplo', 'ejemplo@gmail.com', '$2y$10$y95RgLJpgUISCjsr2y.eueagX9k3OIDZOrn8NZEBsyg1zvuLpxozK');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `accion`
--
ALTER TABLE `accion`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `asignacion_rol`
--
ALTER TABLE `asignacion_rol`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_usuario` (`id_usuario`),
  ADD KEY `id_rol` (`id_rol`),
  ADD KEY `id_grupo` (`id_grupo`),
  ADD KEY `id_rama` (`id_rama`);

--
-- Indices de la tabla `aviso`
--
ALTER TABLE `aviso`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `categoria_rama`
--
ALTER TABLE `categoria_rama`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `competencia`
--
ALTER TABLE `competencia`
  ADD PRIMARY KEY (`id`,`codigo`);

--
-- Indices de la tabla `cumple`
--
ALTER TABLE `cumple`
  ADD PRIMARY KEY (`id_protagonista`,`id_accion`);

--
-- Indices de la tabla `desarrolla`
--
ALTER TABLE `desarrolla`
  ADD PRIMARY KEY (`id_protagonista`,`id_competencia`,`codigo_competencia`);

--
-- Indices de la tabla `documento`
--
ALTER TABLE `documento`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `es_familia`
--
ALTER TABLE `es_familia`
  ADD PRIMARY KEY (`id_protagonista`,`id_familiar`);

--
-- Indices de la tabla `familiar`
--
ALTER TABLE `familiar`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `grupo`
--
ALTER TABLE `grupo`
  ADD PRIMARY KEY (`id`,`codigo`);

--
-- Indices de la tabla `insignia`
--
ALTER TABLE `insignia`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `patrulla`
--
ALTER TABLE `patrulla`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `persona`
--
ALTER TABLE `persona`
  ADD PRIMARY KEY (`id`,`dni`);

--
-- Indices de la tabla `protagonista`
--
ALTER TABLE `protagonista`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `rama`
--
ALTER TABLE `rama`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_grupo` (`id_grupo`),
  ADD KEY `id_rama` (`id_rama`);

--
-- Indices de la tabla `rol`
--
ALTER TABLE `rol`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `sendero`
--
ALTER TABLE `sendero`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `tipo_accion`
--
ALTER TABLE `tipo_accion`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `accion`
--
ALTER TABLE `accion`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `asignacion_rol`
--
ALTER TABLE `asignacion_rol`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `aviso`
--
ALTER TABLE `aviso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `categoria_rama`
--
ALTER TABLE `categoria_rama`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `competencia`
--
ALTER TABLE `competencia`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `documento`
--
ALTER TABLE `documento`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `familiar`
--
ALTER TABLE `familiar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `grupo`
--
ALTER TABLE `grupo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `insignia`
--
ALTER TABLE `insignia`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `patrulla`
--
ALTER TABLE `patrulla`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `persona`
--
ALTER TABLE `persona`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `protagonista`
--
ALTER TABLE `protagonista`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `rama`
--
ALTER TABLE `rama`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `rol`
--
ALTER TABLE `rol`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `sendero`
--
ALTER TABLE `sendero`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tipo_accion`
--
ALTER TABLE `tipo_accion`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `asignacion_rol`
--
ALTER TABLE `asignacion_rol`
  ADD CONSTRAINT `asignacion_rol_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `asignacion_rol_ibfk_2` FOREIGN KEY (`id_rol`) REFERENCES `rol` (`id`),
  ADD CONSTRAINT `asignacion_rol_ibfk_3` FOREIGN KEY (`id_grupo`) REFERENCES `grupo` (`id`),
  ADD CONSTRAINT `asignacion_rol_ibfk_4` FOREIGN KEY (`id_rama`) REFERENCES `rama` (`id`);

--
-- Filtros para la tabla `rama`
--
ALTER TABLE `rama`
  ADD CONSTRAINT `rama_ibfk_1` FOREIGN KEY (`id_grupo`) REFERENCES `grupo` (`id`),
  ADD CONSTRAINT `rama_ibfk_2` FOREIGN KEY (`id_rama`) REFERENCES `rama` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
