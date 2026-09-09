<?php
// Ruta: src/Controladores/ProgresionController.php

// 1. Iniciamos sesión y protegemos la ruta con el middleware que armaste antes
session_start();
require_once __DIR__ . '/AuthMiddleware.php';

// 2. Configuramos las cabeceras para responder JSON
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");

require_once __DIR__ . '/../Datos/ProgresionRepository.php';
require_once __DIR__ . '/../Servicios/ProgresionService.php';

// 3. Capturamos el JSON que enviará el Frontend
$data = json_decode(file_get_contents("php://input"), true);
$accionRequerida = $data['action'] ?? null;

try {
    $repo = new ProgresionRepository();
    $servicio = new ProgresionService($repo);

    // 4. Ruteamos según la acción solicitada
    switch ($accionRequerida) {
        case 'marcar_accion':
            $servicio->registrarAvanceAccion((int)$data['id_protagonista'], (int)$data['id_accion'], (bool)$data['estado']);
            echo json_encode(["status" => "success", "message" => "Acción registrada en la base de datos."]);
            break;

        case 'marcar_competencia':
            $servicio->registrarAvanceCompetencia((int)$data['id_protagonista'], (int)$data['id_competencia'], (int)$data['codigo_competencia'], (bool)$data['estado']);
            echo json_encode(["status" => "success", "message" => "Competencia actualizada."]);
            break;

        case 'otorgar_insignia':
            $servicio->asignarInsigniaManual((int)$data['id_protagonista'], (int)$data['id_insignia']);
            echo json_encode(["status" => "success", "message" => "¡Insignia otorgada y guardada con éxito!"]);
            break;

        default:
            http_response_code(400);
            echo json_encode(["status" => "error", "message" => "Acción no válida o no especificada."]);
    }
} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(["status" => "error", "message" => "Error del servidor: " . $e->getMessage()]);
}
?>