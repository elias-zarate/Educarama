-- 1. Insertar Roles del Sistema
INSERT INTO rol (nombre) VALUES 
('Desarrollador / Admin'),  
('Jefe de Grupo'),          
('Jefe de Rama'),           
('Educador');

-- 2. Insertar Senderos
INSERT INTO sendero (tipo) VALUES 
('Sendero de la salud y el bienestar'),
('Sendero de la paz'),
('Sendero del ambiente'),
('Sendero de las habilidades para la vida');

-- 3. Insertar Categorías de Rama
INSERT INTO categoria_rama (nombre) VALUES 
('Lobatos y Lobeznas'), 
('Scouts'), 
('Caminantes'), 
('Rovers');

-- 4. Insertar Tipos de Acción
INSERT INTO tipo_accion (tipo) VALUES 
('Requerida'), 
('Sugerida');

-- 5. Insertar las 53 Competencias (Rama Scouts = id_categoria_rama: 2)
INSERT INTO competencia (codigo, descripcion, id_categoria_rama, id_sendero) VALUES 
-- Área 1: Salud y bienestar
(1, 'Incorporo habitos de higiene.', 2, 1),
(2, 'Entiendo la importancia de alimentarme de forma saludable.', 2, 1),
(3, 'Reconozco y expreso mis emociones, entendiendo cómo me afectan a mi y a las demas personas.', 2, 1),
(4, 'Soy critica/o con las imagenes y mensajes que los medios de comunicación y las redes sociales nos presentan sobre los modelos corporales y las modas.', 2, 1),
(5, 'Busco información y tomo decisiones conscientes para cuidar mi salud y sentirme bien conmigo misma/o.', 2, 1),
(6, 'Acepto y valoro mi cuerpo y el de los demas, evitando comparaciones.', 2, 1),
(7, 'Reconozco e identifico las particularidades, diferencias y cambios de mi cuerpo a medida que voy creciendo.', 2, 1),
(8, 'Identifico y respeto mis emociones y sentimientos vinculados a la sexualidad y sus cambios.', 2, 1),
(9, 'Valoro el dialogo y el respeto mutuo en mis relaciones, expresando mi opinion de manera segura y respetuosa.', 2, 1),
(10, 'Conozco los recursos y lugares donde puedo obtener informacion y apoyo para proteger mis derechos sexuales.', 2, 1),
(11, 'Descubro los beneficios que tiene la actividad fisica y los deportes para mi bienestar fisico y emocional.', 2, 1),
(12, 'Trabajo en equipo y disfruto de las actividades fisicas y deportivas, fomentando el desarrollo social y emocional.', 2, 1),
(13, 'Identifico situaciones de riesgo y adopto practicas seguras.', 2, 1),
(14, 'Conozco y construyo redes de apoyo y ayuda mutua.', 2, 1),

-- Área 2: Paz y desarrollo
(15, 'Colaboro en la resolución de situaciones de violencia, a través del dialogo, evitando situaciones de conflicto.', 2, 2),
(16, 'Exploro y me identifico con la cultura de mi comunidad y de mi pais proponiendo actividades y proyectos con esta tematica.', 2, 2),
(17, 'Valoro mi historia e identidad, la de mi Pais y respeto sus simbolos patrios.', 2, 2),
(18, 'Valoro y respeto la importancia de los Derechos Humanos en la sociedad e identifico situaciones de vulneracion de estos, buscando activamente ayuda en mi entorno.', 2, 2),
(19, 'Ejercito y promuevo practicas que fomenten la participación democratica en los diferentes contextos en donde me desenvuelvo.', 2, 2),
(20, 'Respeto los acuerdos de convivencia y las decisiones tomadas por los demas, expresando mis propias ideas y posturas de manera empatica y respetuosa.', 2, 2),
(21, 'Promuevo que se garanticen los derechos de todas las personas en los ambitos en los que Participo.', 2, 2),
(22, 'Fomento el respeto a la diversidad en todas sus formas, cuestionando estereotipos y prejuicios en mis interacciones diarias.', 2, 2),
(23, 'Promuevo la participación e inclusión de todas las personas, sin discriminación alguna.', 2, 2),
(24, 'Identifico oportunidades para ayudar a alguien de manera individual y/o colectiva, sin esperar nada a cambio.', 2, 2),
(25, 'Me comprometo y animo a otras personas a trabajar en conjunto para lograr un cambio positivo en el mundo.', 2, 2),
(26, 'Observo lo que pasa a mi alrededor y trato de entender las razones detrás de las desigualdades que veo, actuando de manera responsable en situaciones que afectan a la sociedad.', 2, 2),
(27, 'Me pregunto sobre el origen, la grandeza, belleza y misterios de todo lo que soy, somos y nos rodea.', 2, 2),
(28, 'Dialogo con los demas sobre lo que creo y busco conocer lo que los otros creen, demostrando respeto e interés.', 2, 2),
(29, 'Adhiero a los valores expresados en la Ley y la Promesa Scout comprendiendo su importancia para mi vida.', 2, 2),
(30, 'Me pregunto sobre mis creencias, prioridades, valores y como se manifiesta en mi actuar cotidiano.', 2, 2),

-- Área 3: Ambiente
(31, 'Reconozco mi impacto en el ambiente y en la sociedad adoptando un estilo de vida responsable y sostenible.', 2, 3),
(32, 'Utilizo los recursos naturales de forma consciente y busco alternativas que promuevan la conservacion del ambiente.', 2, 3),
(33, 'Participo de iniciativas para construir una sociedad más respetuosa con el planeta.', 2, 3),
(34, 'Comprendo las causas y consecuencias del impacto ambiental y realizo acciones para preservar y proteger el ambiente.', 2, 3),
(35, 'Comprendo que todo lo que le hacemos a los seres vivos y al ambiente nos afecta reciprocamente.', 2, 3),
(36, 'Entiendo que la Tierra es un lugar precioso y unico en el universo, y que todos tenemos la responsabilidad de protegerla.', 2, 3),
(37, 'Exploro oportunidades junto a mis amigas y amigos para conectarme con la naturaleza, cuidandola y protegiéndola.', 2, 3),
(38, 'Desarrollo habilidades y técnicas que me permitan el disfrute de la vida en la naturaleza', 2, 3),
(39, 'Exploro y contemplo la naturaleza como un espacio que me permite conectar con lo trascendente y el bienestar.', 2, 3),

-- Área 4: Habilidades para la vida
(40, 'Exploro temas de mi interés con la intención de mejorar cada dia.', 2, 4),
(41, 'Experimento diferentes medios y tecnologias que me ayuden a resolver problemas de la vida cotidiana.', 2, 4),
(42, 'Aplico mi conocimiento y habilidades para resolver los desafios y oportunidades que se me presentan.', 2, 4),
(43, 'Comparto mis pasatiempos y especialidades con amigas y amigos, buscando intereses comunes que podamos desarrollar en conjunto.', 2, 4),
(44, 'Asumo un rol de manera responsable en los proyectos y/o actividades de mi Patrulla y/o Unidad.', 2, 4),
(45, 'Lidero actividades y proyectos, acompañando y motivando a mis pares a sumarse', 2, 4),
(46, 'Exploro y experimento con diferentes técnicas y tecnologias para resolver problemas, lograr mis objetivos y descubrir soluciones creativas.', 2, 4),
(47, 'Busco informacion en diversas fuentes, y evaluo su veracidad y relevancia antes de aplicarla de manera critica y creativa.', 2, 4),
(48, 'Comprendo la importancia de ser responsable y ético en el uso de la tecnologia y la informacion.', 2, 4),
(49, 'Administro mis recursos y mi dinero de forma responsable.', 2, 4),
(50, 'Organizo mi tiempo en forma adecuada diferenciando mis responsabilidades de mi tiempo libre.', 2, 4),
(51, 'Enfrento y supero las dificultades que se me presentan y aprendo de la experiencia.', 2, 4),
(52, 'Desarrollo habilidades para trabajar y valorar con otros, valorando y compartiendo ideas, trabajando juntos para lograr nuestras metas.', 2, 4),
(53, 'Participo en diferentes manifestaciones artísticas en las que expreso mis emociones y sentimientos', 2, 4);


-- ==========================================
-- SENDEROS Y ACCIONES / DESAFÍOS
-- ==========================================

/*AREA SALUD Y BIENESTAR*/

