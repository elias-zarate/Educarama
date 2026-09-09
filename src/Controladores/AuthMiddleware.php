<?php
// src/Controladores/AuthMiddleware.php

// Asegurarnos de que la sesión esté iniciada
if (session_status() === PHP_SESSION_NONE) {
    session_start();
}

// Verificar si NO existe el ID de usuario en la sesión
if (!isset($_SESSION['usuario_id'])) {
    header("Location: /Educarama/views/pages/login.php");
    exit; // El exit para detener la ejecución del script
}
?>