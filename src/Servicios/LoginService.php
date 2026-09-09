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

    public function solicitarRecuperacion(array $datos): array
    {
        $email = trim($datos['email'] ?? '');

        if ($email === '' || !filter_var($email, FILTER_VALIDATE_EMAIL)) {
            return [
                'status' => 'error',
                'message' => 'Por favor, ingresá un correo electrónico válido.'
            ];
        }

        $usuario = $this->usuarioRepository->buscarPorEmail($email);

        if (!$usuario) {
            return [
                'status' => 'error',
                'message' => 'No existe un usuario registrado con ese correo.'
            ];
        }

        $token = strtoupper(substr(bin2hex(random_bytes(3)), 0, 6));
        $this->usuarioRepository->guardarTokenRecuperacion($email, $token);

        return [
            'status' => 'success',
            'message' => "Código de recuperación generado con éxito. Tu código simulado es: <strong>{$token}</strong>",
            'email' => $email
        ];
    }

    public function restablecerContrasenia(array $datos): array
    {
        $email = trim($datos['email'] ?? '');
        $token = trim($datos['token'] ?? '');
        $nuevaContrasenia = $datos['nueva_contrasenia'] ?? '';

        if ($email === '' || $token === '' || $nuevaContrasenia === '') {
            return [
                'status' => 'error',
                'message' => 'Todos los campos son obligatorios.'
            ];
        }

        if (strlen($nuevaContrasenia) < 6) {
            return [
                'status' => 'error',
                'message' => 'La nueva contraseña debe tener al menos 6 caracteres.'
            ];
        }

        $usuario = $this->usuarioRepository->buscarPorEmailYToken($email, $token);

        if (!$usuario) {
            return [
                'status' => 'error',
                'message' => 'El código de recuperación es inválido o ha expirado.'
            ];
        }

        $passwordHash = password_hash($nuevaContrasenia, PASSWORD_DEFAULT);
        $this->usuarioRepository->actualizarPasswordYLimpiarToken($email, $passwordHash);

        return [
            'status' => 'success',
            'message' => '¡Contraseña actualizada con éxito!'
        ];
    }
}