-- Acciones / Desafíos para la Competencia 1: "Incorporo habitos de higiene."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Tengo una rutina de higiene personal saludable y responsable. (Por ejemplo: higiene dental, aseo de manos).', 1, 1, 1),
('Colaboro con el orden y la limpieza de mi habitación, de mi casa y de los lugares en los que vivo.', 1, 1, 1),
('Cuido, limpio y ordeno los lugares en los que realizo actividades, acampo y realizo excursiones.', 1, 1, 1),
('Realizo construcciones en campamentos que ayudan a mejorar la comodidad y la higiene del lugar (mesas, sillas, percheros, lavabo, etc.).', 1, 1, 1),
('Organizo correctamente mi mochila para un campamento de 3 días.', 1, 1, 1),
('Uso correctamente mi indumentaria scout y la mantengo en buen estado.', 1, 1, 2),
('Cuando sea necesario, lavo y mantengo mi ropa en buen estado durante los campamentos.', 1, 1, 2),
('Desarrollo una especialidad referida a la higiene o la salud.', 1, 1, 2);

-- Acciones / Desafíos para la Competencia 2: "Entiendo la importancia de alimentarme de forma saludable."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Sé qué alimentos me ayudan a crecer y practico una alimentación saludable.', 2, 2, 1),
('Planifico un menú saludable para un campamento de Patrulla acorde a las actividades a desarrollar y la época del año.', 2, 2, 1),
('Preparo comidas sencillas y saludables en mi casa y para mi Patrulla en actividades de campamento.', 2, 2, 1),
('Conozco las técnicas de manipulación y conservación de alimentos y las aplico en la cocina.', 2, 2, 1),
('Realizo un tutorial, junto a mi Patrulla, sobre como preparar nuestra comida favorita y la comparto con los demás.', 2, 2, 2),
('Realizo algunas recetas tradicionales de cocina rústica o sin utensilios.', 2, 2, 2),
('Preparo un menú para una excursión o campamento de 3 días.', 2, 2, 2),
('Me encargo de hacer las compras de alimentos para una actividad o campamento de Patrulla.', 2, 2, 2),
('Pruebo, junto a mi Patrulla, nuevas recetas y técnicas de cocina con la ayuda de una persona experta.', 2, 2, 2),
('Me desempeño como cocinera o cocinero de mi Patrulla al menos por un ciclo de programa.', 2, 2, 2),
('Desarrollo la especialidad de cocina.', 2, 2, 2);

-- Acciones / Desafíos para la Competencia 3: "Reconozco y expreso mis emociones, entendiendo cómo me afectan a mi y a las demas personas."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Identifico y reflexiono sobre mis emociones en las diferentes situaciones en las que me encuentro y las comparto con los demás.', 3, 3, 1),
('Escucho a los demás con atención y empatía para comprender cómo se sienten y cómo mis palabras y acciones pueden influir en ellos.', 3, 3, 1),
('Aprendo técnicas de relajación como la respiración profunda o la meditación para manejar las emociones intensas.', 3, 3, 1),
('Expreso mis emociones de diversas maneras a través de diferentes formas (ya sean lúdicas, expresivas, artísticas, etc.).', 3, 3, 2),
('Dialogo con personas de mi confianza, sobre cómo me siento y lo que sucede a diario en mi vida.', 3, 3, 2),
('Aprendo a reconocer las señales físicas que acompañan mis emociones, como palpitaciones, sudoración o tensión muscular.', 3, 3, 2);

-- Acciones / Desafíos para la Competencia 4: "Soy critica/o con las imagenes y mensajes que los medios de comunicación y las redes sociales nos presentan sobre los modelos corporales y las modas."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Investigo sobre lo que significa el estereotipo de belleza y discuto sobre el tema con mis compañeras y compañeros de Patrulla.', 4, 4, 1),
('Exploro en los medios de comunicación, en películas y series las imágenes de personas que aparecen usualmente y reflexiono con mi Patrulla sobre las imágenes y los mensajes que transmiten las redes y los medios de comunicación sobre los modelos corporales.', 4, 4, 1),
('En nuestras relaciones y actividades de Patrulla promuevo el respeto hacia mi propio cuerpo y el de los demás.', 4, 4, 1),
('Entiendo por qué no se debe hablar del cuerpo de las demás personas.', 4, 4, 1);

-- Acciones / Desafíos para la Competencia 5: "Busco información y tomo decisiones conscientes para cuidar mi salud y sentirme bien conmigo misma/o."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Busco información confiable sobre cuidados de la salud en la adolescencia.', 5, 5, 1),
('Incorporo hábitos de cuidado de mi salud para sentirme bien conmigo mismo/a, como, por ejemplo: realizar ejercicio de forma regular, comer saludable, realizar deportes o actividad física, disfrutar del aire libre, etc.', 5, 5, 1),
('Defino al menos 3 acciones para cuidar mi salud y sentirme bien, y las pongo en práctica al menos por 4 meses.', 5, 5, 1),
('Me informo sobre qué sustancias pueden dañar mi cuerpo y lo comparto con las demás personas en alguna actividad de la Unidad.', 5, 5, 1);

-- Acciones / Desafíos para la Competencia 6: "Acepto y valoro mi cuerpo y el de los demas, evitando comparaciones."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo actividades, junto a mi Patrulla, de autocuidado que promuevan el bienestar emocional y fisico.', 6, 6, 1),
('Entiendo por qué debemos evitar hacer comparaciones con el cuerpo de las demás personas.', 6, 6, 1),
('Me informo de los cambios que se estan produciendo en mi cuerpo y en mis pensamientos, gustos y sentimientos en esta etapa de mi vida.', 6, 6, 1),
('Sécual es mi peso ideal, cómo se produce el sobrepeso y qué hacer para que mi peso sea el adecuado.', 6, 6, 2),
('Me informo sobre los problemas que causan el alcohol y el tabaco.', 6, 6, 2);

-- Acciones / Desafíos para la Competencia 7: "Reconozco e identifico las particularidades, diferencias y cambios de mi cuerpo a medida que voy creciendo."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Investigo sobre los cambios progresivos que se estan produciendo en mi cuerpo buscando informacion confiable.', 7, 7, 1),
('Reconozco los cambios que se están produciendo en mi cuerpo', 7, 7, 1),
('Comparto información y experiencias con amigas o amigos en todos los ámbitos en los que me desenvuelvo (familia, escuela, scouts...).', 7, 7, 1),
('Identifico las diferencias anatómicas y fisiológicas de mujeres y varones en las distintas etapas de la vida.', 7, 7, 1),
('Conozco las medidas de mi cuerpo y las aplico en mediciones y estimaciones (paso doble, brazos extendidos, altura, etc.).', 7, 7, 5),
('Reviso las medidas de mi cuerpo cada 5 o 6 meses para mantener actualizada mis técnicas de medicion.', 7, 7, 6);

-- Acciones / Desafíos para la Competencia 8: "Identifico y respeto mis emociones y sentimientos vinculados a la sexualidad y sus cambios."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Consulto con personas adultas de confianza (familiares, educadores, médicos.) sobre temas relacionados con la sexualidad y el crecimiento bioldgico para aclarar dudas.', 8, 8, 1),
('Participo, junto a mi Patrulla, en charlas y talleres sobre educacion sexual.', 8, 8, 1),
('Aprendo y utilizo un vocabulario apropiado para hablar sobre la sexualidad y los cambios corporales.', 8, 8, 1),
('Escucho y apoyo a mis amigas y amigos cercanos sobre sus inquietudes, preguntas y experiencias en un ambiente seguro y de confianza vinculado a la sexualidad.', 8, 8, 1);

-- Acciones / Desafíos para la Competencia 9: "Valoro el dialogo y el respeto mutuo en mis relaciones, expresando mi opinion de manera segura y respetuosa."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Expreso mis pensamientos y sentimientos de manera empatica y considerada, teniendo en cuenta como podrian sentirse los demas.', 9, 9, 1),
('Escucho atentamente las ideas de los demas y expongo mis ideas de forma clara, sin agresividad.', 9, 9, 1),
('Comparto informacidén y experiencias con amigas o amigos en todos los ambitos en los que me desenvuelvo (familia, escuela, scouts...).', 9, 9, 1),
('Participo in la evaluacion de la progresion personal de mis amigas y amigos de la Patrulla y también soy evaluado en mi progresion.', 9, 9, 1),
('Construyo relaciones basadas en la confianza, manteniendo mi palabra y siendo honesto en mis relaciones.', 9, 9, 2),
('Desarrollo una especialidad relacionada con la comunicacion.', 9, 9, 2);

