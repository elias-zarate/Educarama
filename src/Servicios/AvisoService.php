<?php

class AvisoServicio
{
    private AvisoRepositorio $repositorio;

    public function __construct(AvisoRepositorio $repositorio)
    {
        $this->repositorio = $repositorio;
    }

    public function listarAvisos(string $filtro): array
    {
        return $this->repositorio->listar($filtro);
    }

    public function buscarAvisoParaEditar(?int $id): ?array
    {
        if (!$id) {
            return null;
        }

        return $this->repositorio->obtenerPorId($id);
    }

    public function guardarDesdeFormulario(array $post): void
    {
        $datos = [
            'titulo' => trim($post['titulo']),
            'descripcion' => trim($post['descripcion']),
            'fecha_inicio' => $post['fecha_inicio'],
            'fecha_fin' => !empty($post['fecha_fin']) ? $post['fecha_fin'] : null,
            'prioridad' => $post['prioridad']
        ];

        if ($datos['titulo'] === '') {
            throw new Exception("El título es obligatorio.");
        }

        if ($datos['descripcion'] === '') {
            throw new Exception("La descripción es obligatoria.");
        }

        if (!empty($post['id'])) {
            $this->repositorio->actualizar((int)$post['id'], $datos);
        } else {
            $this->repositorio->crear($datos);
        }
    }

    public function ejecutarAccion(string $accion, int $id): void
    {
        if ($accion === 'desactivar') {
            $this->repositorio->desactivar($id);
        }

        if ($accion === 'reactivar') {
            $this->repositorio->reactivar($id);
        }
    }

    public function listarAvisosHome(): array
    {
        return $this->repositorio->listarParaHome();
    }
}