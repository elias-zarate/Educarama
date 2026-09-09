<?php

class ProtagonistaRepository
{
    private PDO $conexion;

    public function __construct(PDO $conexion)
    {
        $this->conexion = $conexion;
    }

    public function crear(array $datos): void
    {
        $stmt = $this->conexion->prepare("
            INSERT INTO protagonista
            (promesa, anio_ingreso, id_persona, id_rama, id_patrulla, id_usuario)
            VALUES
            (:promesa, :anio_ingreso, :id_persona, :id_rama, :id_patrulla, NULL)
        ");

        $stmt->execute([
            ':promesa' => $datos['promesa'],
            ':anio_ingreso' => $datos['anio_ingreso'],
            ':id_persona' => $datos['id_persona'],
            ':id_rama' => $datos['id_rama'],
            ':id_patrulla' => $datos['id_patrulla']
        ]);
    }

    public function listarCards(): array
    {
        // Eliminamos p.anio_ingreso porque no existe en la tabla protagonista
        $stmt = $this->conexion->prepare("
            SELECT 
                p.id,
                p.promesa,
                per.nombre,
                per.apellido,
                per.fecha_nacimiento,
                r.nombre AS rama,
                pa.nombre AS patrulla
            FROM protagonista p
            INNER JOIN persona per ON p.id_persona = per.id
            LEFT JOIN rama r ON p.id_rama = r.id
            LEFT JOIN patrulla pa ON p.id_patrulla = pa.id
            ORDER BY per.apellido, per.nombre
        ");
        
        $stmt->execute();

        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function obtenerPerfilPorId(int $id): ?array
    {
        $stmt = $this->conexion->prepare("
            SELECT 
                p.id,
                p.promesa,
                p.anio_ingreso,
                p.id_persona,
                p.id_rama,
                p.id_patrulla,
                per.nombre,
                per.apellido,
                per.fecha_nacimiento,
                per.dni,
                r.nombre AS rama,
                pa.nombre AS patrulla
            FROM protagonista p
            INNER JOIN persona per ON p.id_persona = per.id
            LEFT JOIN rama r ON p.id_rama = r.id
            LEFT JOIN patrulla pa ON p.id_patrulla = pa.id
            WHERE p.id = :id
        ");

        $stmt->execute([':id' => $id]);

        $protagonista = $stmt->fetch(PDO::FETCH_ASSOC);

        return $protagonista ?: null;
    }

    public function actualizar(int $idProtagonista, array $datos): void
{
    $stmt = $this->conexion->prepare("
        UPDATE protagonista
        SET promesa = :promesa,
            anio_ingreso = :anio_ingreso,
            id_rama = :id_rama,
            id_patrulla = :id_patrulla
        WHERE id = :id
    ");

    $stmt->execute([
        ':promesa' => $datos['promesa'],
        ':anio_ingreso' => $datos['anio_ingreso'],
        ':id_rama' => $datos['id_rama'],
        ':id_patrulla' => $datos['id_patrulla'],
        ':id' => $idProtagonista
    ]);
}
}