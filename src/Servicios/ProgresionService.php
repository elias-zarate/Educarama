<?php
// Ruta: src/Servicios/ProgresionService.php
require_once __DIR__ . '/../Datos/ProgresionRepository.php';
require_once __DIR__ . '/../Datos/InsigniaRepository.php';

class ProgresionService {
    private ProgresionRepository $repo;
    private InsigniaRepository $insigniaRepo;

    public function __construct(ProgresionRepository $repo) {
        $this->repo = $repo;
        // Instanciamos el repositorio de insignias para usarlo en la asignación
        $this->insigniaRepo = new InsigniaRepository();
    }

    public function registrarAvanceAccion(int $idProtagonista, int $idAccion, bool $estado): void {
        $this->repo->marcarAccion($idProtagonista, $idAccion, $estado);
    }

    public function registrarAvanceCompetencia(int $idProtagonista, int $idCompetencia, int $codigoCompetencia, bool $estado): void {
        $this->repo->marcarCompetencia($idProtagonista, $idCompetencia, $codigoCompetencia, $estado);
    }

    public function asignarInsigniaManual(int $idProtagonista, int $idInsignia): void {
        // Utilizamos InsigniaRepository para registrar la asignación en la tabla 'obtiene'
        $this->insigniaRepo->otorgarInsigniaAProtagonista($idProtagonista, $idInsignia);
    }

    // --- LÓGICA DE RECOMENDACIÓN AUTOMÁTICA ---
    public function evaluarRecomendacionInsignia(int $idProtagonista, int $idCategoriaRama): array {
        $totalCompetencias = $this->repo->contarCompetenciasPorCategoria($idCategoriaRama);
        
        if ($totalCompetencias === 0) {
            return [
                'porcentaje' => 0,
                'recomendacion_id' => null,
                'mensaje' => "No hay competencias configuradas para esta rama."
            ];
        }

        $desarrolladas = $this->repo->contarCompetenciasDesarrolladas($idProtagonista);
        $porcentaje = ($desarrolladas / $totalCompetencias) * 100;

        $insigniaRecomendadaId = null;
        $mensaje = "";

        if ($porcentaje >= 100) {
            $insigniaRecomendadaId = 4;
            $mensaje = "El protagonista ha completado el 100% de las competencias. ¡Se recomienda otorgar la Insignia de Etapa 4!";
        } elseif ($porcentaje >= 75) {
            $insigniaRecomendadaId = 3;
            $mensaje = "Avance superior al 75%. Cumple con los criterios para recibir la Insignia de Etapa 3.";
        } elseif ($porcentaje >= 50) {
            $insigniaRecomendadaId = 2;
            $mensaje = "Avance superior al 50%. Cumple con los criterios para recibir la Insignia de Etapa 2.";
        } elseif ($porcentaje >= 25) {
            $insigniaRecomendadaId = 1;
            $mensaje = "Avance superior al 25%. Cumple con los criterios para recibir la Insignia de Etapa 1.";
        } else {
            $mensaje = "El protagonista se encuentra en fase inicial. Aún no cumple con el 25% requerido para la primera insignia de etapa.";
        }

        return [
            'total' => $totalCompetencias,
            'desarrolladas' => $desarrolladas,
            'porcentaje' => round($porcentaje, 2),
            'recomendacion_id' => $insigniaRecomendadaId,
            'mensaje' => $mensaje
        ];
    }

    // --- NUEVA LÓGICA DE ALERTAS Y SUGERENCIAS ---
    public function obtenerAlertas(int $idProtagonista, int $idCategoriaRama): array {
        $alertas = [];
        
        // 1. Calculamos el progreso usando la lógica que ya armamos
        $evaluacion = $this->evaluarRecomendacionInsignia($idProtagonista, $idCategoriaRama);
        $recomendacionId = $evaluacion['recomendacion_id'];
        
        // 2. Si el sistema recomienda una insignia, verificamos si ya la tiene
        if ($recomendacionId !== null) {
            $yaLaTiene = $this->repo->tieneInsignia($idProtagonista, $recomendacionId);
            
            if (!$yaLaTiene) {
                // Hay un desfase: tiene el avance, pero no la insignia
                $alertas[] = [
                    'tipo' => 'sugerencia_insignia',
                    'titulo' => 'Insignia Pendiente',
                    'mensaje' => "El protagonista alcanzó el " . $evaluacion['porcentaje'] . "% de avance. Sugerimos otorgar la insignia de la etapa correspondiente.",
                    'insignia_sugerida' => $recomendacionId
                ];
            }
        }
        
        return $alertas;
    }
}
?>