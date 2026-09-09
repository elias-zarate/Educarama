<?php

class UsuarioRepository
{
    private PDO $conexion;

    public function __construct(PDO $conexion)
    {
        $this->conexion = $conexion;
    }

    public function buscarPorEmail(string $email): ?array
    {
        $stmt = $this->conexion->prepare("
            SELECT *
            FROM usuario
            WHERE email = :email
            LIMIT 1
        ");

        $stmt->execute([
            ':email' => $email
        ]);

        $usuario = $stmt->fetch(PDO::FETCH_ASSOC);

        return $usuario ?: null;
    }

    public function crear(array $datos): void
{
    $stmt = $this->conexion->prepare("
        INSERT INTO usuario
        (nombre, email, contrasenia)
        VALUES
        (:nombre, :email, :contrasenia)
    ");

    $stmt->execute([
        ':nombre' => $datos['nombre'],
        ':email' => $datos['email'],
        ':contrasenia' => $datos['password']
    ]);
}
}