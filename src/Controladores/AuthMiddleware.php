<?php
// src/Controladores/AuthMiddleware.php

// Asegurarnos de que la sesión esté iniciada
if (session_status() === PHP_SESSION_NONE) {
    session_start();
}

// Verificar si NO existe el ID de usuario en la sesión
if (!isset($_SESSION['usuario_id'])) {
    // Si no está logueado, lo pateamos a la pantalla de login
    // Ajustá la ruta según dónde incluyas este archivo
    header("Location: /Educarama/views/pages/login.php");
    exit; // El exit es crucial en PHP para detener la ejecución del script
}
?>