<?php

require_once __DIR__ . '/../Datos/Database.php';
require_once __DIR__ . '/../Datos/AvisoRepositorio.php';
require_once __DIR__ . '/../Servicios/AvisoService.php';

class AvisoControlador
{
    private AvisoServicio $servicio;

    public function __construct()
    {
        $conexion = Database::getConexion();

        $repositorio = new AvisoRepositorio($conexion);

        $this->servicio = new AvisoServicio($repositorio);
    }

    public function procesarAcciones(): void
    {
        if (isset($_GET['accion'], $_GET['id'])) {
            $this->servicio->ejecutarAccion($_GET['accion'], (int) $_GET['id']);

            header("Location: gestionar_avisos.php");
            exit;
        }

        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $this->servicio->guardarDesdeFormulario($_POST);

            header("Location: gestionar_avisos.php");
            exit;
        }
    }

    public function obtenerAvisoEditar(): ?array
    {
        $idEditar = isset($_GET['editar']) ? (int) $_GET['editar'] : null;

        return $this->servicio->buscarAvisoParaEditar($idEditar);
    }

    public function listarAvisos(): array
    {
        $filtro = $_GET['filtro'] ?? 'todos';

        return $this->servicio->listarAvisos($filtro);
    }

    public function obtenerFiltroActual(): string
    {
        return $_GET['filtro'] ?? 'todos';
    }

    public function listarAvisosHome(): array
    {
        return $this->servicio->listarAvisosHome();
    }
}