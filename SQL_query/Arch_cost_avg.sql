--Получить средние цены на видеокарты архитектуры Pascal

use Graphic_cards;

SELECT 
    V.[Name] AS Название, 
    AVG(VC.Cost) AS Цена
FROM
    Videocard V JOIN Video_Company VC ON V.id = VC.id_Videocard
    JOIN Architecture A ON V.id_Architect = A.id
WHERE
	A.[Name] = 'Pascal'
GROUP BY V.[Name]
