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

    public function obtenerProtagonistaEditar(): ?array
    {
        if (empty($_GET['editar'])) {
            return null;
        }

        return $this->servicio->obtenerPerfil((int) $_GET['editar']);
    }

    public function listarCards(): array
    {
        return $this->servicio->listarCards();
    }
     
    public function listarProtagonistas(): array
    {
        return $this->servicio->listarProtagonistas();
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

        // --- LÓGICA DE VALIDACIÓN Y SUBIDA DE DOCUMENTOS ---
        if (isset($_FILES['documento']) && $_FILES['documento']['error'] === UPLOAD_ERR_OK) {
            $fileTmpPath = $_FILES['documento']['tmp_name'];
            $fileName = $_FILES['documento']['name'];
            $fileSize = $_FILES['documento']['size'];
            $fileType = $_FILES['documento']['type'];
            
            $maxSize = 5 * 1024 * 1024; // 5MB en bytes
            $allowedTypes = ['application/pdf', 'image/jpeg', 'image/png'];

            // Validación de tamaño
            if ($fileSize > $maxSize) {
                throw new Exception("El archivo supera el tamaño máximo permitido de 5MB.");
            }

            // Validación de tipo
            if (!in_array($fileType, $allowedTypes)) {
                throw new Exception("Formato de archivo no permitido. Solo se aceptan PDF, JPG y PNG.");
            }

            // Definir directorio destino y crearlo si no existe
            $uploadDir = __DIR__ . '/../../public/uploads/documentos/';
            if (!is_dir($uploadDir)) {
                mkdir($uploadDir, 0755, true);
            }

            // Generar nombre único para evitar colisiones
            $newFileName = uniqid('doc_', true) . '_' . preg_replace('/[^a-zA-Z0-9.\-_]/', '', basename($fileName));
            $destPath = $uploadDir . $newFileName;

            // Mover archivo
            if (move_uploaded_file($fileTmpPath, $destPath)) {
                // Inyectamos los datos en el array $_POST para que el servicio los agarre en el próximo paso
                $_POST['ruta_documento'] = '/public/uploads/documentos/' . $newFileName;
                $_POST['nombre_original_documento'] = basename($fileName);
            } else {
                throw new Exception("Hubo un error al guardar el archivo en el servidor.");
            }
        }

        // --- GUARDADO DE DATOS (Alta o Edición) ---
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