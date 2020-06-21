--Получить список видеокарт от Nvidia с энергопотреблением не больше 100 Вт
use Graphic_cards;

SELECT 
    V.[Name] AS Название, [Energy_use]
FROM
    Videocard V
        JOIN
    Manufacturer M ON V.id_Manufact = M.id
WHERE
    M.[Name] = 'Nvidia' AND V.[Energy_use] <= 100
ORDER BY 2
