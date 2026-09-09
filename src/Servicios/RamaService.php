<?php

class RamaService
{
    private RamaRepository $repositorio;

    public function __construct(RamaRepository $repositorio)
    {
        $this->repositorio = $repositorio;
    }

    public function listarRamas(): array
    {
        return $this->repositorio->listarTodo();
    }

    public function obtenerPerfilDesdeUrl($id): ?array
    {
        return $this->repositorio->obtenerPorId($id);
    }

    public function crearDesdeFormulario(array $post): void
    {
        $nombre = trim($post['nombre'] ?? '');
        $idGrupo = $post['id_grupo'] ?? null;
        $idRama = $post['id_rama'] ?? null;

        if ($nombre === '') {
            throw new Exception("El nombre de la rama es obligatorio.");
        }

        if (empty($idGrupo)) {
            throw new Exception("Debe seleccionar un grupo.");
        }

        if (empty($idRama)) {
            throw new Exception("Debe seleccionar una categoría de rama.");
        }

        $this->repositorio->crear([
            'nombre' => $nombre,
            'id_grupo' => $idGrupo,
            'id_rama' => $idRama
        ]);
    }
}
?>