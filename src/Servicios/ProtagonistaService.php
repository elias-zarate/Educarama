<?php

class ProtagonistaService
{
    private PDO $conexion;
    private PersonaRepository $personaRepository;
    private ProtagonistaRepository $protagonistaRepository;

    public function __construct(
        PDO $conexion,
        PersonaRepository $personaRepository,
        ProtagonistaRepository $protagonistaRepository
    ) {
        $this->conexion = $conexion;
        $this->personaRepository = $personaRepository;
        $this->protagonistaRepository = $protagonistaRepository;
    }

    public function registrarDesdeFormulario(array $post): void
    {
        $datos = $this->prepararDatosFormulario($post);

        try {
            $this->conexion->beginTransaction();

            $idPersona = $this->personaRepository->crear([
                'apellido' => $datos['apellido'],
                'nombre' => $datos['nombre'],
                'fecha_nacimiento' => $datos['fecha_nacimiento'],
                'dni' => $datos['dni']
            ]);

            $this->protagonistaRepository->crear([
                'promesa' => $datos['promesa'],
                'anio_ingreso' => $datos['anio_ingreso'],
                'id_persona' => $idPersona,
                'id_rama' => $datos['id_rama'],
                'id_patrulla' => $datos['id_patrulla']
            ]);

            $this->conexion->commit();

        } catch (Exception $e) {
            if ($this->conexion->inTransaction()) {
                $this->conexion->rollBack();
            }

            throw $e;
        }
    }

    public function actualizarDesdeFormulario(int $idProtagonista, array $post): void
    {
        $protagonista = $this->protagonistaRepository->obtenerPerfilPorId($idProtagonista);

        if (!$protagonista) {
            throw new Exception("El protagonista no existe.");
        }

        $datos = $this->prepararDatosFormulario($post);

        try {
            $this->conexion->beginTransaction();

            $this->personaRepository->actualizar((int)$protagonista['id_persona'], [
                'apellido' => $datos['apellido'],
                'nombre' => $datos['nombre'],
                'fecha_nacimiento' => $datos['fecha_nacimiento'],
                'dni' => $datos['dni']
            ]);

            $this->protagonistaRepository->actualizar($idProtagonista, [
                'promesa' => $datos['promesa'],
                'anio_ingreso' => $datos['anio_ingreso'],
                'id_rama' => $datos['id_rama'],
                'id_patrulla' => $datos['id_patrulla']
            ]);

            $this->conexion->commit();

        } catch (Throwable $e)  {
            if ($this->conexion->inTransaction()) {
                $this->conexion->rollBack();
            }

            throw $e;
        }
    }

    public function listarCards(): array
    {
        return $this->protagonistaRepository->listarCards();
    }

    public function obtenerPerfil(int $id): ?array
    {
        return $this->protagonistaRepository->obtenerPerfilPorId($id);
    }

    private function prepararDatosFormulario(array $post): array
    {
        $apellido = trim($post['apellido'] ?? '');
        $nombre = trim($post['nombre'] ?? '');
        $fechaNacimiento = $post['fecha_nacimiento'] ?? '';
        $dni = trim($post['dni'] ?? '');
        $anioIngreso = !empty($post['anio_ingreso']) ? $post['anio_ingreso'] : null;
        $idRama = $post['id_rama'] ?? null;
        $idPatrulla = !empty($post['id_patrulla']) ? $post['id_patrulla'] : null;
        $promesa = isset($post['promesa']) ? 1 : 0;

        if ($apellido === '') {
            throw new Exception("El apellido es obligatorio.");
        }

        if ($nombre === '') {
            throw new Exception("El nombre es obligatorio.");
        }

        if ($fechaNacimiento === '') {
            throw new Exception("La fecha de nacimiento es obligatoria.");
        }

        if ($dni === '') {
            throw new Exception("El DNI es obligatorio.");
        }

        if (empty($idRama)) {
            throw new Exception("Debe seleccionar una rama.");
        }

        return [
            'apellido' => $apellido,
            'nombre' => $nombre,
            'fecha_nacimiento' => $fechaNacimiento,
            'dni' => $dni,
            'anio_ingreso' => $anioIngreso,
            'id_rama' => $idRama,
            'id_patrulla' => $idPatrulla,
            'promesa' => $promesa
        ];
    }
}