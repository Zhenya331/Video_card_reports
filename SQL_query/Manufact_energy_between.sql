--�������� ������ ��������� �� Nvidia � ������������������ �� ������ 100 ��
use Graphic_cards;

SELECT 
    V.[Name] AS ��������, [Energy_use]
FROM
    Videocard V
        JOIN
    Manufacturer M ON V.id_Manufact = M.id
WHERE
    M.[Name] = 'Nvidia' AND V.[Energy_use] <= 100
ORDER BY 2
