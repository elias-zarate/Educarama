<?php

require_once __DIR__ . '/../Datos/Database.php';
require_once __DIR__ . '/../Datos/RamaRepository.php';
require_once __DIR__ . '/../Servicios/RamaService.php';

class RamaController
{
    private PDO $conexion;
    private RamaService $servicio;

    public function __construct()
    {
        $this->conexion = Database::getConexion();

        $repositorio = new RamaRepository($this->conexion);
        $this->servicio = new RamaService($repositorio);
    }

    public function listar(): array
    {
        return $this->servicio->listarRamas();
    }

    public function crearSiEsPost(): void
    {
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $this->servicio->crearDesdeFormulario($_POST);

            header("Location: ramas.php");
            exit;
        }
    }

    public function listarGrupos(): array
    {
        $stmt = $this->conexion->prepare("
            SELECT id, nombre
            FROM grupo
            ORDER BY nombre
        ");
        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function listarCategorias(): array
    {
        $stmt = $this->conexion->prepare("
            SELECT id, nombre
            FROM categoria_rama
            ORDER BY id
        ");
        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }
}
?>
