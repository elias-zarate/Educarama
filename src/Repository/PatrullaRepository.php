<?php // src/Repository/PatrullaRepository.php
declare(strict_types=1);

namespace Educarama\Repository;

use Educarama\Model\Patrulla;
use PDO;

final class PatrullaRepository extends RepositorioBase
{
    public function __construct(PDO $conexion)
    {
        parent::__construct($conexion);
    }

    public function buscarPorId(int $id): ?Patrulla
    {
        return $this->buscarUno('SELECT * FROM patrulla WHERE id = ?', [$id], Patrulla::class);
    }

    public function listarPorSeccion(int $idSeccion): array
    {
        return $this->listar(
            'SELECT * FROM patrulla WHERE id_seccion = ? ORDER BY nombre',
            [$idSeccion],
            Patrulla::class,
        );
    }

    public function buscarPorNombre(int $idSeccion, string $nombre): ?Patrulla
    {
        return $this->buscarUno(
            'SELECT * FROM patrulla WHERE id_seccion = ? AND nombre = ?',
            [$idSeccion, $nombre],
            Patrulla::class,
        );
    }

    /** Cantidad total de patrullas en las secciones de un grupo. */
    public function contarPorGrupo(int $idGrupo): int
    {
        $stmt = $this->conexion->prepare(
            'SELECT COUNT(*) FROM patrulla pa
               JOIN seccion s ON s.id = pa.id_seccion
              WHERE s.id_grupo = ?'
        );
        $stmt->execute([$idGrupo]);
        return (int) $stmt->fetchColumn();
    }

    public function insertar(Patrulla $patrulla): int
    {
        $patrulla->id = $this->insertarFila(
            'INSERT INTO patrulla (nombre, id_seccion) VALUES (:nombre, :id_seccion)',
            ['nombre' => $patrulla->nombre, 'id_seccion' => $patrulla->idSeccion],
        );
        return $patrulla->id;
    }

    public function actualizar(Patrulla $patrulla): void
    {
        $this->ejecutar(
            'UPDATE patrulla SET nombre = :nombre, id_seccion = :id_seccion WHERE id = :id',
            ['nombre' => $patrulla->nombre, 'id_seccion' => $patrulla->idSeccion, 'id' => $this->exigirId($patrulla->id)],
        );
    }

    public function eliminar(int $id): void
    {
        $this->ejecutar('DELETE FROM patrulla WHERE id = ?', [$id]);
    }
}