-- Acciones / Desafíos para la Competencia 10: "Conozco los recursos y lugares donde puedo obtener informacion y apoyo para proteger mis derechos sexuales."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Me informo sobre mis derechos relacionados a mi salud sexual.', 10, 10, 1),
('Conozco y tengo herramientas para defender mis derechos relacionados al desarrollo sano de mi sexualidad.', 10, 10, 1),
('Participo, junto a mi Patrulla, en charlas educativas sobre derechos y salud sexual.', 10, 10, 1),
('Conozco mis derechos y expreso mi disconformidad cuando entiendo que mis derechos, opiniones y necesidades personales están siendo violentadas.', 10, 10, 1),
('Entiendo cuando no estoy obligada/o a hacer lo que me provoque temor o incomodidad.', 10, 10, 1),
('Desarrollo una especialidad vinculada al tema.', 10, 10, 2);

-- Acciones / Desafíos para la Competencia 11: "Descubro los beneficios que tiene la actividad fisica y los deportes para mi bienestar fisico y emocional."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Indago sobre los beneficios que tiene la actividad fisica y los deportes para mi bienestar fisico y emocional.', 11, 11, 1),
('Participo en diversas actividades de mi interés que me desafien fisicamente y que me permitan superarme y mejorar.', 11, 11, 1),
('Invento juegos y desafios fisicos junto a mi Patrulla, promoviendo la creatividad y la diversion.', 11, 11, 1),
('Practico una actividad deportiva y me esfuerzo por mejorar mi rendimiento.', 11, 11, 1),
('Busco y propongo juegos y actividades fisicas para hacer con mi Patrulla.', 11, 11, 1),
('Realizo actividades de senderismo junto a mi Patrulla y Unidad Scout (aplicando técnicas de marcha, alimentacion e hidratación adecuada, tiempos de descanso, etc.).', 11, 11, 1),
('Participo junto a mi Patrulla de paseos en bicicleta, en bote, en Kayak.', 11, 11, 2),
('Desarrollo una especialidad vinculada al deporte y la actividad fisica.', 11, 11, 2);

-- Acciones / Desafíos para la Competencia 12: "Trabajo en equipo y disfruto de las actividades fisicas y deportivas, fomentando el desarrollo social y emocional."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Junto a mi Patrulla, construyo un circuito de obstaculos para jugar junto a mi Unidad.', 12, 12, 1),
('Trazo y sigo señales de pista en un recorrido por lo menos de un kilometro en el campo y de 2 kilometros en area urbana.', 12, 12, 1),
('Realizo con mi Patrulla una actividad de navegacion terrestre, utilizando mapa y brujula.', 12, 12, 1),
('Con mi Patrulla invento un juego o deporte para jugar juntos y con otras amigas y amigos.', 12, 12, 1),
('Organizo con mi Patrulla un campeonato de un deporte o juego para realizar con la Unidad Scout.', 12, 12, 2),
('Participo in juegos y actividades que requieran cooperacion y coordinacion con otros jovenes.', 12, 12, 2),
('Desarrollo una especialidad vinculada al deporte y la actividad fisica.', 12, 12, 2);

-- Acciones / Desafíos para la Competencia 13: "Identifico situaciones de riesgo y adopto practicas seguras."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Identifico situaciones que pueden dañar mi salud y la de mis pares en las actividades que realizo.', 13, 13, 1),
('Utilizo los elementos de seguridad adecuados para participar en actividades fisicas o deportes.', 13, 13, 1),
('Sé cómo actuar en casos de pequeñas heridas, picaduras de insectos, deshidratación, insolación, y hemorragias.', 13, 13, 1),
('Aplico el orden de prioridades en caso de accidentes.', 13, 13, 1),
('Conozco y aplico las técnicas de traslado de heridos.', 13, 13, 1),
('Respeto y aplico las normas de seguridad en las actividades que realizo.', 13, 13, 1),
('Conozco el Plan de Contingencia.', 13, 13, 1),
('Aprendo sobre la seguridad en línea, especialmente en las redes sociales (no compartir información personal, asegurarse de que las configuraciones personales de privacidad en las plataformas de uso frecuente sean adecuadas y seguras, y comunicar cualquier problema a un adulto de confianza).', 13, 13, 2),
('Participo de simulacros de emergencias y accidentes.', 13, 13, 2),
('Desarrollo una especialidad vinculada a la seguridad.', 13, 13, 2),
('Desarrollo una especialidad vinculada a la salud y los primeros auxilios.', 13, 13, 2),
('Me desempeño como enfermero/a de Patrulla durante un ciclo de programa.', 13, 13, 2);

-- Acciones / Desafíos para la Competencia 14: "Conozco y construyo redes de apoyo y ayuda mutua."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Identifico a las personas a quienes puedo pedir ayuda y apoyo en distintos ámbitos (familia, escuela, grupo scout, club, amigos, amigas...).', 14, 14, 1),
('Apoyo a una compañera o compañero de Patrulla en su progresión personal, en el logro de alguna especialidad o en otros temas de su interés.', 14, 14, 1),
('Conversamos en la Patrulla sobre aquellos temas que nos preocupa e interesa.', 14, 14, 1);


/*AREA PAZ Y DESARROLLO*/

-- Acciones / Desafíos para la Competencia 15: "Colaboro en la resolución de situaciones de violencia, a través del dialogo, evitando situaciones de conflicto."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Participo de los órganos de toma de decisiones de la Patrulla y de la Unidad, expresando mis opiniones e ideas con respeto y escuchando las opiniones y decisiones de los demas en todo momento.', 15, 15, 1),
('Colaboro en resolver problemas o conflictos entre mis compañeras y compañeros de Patrulla de manera pacifica.', 15, 15, 1),
('Identifico distintas formas de expresiones de violencia, intolerancia y exclusión en mi escuela u otros lugares que frecuento.', 15, 15, 1),
('Actuo como mediador/a en conflictos entre mis amigas, amigos, compañeros/as de escuela, alentando a escucharse y buscar soluciones pacificas.', 15, 15, 1),
('Sé cómo actuar en situaciones de bullying y de ciberacoso.', 15, 15, 1),
('Coordino diferentes temas en un Consejo de Patrulla en el que se discuten.', 15, 15, 2),
('Participo de un proyecto de la paz que reconoce el Movimiento Scout (personal, comunidad y global).', 15, 15, 2),
('Sé diferenciar entre una broma y una situación de acoso o exclusión.', 15, 15, 2);

-- Acciones / Desafíos para la Competencia 16: "Exploro y me identifico con la cultura de mi comunidad y de mi pais proponiendo actividades y proyectos con esta tematica."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Investigo juegos tradicionales de mi región y/o país y propongo hacerlos con mi Patrulla y Unidad Scout.', 16, 16, 1),
('Investigo relatos, narraciones, leyendas, música y danzas de mi región y mi país y las comparto en una velada de mi Patrulla o un fogón de la Unidad.', 16, 16, 1),
('Propongo actividades a mi Patrulla y a mi Unidad vinculadas a la cultura de mi comunidad.', 16, 16, 1),
('Participo junto a mi Patrulla, Unidad o en forma individual de festividades típicas de la región donde vivo, o en una en la que se celebre la cultura de mi comunidad y/o país.', 16, 16, 1),
('Participo de visitas a museos, monumentos históricos o sitios culturales para aprender sobre la historia local, provincial o nacional.', 16, 16, 1),
('Investigo y recreo junto a mi Patrulla o Unidad, algunas tradiciones culinarias, vestimenta o prácticas culturales de mi región y/o país.', 16, 16, 1),
('Entrevisto a personas mayores de la comunidad para obtener historias y recuerdos de cómo era la vida en tiempos pasados y organizo charlas o presentaciones en las que estas personas puedan compartir sus experiencias.', 16, 16, 2),
('Realizo videos cortos que exploren diferentes aspectos de la cultura local, como la música, la danza, la comida, etc.', 16, 16, 2),
('Investigo sobre la historia del Movimiento Scout a nivel mundial y en nuestro país.', 16, 16, 2),
('Conozco el significado de los colores del pañuelo de mi grupo scout y de su nombre.', 16, 16, 2),
('Participo in proyectos que involucren la restauración, limpieza o conservación de sitios históricos y culturales de la comunidad.', 16, 16, 2),
('Desarrollo una especialidad vinculada a la cultura.', 16, 16, 2);

