<?php

class LoginService
{
    private UsuarioRepository $usuarioRepository;

    public function __construct(UsuarioRepository $usuarioRepository)
    {
        $this->usuarioRepository = $usuarioRepository;
    }

    public function login(array $datos): array
    {
        $email = trim($datos['email'] ?? '');
        $password = trim($datos['password'] ?? '');

        if ($email === '') {
            return [
                'status' => 'error',
                'message' => 'El correo electrónico es obligatorio.'
            ];
        }

        if ($password === '') {
            return [
                'status' => 'error',
                'message' => 'La contraseña es obligatoria.'
            ];
        }

        $usuario = $this->usuarioRepository->buscarPorEmail($email);

        if (!$usuario) {
            return [
                'status' => 'error',
                'message' => 'No existe un usuario registrado con ese correo.'
            ];
        }

        if (!password_verify($password, $usuario['contrasenia'])) {
            return [
                'status' => 'error',
                'message' => 'La contraseña es incorrecta.'
            ];
        }

        session_start();

        $_SESSION['usuario_id'] = $usuario['id'];
        $_SESSION['usuario_nombre'] = $usuario['nombre'];
        $_SESSION['usuario_email'] = $usuario['email'];

        return [
            'status' => 'success',
            'message' => 'Inicio de sesión correcto.'
        ];
    }
}