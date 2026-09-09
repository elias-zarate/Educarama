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

    public function otorgarInsignia(int $idProtagonista, int $idInsignia): void {
        // En PHP sacamos la fecha actual formato MySQL (YYYY-MM-DD)
        $fecha = date('Y-m-d');
        
        $stmt = $this->db->prepare("
            INSERT INTO obtiene (id_protagonista, id_insignia, fecha)
            VALUES (?, ?, ?)
            ON DUPLICATE KEY UPDATE fecha = VALUES(fecha)
        ");
        $stmt->execute([$idProtagonista, $idInsignia, $fecha]);
    }
}
?>