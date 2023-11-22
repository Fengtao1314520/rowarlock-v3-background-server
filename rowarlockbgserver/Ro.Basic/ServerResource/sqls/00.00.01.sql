-- TODO->测试时使用

--INFO: 插入用户
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'user1', 'User Nickname 1', 'Password1', 'user1@example.com',
        '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('6C8dA9ef-de6C-25bc-bFD1-F4cC2E8d5A74', '尹洋', 'e', 'bc', 'v', '2022-01-01 16:00:00');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('34567', 'user3', 'User Nickname 3', 'Password3', 'user3@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('45678', 'user4', 'User Nickname 4', 'Password4', 'user4@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('56789', 'user5', 'User Nickname 5', 'Password5', 'user5@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('67890', 'user6', 'User Nickname 6', 'Password6', 'user6@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('78901', 'user7', 'User Nickname 7', 'Password7', 'user7@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('89012', 'user8', 'User Nickname 8', 'Password8', 'user8@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('90123', 'user9', 'User Nickname 9', 'Password9', 'user9@example.com', '2022-01-01 10:00:00.111');
INSERT INTO ro_userdetails (id, uname, unickname, upsd, uemail, ulogintime)
VALUES ('01234', 'user10', 'User Nickname 10', 'Password10', 'user10@example.com', '2022-01-01 10:00:00.111');

--INFO: 插入统计信息
INSERT INTO ro_statistics (id, userid, userstatus, totalcreatetask, totalassigneetask, lastcreatetasklasttime,
                           lastassigneetasklasttime, jobrate)
VALUES ('16a3e351-2799-4a89-b0cc-dacebbc1ad40', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'active', 115, 93,
        '2022-01-01 10:00:00.111', '2022-01-01 11:30:00.111', 22);

--INFO: 插入task数据
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('685ab401-e58b-4bd0-9c2d-18c37996bd41', 'temp', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task1', 'Task Content 1', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-09 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('6e26bfd9-b30a-4cb6-8e61-c744c8d1f389', 'job', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task2', 'Task Content 2', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-01 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('a637d698-85f7-4e78-b365-1bc8a34728b1', 'release', '7c9c6bf4-bbc9-4aff-88ba-0b49ccecdc44',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task3', 'Task Content 3', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('b7ab82c1-08f9-40b8-9032-7037613f5616', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task4', 'Task Content 4', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('c813860d-cc3e-46a0-8958-775387e93766', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task5', 'Task Content 5', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('d93e061f-06ec-4310-89c2-762b420f5732', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task6', 'Task Content 6', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('e850b71f-7d31-40b6-bb1a-842028ed522b', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task7', 'Task Content 7', 'active', 10, 20, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('f96b9191-9650-40fe-8991-8b89084092d2', 'test', 'c5dfead00-a00c-da79-9bb1-481ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task8', 'Task Content 8', 'done', 30, 40, '2023-09-26 15:30:12.456',
        '2023-11-11 15:30:12.456');
INSERT INTO ro_task (id, "type", createuserid, assigneeuserid, taskname, taskcontent, status, expandtime, elapsedtime,
                     starttime, endtime)
VALUES ('1086883d-69c0-40a8-89b2-99680840123e', 'test', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Task9', 'Task Content 9', 'block', 50, 60, '2023-09-26 15:30:12.456',
        '2023-10-30 15:30:12.456');


--INFO: 插入 ro_release
INSERT INTO ro_release (id, createuserid, assigneeuserid, releasename, releasedescription, status, createdatetime,
                        modifydatetime, releasecontent)
VALUES ('d7c9b5ec-6490-42cc-b4c2-7bcccfd66466', 'c5dfead9-9bb1-4800-a00c-da71ccb5fe19',
        'c5dfead9-9bb1-4800-a00c-da71ccb5fe19', 'Release 最新释放', 'Release 最新释放', 'completed',
        '2022-01-01 00:00:00', '2023-11-22 13:33:34.539',
        '{"author":"Nate Ford","createDate":"1699863533955","description":"RoWarlock 最新释放","modifyDate":"1699863533955","taskId":"a1dfd2ca-1831-4822-8466-658371a41ee8","title":"RoWarlock 释放","basicInfos":{"branchNumber":85894,"isHotfix":false,"productName":"RoWarlock","releaseDate":"Fri Nov 03 2023 00:00:00 GMT+0800 (中国标准时间)","releaseType":"Official","releaseVersion":"2023-2-3-7.1591","tagNumber":4,"tagType":"SVN"},"relatedConfig":[{"key":"All Config Entry.pdf","value":"56445"}],"testEnv":[{"key":"Windows 10","value":"10.0.19044.2965"},{"key":"Linux Redhat","value":"7.1.4 Rel"}],"content":[{"id":"N-1004","type":"New","feature":"息记录新增表情分类，查找会话表情更便捷"},{"id":"N-1005","type":"Improve","feature":"会话中的图片、视频等文件，可使用“多选”批量保存"},{"id":"N-1006","type":"Improve","feature":"远程协助可自适应屏幕大小，自如缩放，操作更便捷"},{"id":"N-1007","type":"Fix","feature":"GIF热图新增搜索功能，热图便捷搜，斗图趣无穷"}]}');