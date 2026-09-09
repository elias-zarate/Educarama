<?php

require_once __DIR__ . '/../Datos/Database.php';
require_once __DIR__ . '/../Datos/PersonaRepository.php';
require_once __DIR__ . '/../Datos/ProtagonistaRepository.php';
require_once __DIR__ . '/../Datos/RamaRepository.php';
require_once __DIR__ . '/../Datos/PatrullaRepository.php';
require_once __DIR__ . '/../Servicios/ProtagonistaService.php';

class ProtagonistaController
{
    private PDO $conexion;
    private ProtagonistaService $servicio;
    private RamaRepository $ramaRepository;
    private PatrullaRepository $patrullaRepository;

    public function __construct()
    {
        $this->conexion = Database::getConexion();

        $personaRepository = new PersonaRepository($this->conexion);
        $protagonistaRepository = new ProtagonistaRepository($this->conexion);

        $this->ramaRepository = new RamaRepository($this->conexion);
        $this->patrullaRepository = new PatrullaRepository($this->conexion);

        $this->servicio = new ProtagonistaService(
            $this->conexion,
            $personaRepository,
            $protagonistaRepository
        );
    }

   

    public function registrarSiEsPost(): void
    {
        $this->guardarSiEsPost();
    }

    public function obtenerPerfilPorId(int $id): ?array
    {
        return $this->servicio->obtenerPerfil($id);
    }

    public function obtenerPerfilDesdeUrl(): ?array
    {
        if (empty($_GET['id'])) {
            return null;
        }

        return $this->servicio->obtenerPerfil((int) $_GET['id']);
    }

    public function listarCards(): array
    {
        return $this->servicio->listarCards();
    }

    public function listarRamas(): array
    {
        return $this->ramaRepository->listarTodo();
    }

    public function listarPatrullas(): array
    {
        return $this->patrullaRepository->listarTodo();
    }

    public function guardarSiEsPost(): void
{
    if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
        return;
    }

    if (!empty($_POST['id'])) {
        $id = (int) $_POST['id'];

        $this->servicio->actualizarDesdeFormulario($id, $_POST);

        header("Location: perfil_protagonista.php?id=" . $id . "&actualizado=1");
        exit;
    }

    $this->servicio->registrarDesdeFormulario($_POST);

    header("Location: protagonistas.php");
    exit;
}
}