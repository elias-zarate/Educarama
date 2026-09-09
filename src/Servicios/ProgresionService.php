<?php
// Ruta: src/Servicios/ProgresionService.php

require_once __DIR__ . '/../Datos/ProgresionRepository.php';

class ProgresionService {
    private ProgresionRepository $repo;

    public function __construct(ProgresionRepository $repo) {
        $this->repo = $repo;
    }

    public function registrarAvanceAccion(int $idProtagonista, int $idAccion, bool $estado): void {
        // El educador tilda o destilda un checkbox de una acción puntual
        $this->repo->marcarAccion($idProtagonista, $idAccion, $estado);
    }

    public function registrarAvanceCompetencia(int $idProtagonista, int $idCompetencia, int $codigoCompetencia, bool $estado): void {
        // El educador valida manualmente que la competencia principal se cumplió
        $this->repo->marcarCompetencia($idProtagonista, $idCompetencia, $codigoCompetencia, $estado);
    }

    public function asignarInsigniaManual(int $idProtagonista, int $idInsignia): void {
        // Se ejecuta cuando el educador hace clic en el botón habilitado de "Otorgar"
        $this->repo->otorgarInsignia($idProtagonista, $idInsignia);
    }
}
?>