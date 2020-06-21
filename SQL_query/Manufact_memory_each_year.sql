--Узнать минимальный, максимальный и средний объем
--видеопамяти видеокарт AMD, выпущенных в каждом году
use Graphic_cards;

SELECT 
    V.[Year] AS Год,
    MIN(V.Memory_size) AS Минимальный,
    MAX(V.Memory_size) AS Максимальный,
    AVG(V.Memory_size) AS Средний
FROM
    Videocard V
        JOIN
    Manufacturer M ON M.id = V.id_Manufact
        WHERE M.[Name] = 'AMD'
GROUP BY V.[Year]
ORDER BY 1
