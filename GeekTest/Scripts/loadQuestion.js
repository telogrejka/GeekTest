var id = 0;

function load(testNum) {
    $.ajax({
        url: '../Testing/GetQuestion?parentQuestion=' + tmp[id],
        success: function (data) {
            $('#divQuestion').html(data);
        }
    });

    if ($("#myForm input[type='radio']:checked").val() > 0) {
        $.ajax({
            url: '../Testing/setAnswer?id=' + id + '&answerID=' + $("#myForm input[type='radio']:checked").val()
        });
    }

    id++;

    if (id > tmp.length) {
        finishTest('Вы ответили на все вопросы. Завершить прохождение теста?', testNum)
    }
}

function finishTest(message, testNum) {
    if (confirm(message))
        location.href = '../Testing/Finish';
    
}