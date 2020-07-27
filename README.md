# Video_card_reports
Client-Server-Database architecture

Данная программа позволяет получать различные отчеты в виде txt файла (в коде Client можно добавить другие способы).

Принцип работы:
1.  Клиенты пытаются авторизоваться (Логины и пароли клиентов находятся в бд Users)

    ![Image alt](https://github.com/Zhenya331/Video_card_reports/raw/master/Images/Autorisation.png)

2.  При успешной авторизации им предоставляется возможность запрашивать отчеты с различными параметрами у сервера

    ![Image alt](https://github.com/Zhenya331/Video_card_reports/raw/master/Images/Queries.png)

3.  Сервер, получив сигнал от клиента, составляет отчет (берет информацию из бд Graphic_cards).

4.  Сервер отправляет отчет клиенту

    ![Image alt](https://github.com/Zhenya331/Video_card_reports/raw/master/Images/Get_Result.png)

- Клиент создан при помощи WPF, Сервер - консольное приложение.
- Отправка данных между клиентом и сервером происходит по TCP протоколу.
- Сервер берет данные из бд при помощи Entity framework.
- Стоит отметить, что сервер берет данные не сразу из бд, а через хранимые процедуры, находящиеся в ней.

  ![Image alt](https://github.com/Zhenya331/Video_card_reports/raw/master/Images/Proc.png)

В БД Users находятя следующие пользователи (UserName; Password):
1.  User1; User1
2.  User2; User2
3.  User4; User4

- Также в сервере имеется система логгирования.
- История действий клиентов отображается в консоли, а также записывается в txt файл по пути Server/bin/Debug/Logs.
- В коде сервера можно добавить свои способы хранения истории действий клиентов.
