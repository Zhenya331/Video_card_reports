--Узнать список видеокарт с объемом видеопамяти между 3072 МБайт и 6144 Мбайт, созданных в 2017 году
use Graphic_cards;

SELECT 
    V.[Name] AS Название, V.Memory_size AS [Объем Памяти]
FROM
    Videocard V
WHERE 
	V.[Year] = 2017 AND
	V.Memory_size BETWEEN 3072 AND 6144
