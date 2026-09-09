<?php

/**
 * @template TEntidad
 */

interface MostrarPorIdInterface
{
    /**
     * @param int $id
     * @return TEntidad|null
     */
    
    public function mostrarPorId(int $id);
}

?>