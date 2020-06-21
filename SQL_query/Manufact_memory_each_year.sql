--������ �����������, ������������ � ������� �����
--����������� ��������� AMD, ���������� � ������ ����
use Graphic_cards;

SELECT 
    V.[Year] AS ���,
    MIN(V.Memory_size) AS �����������,
    MAX(V.Memory_size) AS ������������,
    AVG(V.Memory_size) AS �������
FROM
    Videocard V
        JOIN
    Manufacturer M ON M.id = V.id_Manufact
        WHERE M.[Name] = 'AMD'
GROUP BY V.[Year]
ORDER BY 1
