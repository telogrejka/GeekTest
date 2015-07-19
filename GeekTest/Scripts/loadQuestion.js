//Скрипт выводит на страницу вопрос и варианты ответа на него

var id = 0;

function load(testNum) {
    $.ajax({
        url: '../Testing/GetQuestion?parentQuestion=' + tmp[id],
        success: function (data) {
            $('#divQuestion').html(data);
        }
    });
    id++;
    if (id > tmp.length) {
        finishTest('Вы ответили на все вопросы. Завершить прохождение теста?', testNum)
    }
}

function finishTest(message, testNum) {
    if (confirm(message))
        location.href = '../Results/Index?index=' + testNum;
}