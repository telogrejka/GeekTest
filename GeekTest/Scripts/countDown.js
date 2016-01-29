// Скрипт для отсчета времени до завершения теста

function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = 0;
            alert('Время вышло.');
            location.href = '../Testing/Finish';
        }
    }, 1000);
}

window.onload = function () {
    var Minutes = 60 * duration,
        display = document.querySelector('#countdown');
    startTimer(Minutes, display);
};