<?php
require_once __DIR__ . '/src/Model/Insignia.php';

class InsigniaRepository implements MostrarTodoInterface, MostrarPorIdInterface, AgregarInterface, ActualizarInterface, EliminarInterface {
    private PDO $db;
    public function __construct() { $this->db = Database::getConexion(); }

    public function mostrarTodo(): array {
        $stmt = $this->db->query("SELECT * FROM insignia");
        $items = [];
        while ($row = $stmt->fetch()) { $items[] = new Insignia((int)$row['id'], $row['nombre'], (int)$row['id_categoria_rama']); }
        return $items;
    }

    public function mostrarPorId(int $id): ?Insignia {
        $stmt = $this->db->prepare("SELECT * FROM insignia WHERE id = ?");
        $stmt->execute([$id]);
        $row = $stmt->fetch();
        return $row ? new Insignia((int)$row['id'], $row['nombre'], (int)$row['id_categoria_rama']) : null;
    }

    public function agregar($insignia): void {
        $stmt = $this->db->prepare("INSERT INTO insignia (nombre, id_categoria_rama) VALUES (?, ?)");
        $stmt->execute([$insignia->nombre, $insignia->id_categoria_rama]);
    }

    public function actualizar($insignia): void {
        $stmt = $this->db->prepare("UPDATE insignia SET nombre = ?, id_categoria_rama = ? WHERE id = ?");
        $stmt->execute([$insignia->nombre, $insignia->id_categoria_rama, $insignia->id]);
    }

    public function eliminar(int $id): void {
        $stmt = $this->db->prepare("DELETE FROM insignia WHERE id = ?");
        $stmt->execute([$id]);
    }
}
?>