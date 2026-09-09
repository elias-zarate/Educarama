<?php
require_once __DIR__ . '/Database.php';

class DocumentoRepository {
    private PDO $db;

    public function __construct() {
        $this->db = Database::getConexion();
    }

    public function guardarDocumento(int $idProtagonista, string $rutaArchivo, string $nombreOriginal): void {
        // Asumimos que la tabla se llama 'documento' y tiene estos campos básicos
        $stmt = $this->db->prepare("
            INSERT INTO documento (id_protagonista, ruta_archivo, nombre_original, fecha_subida)
            VALUES (?, ?, ?, NOW())
        ");
        $stmt->execute([$idProtagonista, $rutaArchivo, $nombreOriginal]);
    }

    public function obtenerDocumentosPorProtagonista(int $idProtagonista): array {
        $stmt = $this->db->prepare("
            SELECT * FROM documento 
            WHERE id_protagonista = ? 
            ORDER BY fecha_subida DESC
        ");
        $stmt->execute([$idProtagonista]);
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }
}
?>