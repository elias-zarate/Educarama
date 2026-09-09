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

    public function guardarTokenRecuperacion(string $email, string $token): void
    {
        $stmt = $this->conexion->prepare("
            UPDATE usuario 
            SET token_recuperacion = :token 
            WHERE email = :email
        ");
        $stmt->execute([
            ':token' => $token,
            ':email' => $email
        ]);
    }

    public function buscarPorEmailYToken(string $email, string $token): ?array
    {
        $stmt = $this->conexion->prepare("
            SELECT * 
            FROM usuario 
            WHERE email = :email AND token_recuperacion = :token 
            LIMIT 1
        ");
        $stmt->execute([
            ':email' => $email,
            ':token' => $token
        ]);
        $usuario = $stmt->fetch(PDO::FETCH_ASSOC);
        
        return $usuario ?: null;
    }

    public function actualizarPasswordYLimpiarToken(string $email, string $nuevaContraseniaHash): void
    {
        $stmt = $this->conexion->prepare("
            UPDATE usuario 
            SET contrasenia = :contrasenia, token_recuperacion = NULL 
            WHERE email = :email
        ");
        $stmt->execute([
            ':contrasenia' => $nuevaContraseniaHash,
            ':email' => $email
        ]);
    }
}