-- Acciones / Desafíos para la Competencia 17: "Valoro mi historia e identidad, la de mi Pais y respeto sus simbolos patrios."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Aprendo sobre la historia de mi propia familia y antepasados, y comprendo como esto se relaciona con la historia del pais.', 17, 17, 1),
('Conozco la historia y el sentido de los principales simbolos patrios.', 17, 17, 1),
('Conozco y realizo correctamente los procedimientos de izado y arriado de la bandera nacional.', 17, 17, 1),
('Exploro la vida y los logros de figuras historicas nacionales que hayan influido en la historia de la región donde vivo y/o del pais.', 17, 17, 1),
('Creo materiales visuales que destaquen aspectos importantes de la historia, la cultura y la identidad de la region donde vivo y/o del pais.', 17, 17, 1),
('Recopilo fotos y recuerdos familiares para crear un album o video que muestre la historia y las experiencias de la familia.', 17, 17, 2),
('Exploro diversas canciones que hablen de los simbolos patrios para cantar en nuestros actos y conmemoraciones.', 17, 17, 2);

-- Acciones / Desafíos para la Competencia 18: "Valoro y respeto la importancia de los Derechos Humanos en la sociedad e identifico situaciones de vulneracion de estos, buscando activamente ayuda en mi entorno."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Aprendo sobre los derechos del niño, niñas y adolescentes utilizando diferentes fuentes confiables.', 18, 18, 1),
('Tomo decisiones justas y equitativas en juegos y situaciones cotidianas.', 18, 18, 1),
('Identifico y denuncio cualquier forma de discriminación y/o vulneración de derechos que presencie o conozca.', 18, 18, 1),
('Conozco los números de teléfono de emergencia, líneas de ayuda y organizaciones que trabajan en la protección de los derechos de las niñas y los niños.', 18, 18, 1),
('Con mi Patrulla o Unidad realizo una descubierta sobre los Derechos Humanos en la comunidad en donde vivo, en mi escuela.', 18, 18, 1),
('Participo in proyectos, tanto fuera como dentro del Movimiento Scout, que promueven la igualdad, la justicia y el respeto a los Derechos Humanos en la comunidad.', 18, 18, 2),
('Participo in eventos, colectas o actividades de voluntariado para apoyar a organizaciones que trabajan en defensa de los Derechos Humanos.', 18, 18, 2),
('Mantengo diálogos abiertos con personas de confianza para conocer experiencias y preocupaciones sobre situaciones de vulneración de los DD.HH.', 18, 18, 2);

-- Acciones / Desafíos para la Competencia 19: "Ejercito y promuevo practicas que fomenten la participación democratica en los diferentes contextos en donde me desenvuelvo."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Investigo como funciona la democracia en mi pais y como se toman decisiones en los diferentes niveles de gobierno.', 19, 19, 1),
('Realizo una visita al Consejo Deliberante/ Municipal de mi ciudad y me interiorizo sobre su funcionamiento.', 19, 19, 1),
('Me informo sobre cómo se toman las decisiones en mi Grupo Scout y sobre mis deberes y derechos como miembro de Scouts de Argentina.', 19, 19, 1),
('Respeto las decisiones tomadas en forma democratica en mi Patrulla y Unidad Scout.', 19, 19, 1),
('Practico la escucha activa y el respeto hacia opiniones diferentes, incluso si no estoy de acuerdo.', 19, 19, 1),
('Realizo propuestas de actividades y proyectos teniendo en cuenta los intereses de mi Patrulla.', 19, 19, 1),
('Conozco y participo in espacios de participacion ciudadana in mi barrio y de mi escuela, como por ejemplo el Centro de Estudiantes.', 19, 19, 1),
('Dialogo con los representantes juveniles de mi Grupo Scout para conocer los temas que se tratan en el Consejo de Grupo.', 19, 19, 2),
('Fomento la participacion de todas las personas, asegurando que se escuchen las voces de diferentes grupos y que las decisiones sean justas.', 19, 19, 2),
('Propongo reuniones para discutir temas importantes y asegurarme que todas las personas tengan la oportunidad de hablar', 19, 19, 2),
('Participo in debates constructivos con los demás para analizar las diferentes propuestas y llegar a un entenidmiento común', 19, 19, 2);

-- Acciones / Desafíos para la Competencia 20: "Respeto los acuerdos de convivencia y las decisiones tomadas por los demas, expresando mis propias ideas y posturas de manera empatica y respetuosa."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Conozco el funcionamiento de los órganos de gobierno de la Patrulla y la Unidad y participo de los ámbitos que me corresponden.', 20, 20, 1),
('Participo in la elaboración de los acuerdos de convivencia de mi Unidad Scout.', 20, 20, 1),
('Opino sobre lo que me gusta o no de los acuerdos de convivencia in distintos ámbitos in los que actúo.', 20, 20, 1),
('Conozco y respeto los acuerdos de convivencia de los distintos ámbitos in los que actúo, aunque no siempre esté de acuerdo.', 20, 20, 1),
('Participo in la elaboración de reglas in actividades de grupo, como juegos, deportes o proyectos.', 20, 20, 1);

-- Acciones / Desafíos para la Competencia 21: "Promuevo que se garanticen los derechos de todas las personas in los ambitos in los que Participo."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Conozco cuales son mis derechos y responsabilidades.', 21, 21, 1),
('Hablo con mis educadoras y educadores cuando me doy cuenta de situaciones que vulneran los derechos de algun companero o companera.', 21, 21, 1),
('Investigo sobre problemas de desigualdad y discriminación de géneros in mi comunidad y propongo alguna actividad al respecto.', 21, 21, 1),
('Contribuyo al clima de respeto y tolerancia in los espacios in los que participo.', 21, 21, 1),
('Participo in iniciativas y proyectos de Unidad que buscan crear conciencia sobre los Derechos Humanosy trabajar para su protecci6on y promocion.', 21, 21, 1),
('Propongo una actividad de Patrulla o de Unidad relacionado con la defensa de los derechos de las personas.', 21, 21, 2);

-- Acciones / Desafíos para la Competencia 22: "Fomento el respeto a la diversidad in todas sus formas, cuestionando estereotipos y prejuicios in mis interacciones diarias."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Respeto a mis companeras, companeros y los reconozco como iguales.', 22, 22, 1),
('Ofrezco ayuda y apoyo a companeras y compaferos que puedan estar enfrentando desafios debido a sus diferencias.', 22, 22, 1),
('Aprendo sobre qué son los estereotipos y como pueden afectar a las personas.', 22, 22, 1),
('Soy consciente de como las palabras pueden herir a las demas personas y evito comentarios ofensivos.', 22, 22, 1),
('Realizo acciones in favor de la inclusion in los lugares in los que me desenvuelvo, y me manifiesto in contra de comentarios o actitudes prejuiciosas y estereotipadas.', 22, 22, 1),
('Trato a todas las personas con amabilidad y respeto, independientemente de su apariencia, origen o creencias.', 22, 22, 1),
('Practico la empatia imaginandome como se siente la otra persona in su situacion.', 22, 22, 1),
('Mantengo contacto con scouts de otros paises y me intereso por saber como viven, como es su cultura, etc.', 22, 22, 2),
('Propongo charlas, talleres, proyectos y actividades que promuevan la inclusion y diversidad.', 22, 22, 2),
('Participo, junto con mi Patrulla, in charlas y talleres de sensibilizacion sobre temas de diversidad, inclusion y respeto.', 22, 22, 2),
('Reflexiono regularmente sobre mis creencias y actitudes hacia diferentes grupos de personas.', 22, 22, 2),
('Cuestiono cualquier suposicion que puedan hacer sobre alguien basada in su apariencia o identidad.', 22, 22, 2),
('Promuevo espacios de dialogo para reflexionar sobre situaciones de discriminacion y prejuicios tanto in mi Patrulla como Unidad.', 22, 22, 2),
('Desarrollo la Insignia del cono Sur.', 22, 22, 2);

-- Acciones / Desafíos para la Competencia 23: "Promuevo la participación e inclusión de todas las personas, sin discriminación alguna."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Ayudo a alguien que se incorpora a mi Patrulla a comprender y avanzar in su progresión personal.', 23, 23, 1),
('Propongo a mi Patrulla y a la Unidad Scout juegos y actividades diferentes pensando in la participación e inclusión de todas las personas.', 23, 23, 1),
('Propongo realizar actividades con Patrullas y Unidades de otros grupos scouts.', 23, 23, 1),
('Participo de actividades y eventos con Patrullas y Unidades de mi distrito o zona.', 23, 23, 1),
('Reconozco y respeto las decisiones y preferencias de las demás personas, incluso si son diferentes a las mías.', 23, 23, 1),
('Practico el respeto y la inclusión al jugar y compartir con amigas y amigos, asegurándome de que todas las personas se sientan incluidas.', 23, 23, 1);

