--������ ������ ��������� � ������� ����������� ����� 3072 ����� � 6144 �����, ��������� � 2017 ����
use Graphic_cards;

SELECT 
    V.[Name] AS ��������, V.Memory_size AS [����� ������]
FROM
    Videocard V
WHERE 
	V.[Year] = 2017 AND
	V.Memory_size BETWEEN 3072 AND 6144
