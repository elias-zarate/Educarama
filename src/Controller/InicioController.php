<?php
// src/Controller/InicioController.php
declare(strict_types=1);

namespace Educarama\Controller;

use Educarama\Service\EventoService;
use Educarama\Service\SeccionService;
use Educarama\Service\SolicitudAltaService;
use Educarama\Service\UsuarioService;

final class InicioController extends Controlador
{
    public function __construct(
        UsuarioService $usuarioService,
        private readonly SeccionService $secciones,
        private readonly EventoService $eventos,
        private readonly SolicitudAltaService $solicitudes
    ) {
        parent::__construct($usuarioService);
    }

    public function paginaInicio(): Respuesta
    {
        $usuario = $this->usuario();
        $grupos = array_map(fn ($g) => [
            'grupo'    => $g,
            'proximos' => $this->eventos->proximosPorGrupo($usuario->id, (int) $g->grupo->id),
            'conteo'   => $this->secciones->conteoGrupo((int) $g->grupo->id),
        ], $this->secciones->misGrupos($usuario->id));
        
        return Respuesta::pagina([
            'usuario'   => $usuario,
            'grupos'    => $grupos,
            'solicitud' => $grupos === [] ? ($this->solicitudes->misSolicitudes($usuario->id)[0] ?? null) : null,
        ]);
    }
}