-- Acciones / Desafíos para la Competencia 24: "Identifico oportunidades para ayudar a alguien de manera individual y/o colectiva, sin esperar nada a cambio."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Siempre que tengo oportunidad, realizo buenas acciones personales in los lugares in los que me desenvuelvo.', 24, 24, 1),
('Participo de actividades solidarias y buenas acciones colectivas con mi Patrulla y mi Unidad.', 24, 24, 1),
('Realizo una descubierta in las que identifico las principales organizaciones y servicios públicos de mi comunidad, conozco a que se dedican y las ubico in un mapa.', 24, 24, 1),
('Colaboro con las tareas in mi hogar.', 24, 24, 1),
('Propongo buenas acciones y actividades solidarias para hacer junto a mi Patrulla y colaboro in su organización.', 24, 24, 1),
('Participo in al menos un proyecto solidario junto a mi Unidad.', 24, 24, 2),
('Desarrollo una especialidad vinculada al tema.', 24, 24, 2);

-- Acciones / Desafíos para la Competencia 25: "Me comprometo y animo a otras personas a trabajar en conjunto para lograr un cambio positivo en el mundo."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo una descubierta junto a mi Patrulla o Unidad Scout in la que tomamos conocimiento de algunos problemas de la comunidad. A partir de esa descubierta, identifico un problema y propongo posibles acciones a realizar con mi Patrulla o Unidad.', 25, 25, 1),
('Identifico in mi comunidad a otros grupos de personas (además del Movimiento Scout) con quienes puedo trabajar in conjunto para lograr cambios positivos.', 25, 25, 1),
('Ante algunos problemas detectados in mi escuela o comunidad propongo algunas soluciones y animo a algunos compañeros/as a trabajar in conjunto para solucionarlos.', 25, 25, 1);

-- Acciones / Desafíos para la Competencia 26: "Observo lo que pasa a mi alrededor y trato de entender las razones detrás de las desigualdades que veo, actuando de manera responsable en situaciones que afectan a la sociedad."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo descubiertas con mi Patrulla o Unidad con el propósito de conocer de cerca las realidades y necesidades de mi entorno.', 26, 26, 1),
('A partir de los resultados de la descubierta reflexiono desde una mirada crítica, junto a mi Patrulla, sobre las razones o causas del problema detectado y busco algunas respuestas posibles.', 26, 26, 1),
('Propongo una actividad o Proyecto de Unidad que aborde alguna problemática de mi Comunidad, dentro del campo prioritario solidaridad.', 26, 26, 1),
('Exploro in los Objetivos de Desarrollo Sostenible aquellos objetivos vinculados a las desigualdades y analizo qué cosas puedo hacer.', 26, 26, 2),
('Desarrollo una especialidad vinculada a la solidaridad.', 26, 26, 2);

-- Acciones / Desafíos para la Competencia 27: "Me pregunto sobre el origen, la grandeza, belleza y misterios de todo lo que soy, somos y nos rodea."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo dos marchas in silencio, una de dia y otra de noche, intentando captar los sonidos, los olores, colores y formas, y reflexiono sobre esto.', 27, 27, 1),
('Participo de una actividad de observacion del cielo nocturno y orientacion por las estrellas, me pregunto sobre el origen del universo y lo converso con mi Patrulla y/o Unidad Scout.', 27, 27, 1),
('Realizo y participo de reflexiones y de meditaciones junto a mi Patrulla o Unidad in las excursiones o campamentos.', 27, 27, 1),
('Formulo preguntas sobre el origen, la grandeza, belleza y misterios de todo lo que soy, somos y nos rodea y busco respuestas in distintos ambitos, como, por ejemplo, in mis creencias espirituales, in la ciencia.', 27, 27, 1),
('Participo in la construccion de espacios de reflexion y de meditacion in un campamento de mi Unidad Scout.', 27, 27, 1),
('Elijo un texto de mi confesion religiosa y lo comparto con mis amigas y amigos de la Patrulla, comentando por qué lo elegi.', 27, 27, 2),
('Visito los templos o lugares sagrados de las religiones presentes in la comunidad donde vivo.', 27, 27, 2);

-- Acciones / Desafíos para la Competencia 28: "Dialogo con los demas sobre lo que creo y busco conocer lo que los otros creen, demostrando respeto e interés."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Exploro religiones u opciones espirituales diferentes a la mia.', 28, 28, 1),
('Con mi Patrulla o Unidad compartimos un dia de actividades y juegos y/o una actividad solidaria con un grupo de jovenes de alguna iglesia, mezquita, sinagoga o templo de nuestra comunidad.', 28, 28, 1),
('Con mi Patrulla o Unidad realizo descubiertas sobre las religiones y opciones espirituales presentes in mi comunidad.', 28, 28, 1),
('Confecciono un calendario in el que registro las celebraciones y festividades religiosas de las confesiones de los scouts que integran mi Patrulla o Unidad Scout.', 28, 28, 1),
('Invito a mi Patrulla a cooperar in acciones organizadas por mi comunidad religiosa in favor de las personas vulneradas.', 28, 28, 1),
('Elijo un texto de mi confesión religiosa y lo comparto con mis amigas y amigos de la Patrulla, comentando por qué lo elegí.', 28, 28, 2),
('Visito los templos o lugares sagrados de las religiones presentes in la comunidad donde vivo.', 28, 28, 2);

-- Acciones / Desafíos para la Competencia 29: "Adhiero a los valores expresados en la Ley y la Promesa Scout comprendiendo su importancia para mi vida."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Conozco y comprendo la Ley y la Promesa Scout.', 29, 29, 1),
('He prometido esforzarme por vivir la Ley y la Promesa Scout.', 29, 29, 1),
('Conozco el significado de la flor de lis como emblema del Movimiento Scout.', 29, 29, 1),
('Identifico situaciones in las que aplico la Ley y la Promesa Scout in otros ambitos diferentes a mi Unidad (casa, escuela, la calle, etc.).', 29, 29, 1),
('Reviso los acontecimientos vividos por la Patrulla y la Unidad a la luz de la Ley Scout, participando de momentos de reflexion y evaluacion in el Consejo de Patrulla y la Asamblea.', 29, 29, 1),
('Ayudo a un/a companero/a de Patrulla a comprender el significado de la Ley y la Promesa Scout.', 29, 29, 2),
('Colaboro in la organizacion de actividades como el Fuego de la Ley y in celebraciones donde se realice o renueve la Promesa Scout.', 29, 29, 2);

-- Acciones / Desafíos para la Competencia 30: "Me pregunto sobre mis creencias, prioridades, valores y cOmo se manifiesta in mi actuar cotidiano."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Reflexiono sobre mis creencias, valores y prioridades y trato de identificarlas in mis acciones.', 30, 30, 1),
('Confecciono una lista de acciones in las que se manifiesten aquellas cosas que enseña mi confesión religiosa o espiritualidad y me comprometo a realizarlas.', 30, 30, 1),
('Selecciono el relato de un personaje real que haya demostrado ser coherente con sus creencias espirituales y valores y lo comparto con mi Patrulla y/o Unidad.', 30, 30, 1),
('Evalúo mis acciones in el Consejo de Patrulla, entendiendo que es importante actuar de acuerdo con lo que se piensa y cree.', 30, 30, 1),
('Exploro la herencia espiritual de mi familia y comunidad.', 30, 30, 2),
('Participo de celebraciones (misas, cultos, servicios religiosos, etc.) y festividades vinculadas a mi confesión religiosa o espiritualidad.', 30, 30, 2),
('Busco y selecciono un texto de confesión religiosa o creencia que sea significativo para mí y lo utilizo in mi celebración de Promesa u in otra ocasión especial.', 30, 30, 2),
('Apelo a mi religión, o creencias, para expresar gratitud, una necesidad, alegría o tristeza de forma personal o comunitaria.', 30, 30, 2),
('Desarrollo una especialidad vinculada a la espiritualidad.', 30, 30, 2);


/*AREA AMBIENTE*/

