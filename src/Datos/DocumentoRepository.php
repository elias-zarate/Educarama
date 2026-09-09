<?php
require_once __DIR__ . '/src/Model/Documento.php';

class DocumentoRepository implements MostrarTodoInterface, MostrarPorIdInterface, AgregarInterface, ActualizarInterface, EliminarInterface {
    private PDO $db;
    public function __construct() { $this->db = Database::getConexion(); }

    public function mostrarTodo(): array {
        $stmt = $this->db->query("SELECT * FROM documento");
        $items = [];
        while ($row = $stmt->fetch()) { $items[] = new Documento((int)$row['id'], $row['tipo_documento'], $row['descripcion'], (int)$row['id_protagonista']); }
        return $items;
    }

    public function mostrarPorId(int $id): ?Documento {
        $stmt = $this->db->prepare("SELECT * FROM documento WHERE id = ?");
        $stmt->execute([$id]);
        $row = $stmt->fetch();
        return $row ? new Documento((int)$row['id'], $row['tipo_documento'], $row['descripcion'], (int)$row['id_protagonista']) : null;
    }

    public function agregar($documento): void {
        $stmt = $this->db->prepare("INSERT INTO documento (tipo_documento, descripcion, id_protagonista) VALUES (?, ?, ?)");
        $stmt->execute([$documento->tipo_documento, $documento->descripcion, $documento->id_protagonista]);
    }

    public function actualizar($documento): void {
        $stmt = $this->db->prepare("UPDATE documento SET tipo_documento = ?, descripcion = ?, id_protagonista = ? WHERE id = ?");
        $stmt->execute([$documento->tipo_documento, $documento->descripcion, $documento->id_protagonista, $documento->id]);
    }

    public function eliminar(int $id): void {
        $stmt = $this->db->prepare("DELETE FROM documento WHERE id = ?");
        $stmt->execute([$id]);
    }
}
?>