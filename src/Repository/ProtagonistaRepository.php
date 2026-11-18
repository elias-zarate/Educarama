<?php // src/Repository/ProtagonistaRepository.php
declare(strict_types=1);

namespace Educarama\Repository;

use Educarama\Model\Protagonista;
use Educarama\Repository\Vista\FichaProtagonista;
use Educarama\Repository\Vista\ProtagonistaEnNomina;
use PDO;

final class ProtagonistaRepository extends RepositorioBase
{
    private const SELECT_FICHA =
        'SELECT p.*,
                per.dni              AS persona_dni,
                per.nombre           AS persona_nombre,
                per.apellido         AS persona_apellido,
                per.fecha_nacimiento AS persona_fecha_nacimiento,
                pa.nombre            AS patrulla_nombre
           FROM protagonista p
           JOIN persona per     ON per.id = p.id_persona
           LEFT JOIN patrulla pa ON pa.id = p.id_patrulla';

    public function __construct(PDO $conexion)
    {
        parent::__construct($conexion);
    }

    public function buscarPorId(int $id): ?Protagonista
    {
        return $this->buscarUno('SELECT * FROM protagonista WHERE id = ?', [$id], Protagonista::class);
    }

    public function listarPorSeccion(int $idSeccion): array
    {
        return $this->listar(
            'SELECT p.*
               FROM protagonista p
               JOIN persona per ON per.id = p.id_persona
              WHERE p.id_seccion = ?
              ORDER BY per.apellido, per.nombre',
            [$idSeccion],
            Protagonista::class,
        );
    }

    public function listarPorPatrulla(int $idPatrulla): array
    {
        return $this->listar(
            'SELECT p.*
               FROM protagonista p
               JOIN persona per ON per.id = p.id_persona
              WHERE p.id_patrulla = ?
              ORDER BY per.apellido, per.nombre',
            [$idPatrulla],
            Protagonista::class,
        );
    }

    public function buscarPorPersona(int $idPersona): ?Protagonista
    {
        return $this->buscarUno('SELECT * FROM protagonista WHERE id_persona = ?', [$idPersona], Protagonista::class);
    }

    public function buscarFicha(int $id): ?FichaProtagonista
    {
        return $this->buscarUno(self::SELECT_FICHA . ' WHERE p.id = ?', [$id], FichaProtagonista::class);
    }

    public function listarFichasPorSeccion(int $idSeccion): array
    {
        return $this->listar(
            self::SELECT_FICHA . ' WHERE p.id_seccion = ? ORDER BY per.apellido, per.nombre',
            [$idSeccion],
            FichaProtagonista::class,
        );
    }

    public function listarFichasPorPatrulla(int $idPatrulla): array
    {
        return $this->listar(
            self::SELECT_FICHA . ' WHERE p.id_patrulla = ? ORDER BY per.apellido, per.nombre',
            [$idPatrulla],
            FichaProtagonista::class,
        );
    }

    public function listarNomina(array $idsSecciones): array
    {
        if ($idsSecciones === []) {
            return [];
        }
        $placeholders = implode(',', array_fill(0, count($idsSecciones), '?'));
        return $this->listar(
            "SELECT p.id, p.anio_ingreso, p.promesa, p.id_seccion, p.id_patrulla,
                    per.dni              AS persona_dni,
                    per.nombre           AS persona_nombre,
                    per.apellido         AS persona_apellido,
                    per.fecha_nacimiento AS persona_fecha_nacimiento,
                    pa.nombre            AS patrulla_nombre,
                    s.nombre             AS seccion_nombre,
                    g.id                 AS grupo_id,
                    g.codigo             AS grupo_codigo,
                    g.nombre             AS grupo_nombre,
                    r.id                 AS rama_id,
                    r.titulo             AS rama_titulo,
                    ult_e.titulo         AS etapa_actual_titulo,
                    COALESCE(cd.desarrolladas, 0) AS competencias_desarrolladas,
                    COALESCE(ct.total, 0)         AS competencias_total
               FROM protagonista p
               JOIN persona per     ON per.id = p.id_persona
               LEFT JOIN patrulla pa ON pa.id = p.id_patrulla
               JOIN seccion s       ON s.id = p.id_seccion
               JOIN grupo g         ON g.id = s.id_grupo
               JOIN rama r          ON r.id = s.id_rama
               LEFT JOIN (
                   SELECT el.id_protagonista, e.titulo
                     FROM etapa_lograda el
                     JOIN etapa e ON e.id = el.id_etapa
                    WHERE e.numero = (
                        SELECT MAX(e2.numero)
                          FROM etapa_lograda el2
                          JOIN etapa e2 ON e2.id = el2.id_etapa
                         WHERE el2.id_protagonista = el.id_protagonista
                           AND e2.id_rama = e.id_rama
                    )
               ) AS ult_e ON ult_e.id_protagonista = p.id
               LEFT JOIN (
                   SELECT id_protagonista, COUNT(*) AS desarrolladas
                     FROM competencia_protagonista
                    WHERE estado = 'desarrollada'
                    GROUP BY id_protagonista
               ) AS cd ON cd.id_protagonista = p.id
               LEFT JOIN (
                   SELECT id_rama, COUNT(*) AS total
                     FROM competencia
                    GROUP BY id_rama
               ) AS ct ON ct.id_rama = s.id_rama
              WHERE p.id_seccion IN ($placeholders)
              ORDER BY per.apellido, per.nombre",
            array_values($idsSecciones),
            ProtagonistaEnNomina::class,
        );
    }

    /** Cantidad total de protagonistas en las secciones de un grupo. */
    public function contarPorGrupo(int $idGrupo): int
    {
        $stmt = $this->conexion->prepare(
            'SELECT COUNT(*) FROM protagonista p
               JOIN seccion s ON s.id = p.id_seccion
              WHERE s.id_grupo = ?'
        );
        $stmt->execute([$idGrupo]);
        return (int) $stmt->fetchColumn();
    }

    public function insertar(Protagonista $protagonista): int
    {
        $protagonista->id = $this->insertarFila(
            'INSERT INTO protagonista (id_persona, id_seccion, id_patrulla, promesa, anio_ingreso)
             VALUES (:id_persona, :id_seccion, :id_patrulla, :promesa, :anio_ingreso)',
            $this->parametros($protagonista),
        );
        return $protagonista->id;
    }

    public function actualizar(Protagonista $protagonista): void
    {
        $this->ejecutar(
            'UPDATE protagonista
                SET id_persona = :id_persona, id_seccion = :id_seccion, id_patrulla = :id_patrulla,
                    promesa = :promesa, anio_ingreso = :anio_ingreso
              WHERE id = :id',
            $this->parametros($protagonista) + ['id' => $this->exigirId($protagonista->id)],
        );
    }

    public function eliminar(int $id): void
    {
        $this->ejecutar('DELETE FROM protagonista WHERE id = ?', [$id]);
    }

    private function parametros(Protagonista $protagonista): array
    {
        return [
            'id_persona'   => $protagonista->idPersona,
            'id_seccion'   => $protagonista->idSeccion,
            'id_patrulla'  => $protagonista->idPatrulla,
            'promesa'      => (int) $protagonista->promesa,
            'anio_ingreso' => $protagonista->anioIngreso,
        ];
    }
}