-- Acciones / Desafíos para la Competencia 31: "Reconozco mi impacto in el ambiente y in la sociedad adoptando un estilo de vida responsable y sostenible."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Indago sobre habitos responsables y sostenibles para aplicarlos in mi vida diaria. (Por ejemplo, separacion de residuos, tomar duchas cortas, desenchufar cargadores de celular u otros electrodomésticos si no los usas).', 31, 31, 1),
('Me informo sobre la huella de carbono y propongo actividades para aprender a reducirla.', 31, 31, 1),
('Realizo actividades de concientizacion para mi Patrulla, familia, escuela y lugares que frecuento sobre los habitos responsables y sostenibles.', 31, 31, 1),
('Realizo acciones para reutilizar y reciclar materiales de manera creativa con mi Patrulla, por ejemplo: creo juegos y juguetes con materiales reutilizables junto a mi Patrulla.', 31, 31, 1),
('Planifico un menu variado y nutritivo para mi campamento de Patrulla o Unidad que contenga frutas o verduras de estacion y productos locales.', 31, 31, 1),
('Construyo, junto a mi Patrulla o Unidad, un huerto y aprendo sobre el cuidado del suelo, la importancia de la agricultura sostenible.', 31, 31, 1),
('Se como tratar los residuos in campamento, por ejemplo, construyo un pozo de aguas grasas para el tratamiento correcto.', 31, 31, 1),
('Indago sobre actividades que fomenten la reduccion de residuos y se las propongo a mi Patrulla y Unidad Scout.', 31, 31, 1),
('Realizo cultivos de plantas nativas.', 31, 31, 2),
('Reutilizo los desechos organicos como abono.', 31, 31, 2),
('Investigo de donde provienen los alimentos que consumo.', 31, 31, 2),
('Desarrollo una especialidad o iniciativa regional o global vinculada a la sostenibilidad.', 31, 31, 2);

-- Acciones / Desafíos para la Competencia 32: "Utilizo los recursos naturales de forma consciente y busco alternativas que promuevan la conservacion del ambiente."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Me informo sobre qué son los recursos naturales e indago in los efectos que generan si no se usan correctamente.', 32, 32, 1),
('Investigo sobre los agentes contaminantes presentes in mi barrio y realizo acciones para su reducción.', 32, 32, 1),
('Reflexiono sobre el impacto in la naturaleza que tienen las actividades al aire libre que realizo e intento reducirlo.', 32, 32, 1),
('Separo los residuos (orgánicos y no orgánicos), reparo y reciclo aquello que ya no utilizo.', 32, 32, 1),
('Utilizo los desechos orgánicos de mi casa para crear abono', 32, 32, 1);

-- Acciones / Desafíos para la Competencia 33: "Participo de iniciativas para construir una sociedad más respetuosa con el planeta."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Propongo al menos una actividad y/o proyecto ambiental.', 33, 33, 1),
('Participo de jornadas o campañas de limpieza  de espacios naturales (lagunas, lagos, ríos, mares, sierras, etc.) y espacios verdes (plazas, parques, etc.)', 33, 33, 1),
('Me sumo a iniciativas de otras organizaciones que trabajan por el ambiente.', 33, 33, 1),
('Investigo, junto con mi Patrulla, sobre las energias renovables y propongo actividades sobre como utilizarlas.', 33, 33, 2),
('Construyo y aprendo a utilizar una cocina u horno solar.', 33, 33, 2),
('Promuevo con mi Patrulla acciones que reduzcan el cambio climatico dentro de mi comunidad.', 33, 33, 2),
('Participo in una de las iniciativas o desafios ambientales que propone Scouts de Argentina o la Organizacion Mundial del Movimiento Scout.', 33, 33, 2);

-- Acciones / Desafíos para la Competencia 34: "Comprendo las causas y consecuencias del impacto ambiental y realizo acciones para preservar y proteger el ambiente."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Conozco las causas del cambio climatico y las consecuencias in mi pais y in el mundo.', 34, 34, 1),
('Realizo una descubierta ambiental con mi Patrulla o Unidad in la que exploramos los problemas ambientales de mi comunidad o region y sus causas.', 34, 34, 1),
('Con mi Patrulla o Unidad realizo un safari fotografico in el que capto in imagenes los principales problemas ambientales de mi comunidad o region y organizo una muestra de fotos in un lugar publico.', 34, 34, 1),
('Calculo mi huella de carbono y busco maneras de disminuirla.', 34, 34, 2);

-- Acciones / Desafíos para la Competencia 35: "Comprendo que todo lo que le hacemos a los seres vivos y al ambiente nos afecta reciprocamente."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Me intereso por conocer la relacion de los pueblos originarios con la naturaleza (saberes ancestrales) e identifico algunas practicas que puedan ser aplicadas in mi comunidad.', 35, 35, 1),
('Me informo sobre las especies de flora y fauna in peligro de extincion in mi region y in el pais.', 35, 35, 1),
('Aprendo sobre la importancia de reducir el consumo de plasticos de un solo uso y como afecta esto a la fauna y flora de mi region.', 35, 35, 1),
('Realizo la separacion de los residuos in reciclables y no reciclables.', 35, 35, 1),
('Investigo sobre como los agroquimicos afectan nuestra vida y promuevo el uso de productos ecologicos y amigables para el ambiente in mi hogar y in los espacios donde participo.', 35, 35, 1),
('Aprendo sobre abonos y fertilizantes naturales y los utilizo para mis plantas, huerta, macetas, etc.', 35, 35, 2);

-- Acciones / Desafíos para la Competencia 36: "Entiendo que la Tierra es un lugar precioso y unico in el universo, y que todos tenemos la responsabilidad de protegerla."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Participo in actividades de reforestacion plantando arboles in areas deforestadas o in peligro.', 36, 36, 1),
('Organizo, junto a mi Patrulla, campañas de concientizacion sobre la importancia de reducir, reutilizar y reciclar los materiales para minimizar los desechos.', 36, 36, 1),
('Participo in actividades de limpieza comunitaria para recoger basura y promover un entorno mas limpio y saludable.', 36, 36, 1),
('Realizo una excursion in un curso de agua para analizar su calidad, posible contaminacion y sus causas.', 36, 36, 2),
('Construyo y pruebo un filtro de agua', 36, 36, 2),
('Construyo y utilizo un catalejo y un telescopio.', 36, 36, 2),
('Construyo hornos, cocinas y fogones conservacionistas.', 36, 36, 2);

-- Acciones / Desafíos para la Competencia 37: "Exploro oportunidades junto a mis amigas y amigos para conectarme con la naturaleza, cuidandola y protegiéndola, ya sea in mi comunidad local o in lugares más lejanos"
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Participo de al menos dos campamentos de Patrulla.', 37, 37, 1),
('Propongo y participo in la planificacion de un campamento con mi Patrulla.', 37, 37, 1),
('Puedo identificar la fauna y flora de mi barrio, provincia, ecorregion y/o lugar de campamento.', 37, 37, 1),
('Llevo un registro de mis noches y lugares de campamentos.', 37, 37, 1),
('Puedo trazar y seguir una pista de al menos un kilometro, in un medio natural, utilizando senales de pista.', 37, 37, 1),
('Realizo levantamiento de huellas, identifico las especies y rotulo las muestras.', 37, 37, 1),
('Participo de actividades donde deba utilizar mapas y croquis, uso de signos topograficos.', 37, 37, 1),
('Conozco y practico tecnicas de acecho para juegos y observacion de fauna.', 37, 37, 1),
('Realizo actividades acuaticas (nado, uso de embarcaciones, etc.) respetando las medidas de seguridad.', 37, 37, 2),
('Realizo una coleccion de piedras y minerales, identificando y rotulando cada muestra.', 37, 37, 2),
('Construyo una estacion meteorologica para la prediccion del clima.', 37, 37, 2),
('Construyo un refugio para la observacion de la fauna.', 37, 37, 2);

