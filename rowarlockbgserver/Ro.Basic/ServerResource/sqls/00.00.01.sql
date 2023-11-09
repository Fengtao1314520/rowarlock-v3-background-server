-- TODO->测试时使用

--INFO: 插入用户
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'user1', 'User Nickname 1', 'Password1', 'user1@example.com',
        '2022-01-01 10:00:00'),
       ('23456', 'user2', 'User Nickname 2', 'Password2', 'user2@example.com', '2022-01-01 11:00:00'),
       ('34567', 'user3', 'User Nickname 3', 'Password3', 'user3@example.com', '2022-01-01 12:00:00'),
       ('45678', 'user4', 'User Nickname 4', 'Password4', 'user4@example.com', '2022-01-01 13:00:00'),
       ('56789', 'user5', 'User Nickname 5', 'Password5', 'user5@example.com', '2022-01-01 14:00:00'),
       ('67890', 'user6', 'User Nickname 6', 'Password6', 'user6@example.com', '2022-01-01 15:00:00'),
       ('78901', 'user7', 'User Nickname 7', 'Password7', 'user7@example.com', '2022-01-01 16:00:00'),
       ('89012', 'user8', 'User Nickname 8', 'Password8', 'user8@example.com', '2022-01-01 17:00:00'),
       ('90123', 'user9', 'User Nickname 9', 'Password9', 'user9@example.com', '2022-01-01 18:00:00'),
       ('01234', 'user10', 'User Nickname 10', 'Password10', 'user10@example.com', '2022-01-01 19:00:00');

--INFO: 插入统计信息
INSERT INTO ro_statistics (id, userid, userstatus, totalcreatetask, totalassigneetask, lastcreatetasklasttime,
                           lastassigneetasklasttime, jobrate)
VALUES ('16a3e351-2799-4a89-b0cc-dacebbc1ad40', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'active', '115', '93',
        '2022-01-01 10:00:00',
        '2022-01-01 11:30:00', '22');

--INFO: 插入task数据
INSERT INTO ro_task (id, type, createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('685ab401-e58b-4bd0-9c2d-18c37996bd41', 'temp', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task1', 'Task Content 1', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-09 15:30:12.456'),
       ('6e26bfd9-b30a-4cb6-8e61-c744c8d1f389', 'job', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task2', 'Task Content 2', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-01 15:30:12.456'),
       ('a637d698-85f7-4e78-b365-1bc8a34728b1', 'release', '7c9c6bf4-bbc9-4aff-88ba-0b49ccecdc44',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task3', 'Task Content 3', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456'),
       ('b7ab82c1-08f9-40b8-9032-7037613f5616', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task4', 'Task Content 4', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456'),
       ('c813860d-cc3e-46a0-8958-775387e93766', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task5', 'Task Content 5', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456'),
       ('d93e061f-06ec-4310-89c2-762b420f5732', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task6', 'Task Content 6', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456'),
       ('e850b71f-7d31-40b6-bb1a-842028ed522b', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task7', 'Task Content 7', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456'),
       ('f96b9191-9650-40fe-8991-8b89084092d2', 'test', 'c5dfead00-a00c-da79-9bb1-481ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task8', 'Task Content 8', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456'),
       ('1086883d-69c0-40a8-89b2-99680840123e', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task9', 'Task Content 9', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456');
