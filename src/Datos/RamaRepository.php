<?php

class RamaRepository
{
    private PDO $conexion;

    public function __construct(PDO $conexion)
    {
        $this->conexion = $conexion;
    }

    public function listarTodo(): array
    {
        $stmt = $this->conexion->prepare("
            SELECT 
                r.id,
                r.nombre,
                r.id_grupo,
                r.id_rama,
                g.nombre AS grupo,
                cr.nombre AS categoria
            FROM rama r
            LEFT JOIN grupo g ON r.id_grupo = g.id
            LEFT JOIN categoria_rama cr ON r.id_rama = cr.id
            ORDER BY r.nombre
        ");

        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function crear(array $datos): void
    {
        $stmt = $this->conexion->prepare("
            INSERT INTO rama
            (nombre, id_grupo, id_rama)
            VALUES
            (:nombre, :id_grupo, :id_rama)
        ");

        $stmt->execute([
            ':nombre' => $datos['nombre'],
            ':id_grupo' => $datos['id_grupo'],
            ':id_rama' => $datos['id_rama']
        ]);
    }
}

?>