-- Acciones / Desafíos para la Competencia 38: "Desarrollo habilidades y técnicas que me permitan el disfrute de la vida in la naturaleza."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Aplico técnicas de senderismo (tipo de marcha, alimentación adecuada, tiempos de descanso in las excursiones) con mi Patrulla y/o la Unidad.', 38, 38, 1),
('Realizo caminatas de diferentes niveles de dificultad para mejorar la resistencia física, la coordinación y la confianza in el entorno natural.', 38, 38, 1),
('Participo in el diseño e instalación de las construcciones básicas de un campamento de Patrulla que nos permita disfrutar de la naturaleza: ubicación y armado de la carpa y del sector de Patrulla, preparación para situaciones de tormentas, cocina, mesa...', 38, 38, 1),
('Utilizo adecuadamente algunas herramientas útiles in la vida al aire libre (cortaplumas, hacha de mano, sierra, etc.) respetando las medidas de seguridad y los cuidados para su uso.', 38, 38, 1),
('Conozco y utilizo distintos tipos de fuego, de acuerdo con mis necesidades (cocina, calor, iluminación) y circunstancias, respetando las reglas de seguridad.', 38, 38, 1),
('Organizo mi mochila para un campamento de Patrulla o Unidad.', 38, 38, 1),
('Aplico la técnica “no dejar huellas” in los lugares in los que acampo o realizo excursiones.', 38, 38, 1),
('Busco información y comparto con mi Patrulla sobre áreas naturales protegidas.', 38, 38, 2),
('Mantengo actualizada la lista de materiales y elementos personales para un campamento de Patrulla.', 38, 38, 2),
('Construyo un periscopio y un acuascopio para la observación de la naturaleza.', 38, 38, 2),
('Obtengo una especialidad vinculada al campismo y la vida al aire libre.', 38, 38, 2);

-- Acciones / Desafíos para la Competencia 39: "Exploro y contemplo la naturaleza como un espacio que me permite conectar con lo trascendente y el bienestar."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Participo de actividades de observación astronómica para aprender sobre las estrellas, planetas y otros cuerpos celestes, y apreciar la belleza del cielo nocturno in entornos naturales alejados de la contaminación lumínica.', 39, 39, 1),
('Realizo, junto a mi Patrulla, paseos in la naturaleza de manera consciente, prestando atención a los detalles, los sonidos y los aromas del entorno, percibiendo la naturaleza.', 39, 39, 1),
('Diseño y construyo un espacio de reflexión, meditación u oración para mi Patrulla o Unidad in un campamento.', 39, 39, 1),
('Participo de momentos de reflexión, meditación u oración in la naturaleza.', 39, 39, 1),
('Busco textos religiosos o espirituales que hagan referencia a la naturaleza y la comparto in las reflexiones de mi Patrulla o Unidad.', 39, 39, 1),
('Realizo una lista de las acciones que puedo realizar para cuidar nuestra "Casa Común".', 39, 39, 1),
('Me conecto con la naturaleza a través de actividades creativas como la pintura o la creación de objetos, juguetes, adornos, guirlandas, etc. con elementos naturales.', 39, 39, 2),
('Participo de una excursión nocturna con los ojos vendados intentando captar los sonidos, aromas y sensaciones del entorno.', 39, 39, 2),
('Realizo actividades al aire libre que me permitan conectar con mi cuerpo, tales como excursiones, caminatas, juegos de Kim, etc.', 39, 39, 2);


/*AREA HABILIDADES PARA LA VIDA*/

-- Acciones / Desafíos para la Competencia 40: "Exploro temas de mi interés con la intención de mejorar cada dia."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo un listado de los temas sobre los que me gustaría aprender.', 40, 40, 1),
('Elijo al menos dos temas de mi lista y me propongo aprender sobre ellos.', 40, 40, 1),
('Propongo a mi Patrulla o Unidad una descubierta sobre un tema que me interese explorar.', 40, 40, 1),
('Evalúo mi progresión personal y me planteo nuevas acciones para seguir avanzando.', 40, 40, 1),
('Investigo y aplico técnicas de estudio in los temas que deseo y necesito aprender.', 40, 40, 2),
('Desarrollo una especialidad que me interesa.', 40, 40, 2),
('Desempeño un rol in mi Patrulla de acuerdo con mis intereses al menos por un ciclo de programa.', 40, 40, 2);

-- Acciones / Desafíos para la Competencia 41: "Experimento diferentes medios y tecnologias que me ayuden a resolver problemas de la vida cotidiana"
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Utilizo herramientas básicas (martillo, serrucho, destornillador, pinza, tenazas, etc.) para construir o reparar elementos de mi casa, de mi Patrulla, mi Unidad o Grupo Scout.', 41, 41, 1),
('Conozco el funcionamiento de los electrodomésticos y los utilizo para resolver tareas de mi casa.', 41, 41, 1),
('Utilizo tutoriales para resolver dudas sobre el arreglo o uso de elementos de la vida cotidiana.', 41, 41, 1),
('Utilizo juegos in línea o aplicaciones educativas que involucren la resolución de problemas utilizando tecnología. (por ejemplo, GPS, brújula digital, etc.).', 41, 41, 2),
('Utilizo aplicaciones útiles para medir los pasos y las distancias, realizar pronóstico del clima, etc', 41, 41, 2),
('Conozco el uso de las estructuras, caballetes, poleas, anclajes, tensores y las aplico in las construcciones de campamento.', 41, 41, 2),
('Me desempeño como intendente/a de patrulla al menos por un Ciclo de Programa.', 41, 41, 2),
('Desarrollo una especialidad o insignia especial relacionada con la tecnología.', 41, 41, 2);

-- Acciones / Desafíos para la Competencia 42: "Aplico mi conocimiento y habilidades para resolver los desafios y oportunidades que se me presentan."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Soy capaz de hacer algunos arreglos in mi casa.', 42, 42, 1),
('Colaboro in el mantenimiento y arreglo del local de la Unidad y de los materiales de mi Patrulla', 42, 42, 1),
('Puedo orientarme utilizando una brújula y un mapa, por indicios y medios naturales (sol, estrellas, vegetación, etc.).', 42, 42, 1),
('Utilizo mis especialidades para ayudar a otras personas o resolver problemas que se les presentan.', 42, 42, 1),
('Aplico técnicas de estimación de alturas y de distancia in actividades de mi Patrulla.', 42, 42, 1),
('Realizo una previsión del tiempo por indicios naturales o por instrumentos.', 42, 42, 2),
('Sé cómo realizar arreglos a nuestra carpa de Patrulla ante situaciones imprevistas.', 42, 42, 2);

-- Acciones / Desafíos para la Competencia 43: "Comparto mis pasatiempos y especialidades con amigas y amigos, buscando intereses comunes que podamos desarrollar in conjunto."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Participo con mi Patrulla in actividades relacionadas con temas referidos a oficios, trabajos y profesiones.', 43, 43, 1),
('Coordino con los integrantes de mi Patrulla para desarrollar actividades que nos permitan el logro de al menos una competencia.', 43, 43, 1),
('Identifico pasatiempos o intereses personales que puedan convertirse in una especialidad.', 43, 43, 1),
('Desarrollo, junto a mi Patrulla, una descubierta relacionada a un oficio o profesión que nos interese, reconociendo cuáles son las habilidades y competencias necesarias para poder desarrollarlos.', 43, 43, 2),
('Ayudo a un integrante de mi Patrulla a desarrollar una especialidad.', 43, 43, 2);

-- Acciones / Desafíos para la Competencia 44: "Asumo un rol de manera responsable in los proyectos y/o actividades de mi Patrulla y/o Unidad."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Desempeño diversos roles in mi Patrulla y lo hago con responsabilidad.', 44, 44, 1),
('Apoyo a los miembros de mi Patrulla que estén asumiendo nuevos roles o responsabilidades, compartiendo mis experiencias y conocimientos.', 44, 44, 1),
('Coordino una misión de Patrulla realizada in el contexto de un proyecto de la Unidad.', 44, 44, 1),
('Colaboro in la organización de actividades y reuniones de mi Patrulla.', 44, 44, 1),
('Busco ideas de actividades y proyectos de Unidad y las propongo a mi Consejo de Patrulla (para ser presentadas in la Asamblea).', 44, 44, 1);

-- Acciones / Desafíos para la Competencia 45: "Lidero actividades y proyectos, acompañando y motivando a mis pares a sumarse"
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Asumo la responsabilidad de coordinar a mi Patrulla in una misión o actividad que deba realizar: por ejemplo, una excursión o campamento de patrulla.', 45, 45, 1),
('Propongo ideas de actividades, temas a tratar y soluciones a problemas in mi Consejo de Patrulla.', 45, 45, 1),
('Comparto habilidades específicas relacionadas con los roles in la Patrulla, ayudando a otros a mejorar sus habilidades y competencias.', 45, 45, 1),
('Asumo distintas responsabilidades in las actividades y misiones que realiza mi Patrulla y Unidad.', 45, 45, 1),
('Diseño y coordino una construcción de campamento con mi Patrulla o Unidad.', 45, 45, 2);

