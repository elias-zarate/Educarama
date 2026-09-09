<?php
require_once __DIR__ . '/Database.php';

class ProgresionRepository {
    private PDO $db;

    public function __construct() { 
        $this->db = Database::getConexion(); 
    }

    public function marcarAccion(int $idProtagonista, int $idAccion, bool $cumplida): void {
        $stmt = $this->db->prepare("
            INSERT INTO cumple (id_protagonista, id_accion, accion_cumplida)
            VALUES (?, ?, ?)
            ON DUPLICATE KEY UPDATE accion_cumplida = VALUES(accion_cumplida)
        ");
        $stmt->execute([$idProtagonista, $idAccion, (int)$cumplida]);
    }

    public function marcarCompetencia(int $idProtagonista, int $idCompetencia, int $codigoCompetencia, bool $desarrollada): void {
        $stmt = $this->db->prepare("
            INSERT INTO desarrolla (id_protagonista, id_competencia, codigo_competencia, competencia_desarrollada)
            VALUES (?, ?, ?, ?)
            ON DUPLICATE KEY UPDATE competencia_desarrollada = VALUES(competencia_desarrollada)
        ");
        $stmt->execute([$idProtagonista, $idCompetencia, $codigoCompetencia, (int)$desarrollada]);
    }

    public function contarCompetenciasPorCategoria(int $idCategoriaRama): int {
        $stmt = $this->db->prepare("SELECT COUNT(*) FROM competencia WHERE id_categoria_rama = ?");
        $stmt->execute([$idCategoriaRama]);
        return (int) $stmt->fetchColumn();
    }

    public function contarCompetenciasDesarrolladas(int $idProtagonista): int {
        $stmt = $this->db->prepare("SELECT COUNT(*) FROM desarrolla WHERE id_protagonista = ? AND competencia_desarrollada = 1");
        $stmt->execute([$idProtagonista]);
        return (int) $stmt->fetchColumn();
    }

    // --- NUEVA CONSULTA PARA ALERTAS ---
    public function tieneInsignia(int $idProtagonista, int $idInsignia): bool {
        $stmt = $this->db->prepare("SELECT COUNT(*) FROM obtiene WHERE id_protagonista = ? AND id_insignia = ?");
        $stmt->execute([$idProtagonista, $idInsignia]);
        return (bool) $stmt->fetchColumn();
    }
}
?>