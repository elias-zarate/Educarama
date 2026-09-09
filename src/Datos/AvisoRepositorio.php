<?php

class AvisoRepositorio
{
    private PDO $conexion;

    public function __construct(PDO $conexion)
    {
        $this->conexion = $conexion;
    }

    public function obtenerPorId(int $id): ?array
    {
        $stmt = $this->conexion->prepare("
            SELECT *
            FROM aviso
            WHERE id = :id
        ");

        $stmt->execute([':id' => $id]);
        $aviso = $stmt->fetch(PDO::FETCH_ASSOC);

        return $aviso ?: null;
    }

    public function listar(string $filtro = 'todos'): array
    {
        $sql = "SELECT * FROM aviso";

        if ($filtro === 'activos') {
            $sql .= " WHERE activo = 1";
        } elseif ($filtro === 'inactivos') {
            $sql .= " WHERE activo = 0";
        }

        $sql .= " ORDER BY activo DESC, fecha_inicio DESC";

        $stmt = $this->conexion->prepare($sql);
        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function crear(array $datos): void
    {
        $stmt = $this->conexion->prepare("
            INSERT INTO aviso 
            (titulo, descripcion, fecha_inicio, fecha_fin, prioridad, activo)
            VALUES 
            (:titulo, :descripcion, :fecha_inicio, :fecha_fin, :prioridad, 1)
        ");

        $stmt->execute([
            ':titulo' => $datos['titulo'],
            ':descripcion' => $datos['descripcion'],
            ':fecha_inicio' => $datos['fecha_inicio'],
            ':fecha_fin' => $datos['fecha_fin'],
            ':prioridad' => $datos['prioridad']
        ]);
    }

    public function actualizar(int $id, array $datos): void
    {
        $stmt = $this->conexion->prepare("
            UPDATE aviso
            SET titulo = :titulo,
                descripcion = :descripcion,
                fecha_inicio = :fecha_inicio,
                fecha_fin = :fecha_fin,
                prioridad = :prioridad
            WHERE id = :id
        ");

        $stmt->execute([
            ':titulo' => $datos['titulo'],
            ':descripcion' => $datos['descripcion'],
            ':fecha_inicio' => $datos['fecha_inicio'],
            ':fecha_fin' => $datos['fecha_fin'],
            ':prioridad' => $datos['prioridad'],
            ':id' => $id
        ]);
    }

    public function desactivar(int $id): void
    {
        $stmt = $this->conexion->prepare("
            UPDATE aviso 
            SET activo = 0 
            WHERE id = :id
        ");

        $stmt->execute([':id' => $id]);
    }

    public function reactivar(int $id): void
    {
        $stmt = $this->conexion->prepare("
            UPDATE aviso
            SET activo = 1 
            WHERE id = :id
        ");

        $stmt->execute([':id' => $id]);
    }

    public function listarParaHome(): array
    {
        $stmt = $this->conexion->prepare("
            SELECT *
            FROM aviso
            WHERE activo = 1
              AND fecha_inicio <= CURDATE()
              AND (fecha_fin IS NULL OR fecha_fin >= CURDATE())
            ORDER BY fecha_inicio DESC
            LIMIT 5
        ");

        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }
}