-- Acciones / Desafíos para la Competencia 46: "Exploro y experimento con diferentes técnicas y tecnologias para resolver problemas, lograr mis objetivos y descubrir soluciones creativas."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Experimento con técnicas de supervivencia in la naturaleza, como encender fuego sin fósforos, cocina sin utensilios, potabilización de agua, refugios improvisados, etc.', 46, 46, 1),
('Realizo, junto a mi Patrulla, actividades de orientación y/o exploración, utilizando mapas, croquis y/o brújulas.', 46, 46, 1),
('Exploro y utilizo distintas aplicaciones para resolver temas de mis excursiones, campamentos y salidas, así como de mi vida cotidiana.', 46, 46, 1),
('Realizo, junto a mi Patrulla, una exploración por la ciudad utilizando GPS para orientarme por las calles y registro in esta herramienta los lugares más importantes de mi recorrido.', 46, 46, 1),
('Propongo una solución creativa a un problema o tarea que enfrentamos con la Patrulla o la Unidad.', 46, 46, 1),
('Construyo una cocina o una ducha solar o alguna otra solución técnica creativa que mejore el bienestar in el campamento.', 46, 46, 1),
('Realizo el levantamiento de un croquis topográfico.', 46, 46, 2),
('Utilizo mapas de transporte público o aplicaciones móviles para ayudarme a navegar por la ciudad y encontrar las rutas más convenientes y eficientes.', 46, 46, 2);

-- Acciones / Desafíos para la Competencia 47: "Busco informacion in diversas fuentes, y evaluo su veracidad y relevancia antes de aplicarla de manera critica y creativa."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Realizo búsquedas de información, evaluando su veracidad, formalidad y credibilidad y lo aplico in mis investigaciones.', 47, 47, 1),
('Investigo temas de mi interés utilizando técnicas de búsqueda de la información.', 47, 47, 1),
('Indago in diversas fuentes, ideas de actividades para hacer con mi Patrulla y de proyectos para realizar con la Unidad.', 47, 47, 1),
('Busco información sobre un tema, sobre el que necesitamos información, para una actividad de Patrulla o de Unidad.', 47, 47, 1),
('Desarrollo una especialidad vinculada a la informática y/o a la comunicación.', 47, 47, 2);

-- Acciones / Desafíos para la Competencia 48: "Comprendo la importancia de ser responsable y ético in el uso de la tecnologia y la informacion."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Me comunico de manera respetuosa y adecuada con mi Patrulla a través de diferentes medios digitales y analógicos.', 48, 48, 1),
('Participo, junto a mi Patrulla, de un taller sobre Huella e Identidad Digital.', 48, 48, 1),
('Identifico y comprendo el daño que puede realizarse desde las redes sociales y el entorno digital.', 48, 48, 1),
('Me informo sobre los riesgos existentes in las redes y cómo protegerme de ellos.', 48, 48, 1),
('Soy promotor/a de buenas prácticas in las redes.', 48, 48, 2),
('Soy consciente del tiempo que utilizo in el entorno digital y puedo limitarlo para que no me haga daño.', 48, 48, 2),
('Desarrollo una especialidad relacionada con el uso responsable de las redes sociales.', 48, 48, 2);

-- Acciones / Desafíos para la Competencia 49: "Administro mis recursos y mi dinero de forma responsable."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Administro el dinero que me brindan, registrando mis gastos in una libreta o aplicación y estableciendo metas de ahorro.', 49, 49, 1),
('Colaboro in la confección de un presupuesto para una actividad o misión de mi Patrulla.', 49, 49, 1),
('Realizo un presupuesto para una actividad personal o una compra que deseo realizar y obtengo los recursos necesarios.', 49, 49, 1),
('Organizo una actividad con mi Patrulla para recaudar los fondos que necesitamos.', 49, 49, 1),
('Me propongo asumir el rol de tesorero in mi Patrulla al menos por un ciclo de programa.', 49, 49, 2);

-- Acciones / Desafíos para la Competencia 50: "Organizo mi tiempo in forma adecuada diferenciando mis responsabilidades de mi tiempo libre."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Organizo mis diferentes tareas (estudio, tiempo libre, scouts, deportes, amigos/as, etc.) por medio de una agenda, calendario semanal u aplicación que me ayude a organizar mis tiempos.', 50, 50, 1),
('Organizo mis tareas otorgándole tiempo y prioridades a cada una.', 50, 50, 1),
('Le dedico al estudio el tiempo necesario para aprender.', 50, 50, 1),
('Organizo mi día para tener tiempo libre y de descanso, teniendo al menos 8 horas de sueño por la noche.', 50, 50, 1),
('Investigo y aplico técnicas de estudio y de concentración.', 50, 50, 2);

-- Acciones / Desafíos para la Competencia 51: "Enfrento y supero las dificultades que se me presentan y aprendo de la experiencia."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Reviso mi progresión personal, analizando aquellas cosas que hice bien y otras no tanto, y proponiéndome nuevos desafíos.', 51, 51, 1),
('Celebro mis logros y acepto cuando las cosas no me salen bien para poder mejorarlas.', 51, 51, 1),
('Afronto los desafíos de manera positiva y constructiva.', 51, 51, 1),
('En un Consejo de Patrulla evalúo mi desempeño y el de mis compañeras y compañeros in los roles de Patrulla.', 51, 51, 1),
('Pienso in al menos dos circunstancias in las que superé una situación adversa e identifico como lo hice, a quién pedí ayuda, como me sentí.', 51, 51, 1),
('Investigó y comparto la historia o testimonio de al menos tres personajes históricos que superaron dificultades y situaciones adversas.', 51, 51, 1),
('Participo in la evaluación de las actividades de patrulla y de Unidad identificando lo que hicimos bien y lo que debemos mejorar.', 51, 51, 2),
('Participo de un campamento o excursión con los elementos mínimos, durmiendo in refugios improvisados y realizando cocina rústica.', 51, 51, 2);

-- Acciones / Desafíos para la Competencia 52: "Desarrollo habilidades para trabajar y valorar con otros, valorando y compartiendo ideas, trabajando juntos para lograr nuestras metas."
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Me animo a hablar in público sobre temas de mi interés, compartiendo mis ideas y puntos de vista con respeto y claridad.', 52, 52, 1),
('Me intereso por el uso de diferentes tipos de medios de comunicación (radio, plataformas digitales, etc.).', 52, 52, 1),
('Conozco las claves para lograr una buena comunicación y las aplico in mi patrulla, familia, escuela y otros lugares que frecuento.', 52, 52, 1),
('Utilizo adecuadamente las redes sociales para compartir nuestras aventuras, exploraciones, actividades de patrulla y proyectos.', 52, 52, 1),
('Aprendo claves y las utilizo para comunicarme in el desarrollo de actividades.', 52, 52, 1),
('Puedo utilizar distintos códigos para comunicarme: morse, semáforo.', 52, 52, 1),
('Participo de un JOTA o JOTI.', 52, 52, 2);

-- Acciones / Desafíos para la Competencia 53: "Participo in diferentes manifestaciones artísticas in las que expreso mis emociones y sentimientos"
INSERT INTO accion (descripcion, id_competencia, codigo_competencia, id_tipo_accion) VALUES 
('Expreso mis emociones de diversas maneras a través de diferentes propuestas (escribo historias, canto, video, historietas, toco algún instrumento musical, pintura, teatro, danza, etc.).', 53, 53, 1),
('Realizo canciones y danzas, junto a mi Patrulla y Unidad.', 53, 53, 1),
('Participo de veladas y fogones, junto a mi Patrulla y mi Unidad.', 53, 53, 1),
('Propongo ideas y ayudo a preparar representaciones artísticas con mi Patrulla.', 53, 53, 1),
('Organizo con mi Patrulla una presentación (canción, sketch, danza...) para una velada o fogón de la Unidad.', 53, 53, 1),
('Expreso mis experiencias y sentimientos in el Libro de Oro de la Patrulla.', 53, 53, 1),
('Propongo a mi Patrulla una actividad artística, que permita expresar emociones y sentimientos.', 53, 53, 2),
('Compongo, junto a mi Patrulla, canciones referidas al Movimiento Scout.', 53, 53, 2),
('Me propongo realizar alguna actividad personal relacionada con el arte, por ejemplo: aprender a tocar un instrumento musical, pintar, dibujar historietas, cantar, actuar, etc.', 53, 53, 2),
('Construyo un instrumento musical.', 53, 53, 2),
('Desarrollo una especialidad referida al arte.', 53, 53, 2),
('Desempeño el rol de Responsable de Expresión in mi Patrulla al menos por un ciclo de programa.', 53, 53, 2);