<?php
// (Mismos requires de interfaces y Database.php)
require_once __DIR__ . '/src/Dominio/Model/Grupo.php';

class GrupoRepository implements MostrarTodoInterface, MostrarPorIdInterface, AgregarInterface, ActualizarInterface, EliminarInterface {
    private PDO $db;
    public function __construct() { $this->db = Database::getConexion(); }

    public function mostrarTodo(): array {
        $stmt = $this->db->query("SELECT * FROM grupo");
        $items = [];
        while ($row = $stmt->fetch()) {
            $items[] = new Grupo((int)$row['id'], $row['codigo'], $row['nombre']);
        }
        return $items;
    }

    public function mostrarPorId(int $id): ?Grupo {
        $stmt = $this->db->prepare("SELECT * FROM grupo WHERE id = ?");
        $stmt->execute([$id]);
        $row = $stmt->fetch();
        return $row ? new Grupo((int)$row['id'], $row['codigo'], $row['nombre']) : null;
    }

    public function agregar($grupo): void {
        $stmt = $this->db->prepare("INSERT INTO grupo (codigo, nombre) VALUES (?, ?)");
        $stmt->execute([$grupo->codigo, $grupo->nombre]);
    }

    public function actualizar($grupo): void {
        $stmt = $this->db->prepare("UPDATE grupo SET codigo = ?, nombre = ? WHERE id = ?");
        $stmt->execute([$grupo->codigo, $grupo->nombre, $grupo->id]);
    }

    public function eliminar(int $id): void {
        $stmt = $this->db->prepare("DELETE FROM grupo WHERE id = ?");
        $stmt->execute([$id]);
    }
}
?>