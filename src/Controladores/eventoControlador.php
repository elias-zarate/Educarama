<?php

require_once __DIR__ . '/../Datos/Database.php';
require_once __DIR__ . '/../Datos/eventoRepositorio.php';
require_once __DIR__ . '/../Servicios/EventoService.php';

class EventoControlador
{
    private EventoServicio $servicio;

    public function __construct()
    {
        $conexion = Database::getConexion();

        $repositorio = new EventoRepositorio($conexion);

        $this->servicio = new EventoServicio($repositorio);
    }

    public function procesarAcciones(): void
    {
        if (isset($_GET['accion'], $_GET['id'])) {
            $this->servicio->ejecutarAccion($_GET['accion'], (int) $_GET['id']);

            header("Location: gestionar_eventos.php");
            exit;
        }

        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $this->servicio->guardarDesdeFormulario($_POST);

            header("Location: gestionar_eventos.php");
            exit;
        }
    }

    public function obtenerEventosParaEditar(): ?array
    {
        $idEditar = isset($_GET['editar']) ? (int) $_GET['editar'] : null;

        return $this->servicio->buscarEventoParaEditar($idEditar);
    }

    public function listarEventos(): array
    {
        $filtro = $_GET['filtro'] ?? 'todos';

        return $this->servicio->listarEventos($filtro);
    }

    public function obtenerFiltroActual(): string
    {
        return $_GET['filtro'] ?? 'todos';
    }

    public function listarEventosHome(): array
    {
        return $this->servicio->listarEventosHome();
    }
}