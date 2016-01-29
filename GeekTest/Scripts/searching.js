function search() {
    $.ajax({
        url: '../Test/Search?searchString=' + document.getElementById('searhTB').value,
        success: function (data) {
            $('#divResult').html(data);
        }
    });
    $('#searhTB').val("");
}